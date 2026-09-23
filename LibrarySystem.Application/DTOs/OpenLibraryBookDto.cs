namespace LibrarySystem.Application.DTOs
{
    public class OpenLibraryBookDto
    {
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;
        public string? CoverUrl { get; set; }
        public int? PublishYear { get; set; }
    }
}
