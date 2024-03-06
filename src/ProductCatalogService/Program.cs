using Microsoft.Extensions.Hosting;
using ProductCatalogService;
using ProductCatalogService.Middleware;
using ProductCatalogService.Repositories;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

ConfigureLogging(builder);

// Add services to the container.
builder.Services.AddDbContext<ProductCatalogServiceContext>(options =>
                              options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString") ?? throw new InvalidOperationException("Connection string 'ProductCatalogServiceContext' not found.")));
builder.Services.AddControllers();
//builder.Services.AddControllers().AddJsonOptions(x =>
//       x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve
//   );
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<DataInitializerService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Host.UseSerilog();


var app = builder.Build();

// Register exception handling middleware
app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
  var services = scope.ServiceProvider;
  var dataInitializerService = services.GetRequiredService<DataInitializerService>();
  await dataInitializerService.SeedAsync();
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
