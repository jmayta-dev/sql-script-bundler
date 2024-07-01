using MediatR;
using SSB.Application.Errors;
using SSB.Domain.Contracts;
using SSB.Shared.Abstractions;
using System.Collections.Immutable;

namespace SSB.Application.UseCases.Queries.GetAllBranches
{
    public class GetAllBranchesQueryHandler :
        IRequestHandler<GetAllBranchesQuery, Result<IImmutableList<GetAllBranchesBranchDTO>>>
    {

        #region Properties & Variables
        private readonly IGitService _gitService;
        #endregion // Properties & Variables

        #region Constructor
        public GetAllBranchesQueryHandler(IGitService gitService)
        {
            ArgumentNullException.ThrowIfNull(gitService);
            _gitService = gitService;
        }
        #endregion // Constructor

        #region Methods
        public async Task<Result<IImmutableList<GetAllBranchesBranchDTO>>> Handle(
            GetAllBranchesQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.ProjectPath))
                return new Error(
                    "ProjectPath.NotProvided",
                    "Especifique un directorio de trabajo para procesar.");

            // check git-bash installation
            var bashInstallationVerification = await _gitService.CheckBashInstallation();
            if (bashInstallationVerification.IsFailure)
                return GitErrors.BashNotInstalled;

            // check repository existence
            var repositoryValidation = await _gitService.CheckRepositoryExists(request.ProjectPath);
            if (repositoryValidation.IsFailure)
                return GitErrors.RepositoryNotFound;

            var resultGettingBranchList = await GetBranchList(request.ProjectPath);
            if (resultGettingBranchList.IsFailure || resultGettingBranchList.Value == null)
                return Result<IImmutableList<GetAllBranchesBranchDTO>>.Failure(
                    new Error("GetAllBranchesQueryHandler.GetBranchList"));

            return Result<IImmutableList<GetAllBranchesBranchDTO>>.Success(
                    resultGettingBranchList.Value.ToImmutableList());
        }

        private async Task<Result<List<GetAllBranchesBranchDTO>>> GetBranchList(
            string workingDirectory)
        {
            List<GetAllBranchesBranchDTO> branchList = [];

            // command execution
            var resultCommandExecuted = await _gitService.ExecuteCommand(
                "git branch -a", workingDirectory);
            if (resultCommandExecuted.IsFailure || resultCommandExecuted == null)
                return GitErrors.CommandExecutionError;

            // process command output
            if (!string.IsNullOrWhiteSpace(resultCommandExecuted.Value))
            {
                string[] lines = resultCommandExecuted.Value.Split(
                    '\n', StringSplitOptions.RemoveEmptyEntries);

                List<string[]> columns = lines.Select(
                    l => l.Split(" ", StringSplitOptions.RemoveEmptyEntries)).ToList();

                string[] workingBranch = columns.FirstOrDefault(
                    l => l.Length > 1, [string.Empty, string.Empty]);

                if (columns.Remove(workingBranch))
                    columns.Add(workingBranch.Skip(1).ToArray());

                var branches = columns.SelectMany(c => c).ToList();
                branchList = branches.ConvertAll(b => new GetAllBranchesBranchDTO
                {
                    Name = b,
                    IsCurrent = b == workingBranch[1]
                }).ToList();
            }
            return Result<List<GetAllBranchesBranchDTO>>.Success(branchList);
        }
        #endregion // Methods
    }
}
