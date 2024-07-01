using MediatR;
using SSB.Shared.Abstractions;
using System.Collections.Immutable;

namespace SSB.Application.UseCases.Queries.ProcessScripts;

public record ProcessScriptQuery(
    string ProjectPath, string SourceBranch, string WorkingBranch)
        : IRequest<Result<IImmutableList<ProcessScriptsScriptDTO>>>;
