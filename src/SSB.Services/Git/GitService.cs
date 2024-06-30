using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SSB.Application.Errors;
using SSB.Domain.Contracts;
using SSB.Shared.Abstractions;
using System.Diagnostics;

namespace SSB.Services.Git;

public class GitService : IGitService
{
    #region Constants
    private readonly string INSTALLATION_PATH;
    #endregion

    #region Properties & Variables
    //
    // private
    //
    private bool disposed = false;
    private readonly ILogger<GitService> _logger;
    #endregion

    #region Constructor
    public GitService(IConfiguration configuration, ILogger<GitService> logger)
    {
        ArgumentNullException.ThrowIfNull(configuration);

        INSTALLATION_PATH = configuration["Git:Bash:InstallationPath"] ??
            throw new InvalidOperationException(
                "Git.Bash.InstallationPath key not found in settings file.");

        _logger = logger;
    }
    #endregion

    #region Disposable Support
    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                // something to do
            }
            disposed = true;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    #endregion

    #region Methods
    public async Task<Result> CheckBashInstallation(CancellationToken cancellationToken = default)
    {
        return await Task.Run(() =>
        {
            _logger.LogDebug("⏳ Verificando instalación de {Application}...", "Git Bash");
            if (!File.Exists(INSTALLATION_PATH))
                return GitErrors.BashNotInstalled;

            _logger.LogDebug("✅ Verificación completa.");
            return Result.Success();
        }, cancellationToken);
    }

    public async Task<Result> CheckRepositoryExists(
        string workDir, CancellationToken cancellationToken = default)
    {
        return await Task.Run(() =>
        {
            _logger.LogDebug("⏳ Verificando repositorio...");
            if (!Directory.Exists(Path.Combine(workDir, ".git")))
                return GitErrors.RepositoryNotFound;

            _logger.LogDebug("✅ Verificación completa.");
            return Result.Success();
        }, cancellationToken);
    }

    public async Task<Result<string>> GetBranchName(
        string? repositoryPath = null, CancellationToken cancellationToken = default)
    {
        string[] commandSegments = [
            "git",
            "rev-parse",
            "--abbrev-ref",
            "HEAD"
        ];
        Result<string> resultCommandExecution = await ExecuteCommand(
            command: string.Join(" ", commandSegments),
            workingDirectory: repositoryPath,
            cancellationToken);

        if (resultCommandExecution.IsFailure)
            return resultCommandExecution.Error;

        return Result<string>.Success(resultCommandExecution.Value!);
    }

    public async Task<Result<string>> ExecuteCommand(
        string command,
        string? workingDirectory = null,
        CancellationToken cancellationToken = default)
    {
        string output;   // output container
        string error;    // error container

        ProcessStartInfo processStartInfo = new()
        {
            FileName = INSTALLATION_PATH,
            Arguments = $"-c \"{command}\"",
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            RedirectStandardInput = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        if (!string.IsNullOrWhiteSpace(workingDirectory))
            processStartInfo.WorkingDirectory = workingDirectory;

        using Process process = new();
        process.StartInfo = processStartInfo;
        process.Start();

        _logger.LogDebug("▶️ Ejecutando comando: {Command}", command);

        // read command output
        output = await process.StandardOutput.ReadToEndAsync(cancellationToken);
        error = await process.StandardError.ReadToEndAsync(cancellationToken);

        await process.WaitForExitAsync(cancellationToken);

        if (process.ExitCode != 0)
        {
            _logger.LogError("💥 Error en ejecución de comando: {Error}", error);
            return GitErrors.CommandExecutionError;
        }
        _logger.LogInformation("🔊 Respuesta:{NewLine}{@Message}", Environment.NewLine, output);
        return Result<string>.Success(output);
    }
    #endregion

}
