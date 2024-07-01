using SSB.Shared.Attributes;

namespace SSB.Domain.Enums;

public enum ScriptStatus
{
    [StringValue("Agregado")]
    Added = 'A',
    [StringValue("Copiado")]
    Copied = 'C',
    [StringValue("Eliminado")]
    Deleted = 'D',
    [StringValue("Modificado")]
    Modified = 'M',
    [StringValue("Emparejamiento Roto")]
    PairingBroken = 'B',
    [StringValue("Renombrado")]
    Renamed = 'R',
    [StringValue("Tipo Cambiado")]
    TypeChanged = 'T',
    [StringValue("Desconocido")]
    Unknown = 'X',
    [StringValue("No Fusionado")]
    Unmerged = 'U'
}
