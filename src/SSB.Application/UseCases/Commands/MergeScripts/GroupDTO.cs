using SSB.Domain.Enums;

namespace SSB.Application.UseCases.Commands.MergeScripts;

/// <summary>
/// Group Data Transfer Object to store group information and list of
/// Script Data Transfer Objects for merging process
/// </summary>
public record GroupDTO
{
    public required string Name { get; init; }
    public ScriptBundleOperation Operation { get; init; }
    public List<MergeScriptsScriptDTO> ScriptList { get; set; } = [];
}
