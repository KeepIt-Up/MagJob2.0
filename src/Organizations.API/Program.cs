using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Organizations.API.Common.Accessors;
using Organizations.API.Data;
using Organizations.API.Data.Seeding;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddServicesAndRepositories();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IUserAccessor, UserAccessor>();

// Add DbContext for PostgreSQL
builder.Services.AddDbContext<OrganizationDbContext>();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGenWithKeycloak(builder.Configuration);

// Cors configuration
builder.Services.AddCors(options =>
{
    options.AddPolicy("localhost",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.Audience = builder.Configuration["Keycloak:Audience"];
    options.MetadataAddress = builder.Configuration["Keycloak:MetadataUrl"];
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["Keycloak:ValidateIssuer"]
    };
});

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    // Apply migrations and seed data in development    
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<OrganizationDbContext>();

        // Apply pending migrations
        await context.Database.MigrateAsync();

        // Seed the database
        await DatabaseSeeder.SeedAsync(context);
    }

    app.UseCors("localhost");
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
