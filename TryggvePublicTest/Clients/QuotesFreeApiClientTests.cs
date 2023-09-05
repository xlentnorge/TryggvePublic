using FakeItEasy;
using FluentAssertions;
using Newtonsoft.Json;
using PublicHomePage.Clients;
using PublicHomePage.Models.QuoteClient;

namespace TryggvePublicTest.Clients;

public class QuotesFreeApiClientTests
{
    private readonly IQuotesFreeApiClient _quotesFreeApiClient;
    private readonly HttpClient _httpClient;
    private readonly HttpMessageHandler handler;

    public QuotesFreeApiClientTests()
    {
        handler = A.Fake<HttpMessageHandler>();
        _httpClient = new HttpClient(handler);
        _httpClient.BaseAddress = new Uri("https://test.no");
        _quotesFreeApiClient = new QuotesFreeApiClient(_httpClient);
    }

    [Fact]
    public async Task GetQuotes_ShouldReturnQuotes()
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
        var json = JsonConvert.SerializeObject(quotes);
        var response = new HttpResponseMessage
        {
            // Serialize as json
            Content = new StringContent(json)
        };
        A.CallTo(handler)
            .WithReturnType<Task<HttpResponseMessage>>()
            .Where(call => call.Method.Name == "SendAsync")
            .Returns(response);

        // Act
        var result = await _quotesFreeApiClient.GetQuotes();

        //Verify
        var list = result.ToList();
        list.Should().NotBeEmpty();
        list.FirstOrDefault().Should().BeEquivalentTo(quotes.FirstOrDefault());
        A.CallTo(handler)
            .WithReturnType<Task<HttpResponseMessage>>()
            .Where(call => call.Method.Name == "SendAsync")
            .MustHaveHappenedOnceExactly();
    }
}