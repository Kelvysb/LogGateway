using Elastic.CommonSchema.Serilog;
using LogGateway.Services;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Sinks.Elasticsearch;

namespace LogGateway.Extenssions
{
    public static class ServiceExtensions
    {
        public static WebApplicationBuilder AddServices(this WebApplicationBuilder builder)
        {
            SetConfiguration(builder);
            AddSwagger(builder);

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration)
                .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(builder.Configuration["ELASTIC_URL"]))
                    {
                      ModifyConnectionSettings = x => x.BasicAuthentication(
                          builder.Configuration["ELASTIC_USER"],
                          builder.Configuration["ELASTIC_PASSWORD"]),
                      CustomFormatter = new EcsTextFormatter()
                })
                .WriteTo.Console(new EcsTextFormatter())
                .CreateLogger();

            builder.Services.AddTransient<ILogService, LogService>();
            builder.Services.AddSingleton(Log.Logger);

            builder.WebHost.UseUrls($"http://*:{builder.Configuration["LOG_GATEWAY_PORT"]}");

            return builder;
        }

        private static void AddSwagger(WebApplicationBuilder builder)
        {
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "LogGateway", Description = "Log Gateway", Version = "v1" });
            });
        }

        private static void SetConfiguration(WebApplicationBuilder builder)
        {
            builder.Configuration
                .SetBasePath(Directory.GetCurrentDirectory())                      
                .AddEnvironmentVariables()
                .Add(new DotEnvConfigProvider("../Docker/.env"))
                .Build();
        }
    }
}
