using Core.Middleware;
using OpenTelemetry.Exporter;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
var otelUrl = builder.Configuration["Clients:Otel"];

builder.Services.AddOpenTelemetry()
	.ConfigureResource(resource => resource.AddService("Core"))
	.WithTracing(tracing => tracing
		.AddAspNetCoreInstrumentation()
		.AddHttpClientInstrumentation()
		.SetResourceBuilder(
			ResourceBuilder.CreateDefault()
				.AddService("Core")  // ?? Nome que aparece no Jaeger
		)
		.AddOtlpExporter(opt =>
		{
			opt.Endpoint = new Uri(otelUrl);
			opt.Protocol = OtlpExportProtocol.Grpc;
		}))
	.WithMetrics(metrics => metrics
	.SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("Core"))
		.AddAspNetCoreInstrumentation()
		.AddHttpClientInstrumentation()
		.AddOtlpExporter(options =>
		{
			options.Endpoint = new Uri(otelUrl);
		}));

// Configuração de Logs
builder.Logging.Configure(options =>
{
    options.ActivityTrackingOptions = ActivityTrackingOptions.TraceId | ActivityTrackingOptions.SpanId;
});
builder.Logging.AddOpenTelemetry(logging =>
{
	logging.IncludeFormattedMessage = true;
	logging.IncludeScopes = true;
	logging.AddOtlpExporter(options =>
	{
		//options.Endpoint = new Uri("http://otel:4317");
		options.Endpoint = new Uri(otelUrl);
	});
});


var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.Run();
