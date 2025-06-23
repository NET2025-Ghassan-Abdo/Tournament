namespace Tournament.Core.DTOs
{
    public record TournamentUpdateDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
    }
}
