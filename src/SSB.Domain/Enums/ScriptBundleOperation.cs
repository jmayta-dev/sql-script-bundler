using SSB.Shared.Attributes;

namespace SSB.Domain.Enums;

public enum ScriptBundleOperation
{
    [StringValue("Unir")]
    Merge,
    [StringValue("Replicar")]
    Replicate
}
