using Microsoft.ApplicationInsights.Extensibility;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var aiOptions = new Microsoft.ApplicationInsights.AspNetCore.Extensions.ApplicationInsightsServiceOptions();
aiOptions.EnableAdaptiveSampling = true;

var tpBuilder = TelemetryConfiguration.Active.DefaultTelemetrySink.TelemetryProcessorChainBuilder;
tpBuilder.UseAdaptiveSampling(maxTelemetryItemsPerSecond: 1);

builder.Services.AddApplicationInsightsTelemetry(aiOptions);

var app = builder.Build();
app.UseSwagger();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
