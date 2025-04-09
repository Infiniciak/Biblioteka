using Library;
using System.Net.Http.Json;

public class BorrowService
{
    private readonly HttpClient _http;

    public BorrowService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<BorrowsDto>> GetAllBorrowsAsync()
    {
        {
            var borrows = await _http.GetFromJsonAsync<List<BorrowsDto>>("api/borrows");
            return borrows ?? new List<BorrowsDto>(); // Return empty list if null
        }
    }
}