using ENS_API.Controllers;
using ENS_API.Data;
using ENS_API.Extensions;
using ENS_API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Services
builder.Services.AddScoped<FileService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<HashGenerator>();
builder.Services.AddScoped<JWTProvider>();
// Controllers
builder.Services.AddScoped<UsersController>();
builder.Services.AddScoped<ContactsController>();
builder.Services.AddScoped<AuthController>();

// Authentication services
builder.Services.AddScoped<HashGenerator>();
builder.Services.AddScoped<JWTProvider>();
builder.Services.AddScoped<AuthService>();

builder.Services.Configure<JWTOptions>(builder.Configuration.GetSection(nameof(JWTOptions)));


// That's needed for some reason (don't know why, it's not built-in)
builder.Services.AddHttpContextAccessor();

// adding+configuring extension file
builder.Services.AddApiAuthentication(
    builder.Services.BuildServiceProvider().GetRequiredService<IOptions<JWTOptions>>()
);


// Configuring Database Context
builder.Services.AddDbContext<ENSDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DbConnection"));
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
