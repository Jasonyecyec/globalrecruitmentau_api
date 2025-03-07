using globalrecruitmentau_api.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

//using Microsoft.AspNetCore.Server.Kestrel.Core; // For Kestrel configuration (if using HTTPS)
//using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Debug the connection string
//Console.WriteLine($"Connection String: {builder.Configuration["ConnectionStrings__DefaultConnection"]}");

Console.WriteLine($"Connection String: {builder.Configuration.GetConnectionString("DefaultConnection")}");


// Add EF Core services with MySQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 32)))); // Match your MySQL version

// Configure Kestrel to use HTTPS with the development certificate
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(8080);
    //options.ListenAnyIP(8081, listenOptions =>
    //    listenOptions.UseHttps("/root/.dotnet/corefx/cryptography/x509stores/my/B25A8E51BFC19C93A1449BFF09635D0AEAE7B16C.pfx", ""));
});
// B25A8E51BFC19C93A1449BFF09635D0AEAE7B16C
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
