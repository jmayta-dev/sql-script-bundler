namespace SSB.Application.UseCases.Commands.MergeScripts;

/// <summary>
/// Script Data Transfer Objects for processing
/// </summary>
public record MergeScriptsScriptDTO
{
    public required string Path { get; init; }
    public required string Group { get; init; }
}