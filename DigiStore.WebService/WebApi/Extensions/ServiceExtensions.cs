using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DigiStore.WebService.Application.Interfaces;
using DigiStore.WebService.Application.Services;
using DigiStore.WebService.Infrastructure.Data;
using DigiStore.WebService.Infrastructure.Repositories;
using DigiStore.WebService.Infrastructure.UnitOfWork;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace DigiStore.WebService.WebApi.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Get connection string from secure sources (priority order):
            // 1. Environment variable (most secure)
            // 2. Azure Key Vault (via configuration)
            // 3. User Secrets (development only)
            // 4. appsettings.json (fallback, not recommended for production)
            var connectionString = GetSecureConnectionString(configuration);

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));

            // Address repository
            services.AddScoped<SpExecutor>();
            services.AddScoped<AddressRepository>();
            services.AddScoped<SepetRepository>();
            services.AddScoped<TeklifRepository>();
            services.AddScoped<UrunRepository>();
            services.AddScoped<KategoriRepository>();
            services.AddScoped<AnaKategoriRepository>();
            services.AddScoped<CariRepository>();
            services.AddScoped<TanimlamalarRepository>();
            services.AddScoped<OdemeRepository>();
        }

        private static string GetSecureConnectionString(IConfiguration configuration)
        {
            // Priority 1: Environment variable (most secure for production)
            var envConnectionString = Environment.GetEnvironmentVariable("DIGISTORE_CONNECTION_STRING");
            if (!string.IsNullOrEmpty(envConnectionString))
            {
                Console.WriteLine("Using connection string from environment variable");
                return envConnectionString;
            }

            // Priority 2: Azure Key Vault or other secure configuration providers
            // If using Azure Key Vault, it will be loaded through configuration
            var keyVaultConnectionString = configuration["ConnectionStrings:DefaultConnection"];
            if (!string.IsNullOrEmpty(keyVaultConnectionString) && 
                !keyVaultConnectionString.Contains("YOUR_"))
            {
                Console.WriteLine("Using connection string from configuration (Key Vault or Secrets)");
                return keyVaultConnectionString;
            }

            // Priority 3: User Secrets (development only)
            var userSecretsConnectionString = configuration.GetConnectionString("DefaultConnection");
            if (!string.IsNullOrEmpty(userSecretsConnectionString))
            {
                Console.WriteLine("Using connection string from User Secrets or appsettings");
                return userSecretsConnectionString;
            }

            throw new InvalidOperationException(
                "No connection string found. Please set the connection string using one of these methods:\n" +
                "1. Environment variable: DIGISTORE_CONNECTION_STRING\n" +
                "2. Azure Key Vault (recommended for production)\n" +
                "3. User Secrets: dotnet user-secrets set \"ConnectionStrings:DefaultConnection\" \"your-connection-string\"\n" +
                "4. appsettings.json (not recommended for production)");
        }

        public static void AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ServiceExtensions).Assembly);
            services.AddScoped<IAdresService, Application.Services.AdresService>();
            services.AddScoped<ISepetService, Application.Services.SepetService>();
            services.AddScoped<ITeklifService, Application.Services.TeklifService>();
            services.AddScoped<IUrunService, Application.Services.UrunService>();
            services.AddScoped<IKategoriService, Application.Services.KategoriService>();
            services.AddScoped<IAnaKategoriService, Application.Services.AnaKategoriService>();
            services.AddScoped<ICariService, Application.Services.CariService>();
            services.AddScoped<ITanimlamalarService, Application.Services.TanimlamalarService>();
            services.AddScoped<IOdemeService, Application.Services.OdemeService>();

            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining<DigiStore.WebService.Application.DTOs.AdresDto>();
        }
    }
}