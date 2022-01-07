using System;
using System.Buffers;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using FluentValidation.AspNetCore;
using JsonApiSerializer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.ObjectPool;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using PorterGroup.Desafio.Api.Configurations;
using PorterGroup.Desafio.Api.Constants;
using PorterGroup.Desafio.Api.Contexts;
using PorterGroup.Desafio.Api.Filters;
using PorterGroup.Desafio.Api.Interceptors;
using PorterGroup.Desafio.Shared.Contexts;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace PorterGroup.Desafio.Api.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class ServicesExtension
    {
        private const string BearerAuthenticationDescription = "JWT Authorization header using the Bearer scheme. Enter 'Bearer' [space] and then your token in the text input below. Example: 'Bearer 12345abcdef'";

        public static IServiceCollection AddApi(this IServiceCollection services, IConfiguration configuration) =>
            services
                .AddScoped<IRequestContextHolder, RequestContextHolder>()
                .ConfigControllersPipeline()
                .ConfigAppVersioning()
                .ConfigSwagger()
                .AddJwt(configuration)
                .Configure<GzipCompressionProviderOptions>(gzipCompressionOptions =>
                    gzipCompressionOptions.Level = CompressionLevel.Fastest)
                .AddResponseCompression(compressionOptions =>
                {
                    compressionOptions.EnableForHttps = true;
                    compressionOptions.Providers.Add<GzipCompressionProvider>();
                })
                .AddHsts(opts =>
                {
                    opts.MaxAge = TimeSpan.FromDays(365);
                    opts.IncludeSubDomains = true;
                    opts.Preload = true;
                });

        private static IServiceCollection ConfigControllersPipeline(this IServiceCollection services) =>
            services
                .ConfigureCors()
                .AddControllers(mvcOptions =>
                {
                    mvcOptions.Filters.Add<ExceptionFilter>(order: 0);
                    mvcOptions.Filters.Add<ControllersFilter>(order: 1);
                })
                .ConfigureApiBehaviorOptions(opt => opt
                    .SuppressModelStateInvalidFilter = true)
                .Services
                .AddTransient<IValidatorInterceptor, FluentValidatorInterceptor>();

        private static IServiceCollection ConfigureCors(this IServiceCollection services) =>
            services.AddCors(options =>
            {
                options.AddPolicy("MyPolicy", builder =>
                builder.AllowAnyOrigin()
                 .AllowAnyMethod()
                 .WithExposedHeaders("Pagination")
                 .AllowAnyHeader());
            });

        private static IServiceCollection ConfigAppVersioning(this IServiceCollection services) =>
            services
                .AddApiVersioning(o => o.ReportApiVersions = true)
                .AddVersionedApiExplorer(o =>
                {
                    o.GroupNameFormat = "'v'VVV";
                    o.SubstituteApiVersionInUrl = true;
                });

        private static IServiceCollection ConfigSwagger(this IServiceCollection services) =>
            services
                .AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>()
                .AddSwaggerGen(o =>
                {
                    o.OperationFilter<SwaggerDefaultValues>();
                    o.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        In = ParameterLocation.Header,
                        Description = BearerAuthenticationDescription,
                        Name = "Authorization",
                        Type = SecuritySchemeType.ApiKey,
                    });
                    o.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer",
                                },
                            },
                            Array.Empty<string>()
                        },
                    });

                    var xmlApiFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                    var xmlApiPath = Path.Combine(AppContext.BaseDirectory, xmlApiFile);

                    o.ExampleFilters();
                    o.OperationFilter<AddResponseHeadersFilter>();
                    o.CustomSchemaIds(type => type.FullName);
                    o.IncludeXmlComments(xmlApiPath);
                })
                .AddSwaggerExamplesFromAssemblyOf<Startup>();
    }
}
