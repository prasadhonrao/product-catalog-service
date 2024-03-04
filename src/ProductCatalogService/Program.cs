using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

var builder = WebApplication.CreateBuilder(args);

ConfigureLogging(builder);

builder.Services.AddDbContext<ProductCatalogServiceContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString") ?? throw new InvalidOperationException("Connection string 'ProductCatalogServiceContext' not found.")));

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Host.UseSerilog();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

void ConfigureLogging(WebApplicationBuilder builder)
{
  Log.Logger = new LoggerConfiguration()
      .MinimumLevel.Information()
      .Enrich.FromLogContext()
      .Enrich.WithMachineName()
      .WriteTo.Console(theme: AnsiConsoleTheme.Literate)
      .WriteTo.File("logs/product-catalog-service.log", rollingInterval: RollingInterval.Day)
      .CreateLogger();

  builder.Host.UseSerilog();
}
