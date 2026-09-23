using LibrarySystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibrarySystem.Application.DTOs
{
    public class BookDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;
        public bool IsAvailable { get; set; }

        public string? CoverUrl { get; set; }
    }
}
