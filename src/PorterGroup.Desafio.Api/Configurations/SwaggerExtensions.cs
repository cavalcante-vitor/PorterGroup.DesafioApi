using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;

namespace PorterGroup.Desafio.Api.Configurations
{
    [ExcludeFromCodeCoverage]
    internal static class SwaggerExtensions
    {
        public static IApplicationBuilder ConfigureSwagger(
            this IApplicationBuilder app,
            IConfiguration config,
            IApiVersionDescriptionProvider provider)
        {
            var useSwagger = config.GetValue<bool>("UseSwagger");

            if (!useSwagger)
            {
                return app;
            }

            return app
                .UseSwagger()
                .UseSwaggerUI(o => provider.ApiVersionDescriptions
                    .ToList()
                    .ForEach(d =>
                        o.SwaggerEndpoint($"/swagger/{d.GroupName}/swagger.json", d.GroupName.ToUpper())));
        }
    }
}
