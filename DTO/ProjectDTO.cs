namespace Fundsy_backend.DTO
{
    public class ProjectDTO
    {
        public string? Title { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public decimal Goal { get; set; }
        public int CategoryId { get; set; }
        public string ProjectImage { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
    }
}
