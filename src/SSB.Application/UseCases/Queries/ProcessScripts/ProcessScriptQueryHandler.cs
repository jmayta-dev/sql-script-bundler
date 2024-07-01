using AutoMapper;
using MediatR;
using SSB.Application.Errors;
using SSB.Domain.Contracts;
using SSB.Domain.Entities;
using SSB.Domain.Enums;
using SSB.Shared.Abstractions;
using System.Collections.Immutable;

namespace SSB.Application.UseCases.Queries.ProcessScripts
{
    public class ProcessScriptQueryHandler :
        IRequestHandler<ProcessScriptQuery,
        Result<IImmutableList<ProcessScriptsScriptDTO>>>
    {
        #region Properties & Variables
        //
        // dependencies
        //
        private readonly IGitService _gitService;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public ProcessScriptQueryHandler(IGitService gitService, IMapper mapper)
        {
            ArgumentNullException.ThrowIfNull(gitService);
            ArgumentNullException.ThrowIfNull(mapper);

            _gitService = gitService;
            _mapper = mapper;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Handle method.
        /// Process the scripts in the folder path passed in the request.
        /// </summary>
        /// <param name="request">Process Script Object Request</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Result<IImmutableList<ProcessScriptsScriptDTO>>> Handle(
            ProcessScriptQuery request, CancellationToken cancellationToken)
        {
            // check git-bash installation
            var bashInstallationVerification = await _gitService.CheckBashInstallation();
            if (bashInstallationVerification.IsFailure)
                return GitErrors.BashNotInstalled;

            // check repository existence
            var repositoryValidation = await _gitService.CheckRepositoryExists(request.ProjectPath);
            if (repositoryValidation.IsFailure)
                return GitErrors.RepositoryNotFound;

            // get files throught command execution
            string[] commandSegments = [
                "git",
                "diff",
                "--name-status",
                "--diff-filter=tuxb",
                $"{request.SourceBranch}..{request.WorkingBranch}",
                "--pretty=format:",
                "| sort",
                "| grep \"^.*\\.sql$\""
            ];

            var getFileLogCommandExecution = await _gitService.ExecuteCommand(
                command: string.Join(" ", commandSegments),
                workingDirectory: request.ProjectPath);

            if (getFileLogCommandExecution.IsFailure)
                return GitErrors.CommandExecutionError;

            // process the output command execution
            var processOutputResult = ProcessOutput(getFileLogCommandExecution.Value);
            if (processOutputResult.IsFailure)
                return Result<IImmutableList<ProcessScriptsScriptDTO>>.Failure(
                    new Error("ProcessOutput", "Error al procesar la salida del comando."));

            // empty result value
            if (processOutputResult.Value == null || processOutputResult.Value.Count == 0)
                return processOutputResult;

            // handle duplicates
            ImmutableList<ProcessScriptsScriptDTO> temporalScriptList =
                processOutputResult.Value
                    .AsEnumerable() // convert to mutable 
                    .ToHashSet<ProcessScriptsScriptDTO>() // remove duplicates
                    .ToImmutableList(); // convert to immutable

            return Result<IImmutableList<ProcessScriptsScriptDTO>>.Success(temporalScriptList);
        }

        /// <summary>
        /// Process the output command passed by parameter. If it's empty string
        /// or null, returns a success result object, this as result of processing nothing.
        /// </summary>
        /// <param name="output"></param>
        /// <returns></returns>
        private Result<IImmutableList<ProcessScriptsScriptDTO>> ProcessOutput(string? output)
        {
            List<ProcessScriptsScriptDTO> scriptDTOs = [];

            if (!string.IsNullOrWhiteSpace(output))
            {
                var scriptBuilder = Script.ScriptBuilder.Empty();

                // separe lines one by one
                string[] lines = output!.Split("\n", StringSplitOptions.RemoveEmptyEntries);
                foreach (string line in lines)
                {
                    string[] columnLine = line.Split("\t", StringSplitOptions.RemoveEmptyEntries);
                    string status = columnLine[0];
                    string filePath =
                        Convert.ToChar(status[0]) == Convert.ToChar(ScriptStatus.Renamed) ?
                            columnLine[2] : columnLine[1];

                    // build Script entity
                    var script = scriptBuilder
                        .WithDescription(
                            $"Archivo con nombre " +
                            filePath.Split("/").LastOrDefault("sin-nombre.sql"))
                        .WithPath(filePath)
                        .WithStatus(status[0])
                        .Build();

                    // map from Script to ScriptDTO
                    var scriptDTO = _mapper.Map<ProcessScriptsScriptDTO>(script);

                    scriptDTOs.Add(scriptDTO);
                }
            }
            return Result<IImmutableList<ProcessScriptsScriptDTO>>.Success(scriptDTOs.ToImmutableList());
        }
        #endregion
    }
}
