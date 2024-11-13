using EmergencyNS.Components;
using ENS_API.Controllers;
using ENS_API.Data;
using ENS_API.Services;
using ENS_API.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ENS_API.Workers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Adding API
builder.Services.AddScoped<FileService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddSingleton<EmailService>();
builder.Services.AddScoped<HashGenerator>();
builder.Services.AddScoped<JWTProvider>();
builder.Services.AddSingleton<KafkaProducerService>();

builder.Services.Configure<JWTOptions>(builder.Configuration.GetSection(nameof(JWTOptions)));
builder.Services.AddApiAuthentication(
    builder.Services.BuildServiceProvider().GetRequiredService<IOptions<JWTOptions>>()
);

// Registration of backgroud worker
builder.Services.AddHostedService<NotificationWorker>();

builder.Services.AddHttpContextAccessor();


builder.Services.AddDbContext<ENSDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DbConnection"));
});
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
