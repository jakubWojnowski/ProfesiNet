using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using ProfesiNet.Shared.MsSql;
using ProfesiNet.Users.Domain.Interfaces;
using ProfesiNet.Users.Infrastructure.Authentication;
using ProfesiNet.Users.Infrastructure.Persistence;
using ProfesiNet.Users.Infrastructure.Repositories;
using ProfesiNet.Users.Infrastructure.Settings;

[assembly: InternalsVisibleTo("ProfesiNet.Users.Api")]
namespace ProfesiNet.Users.Infrastructure.Extension;
public static class ServiceCollectionExtension
{
   
    internal static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var authenticationSettings = new AuthenticationSettings();
        configuration.GetSection(AuthenticationSettings.SectionName).Bind(authenticationSettings);
        services.AddSingleton(authenticationSettings);

        services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                option.DefaultSignOutScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
            {
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/Logout";
                options.AccessDeniedPath = "/Account/AccessDenied";
            })
            .AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = authenticationSettings.JwtIssuer,
                    ValidAudience = authenticationSettings.JwtIssuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey))
                };
                cfg.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];
                        var path = context.HttpContext.Request.Path;
                        if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/chat"))
                        {
                            context.Token = accessToken;
                        }

                        return Task.CompletedTask;
                    }
                };
            });
        services.AddMsSql<ProfesiNetUserDbContext>();
        services.AddScoped<IUserRepository, UserRepository>()
            .AddScoped<IExperienceRepository, ExperienceRepository>()
            .AddScoped<IEducationRepository, EducationRepository>()
            .AddScoped<ICertificateRepository, CertificateRepository>()
            .AddScoped<ISkillRepository, SkillRepository>()
            .AddScoped<IPhotoRepository, PhotoRepository>()
            .AddScoped<IJwtProvider, JwtProvider>();
        return services;
    }
}