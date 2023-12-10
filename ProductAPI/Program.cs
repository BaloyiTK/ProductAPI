using Microsoft.EntityFrameworkCore;
using ProductAPI.Data;
using ProductAPI.Repositories.Implementation;
using ProductAPI.Repositories.Interface;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Dependency Injection (DI) Configuration

// Adding a DbContext (Database Context) to the dependency injection container.
// This allows the application to use the configured ApplicationDBContext in other parts of the code.

builder.Services.AddDbContext<ApplicationDBContext>(options =>
{
    // Configuring the DbContext to use SQL Server as the database provider.
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString"));
    // The connection string is retrieved from the configuration using the key "DefaultConnectionString".
    // This connection string typically contains information about the database server, database name, credentials, etc.
    // The connection string is set up in the configuration files (e.g., appsettings.json) or environment variables.
    // The UseSqlServer method sets the database provider to SQL Server and specifies the connection string.
});

builder.Services.AddScoped<IProductRepository, ProductRepository>();




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
