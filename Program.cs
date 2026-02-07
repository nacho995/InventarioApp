using InventarioApp.Components;
using Microsoft.EntityFrameworkCore;
using InventarioApp.Data;
using MudBlazor.Services;
using InventarioApp.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<StockService>();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("InventarioDB")), ServiceLifetime.Transient);

builder.Services.AddTransient<ProductService>();
builder.Services.AddTransient<CategoriesService>();

builder.Services.AddMudServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
