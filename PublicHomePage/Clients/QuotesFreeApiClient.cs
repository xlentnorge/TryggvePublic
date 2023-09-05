using Newtonsoft.Json;
using PublicHomePage.Models.QuoteClient;

namespace PublicHomePage.Clients;

public class QuotesFreeApiClient : IQuotesFreeApiClient
{
    private readonly HttpClient _httpClient;

    public QuotesFreeApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<Quote>> GetQuotes()
    {
        var response = await _httpClient.GetAsync("quotes");
        var quotes = JsonConvert.DeserializeObject<List<Quote>>(response.Content.ReadAsStringAsync().Result);
        return quotes ?? new List<Quote>();
    }
}