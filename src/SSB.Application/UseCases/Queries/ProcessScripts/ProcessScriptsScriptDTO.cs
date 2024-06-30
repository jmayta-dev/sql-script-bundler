namespace SSB.Application.UseCases.Queries.ProcessScripts
{
    /// <summary>
    /// Script Data Transfer Objects for processing
    /// </summary>
    public record ProcessScriptsScriptDTO
    {
        public string? Description { get; set; }
        public string? FileName { get; set; }
        public string? Group { get; set; }
        public string? Observation { get; set; }
        public string? Path { get; set; }
        public string? StatusDescription { get; set; }
    }
}