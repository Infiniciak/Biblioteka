using System.Net.Http.Json;
using Library;
public class BookService
{
    private readonly HttpClient _http;

    public BookService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<BooksDto>> GetAllBooksAsync()
    {
        var books = await _http.GetFromJsonAsync<List<BooksDto>>("api/books");
        return books ?? new List<BooksDto>(); // Return empty list if null
    }

}