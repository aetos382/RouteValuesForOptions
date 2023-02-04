using Microsoft.AspNetCore.Cors.Infrastructure;

using RouteValuesForOptions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSingleton<ICorsPolicyProvider, MyCorsPolicyProvider>();
builder.Services.AddCors();
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseRouting();
app.UseCors();

app.UseAuthorization();
app.MapControllers();

app.Run();
