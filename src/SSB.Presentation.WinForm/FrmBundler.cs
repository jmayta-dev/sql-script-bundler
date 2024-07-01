using MediatR;
using Microsoft.Extensions.Logging;
using SSB.Application.UseCases.Commands.MergeScripts;
using SSB.Application.UseCases.Queries.GetAllBranches;
using SSB.Application.UseCases.Queries.ProcessScripts;
using SSB.Domain.Enums;
using SSB.Presentation.WinForm.Utils;
using SSB.Shared.Abstractions;
using System.Collections.Immutable;

namespace SSB.Presentation.WinForm;

public partial class FrmBundler : Form
{
    #region Constants
    //
    // constants
    //
    private const string OUTPUT_ROOT_FOLDER_NAME = "bin";
    #endregion // Constants

    #region Properties & Variables
    //
    // dependency 
    //
    private readonly ILogger<FrmBundler> _logger;
    private readonly IMediator _mediator;
    //
    // public
    //

    //
    // private 
    //
    private CancellationTokenSource _cancellationTokenSource;
    private string _projectPath = string.Empty;
    private readonly List<ProcessScriptsScriptDTO> _processedScripts = [];
    private readonly List<ProcessScriptsScriptDTO> _includedScripts = [];
    private readonly List<ProcessScriptsScriptDTO> _excludedScripts = [];
    #endregion // Properties & Variables 

    #region Constructor
    public FrmBundler() => InitializeComponent();

    public FrmBundler(ILogger<FrmBundler> logger, IMediator mediator) : this()
    {
        _logger = logger;
        _mediator = mediator;
    }
    #endregion // Constructor

    #region Events
    private async void BtnBrowseFolder_Click(object sender, EventArgs e)
    {
        // show folder browser dialog
        using FolderBrowserDialog folderBrowserDialog = new();
        folderBrowserDialog.Description = "Seleccione su carpeta de trabajo.";
        folderBrowserDialog.ShowHiddenFiles = false;
        folderBrowserDialog.ShowNewFolderButton = false;
        folderBrowserDialog.RootFolder = Environment.SpecialFolder.Personal;

        DialogResult dialogResult = folderBrowserDialog.ShowDialog();
        if (dialogResult == DialogResult.OK)
        {
            _projectPath = folderBrowserDialog.SelectedPath;
            TxtProjectPath.Text = _projectPath;

            CklGroups.Items.Clear();
            GbxGroups.Enabled = false;

            await GetBranches(_projectPath);
        }
    }

    private void BtnCancel_Click(object sender, EventArgs e)
    {
        _cancellationTokenSource?.Cancel();
    }

    private void BtnExclude_Click(object sender, EventArgs e)
    {
        List<ProcessScriptsScriptDTO>? temporalIncludedScripts = _includedScripts;
#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
        List<ProcessScriptsScriptDTO>? selectedIncludedScripts =
            DgvIncludedScripts
                .SelectedRows
                .Cast<DataGridViewRow>()
                .Select(r => r.DataBoundItem as ProcessScriptsScriptDTO)
                .Where(s => s != null)
                .ToList();
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.

        temporalIncludedScripts = temporalIncludedScripts.Except(selectedIncludedScripts).ToList();
        _includedScripts.Clear();
        _includedScripts.AddRange(temporalIncludedScripts);
        _excludedScripts.AddRange(selectedIncludedScripts);

        FillDgvIncludedScripts(_includedScripts.ToImmutableList());
        FillDgvExcludedScrips(_excludedScripts.ToImmutableList());
        // set to null for fast garbage collection
        temporalIncludedScripts = null;
        selectedIncludedScripts = null;
    }

