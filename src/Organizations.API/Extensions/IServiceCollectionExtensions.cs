using Microsoft.OpenApi.Models;
internal static class ServiceCollectionExtensions
{
    /// <summary>
    /// Add swagger gen with keycloak to the services collection.
    /// </summary>
    /// <param name="services">The services collection.</param>
    /// <param name="configuration">The configuration.</param>
    /// <returns>The services collection.</returns>
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

    /// <summary>
    /// Add repositories to the services collection.
    /// </summary>
    /// <param name="services">The services collection.</param>
    /// <returns>The services collection.</returns>
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.Scan(scan => scan.FromAssemblyOf<Program>()
        .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Repository")))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        return services;
    }

    /// <summary>
    /// Add cors policies to the services collection.
    /// </summary>
    /// <param name="services">The services collection.</param>
    /// <param name="configuration">The configuration.</param>
    /// <returns>The services collection.</returns>
    /// <exception cref="Exception">Thrown when the cors policies are not set in the configuration.</exception>
    public static IServiceCollection AddCorsPolicies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("default",
                corsBuilder =>
                {
                    var allowedOrigins = configuration["Cors:AllowedOrigins"];
                    var allowedMethods = configuration["Cors"];
                    corsBuilder.WithOrigins(configuration["Cors:AllowedOrigins"] ?? throw new Exception("Cors:AllowedOrigins is not set"))
                               .WithMethods(configuration["Cors:AllowedMethods"] ?? throw new Exception("Cors:AllowedMethods is not set"))
                               .WithHeaders(configuration["Cors:AllowedHeaders"] ?? throw new Exception("Cors:AllowedHeaders is not set"));
                });
        });

        return services;
    }
}
