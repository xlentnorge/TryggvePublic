using System.Text;
using FakeItEasy;
using FluentAssertions;
using Newtonsoft.Json;
using PublicHomePage.Clients;
using PublicHomePage.Models.QuoteClient;

namespace TryggvePublicTest.Clients;

public class QuoteClientTests
{
    private readonly IQuoteClient _quoteClient;
    private readonly HttpClient _httpClient;
    private readonly HttpMessageHandler handler;

    public QuoteClientTests()
    {
        handler = A.Fake<HttpMessageHandler>();
        _httpClient = new HttpClient(handler);
        _httpClient.BaseAddress = new Uri("https://test.no");
        _quoteClient = new QuoteClient(_httpClient);
    }

    [Fact]
    public async Task GetQuotes_ShouldReturnQuotes()
    {
        // Arrange
        var quotes = new List<Quote>()
        {
            new Quote()
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
        var result = await _quoteClient.GetQuotes();

        //Verify
        result.Should().NotBeEmpty();
    }
}