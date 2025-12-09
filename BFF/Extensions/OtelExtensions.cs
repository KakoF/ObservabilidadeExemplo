using BFF.Meters;
using OpenTelemetry.Exporter;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace BFF.Extensions
{
    public static class OtelExtensions
    {
        public static void AddOtel(this WebApplicationBuilder builder)
        {
            var appName = "BFF";
            var otelUrl = builder.Configuration["Clients:Otel"];

            var resourceBuilder = ResourceBuilder.CreateDefault().AddService(appName).AddAttributes(new[]
            {
                new KeyValuePair<string, object>("app", appName),
                new KeyValuePair<string, object>("env", builder.Environment.EnvironmentName),
                new KeyValuePair<string, object>("host.name", Environment.MachineName)
            });

            builder.Services.AddOpenTelemetry()
             .WithMetrics(metrics => metrics
                 .SetResourceBuilder(resourceBuilder)
                 .AddAspNetCoreInstrumentation()
                 .AddHttpClientInstrumentation()
                 .AddRuntimeInstrumentation()
                 .AddProcessInstrumentation()
                 .AddEventCountersInstrumentation(options =>
                 {
                     options.AddEventSources("Microsoft.AspNetCore.Hosting", "System.Net.Http");
                 })
                 .AddMeter($"{appName}.*")
                 .AddOtlpExporter(options =>
                 {
                     options.Endpoint = new Uri(otelUrl);
                 }))
             .WithTracing(tracing => tracing
                 .AddSource(appName)
                 .AddAspNetCoreInstrumentation()
                 //.AddHttpClientInstrumentation()
                 .SetResourceBuilder(resourceBuilder)
                 .AddOtlpExporter(opt =>
                 {
                     opt.Endpoint = new Uri(otelUrl!);
                     opt.Protocol = OtlpExportProtocol.Grpc;
                 }));

            builder.Services.AddSingleton<AppMetrics>();
        }
    }
}
