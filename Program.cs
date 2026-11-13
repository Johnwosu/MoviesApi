using System.Data;
using Microsoft.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Register SQL Server connection for Dapper.
builder.Services.AddScoped<IDbConnection>(sp =>
    new SqlConnection(
        builder.Configuration.GetConnectionString(
            "MoviesAdminContext")));

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();