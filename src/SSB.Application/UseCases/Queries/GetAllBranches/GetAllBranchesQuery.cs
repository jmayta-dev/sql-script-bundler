using MediatR;
using SSB.Shared.Abstractions;
using System.Collections.Immutable;

namespace SSB.Application.UseCases.Queries.GetAllBranches
{
    public record GetAllBranchesQuery(string ProjectPath) :
        IRequest<Result<IImmutableList<GetAllBranchesBranchDTO>>>;
}
