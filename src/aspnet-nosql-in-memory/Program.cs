using ProductCatalogAPI.Data;
using ProductCatalogAPI.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IDatabaseAdapter, InMemoryDatabaseAdapter>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

// Initialize dummy data
var databaseAdapter = app.Services.GetRequiredService<IDatabaseAdapter>();
var category1 = new Category { Name = "Category 1", Description = "This is category 1" };
var category2 = new Category { Name = "Category 2", Description = "This is category 2" };
databaseAdapter.CreateCategoryAsync(category1).Wait();
databaseAdapter.CreateCategoryAsync(category2).Wait();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
