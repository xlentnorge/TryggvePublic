using PublicHomePage.Models.QuoteClient;

namespace PublicHomePage.Models.ViewModels;

public class HomeModel
{
    public HomeModel(Quote quote)
    {
        Quote = quote;
    }

    public Quote Quote { get; set; }
}