using System.Net;
using Microsoft.EntityFrameworkCore;
using SchwabApi.WebApi.Data;
using SchwabApi.WebpApi.Services;

var builder = WebApplication.CreateBuilder(args);

var connString = builder.Configuration
    .GetConnectionString("DefaultConnection");

// Add DB context
builder.Services.AddDbContext<ApplicationDbContext>(
    options => options.UseNpgsql(connString)
);
// Add services to the container.
builder.WebHost.ConfigureKestrel(options =>
{
    options.Listen(IPAddress.Loopback, 5001, listenOptions => listenOptions.UseHttps()); // ✅ Force HTTPS on 5001 (IPv4)
    options.Listen(IPAddress.Loopback, 5000); // ✅ HTTP on 5000 (optional)
});

// Enable CORS Policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactAppLocal", policy =>
    {
        // Corrected: Removed the trailing slash
        policy.WithOrigins("http://localhost:5173")  // Allow local front-end app
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.Configure<SchwabApiSettings>(builder.Configuration.GetSection("SchwabApi"));
builder.Services.AddScoped<SchwabApiAccountService>();
builder.Services.AddScoped<SchwabAuthService>();
builder.Services.AddScoped<SchwabApiTransactionsService>();


builder.Services.AddSingleton<ITokenStore, TokenStore>();
builder.Services.AddHttpClient<SchwabAuthService>();
builder.Services.AddControllers();
// builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.RoutePrefix = "swagger";
    });
    app.MapOpenApi();
}
app.UseCors("AllowReactAppLocal");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
