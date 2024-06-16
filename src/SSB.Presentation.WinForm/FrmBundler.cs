using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

namespace SSB.Presentation.WinForm
{
    public partial class FrmBundler : Form
    {
        #region Constants
        //
        // constants
        //
        private const string GIT_BASH_INSTALLATION_PATH =
            @"C:\Program Files\Git\bin\bash.exe";

        private const string PROJECT_PATH =
            @"C:\TMP\SSB Test";

        private const string OUTPUT_PATH =
            @"C:\TMP\SSB Test\Bin";

        private const string XML_SCHEMA_FILE =
            @"E:\Data\projects\csharp\sql-script-bundler\docs\ejemplo-comentario-documentacion.xsd";

        private const string AUTHOR = "Jheison";
        #endregion // Constants

        #region Properties & Variables
        //
        // private 
        //
        private readonly ILogger<FrmBundler> _logger;
        private readonly List<ScriptFile> scripts = [];
        #endregion

        #region Constructor
        public FrmBundler()
        {
            InitializeComponent();
        }

        public FrmBundler(ILogger<FrmBundler> logger) : this()
        {
            _logger = logger;
        }
        #endregion

        #region Events
        private void BtnEmpaquetar_Click(object sender, EventArgs e)
        {
            // group by folder
            var groups = scripts.GroupBy(s => s.GroupFolder).ToList();
            // read and fusion
        }

        private void BtnProcess_Click(object sender, EventArgs e)
        {
            // verify git bash is installed
            Result gitBashInstalledResult =
                GitBashInstalled(GIT_BASH_INSTALLATION_PATH);
            if (gitBashInstalledResult.IsFailure)
            {
                NotifyWarning(gitBashInstalledResult.Error);
                return;
            }

            // procesar carpeta
            ProcessProjectPath();
        }

        private void FrmBundler_Load(object sender, EventArgs e)
        {

        }
        #endregion // Events

