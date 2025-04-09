using System.Net.Http.Json;
using Library;
public class CategoryService
{
    private readonly HttpClient _http;

    public CategoryService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<CategoriesDto>> GetAllCategoriesAsync()
    {
        {
            var categories = await _http.GetFromJsonAsync<List<CategoriesDto>>("api/categories");
            return categories ?? new List<CategoriesDto>(); // Return empty list if null
        }
    }
}