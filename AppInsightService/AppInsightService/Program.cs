using AppInsightService;
using Microsoft.ApplicationInsights.Extensibility;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var aiOptions = new Microsoft.ApplicationInsights.AspNetCore.Extensions.ApplicationInsightsServiceOptions();
//<<<<<<< HEAD
//=======

//void AdaptiveTelCallback(double afterSamplingTelemetryItemRatePerSecond, double currentSamplingPercentage, double newSamplingPercentage, bool isSamplingPercentageChanged, SamplingPercentageEstimatorSettings settings)
//{ }

//builder.Services.Configure<TelemetryConfiguration>((config) =>
//{
//    SamplingPercentageEstimatorSettings samplingPercentageEstimatorSettings = new SamplingPercentageEstimatorSettings();
//    samplingPercentageEstimatorSettings.EvaluationInterval = new TimeSpan(0, 0, 15);
//    samplingPercentageEstimatorSettings.InitialSamplingPercentage = 100;
//    samplingPercentageEstimatorSettings.MovingAverageRatio = 0.90; //Highly reactive to sudden changes
//    samplingPercentageEstimatorSettings.SamplingPercentageIncreaseTimeout = new TimeSpan(0,0,15);
//    samplingPercentageEstimatorSettings.EvaluationInterval = new TimeSpan(0, 0, 10); //shorten the interval to catch the certain burst
//    samplingPercentageEstimatorSettings.MaxTelemetryItemsPerSecond = 1;
//    AdaptiveSamplingPercentageEvaluatedCallback adaptiveSamplingPercentageEvaluatedCallback = AdaptiveTelCallback;

//    var tpBuilder = TelemetryConfiguration.Active.DefaultTelemetrySink.TelemetryProcessorChainBuilder;
//    //tpBuilder.UseAdaptiveSampling(maxTelemetryItemsPerSecond: 1, excludedTypes: "Exception");
//    tpBuilder.UseAdaptiveSampling(samplingPercentageEstimatorSettings, adaptiveSamplingPercentageEvaluatedCallback, excludedTypes: "Exception", includedTypes: "Request;Trace");
//    tpBuilder.Build();
//});

//>>>>>>> 5d4c3ab (adaptive sampling configuration added)
aiOptions.EnableAdaptiveSampling = false;
builder.Services.AddApplicationInsightsTelemetry(aiOptions);
builder.Services.AddApplicationInsightsTelemetryProcessor<AdaptiveTelemetryProcessor>();
var app = builder.Build();
var config = app.Configuration.Get<TelemetryConfiguration>();
var tpBuilder = config.DefaultTelemetrySink.TelemetryProcessorChainBuilder;
tpBuilder.UseSampling(20.0, excludedTypes: "Exception;Trace");
tpBuilder.Build();
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
