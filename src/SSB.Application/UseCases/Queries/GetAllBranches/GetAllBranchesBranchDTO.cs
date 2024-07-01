namespace SSB.Application.UseCases.Queries.GetAllBranches
{
    public record GetAllBranchesBranchDTO
    {
        public required string Name { get; set; }
        public bool IsCurrent { get; set; }
    }
}
