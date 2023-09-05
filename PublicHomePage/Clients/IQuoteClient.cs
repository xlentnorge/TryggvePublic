using PublicHomePage.Models.QuoteClient;

namespace PublicHomePage.Clients;

public interface IQuoteClient
{
    Task<IEnumerable<Quote>> GetQuotes();
}