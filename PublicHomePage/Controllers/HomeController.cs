using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PublicHomePage.Models;
using PublicHomePage.Models.ViewModels;
using PublicHomePage.Providers;

namespace PublicHomePage.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IQuoteProvider _quoteProvider;

    public HomeController(ILogger<HomeController> logger, IQuoteProvider quoteProvider)
    {
        _logger = logger;
        _quoteProvider = quoteProvider;
    }

    public async Task<IActionResult> Index()
    {
        var quote = await _quoteProvider.QuoteOfTheDay();
        var viewModel = new HomeModel(quote);
        return View(viewModel);
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
