using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PublicHomePage.Clients;
using PublicHomePage.Models;

namespace PublicHomePage.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IQuoteClient _quoteClient;

    public HomeController(ILogger<HomeController> logger, IQuoteClient quoteClient)
    {
        _logger = logger;
        _quoteClient = quoteClient;
    }

    public async Task<IActionResult> Index()
    {
        var quotes = await _quoteClient.GetQuotes();
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