    private void BtnExit_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void BtnInclude_Click(object sender, EventArgs e)
    {
        List<ProcessScriptsScriptDTO?> excludedScripts =
            DgvExcludedScripts.SelectedRows.Cast<DataGridViewRow>()
                .Select(r => r.DataBoundItem as ProcessScriptsScriptDTO)
                .Where(s => s != null)
                .ToList();

        if (excludedScripts.Any(s => s?.StatusDescription == "Eliminado"))
        {
            var error = new Error(
                "IncludeScripts.Deleted",
                string.Concat(
                    "No se puede incluir scripts con estado \"Eliminado\".",
                    "Deseleccione y vuelva a intentar."));
            Notify.Warning(_logger, error);
            return;
        }
        else
        {
            var temporalExcluded = _excludedScripts.Except(excludedScripts);
            _includedScripts.AddRange(excludedScripts!);
            _excludedScripts.Clear();
            _excludedScripts.AddRange(temporalExcluded!);
            temporalExcluded = null;

            FillDgvIncludedScripts(_includedScripts.ToImmutableList());
            FillDgvExcludedScrips(_excludedScripts.ToImmutableList());
        }
    }

    private async void BtnMerge_Click(object sender, EventArgs e)
    {
        TslblProcessing.Visible = true;

        _cancellationTokenSource = new CancellationTokenSource();
        await MergeScripts(_cancellationTokenSource.Token);

        TslblProcessing.Visible = false;
    }


    private async void BtnProcess_Click(object sender, EventArgs e)
    {
        TslblProcessing.Visible = true;

        await ProcessProjectPath();

        TslblProcessing.Visible = false;
    }

