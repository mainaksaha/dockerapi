using AppInsightService;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.ApplicationInsights.WindowsServer.Channel.Implementation;
using Microsoft.ApplicationInsights.WindowsServer.TelemetryChannel;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var aiOptions = new Microsoft.ApplicationInsights.AspNetCore.Extensions.ApplicationInsightsServiceOptions();
aiOptions.EnableAdaptiveSampling = true;
builder.Services.AddApplicationInsightsTelemetry(aiOptions);
builder.Services.AddApplicationInsightsTelemetryProcessor<AdaptiveTelemetryProcessor>();
var app = builder.Build();
//app.
//Startup.Configure(app,)
app.UseSwagger();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{    
    app.UseSwaggerUI();
}
app.UseAuthorization();

app.MapControllers();

app.Run();
