using System.Text.Json;
using LibrarySystem.Application.DTOs;
using LibrarySystem.Application.Interfaces.Services;

namespace LibrarySystem.Infrastructure.ExternalServices.OpenLibrary;

public class OpenLibraryClient : IOpenLibraryClient
{
    private readonly HttpClient _httpClient;

    public OpenLibraryClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("https://openlibrary.org/");
    }

    public async Task<List<OpenLibraryBookDto>> SearchAsync(string query, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(query))
            return new List<OpenLibraryBookDto>();

        var url = $"search.json?q={Uri.EscapeDataString(query)}&limit=10";

        try
        {
            var response = await _httpClient.GetAsync(url, cancellationToken);

            if (!response.IsSuccessStatusCode)
                return new List<OpenLibraryBookDto>();

            var json = await response.Content.ReadAsStringAsync(cancellationToken);
            var result = JsonSerializer.Deserialize<OpenLibraryResponse>(json);

            if (result?.Docs == null || result.Docs.Count == 0)
                return new List<OpenLibraryBookDto>();

            return result.Docs
                .Where(d => !string.IsNullOrWhiteSpace(d.Title))
                .Select(MapToDto)
                .ToList();
        }
        catch (HttpRequestException)
        {
            // Open Library är nere eller nätverksfel
            return new List<OpenLibraryBookDto>();
        }
        catch (JsonException)
        {
            // Kunde inte tolka svaret
            return new List<OpenLibraryBookDto>();
        }
    }

    public async Task<OpenLibraryBookDto?> GetByIsbnAsync(string isbn, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(isbn))
            return null;

        var url = $"search.json?q=isbn:{Uri.EscapeDataString(isbn)}&limit=1";

        try
        {
            var response = await _httpClient.GetAsync(url, cancellationToken);

            if (!response.IsSuccessStatusCode)
                return null;

            var json = await response.Content.ReadAsStringAsync(cancellationToken);
            var result = JsonSerializer.Deserialize<OpenLibraryResponse>(json);

            var doc = result?.Docs?.FirstOrDefault();
            if (doc == null || string.IsNullOrWhiteSpace(doc.Title))
                return null;

            return MapToDto(doc);
        }
        catch (HttpRequestException)
        {
            return null;
        }
        catch (JsonException)
        {
            return null;
        }
    }

    private static OpenLibraryBookDto MapToDto(OpenLibraryDoc doc)
    {
        return new OpenLibraryBookDto
        {
            Title = doc.Title,
            Author = doc.AuthorName?.FirstOrDefault() ?? "Okänd författare",
            ISBN = doc.Isbn?.FirstOrDefault() ?? string.Empty,
            CoverUrl = doc.CoverId.HasValue
                ? $"https://covers.openlibrary.org/b/id/{doc.CoverId}-M.jpg"
                : null,
            PublishYear = doc.FirstPublishYear
        };
    }
}