using FakeItEasy;
using FluentAssertions;
using PublicHomePage.Clients;
using PublicHomePage.Models.QuoteClient;
using PublicHomePage.Providers;

namespace TryggvePublicTest.Providers;

public class QuoteProviderTests
{
    private readonly IQuotesFreeApiClient _quotesFreeApiClientFake;
    private readonly IQuoteProvider _quoteProvider;

    public QuoteProviderTests()
    {
        _quotesFreeApiClientFake = A.Fake<IQuotesFreeApiClient>();
        _quoteProvider = new QuoteProvider(_quotesFreeApiClientFake);
    }

    [Fact]
    public async void TestQuoteOftheDay()
    {
        // Arrange
        var quotes = new List<Quote>()
        {
            new()
            {
                Text = "FakeItEasy is fun",
                Author = "My, Myself and I"
            }
        };
        A.CallTo(() => _quotesFreeApiClientFake.GetQuotes()).Returns(quotes);


        // Act
        var result = await _quoteProvider.QuoteOfTheDay();

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(quotes.FirstOrDefault());
        A.CallTo(() => _quotesFreeApiClientFake.GetQuotes()).MustHaveHappenedOnceExactly();
    }
}