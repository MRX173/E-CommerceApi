using System.Text;
using Application.Abstractions;
using Application.Options;
using Domain.UserAggregate.Entities;
using E_CommerceApp.Services;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace E_CommerceApp;

public static class DependencyInjection
{
    
    public static IServiceCollection AddProgramServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddControllers();
        services.AddIdentity<CustumUser, UserRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 5;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.ClaimsIdentity.UserIdClaimType = "IdentityId";
            })
            .AddEntityFrameworkStores<ApplicationDbContext>();
        var jwtSettings = new JwtSettings();
        configuration.Bind(nameof(JwtSettings), jwtSettings);
        var jwtSection = configuration.GetSection(nameof(JwtSettings));
        services.Configure<JwtSettings>(jwtSection);
        services
            .AddAuthentication(a =>
            {
                a.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                a.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                a.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(jwt =>
            {
                jwt.SaveToken = true;
                jwt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.SigningKey)),
                    ValidateIssuer = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidateAudience = true,
                    ValidAudience = jwtSettings.Audience,
                    RequireExpirationTime = false,
                    ValidateLifetime = true
                };
                jwt.Audience = jwtSettings.Audience;
                jwt.ClaimsIssuer = jwtSettings.Issuer;
            });
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
            {
                Name = "JWT Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
            });
        });
        services.AddAuthorization();
        services.AddAutoMapper(typeof(Program).Assembly);
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddHttpContextAccessor();
        services.AddScoped<ICurrentUser, CurrentUser>();
        return services;
    }
}