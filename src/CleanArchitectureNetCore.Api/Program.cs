using CleanArchitectureNetCore.Api;

// Log.Logger = new LoggerConfiguration()
//     .WriteTo.Console()
//     .CreateBootstrapLogger();
//
// Log.Information("GloboTicket API starting");

var builder = WebApplication.CreateBuilder(args);

// builder.Host.UseSerilog((context, loggerConfiguration) => loggerConfiguration
//     .WriteTo.Console()
//     .ReadFrom.Configuration(context.Configuration));

var app = builder
    .ConfigureServices()
    .ConfigurePipeline();

// app.UseSerilogRequestLogging();

// await app.ResetDatabaseAsync();

app.Run();

public partial class Program
{
}


/*
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
*/