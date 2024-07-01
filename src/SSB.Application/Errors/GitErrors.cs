using SSB.Shared.Abstractions;

namespace SSB.Application.Errors;

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