using AutoMapper;
using SSB.Application.UseCases.Queries.ProcessScripts;
using SSB.Domain.Entities;

namespace SSB.Application.Profiles;

internal class ScriptProfile : Profile
{
    public ScriptProfile()
    {
        CreateMap<Script, ProcessScriptsScriptDTO>();
    }
}
