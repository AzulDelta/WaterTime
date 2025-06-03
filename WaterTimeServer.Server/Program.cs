using Blazored.LocalStorage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MudBlazor.Services;
using WaterTimeServer.Application.Services;
using WaterTimeServer.Infraestructure.Contextos;
using WaterTimeServer.Infraestructure.Repositories;
using WaterTimeServer.Server.Components;
using WaterTimeServer.Shared.DTOs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

builder.Services.AddServerSideBlazor()
                .AddCircuitOptions(o => o.DetailedErrors = true);

builder.Services.Configure<AppSettings>(
    builder.Configuration.GetSection("AppSettings"));

builder.Services.AddHttpClient("interno", (serviceProvider, client) =>
{
    AppSettings settings = serviceProvider.GetRequiredService<IOptions<AppSettings>>().Value;
    client.BaseAddress = new Uri(settings.BaseUrl);
});

//builder.Services.AddCors(opcoes =>
//{
//    opcoes.AddDefaultPolicy(politica =>
//    {
//        politica.AllowAnyOrigin()
//               .AllowAnyHeader()
//               .AllowAnyMethod();
//    });
//});

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddMudServices();
builder.Services.AddHttpClient();

builder.Services.AddScoped<UsuarioRepository>();
builder.Services.AddScoped<UsuarioService>();

builder.Services.AddHostedService<ServicoNotificacaoWorker>();

builder.Services.AddScoped<NotificacaoService>();
builder.Services.AddScoped<NotificacaoRepository>();

builder.Services.AddDbContext<WaterTimeContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();          // services for MVC/API

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAntiforgery();

app.MapControllers();                      // ← exposes /api/… routes
app.MapStaticAssets();
app.MapRazorComponents<App>()
   .AddInteractiveServerRenderMode();

app.Run();
