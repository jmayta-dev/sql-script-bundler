using AutoMapper;
using SSB.Application.UseCases.Commands.MergeScripts;
using SSB.Application.UseCases.Queries.ProcessScripts;

namespace SSB.Presentation.WinForm.MapProfiles;

public class ScriptDTOProfile : Profile
{
    public ScriptDTOProfile()
    {
        CreateMap<ProcessScriptsScriptDTO, MergeScriptsScriptDTO>();
    }
}
