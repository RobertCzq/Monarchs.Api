using Monarchs.Common.Interfaces;
using Monarchs.Common.Utils;
using Monarchs.Infastructure.DAO;
using Monarchs.Infastructure.Providers;
using Monarchs.Infastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddMemoryCache();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IConnectionStringProvider>(new ConnectionStringProvider(builder.Configuration.GetConnectionString("Database")));
builder.Services.AddScoped<IMonarchsDataStore, JsonMonarchsDataStore>();
builder.Services.AddScoped<IMonarchsCache, MonarchsCache>();
builder.Services.AddScoped<ICacheProvider, CacheProvider>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
