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
var resourceBuilder = ResourceBuilder.CreateDefault()
	.AddService("Core")
	.AddAttributes(new[]
	{
		new KeyValuePair<string, object>("app", "Core"),
		new KeyValuePair<string, object>("env", builder.Environment.EnvironmentName),
		new KeyValuePair<string, object>("host.name", Environment.MachineName)
	});

builder.Services.AddOpenTelemetry()
	.ConfigureResource(resource => resource.AddService("Core"))
	.WithTracing(tracing => tracing
		.AddAspNetCoreInstrumentation()
		//.AddHttpClientInstrumentation()
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
	.SetResourceBuilder(resourceBuilder)
		.AddView("*", new MetricStreamConfiguration
		{
			TagKeys = new[] { "app", "env", "host.name" }
		})
		.AddAspNetCoreInstrumentation()
		.AddHttpClientInstrumentation()
		.AddRuntimeInstrumentation()
		.AddProcessInstrumentation()
		.AddEventCountersInstrumentation(options =>
		{
			options.AddEventSources("Microsoft.AspNetCore.Hosting", "System.Net.Http");
		})
		.AddOtlpExporter(options =>
		{
			options.Endpoint = new Uri(otelUrl);
		}));

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
