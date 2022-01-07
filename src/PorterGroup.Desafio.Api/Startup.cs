using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using PorterGroup.Desafio.Api.Configurations;
using PorterGroup.Desafio.Api.Extensions;
using PorterGroup.Desafio.Infra.IoC.DependencyInjection;
using PorterGroup.Desafio.WarmUp.Extensions;

namespace PorterGroup.Desafio.Api
{
    [ExcludeFromCodeCoverage]
    internal class Startup
    {
        private const string HealthCheckLiveness = "live";

        public Startup(IConfiguration configuration) =>
            Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var jsonSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            };

            JsonConvert.DefaultSettings = () => jsonSettings;

            services
                .AddApi(Configuration)
                .AddIoc(Configuration)
                .AddWarmUp();
        }

        public void Configure(
            IApplicationBuilder app,
            IWebHostEnvironment env,
            IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app
                .ConfigureSwagger(Configuration, provider)
                .UseCors("MyPolicy")
                .UseForwardedHeaders(new ForwardedHeadersOptions
                {
                    ForwardedHeaders = ForwardedHeaders.All,
                })
                .Use(async (context, next) =>
                {
                    context.Response.Headers.Add("X-Frame-Options", "DENY");
                    await next();
                })
                .UseRouting()
                .UseAuthentication()
                .UseAuthorization()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                    endpoints.MapHealthChecks("/readiness", new HealthCheckOptions
                    {
                        ResultStatusCodes =
                        {
                            [HealthStatus.Healthy] = StatusCodes.Status200OK,
                            [HealthStatus.Degraded] = StatusCodes.Status200OK,
                            [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable,
                        },
                    });
                    endpoints.MapHealthChecks("/healthcheck", new HealthCheckOptions
                    {
                        Predicate = (func) => func.Tags.Contains(HealthCheckLiveness),
                    });
                })
                .UseResponseCompression();
        }
    }
}