    private async void BtnSyncFolder_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(_projectPath)) return;

        CklGroups.Items.Clear();
        GbxGroups.Enabled = false;

        await GetBranches(_projectPath);
    }

    private void FrmBundler_FormClosing(object sender, FormClosingEventArgs e)
    {
        _cancellationTokenSource?.Dispose();
    }

    private void FrmBundler_Load(object sender, EventArgs e)
    {
        _cancellationTokenSource = new CancellationTokenSource();
    }
    #endregion // Events

    #region Methods
    /// <summary>
    /// Check information in controls for use them in ProcessScripts query
    /// </summary>
    /// <returns>Success or failure Result object</returns>
    private Result CheckProcessScriptsQueryParameters()
    {
        // validate project path
        if (string.IsNullOrWhiteSpace(TxtProjectPath.Text))
        {
            var error = new Error(
                "ProjectPath.NotProvided",
                "Ingrese la ruta del proyecto a procesar.");
            Notify.Warning(_logger, error);
            return Result.Failure(error);
        }
        // validate selected branch
        if (CmbProjectBranches.SelectedItem == null || CmbProjectBranches.SelectedIndex < 0)
        {
            var error = new Error(
                "GitBranch.NotProvided",
                "Seleccione una rama de referencia antes de procesar.");
            Notify.Warning(_logger, error);
            return Result.Failure(error);
        }
        // validate current working branch
        if (string.IsNullOrWhiteSpace(TxtWorkingBranch.Text))
        {
            var error = new Error(
                "GitBranch.SetWorkingBranch",
                "No hay una rama activa actualmente. Intente activando una " +
                "y actualice las ramas.");
            Notify.Warning(_logger, error);
            return Result.Failure(error);
        }
        return Result.Success();
    }

    /// <summary>
    /// Fill the check list box CklGroups with list of groups passed as parameter
    /// </summary>
    /// <param name="groupList"></param>
    private void FillCklGroups(IImmutableList<string> groupList)
    {
        if (groupList == null)
        {
            Notify.Error(_logger, new Error(
                "FillCklGroups",
                "La lista de grupos enviada como parámetro tiene valor nulo."));
            return;
        }

        CklGroups.Items.Clear();

        foreach (var group in groupList)
        {
            CklGroups.Items.Add(group, true);
        }
    }

    /// <summary>
    /// Fill with data the data grid view for Project Branches
    /// </summary>
    /// <param name="branchList"></param>
    private void FillCmbProjectBranches(IImmutableList<GetAllBranchesBranchDTO> branchList)
    {
        CmbProjectBranches.DataSource = branchList.ToList();
        CmbProjectBranches.DisplayMember = "Name";
        CmbProjectBranches.ValueMember = "Name";
    }

    /// <summary>
    /// Fill excluded scripts DataGridView with list of Script Data Transfer Objects
    /// </summary>
    /// <param name="processScriptsScriptDTOs"></param>
    private void FillDgvExcludedScrips(
        IImmutableList<ProcessScriptsScriptDTO> processScriptsScriptDTOs)
    {
        if (processScriptsScriptDTOs == null)
        {
            Notify.Error(_logger, new Error(
                "Scripts.FillDataGridView",
                "La lista de scripts enviada como parámetro tiene valor nulo."));
            return;
        }
        DgvExcludedScripts.AutoGenerateColumns = false;
        DgvExcludedScripts.AllowUserToAddRows = false;
        DgvExcludedScripts.AllowUserToDeleteRows = false;
        DgvExcludedScripts.AllowUserToOrderColumns = false;
        DgvExcludedScripts.AllowUserToResizeRows = false;

        DgvExcludedScripts.DataSource = null;
        DgvExcludedScripts.DataSource = processScriptsScriptDTOs.ToList();
        colExcludedDescripcion.DataPropertyName = "Description";
        colExcludedFilePath.DataPropertyName = "FileName";
        colExcludedGroup.DataPropertyName = "Group";
        colExcludedStatus.DataPropertyName = "StatusDescription";
    }

    /// <summary>
    /// Fill included scripts DataGridView with list of Script Data Transfer Objects
    /// </summary>
    /// <param name="scriptDTOs">Inmutable List of Script Data Transfer Objects</param>
    private void FillDgvIncludedScripts(IImmutableList<ProcessScriptsScriptDTO> scriptDTOs)
    {
        if (scriptDTOs == null)
        {
            Notify.Error(_logger, new Error(
                "Scripts.FillDataGridView",
                "La lista de scripts enviada como parámetro tiene valor nulo."));
            return;
        }
        DgvIncludedScripts.AutoGenerateColumns = false;
        DgvIncludedScripts.AllowUserToAddRows = false;
        DgvIncludedScripts.AllowUserToDeleteRows = false;
        DgvIncludedScripts.AllowUserToOrderColumns = false;
        DgvIncludedScripts.AllowUserToResizeRows = false;

        DgvIncludedScripts.DataSource = null;
        DgvIncludedScripts.DataSource = scriptDTOs.ToList();
        colDescription.DataPropertyName = "Description";
        colFileName.DataPropertyName = "FileName";
        colObs.DataPropertyName = "Observation";
        colPathGroup.DataPropertyName = "Group";
        colStatus.DataPropertyName = "StatusDescription";
    }

    /// <summary>
    /// Get all branches list from project path with a Git repository
    /// </summary>
    /// <param name="projectPath">Absolute path to project</param>
    /// <returns>Returns the result of operation</returns>
    private async Task<Result> GetBranches(string projectPath)
    {
        DgvExcludedScripts.DataSource = null;
        DgvIncludedScripts.DataSource = null;

        _excludedScripts.Clear();
        _includedScripts.Clear();
        _processedScripts.Clear();

        // clear controls relationated
        TxtWorkingBranch.Clear();
        CmbProjectBranches.DataSource = null;
        CmbProjectBranches.Items.Clear();
        CklGroups.Items.Clear();

        // send query to get all branches in repo
        var resultGetAllBranches = await _mediator.Send(
            new GetAllBranchesQuery(projectPath));

        if (resultGetAllBranches.IsFailure)
        {
            Notify.Warning(_logger, resultGetAllBranches.Error);
            return resultGetAllBranches.Error;
        }

        if (resultGetAllBranches.Value == null || resultGetAllBranches.Value.Count == 0)
        {
            Notify.Information(
                _logger,
                text: "No se encontraron ramas en el repositorio actual.",
                caption: "Obtener Ramas:");
            return Result.Success();
        }

        List<GetAllBranchesBranchDTO> branchesExceptCurrent = [.. resultGetAllBranches.Value];
        GetAllBranchesBranchDTO? workingBranch =
            resultGetAllBranches.Value.FirstOrDefault(b => b.IsCurrent);

        if (workingBranch == null)
        {
            TxtWorkingBranch.Clear();
            Notify.Information(
                _logger,
                text: "No hay una rama activa en el repositorio actual.",
                caption: "Obtener Ramas:");
        }
        else
        {
            branchesExceptCurrent = branchesExceptCurrent.Except([workingBranch]).ToList();
            TxtWorkingBranch.Text = workingBranch.Name;


            // fill Group checked list box
            Result<List<string>> gettingGroupResult =
                GetGroupsFromProcessedScripts(_processedScripts);

            if (gettingGroupResult.IsFailure)
            {
                Notify.Error(_logger, gettingGroupResult.Error);
                return gettingGroupResult.Error;
            }
            else if (gettingGroupResult.Value == null)
                FillCklGroups(new List<string>().ToImmutableList());
            else
                FillCklGroups(gettingGroupResult.Value.ToImmutableList());

            FillCmbProjectBranches(branchesExceptCurrent.ToImmutableList());
        }
        return Result.Success();
    }

    /// <summary>
    /// Get groups from checked list box
    /// </summary>
    /// <returns></returns>
    private List<GroupDTO> GetGroupsFromCheckedListBox()
    {
        List<GroupDTO> groups = [];

        for (int i = 0; i < CklGroups.Items.Count; i++)
        {
            GroupDTO group = new()
            {
                Name = CklGroups.Items[i].ToString() ?? string.Empty,
                Operation = CklGroups.GetItemCheckState(i) == CheckState.Checked ?
                    ScriptBundleOperation.Merge : ScriptBundleOperation.Replicate
            };
            groups.Add(group);
        }
        return groups;
    }

    /// <summary>
    /// Get groups from processed scripts.
    /// </summary>
    /// <param name="processedScripts"></param>
    /// <returns></returns>
    private static Result<List<string>> GetGroupsFromProcessedScripts(
        List<ProcessScriptsScriptDTO> processedScripts)
    {
        if (processedScripts == null)
        {
            var error = new Error(
                "GetGroupsFromProcessedScripts",
                "La lista de grupos enviada como parámetro tiene valor nulo.");
            return error;
        }

        List<string> groups = [];

        if (processedScripts.Count > 0)
            groups = processedScripts
                .Select(s => s.Group!) // get groups
                .Where(g => g != null) // except nulls
                .Distinct() // remove duplicates
                .ToList();

        return Result<List<string>>.Success(groups); ;
    }

    /// <summary>
    /// Prepare data and sends merge scripts command
    /// </summary>
    /// <returns>Result of operation.</returns>
    private async Task<Result> MergeScripts(CancellationToken cancellationToken = default)
    {
        if (_includedScripts.Count == 0)
        {
            Error error = new(
                "MergeScripts.NoScripts",
                string.Concat([
                    "No se encontraron scripts para fusionar. ",
                    "Procese un proyecto antes de usar esta opción."
                    ]));
            Notify.Warning(_logger, error);

            return Result.Failure(error);
        }

        // prepare groups
        List<GroupDTO> groupList = [];
        if (CklGroups.Items.Count == 0)
        {
            Notify.Information(
                _logger,
                text: string.Concat(
                    ["No se cuenta con información de agrupamiento (Grupos). ",
                    "Revise su estructura de carpetas."]),
                caption: "Sin Grupos de Referencia");
        }
        else
        {
            groupList.AddRange(GetGroupsFromCheckedListBox());
        }

        // prepare scripts
        List<MergeScriptsScriptDTO> scriptsForMerge = [];

        foreach (var script in _includedScripts)
        {
            MergeScriptsScriptDTO dto = new()
            {
                Group = script.Group!,
                Path = Path.Combine(TxtProjectPath.Text, script.Path!.Replace("/", "\\"))
            };
            scriptsForMerge.Add(dto);
        }

        // assign scripts to each group
        groupList.ForEach(
            g => g.ScriptList.AddRange(
                scriptsForMerge.Where(s => s.Group == g.Name)));

        // SEND COMMAND to merge scripts
        Result resultMergeScripts = await SendMergeScriptsCommand(
            groupList.ToImmutableList(),
            cancellationToken);

        if (resultMergeScripts.IsFailure)
        {
            Notify.Warning(_logger, resultMergeScripts.Error);
            return resultMergeScripts;
        }

        Notify.Information(
            logger: _logger,
            text: string.Concat(
                "Los scripts fueron fusionados correctamente.",
                Environment.NewLine,
                "Verifíquelos en su carpeta de salida."),
            caption: "Fusión de archivos");

        return Result.Success();
    }

    /// <summary>
    /// Process project path to get the list of scripts for merging
    /// </summary>
    /// <returns></returns>
    private async Task<Result> ProcessProjectPath()
    {
        // create token for operation
        _cancellationTokenSource = new CancellationTokenSource();
        // clear related objects
        DgvExcludedScripts.DataSource = null;
        DgvIncludedScripts.DataSource = null;

        _processedScripts.Clear();
        _includedScripts.Clear();
        _excludedScripts.Clear();

        // check information in controls
        if (CheckProcessScriptsQueryParameters().IsFailure)
            return Result.Failure(new Error("ProcessProjectPath.InvalidQueryParameters"));

        // PROCESS SCRIPTS
        string sourceBranch = CmbProjectBranches.SelectedValue?.ToString() ?? string.Empty;

        // send query
        Result<IImmutableList<ProcessScriptsScriptDTO>> resultProcessScriptQuery =
            await _mediator.Send(new ProcessScriptQuery(
                ProjectPath: TxtProjectPath.Text.Trim(),
                SourceBranch: sourceBranch,
                WorkingBranch: TxtWorkingBranch.Text.Trim()));

        if (resultProcessScriptQuery.IsFailure)
        {
            Notify.Warning(_logger, resultProcessScriptQuery.Error);
            return resultProcessScriptQuery.Error;
        }

        // result without values returned
        if (resultProcessScriptQuery.Value is null || resultProcessScriptQuery.Value.Count == 0)
        {
            Notify.Information(
                _logger,
                text: "Se procesó corretamente. Sin resultados para mostrar.",
                caption: "Procesamiento Finalizado:");
        }
        else
        {
            _processedScripts.AddRange(resultProcessScriptQuery.Value);
            _excludedScripts.AddRange(_processedScripts.Where(s => s.StatusDescription == "Eliminado"));
            _includedScripts.AddRange(_processedScripts.Except(_excludedScripts));

            // get and fill group checked list box
            GbxGroups.Enabled = false;

            Result<List<string>> gettingGroupResult =
                GetGroupsFromProcessedScripts(_processedScripts);

            if (gettingGroupResult.IsFailure)
            {
                Notify.Error(_logger, gettingGroupResult.Error);
                return gettingGroupResult.Error;
            }
            else if (gettingGroupResult.Value == null)
                FillCklGroups(new List<string>().ToImmutableList());
            else
            {
                FillCklGroups(gettingGroupResult.Value.ToImmutableList());
                GbxGroups.Enabled = true;
            }

            // fill data grid view
            FillDgvIncludedScripts(_includedScripts.ToImmutableList());
        }

        return Result.Success();
    }

    /// <summary>
    /// Sends the merge script command
    /// </summary>
    /// <param name="groupDTOs"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    private async Task<Result> SendMergeScriptsCommand(
        IImmutableList<GroupDTO> groupDTOs,
        CancellationToken cancellationToken = default)
    {
        // get group list
        var mergeScriptCommand = new MergeScriptsCommand(
            RootOutputPath: Path.Combine(TxtProjectPath.Text.Trim(), "bin"),
            GroupList: [.. groupDTOs],
            InputPath: TxtProjectPath.Text.Trim());

        return await _mediator.Send(mergeScriptCommand, cancellationToken);
    }
    #endregion
}