using PublicHomePage.Clients;
using PublicHomePage.Models.QuoteClient;

namespace PublicHomePage.Providers;

public class QuoteProvider : IQuoteProvider
{
    private readonly IQuotesFreeApiClient _quotesFreeApiClient;

    public QuoteProvider(IQuotesFreeApiClient quotesFreeApiClient)
    {
        _quotesFreeApiClient = quotesFreeApiClient;
    }

    public async Task<Quote> QuoteOfTheDay()
    {
        var quotes = await _quotesFreeApiClient.GetQuotes();
        var random = new Random();
        var enumerable = quotes.ToList();
        var index = random.Next(0, enumerable.Count());
        return enumerable.ElementAt(index);
    }
}