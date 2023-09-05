using Newtonsoft.Json;
using PublicHomePage.Models.QuoteClient;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace PublicHomePage.Clients;

public class QuoteClient : IQuoteClient
{
    private readonly HttpClient _httpClient;

    public QuoteClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<Quote>> GetQuotes()
    {
        var results = await _httpClient.GetAsync("quotes");
        var contents = JsonConvert.DeserializeObject<List<Quote>>(results.Content.ReadAsStringAsync().Result);
        return contents ?? new List<Quote>();
    }
}