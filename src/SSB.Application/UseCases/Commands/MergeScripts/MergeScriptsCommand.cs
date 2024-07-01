using MediatR;
using SSB.Shared.Abstractions;

namespace SSB.Application.UseCases.Commands.MergeScripts;

public record MergeScriptsCommand(
    string RootOutputPath, List<GroupDTO> GroupList, string? InputPath = null) : IRequest<Result>;
