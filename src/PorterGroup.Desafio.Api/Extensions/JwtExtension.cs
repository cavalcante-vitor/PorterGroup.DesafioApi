using System;
using System.Diagnostics.CodeAnalysis;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using PorterGroup.Desafio.Api.Models;

namespace PorterGroup.Desafio.Api.Extensions
{
    [ExcludeFromCodeCoverage]
    internal static class JwtExtension
    {
        public static IServiceCollection AddJwt(this IServiceCollection services, IConfiguration configuration)
        {
            var secretKey = configuration.GetValue<string>("Jwt:SecretKey");
            var validateSigningKey = !string.IsNullOrWhiteSpace(secretKey);

            services
                .AddAuthentication(authOptions =>
                {
                    authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(jwtOptions =>
                {
                    jwtOptions.RequireHttpsMetadata = false;
                    jwtOptions.SaveToken = true;
                    jwtOptions.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuerSigningKey = validateSigningKey,
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        RequireExpirationTime = true,
                        ClockSkew = TimeSpan.Zero,
                    };

                    if (validateSigningKey)
                    {
                        var secretKeyBytes = Encoding.UTF8.GetBytes(secretKey);
                        jwtOptions.TokenValidationParameters.IssuerSigningKey = new SymmetricSecurityKey(secretKeyBytes);
                    }
                    else
                    {
                        jwtOptions.TokenValidationParameters.RequireExpirationTime = false;
                        jwtOptions.TokenValidationParameters.RequireSignedTokens = false;
                        jwtOptions.TokenValidationParameters.SignatureValidator = (token, _) =>
                            new JwtSecurityToken(token);
                    }

                    jwtOptions.Events = new JwtBearerEvents
                    {
                        OnChallenge = async (context) =>
                        {
                            context.HandleResponse();
                            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                            await context.HttpContext.Response.WriteAsJsonAsync(ErrorResponse.FromBadAuthorization());
                        },
                        OnForbidden = async (context) =>
                        {
                            context.Response.StatusCode = StatusCodes.Status403Forbidden;
                            await context.HttpContext.Response.WriteAsJsonAsync(ErrorResponse.FromBadAuthorization());
                        },
                    };
                });

            return services;
        }
    }
}
