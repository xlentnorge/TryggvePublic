using PublicHomePage.Models.QuoteClient;

namespace PublicHomePage.Providers;

public interface IQuoteProvider
{
   public Task<Quote> QuoteOfTheDay();
}