using PublicHomePage.Clients;
using PublicHomePage.Providers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IQuoteProvider, QuoteProvider>();
builder.Services.AddScoped<IQuotesFreeApiClient, QuotesFreeApiClient>();
builder.Services.AddHttpClient<IQuotesFreeApiClient, QuotesFreeApiClient>(client =>
{
    client.BaseAddress = new Uri("https://type.fit/api/");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run(); 
