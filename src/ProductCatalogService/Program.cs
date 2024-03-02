using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProductCatalogService.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ProductCatalogServiceContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ProductCatalogServiceContext") ?? throw new InvalidOperationException("Connection string 'ProductCatalogServiceContext' not found.")));

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
