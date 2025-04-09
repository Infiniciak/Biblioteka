
using Library;
using System.Net.Http.Json;

public class MemberService
{
    private readonly HttpClient _httpClient;

    public MemberService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<MembersDto>> GetAllMembersAsync()
    {
        {
            var members = await _httpClient.GetFromJsonAsync<List<MembersDto>>("api/members");
            return members ?? new List<MembersDto>(); // Return empty list if null
        }
    }
   
}