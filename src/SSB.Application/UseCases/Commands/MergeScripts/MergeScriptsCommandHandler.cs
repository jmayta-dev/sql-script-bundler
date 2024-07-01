using MediatR;
using SSB.Domain.Contracts;
using SSB.Domain.Enums;
using SSB.Shared.Abstractions;
using SSB.Shared.Helpers;
using System.Text;

namespace SSB.Application.UseCases.Commands.MergeScripts;

public class MergeScriptsCommandHandler : IRequestHandler<MergeScriptsCommand, Result>
{
    #region Properties & Variables
    private readonly IGitService _gitService;
    private string? _rootOutputPath;
    private string? _rootInputPath;
    #endregion // Properties & Variables

    #region Constructor
    public MergeScriptsCommandHandler(IGitService gitService)
    {
        ArgumentNullException.ThrowIfNull(gitService);
        _gitService = gitService;
    }
    #endregion // Constructor


    #region Methods
    /// <summary>
    /// Command Handler.
    /// Merge the list of Scripts Data Transfer Objects passed by the request object
    /// in a single script file.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<Result> Handle(
        MergeScriptsCommand request, CancellationToken cancellationToken)
    {
        if (request?.GroupList == null)
            return new Error("List.NullOrEmptyList", "No se enviaron elementos para procesar.");

        // if no elements, execution finalize immediately
        if (request.GroupList.Count == 0)
            return Result.Success();

        // prepare output directory
        string outputFolderName, outputFolderPath;

        _rootOutputPath = string.IsNullOrWhiteSpace(request.RootOutputPath) ? "." : request.RootOutputPath;
        _rootInputPath = string.IsNullOrWhiteSpace(request.InputPath) ? "." : request.InputPath;
        outputFolderName = await GetOutputFolderName(_rootInputPath, cancellationToken);
        outputFolderPath = Path.Combine(_rootOutputPath, outputFolderName);

        if (Directory.Exists(outputFolderPath))
            Directory.Delete(outputFolderPath, recursive: true);

        Directory.CreateDirectory(outputFolderPath);

        List<Task<Result>> processTasks = [];

        foreach (var group in request.GroupList)
        {
            if (group.ScriptList == null || group.ScriptList.Count == 0)
                continue; // continues with next group

            if (group.Operation == ScriptBundleOperation.Merge)
                processTasks.Add(
                    MergeScriptsAsync(group, outputFolderPath, cancellationToken));
            else if (group.Operation == ScriptBundleOperation.Replicate)
                processTasks.Add(
                    ReplicateScriptsAsync(group, outputFolderPath, cancellationToken));
            else
                return Result.Failure(
                    new Error(
                        Code: "Operation.NotConfigured",
                        Description: "La operación no está configurada o implementada."));
        }

        Result[] resultBulkProcess = await Task.WhenAll(processTasks);

        StringBuilder errors = new();
        bool hasFailures = false;

        // if errors collect them
        foreach (var result in resultBulkProcess)
        {
            if (result.IsFailure)
            {
                hasFailures = true;
                errors.AppendLine(result.Error.Description);
            }
        }

        if (hasFailures)
        {
            return Result.Failure(new Error("MergeScriptsCommandHandler", errors.ToString()));
        }

        return Result.Success();
    }

    /// <summary>
    /// Merges the group of scripts in a single file.
    /// </summary>
    /// <param name="groupForMerge"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    private async Task<Result> MergeScriptsAsync(
        GroupDTO groupForMerge,
        string outputFolderPath,
        CancellationToken cancellationToken = default)
    {
        string outputScriptFileName = $"{groupForMerge.Name}.sql";
        string absoluteOutputFilePath = Path.Combine(outputFolderPath, outputScriptFileName);

        // write the script file
        using StreamWriter writer = new(absoluteOutputFilePath);
        foreach (var script in groupForMerge.ScriptList)
        {
            StringBuilder scriptText = new();
            if (!File.Exists(script.Path)) continue;

            // read the script content
            string currentScriptContent = await File.ReadAllTextAsync(script.Path, cancellationToken);
            scriptText.AppendLine(currentScriptContent);

            // write the string builder content obtained from scripts
            await writer.WriteLineAsync(scriptText, cancellationToken);
            // add a new line with 80 hyphens (-)
            writer.WriteLine(new string('-', 80));
        }
        return Result.Success();
    }

    private async Task<Result> ReplicateScriptsAsync(
        GroupDTO groupForReplicate,
        string outputFolderPath,
        CancellationToken cancellationToken = default)
    {
        int iteration = 0;
        foreach (var script in groupForReplicate.ScriptList)
        {
            if (!File.Exists(script.Path)) continue;

            iteration++;
            string scriptFileName = $"{groupForReplicate.Name}_{iteration}.sql";
            string absotuleOutputPath = Path.Combine(outputFolderPath, scriptFileName);
            // copy the script in a new location
            try
            {
                await Task.Run(
                    () => File.Copy(
                        Path.Combine(script.Path),
                        Path.Combine(absotuleOutputPath)
                        ), cancellationToken);
            }
            catch (Exception ex)
            {
                return new Error(
                    "MergeScriptsCommandHandler.ReplicateScriptsAsync",
                    ex.Message);
            }
        }
        return Result.Success();
    }

    private async Task<string> GetOutputFolderName(string? repositoryPath = default, CancellationToken cancellationToken = default)
    {
        string outputFolderName;
        Result<string> resultGetBranchName = await _gitService.GetBranchName(repositoryPath);
        if (resultGetBranchName.IsFailure || string.IsNullOrEmpty(resultGetBranchName.Value))
        {
            var randomStringName = StringHelpers.RandomString(StringHelpers.PrintableSafe, 6);
            outputFolderName = $"No-Name-{randomStringName}";
        }
        else
        {
            outputFolderName = resultGetBranchName.Value.Trim();
        }
        return outputFolderName;
    }
    #endregion // Methods
}