        #region Methods
        private Result<string> ExcecuteGitCommand(string command)
        {
            string output = string.Empty;   // output container
            string error = string.Empty;    // error container

            ProcessStartInfo processStartInfo = new()
            {
                FileName = GIT_BASH_INSTALLATION_PATH,
                Arguments = $"-c \"git {command}\"",
                WorkingDirectory = PROJECT_PATH,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using Process process = new();
            process.StartInfo = processStartInfo;
            process.Start();

            _logger.LogInformation("{Message}: {Command}", "▶️ Ejecutando comando", command);

            // read command output
            output = process.StandardOutput.ReadToEnd();
            error = process.StandardError.ReadToEnd();

            process.WaitForExit();

            if (process.ExitCode != 0)
            {
                _logger.LogError("💥 Error en ejecución de comando: {Error}", error);
                return GitErrors.CommandExecutionError;
            }
            _logger.LogInformation("🔊 {NewLine}{@Message}", Environment.NewLine, output);
            return Result<string>.Success(output);
        }

        private Result GitBashInstalled(string gitBashPath)
        {
            _logger.LogInformation("{Message}", "⌛ Validando instalación de Git Bash...");
            if (!File.Exists(gitBashPath))
                return Result.Failure(GitErrors.BashNotInstalled);

            _logger.LogInformation("{Message}", "✅ Validación completa...");
            return Result.Success();
        }

        private Result GitRepositoryExists(string directoryPath)
        {
            _logger.LogInformation("{Message}", "⌛ Verificando repositorio...");
            if (!Directory.Exists(Path.Combine(directoryPath, ".git")))
                return Result.Failure(GitErrors.RepositoryNotFound);

            _logger.LogInformation("{Message}", "✅ Verificación completa...");
            return Result.Success();
        }

        private void NotifyError(Error error)
        {
            _logger.LogError("{Message}", error.Description);
            MessageBox.Show(error.Description, error.Code,
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void NotifyWarning(Error error)
        {
            _logger.LogWarning("{Message}", error.Description);
            MessageBox.Show(error.Description, error.Code,
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void ProcessProjectPath()
        {
            // verify that git repository exists in given path
            Result repositoryExistsResult = GitRepositoryExists(PROJECT_PATH);
            if (repositoryExistsResult.IsFailure)
            {
                NotifyWarning(repositoryExistsResult.Error);
                return;
            }
            // verify commits
            Result commitsExistingResult = ExcecuteGitCommand("log --oneline");
            if (commitsExistingResult.IsFailure)
            {
                NotifyError(commitsExistingResult.Error);
                return;
            }
            // get files commited: build command
            StringBuilder command = new();
            command.AppendJoin(" ",
                "log",
                $"--author=\"{AUTHOR}\"",
                "--name-status",
                "--diff-filter=tuxb",
                "--pretty=format:",
                "| sort",
                "| uniq",
                "| grep \"^.*\\.sql$\""
            );

            Result<string> changedFilesResult = ExcecuteGitCommand(command.ToString());
            if (changedFilesResult.IsFailure)
            {
                NotifyError(changedFilesResult.Error);
                return;
            }

            // process output
            string commandResult = changedFilesResult.Value ?? string.Empty;
            string[] lines = commandResult.Split(
                "\n", StringSplitOptions.RemoveEmptyEntries);

            foreach (var line in lines)
            {
                string[] parts = line.Split("\t", StringSplitOptions.RemoveEmptyEntries);
                string status = parts[0]; // A | U | D
                string scriptPath = status[0] == 'R' ? parts[2] : parts[1];    // 02_SP/KPY_ManSolCre_SP.sql

                string[] pathSections = scriptPath.Split("/");
                string folder = pathSections[0]; // 02_SP
                string scriptName = pathSections.Last(); // KPY_ManSolCre_SP.sql

                scripts.Add(new ScriptFile
                {
                    Status = status,
                    GroupFolder = folder,
                    FileName = scriptName
                });
            }

            DgvScripts.DataSource = scripts     // script container
                .OrderBy(s => s.GroupFolder)    // order by group folder
                .ThenBy(s => s.FileName)        // then order by filename
                .ToList();
            colStatus.DataPropertyName = "Status";
            colPathGroup.DataPropertyName = "GroupFolder";
            colFileName.DataPropertyName = "FileName";

            var groups = scripts.GroupBy(s => s.GroupFolder);
            if (groups.Any())
            {
                foreach (var group in groups)
                {
                    _logger.LogInformation("{Message} {Group}", "Procesando grupo", group.Key);
                    var outputScriptName = group.Key ?? string.Empty;
                    var absoluteOutputPath = Path.Combine(OUTPUT_PATH, $"{outputScriptName}.sql");
                    using StreamWriter writer = new(absoluteOutputPath);
                    // get scripts by group
                    foreach (var scriptFile in scripts.Where(s => s.GroupFolder == group.Key))
                    {
                        var absoluteScriptPath = Path.Combine(PROJECT_PATH, group.Key!, scriptFile.FileName ?? string.Empty);
                        if (File.Exists(absoluteScriptPath))
                        {
                            if (group.Key.In("02_Procedimientos", "03_Funciones"))
                            {
                                // validate comment xml block
                                var validationResult = ValidateScriptCommentBlock(absoluteScriptPath);
                                if (validationResult.IsFailure)
                                {
                                    _logger.LogWarning("{Message}", validationResult.Error.Description);
                                }
                            }
                            _logger.LogInformation("{Message} {Group}", "Procesando archivo", scriptFile.FileName);
                            string scriptFileContent = File.ReadAllText(absoluteScriptPath);
                            writer.WriteLine(scriptFileContent);
                            writer.WriteLine(new string('-', 80));
                            _logger.LogInformation("{File} {Message}", scriptFile.FileName, "Archivo procesado con éxito");
                        }
                    }
                }
            }
            // generar changelog con cambios realizados
            _logger.LogInformation("{Message}", "🙌 Procesamiento Exitoso!");
        }

        private Result ValidateScriptCommentBlock(string scriptPath, char? statusChar = null)
        {
            // valida que el archivo exista
            if (!File.Exists(scriptPath))
                return Result.Failure(new Error("Script.FileNotFound", "No existe el script en la ruta brindada."));

            // captura encabezado de comentario
            string scriptText = File.ReadAllText(scriptPath);

            // extrae el bloque del comentario
            string xmlPattern = @"\/\*([\s\S]*?)\*\/";
            Match match = Regex.Match(scriptText, xmlPattern);

            string xmlCommentText = string.Empty;
            if (match.Success)
            {
                xmlCommentText = match.Groups[1].Value;
            }

            if (string.IsNullOrWhiteSpace(xmlCommentText))
            {
                return Result.Failure(new Error("Script.NoCommentBlock", "El script no cuenta con bloque de comentario."));
            }

            // validar estructura/formato
            var resultValidateXml = ValidateXmlStructure(xmlCommentText, Path.Combine(XML_SCHEMA_FILE));
            if (resultValidateXml.IsFailure)
            {
                // qué pasa cuando falla la validación
                return resultValidateXml;
            }

            // validar bloques según tipo de modificación
            XDocument xDoc = XDocument.Parse(xmlCommentText);

            if (statusChar != null)
            {
                switch ((NameStatus)statusChar)
                {
                    case NameStatus.Added:
                        // validar que el "objetivo, autor y fecha de creación" no sea vacío
                        XElement? descriptionXmlElement = xDoc.Descendants("objetivo").FirstOrDefault();
                        XElement? authorXmlElement = xDoc.Descendants("autor").FirstOrDefault();
                        XElement? creationDateXmlElement = xDoc.Descendants("fechaCreacion").FirstOrDefault();
                        if (string.IsNullOrWhiteSpace(descriptionXmlElement?.Value))
                        {
                            return Result.Failure(
                                new Error("Script.NoDescription", "Ingrese el \"objetivo\" del script."));
                        }
                        break;
                    case NameStatus.Copied:
                    case NameStatus.Modified:
                    case NameStatus.Renamed:
                        // validar que el último comentario no sea vacío y tenga sus parámetros
                        XElement? commentXmlElement = xDoc.Descendants("modificacion").LastOrDefault();
                        if (string.IsNullOrWhiteSpace(commentXmlElement?.Value))
                        {
                            return Result.Failure(
                                new Error("Script.NoComment", "Script sin \"comentario\" de modificación"));
                        }
                        break;
                    default:
                        break;
                }
            }
            return Result.Success();
        }

        private Result ValidateXmlStructure(string xml, string xsdPath)
        {
            if (!File.Exists(xsdPath))
                return Result.Failure(new Error("File.NotFound", "No existe el archivo en la ruta brindada."));

            XmlSchemaSet schemas = new();
            schemas.Add(string.Empty, xsdPath);
            XmlDocument xmlDoc = new();
            xmlDoc.LoadXml(xml);

            xmlDoc.Schemas = schemas;

            xmlDoc.Validate((sender, e) =>
            {
                if (e.Severity == XmlSeverityType.Warning)
                {
                    Console.WriteLine($"Warning: {e.Message}");
                }
                else if (e.Severity == XmlSeverityType.Error)
                {
                    Result.Failure(new Error("XmlValidator.Error", $"{e.Message}")); // TODO:cambiar
                }
            });

            return Result.Success();
        }
    }
    #endregion // Methods
}

public sealed record Error(string Code, string? Description = null)
{
    public static readonly Error None = new(string.Empty);

}

public record Result
{
    #region Properties
    public bool IsSuccess { get; set; }
    public bool IsFailure => !IsSuccess;
    public Error Error { get; }
    #endregion // Properties

    #region Constructor
    public Result(bool isSuccess, Error error)
    {
        if (isSuccess && error != Error.None || !isSuccess && error == Error.None)
            throw new ArgumentException("Invalid error", nameof(error));

        IsSuccess = isSuccess;
        Error = error;
    }
    #endregion // Constructor

    #region Methods
    public static Result Success() => new(true, Error.None);
    public static Result Failure(Error error) => new(false, error);
    #endregion // Methods
}

public sealed record Result<T> : Result where T : class
{
    public T? Value { get; init; }

    public Result(bool isSuccess, Error error, T? value = default) : base(isSuccess, error)
    {
        Value = value;
    }

    public static Result<T> Success(T? value) => new Result<T>(true, Error.None, value);
    public static Result<T> Failure(Error error, T? value = default) => new Result<T>(false, Error.None, null);

    public static implicit operator Result<T>(Error error) => Failure(error);
}

// pasar a capa servicio git
public static class GitErrors
{
    public static readonly Error BashNotInstalled = new(
        "Git.BashNotInstalled",
        "Git Bash no está instalado en su ordenador.");

    public static readonly Error RepositoryNotFound = new(
        "Git.RepositoryNotFound",
        "No existe un repositorio en la ruta especificada.");

    public static readonly Error CommandExecutionError = new(
        "Git.CommandExecutionError",
        "Error de ejecución de comando");
}

public record ScriptFile
{
    public string? Status { get; set; }
    public string? GroupFolder { get; set; }
    public string? FileName { get; set; }
    public string? Description { get; set; }
    public string? Observation { get; set; }
}

public enum NameStatus
{
    Added = 'A',
    Copied = 'C',
    Deleted = 'D',
    Modified = 'M',
    Renamed = 'R',
    TypeChanged = 'T',
    Unmerged = 'U',
    Unknown = 'X',
    PairingBroken = 'B'
}

public static class ExtensionClass
{
    /// <summary>
    /// Determines whether a value is contained in list of params.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value"></param>
    /// <param name="params">List of values to compare.</param>
    /// <returns></returns>
    public static bool In<T>(this T value, params T[] @params)
    {
        ArgumentNullException.ThrowIfNull(@params, nameof(@params));
        return @params.ToHashSet().Contains(value);
    }
}