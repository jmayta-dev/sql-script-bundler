using SSB.Shared.Abstractions;

namespace SSB.Domain.Contracts;

public interface IGitService : IDisposable
{
    /// <summary>
    /// Check if GitBash is installed in the system
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Result> CheckBashInstallation(CancellationToken cancellationToken = default);
    Task<Result> CheckRepositoryExists(string workingDirectory, CancellationToken cancellationToken = default);
    Task<Result<string>> GetBranchName(string? repositoryPath = null, CancellationToken cancellationToken = default);
    /// <summary>
    /// Execute a git command
    /// </summary>
    /// <param name="command"></param>
    /// <param name="workingDirectory">Optional: Working directory where the command runs</param>
    /// <returns>The command output if was succesfull.</returns>
    Task<Result<string>> ExecuteCommand(string command, string? workingDirectory = null, CancellationToken cancellationToken = default);
}