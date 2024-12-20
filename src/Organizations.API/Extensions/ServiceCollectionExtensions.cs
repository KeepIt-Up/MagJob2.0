using Microsoft.OpenApi.Models;
internal static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSwaggerGenWithKeycloak(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSwaggerGen(o =>
        {
            o.CustomSchemaIds(id => id.FullName!.Replace("+", "-"));

            o.AddSecurityDefinition("Keycloak", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.OAuth2,
                Flows = new OpenApiOAuthFlows
                {
                    Implicit = new OpenApiOAuthFlow
                    {
                        AuthorizationUrl = new Uri(configuration["Keycloak:AuthorizationUrl"]!),
                        Scopes = new Dictionary<string, string>
                        {
                            { "openid", "openid" },
                            { "profile", "profile" },
                            { "email", "email" }
                        }
                    }
                }
            });

            var securityRequirement = new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Keycloak"
                        },
                        In = ParameterLocation.Header,
                        Name = "Bearer",
                        Scheme = "Bearer",
                    },
                    new List<string>()
                }
            };

            o.AddSecurityRequirement(securityRequirement);
        });

        return services;
    }

    public static IServiceCollection AddServicesAndRepositories(this IServiceCollection services)
    {
        services.Scan(scan => scan
            .FromAssemblyOf<Program>()
            .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Service")))
        .AsImplementedInterfaces()
            .WithScopedLifetime()
        .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Repository")))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        return services;
    }
}