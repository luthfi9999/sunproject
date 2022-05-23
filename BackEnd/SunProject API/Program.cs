global using Serilog;
global using SunProject_Application.Interface;
using FluentValidation;
using FluentValidation.AspNetCore;
using SunProject_Application;
using SunProject_Application.Command.AddPromotions;
using SunProject_Application.Command.AddStores;
using SunProject_Application.Service;
using SunProject_Infrastructure;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

//Set Configuration
var config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.Development.json")
    .AddEnvironmentVariables(prefix: "ASPNETCORE_")
    .Build();

var maxFileCount = config.GetValue<int>("MaxFilecount", 10);
var logFilePath = config.GetValue<string>("LogFilePath", "../logs/log-.txt");
//Add Logging
builder.Host.UseSerilog();

var logBuilder = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .Enrich.WithThreadId();

Serilog.Debugging.SelfLog.Enable(msg => Debug.WriteLine(msg));

logBuilder.WriteTo.Console();
logBuilder.WriteTo.File(logFilePath, rollingInterval : RollingInterval.Day, retainedFileCountLimit : maxFileCount);

Log.Logger = logBuilder.CreateLogger();

// Add services to the container.
builder.Services.AddInfrastructure(config);
builder.Services.AddApplication();
builder.Services.AddScoped<IDataReader, DataReader>();
builder.Services.AddControllers()
    .AddFluentValidation();

// Add FluentValidation Class
builder.Services.AddTransient<IValidator<AddStoresCommandRequest>, AddStoresCommandValidator>();
builder.Services.AddTransient<IValidator<AddPromotionsCommandRequest>, AddPromotionsCommandValidator>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpLogging(opt =>
{
    opt.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.All;
});

var app = builder.Build();
// Shows UseCors with CorsPolicyBuilder.
app.UseCors(builder =>
{
    builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpLogging();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


try
{
    Log.Information("Starting Up ...");
    app.Run();
}
catch(Exception ex)
{
    Log.Fatal(ex, "Application Start-Up Failed.");
}
finally
{
    Log.CloseAndFlush();
}
