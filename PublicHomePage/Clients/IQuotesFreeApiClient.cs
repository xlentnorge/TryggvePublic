using PublicHomePage.Models.QuoteClient;

namespace PublicHomePage.Clients;

public interface IQuotesFreeApiClient
{
    Task<IEnumerable<Quote>> GetQuotes();
}