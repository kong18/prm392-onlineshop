using PRM392.OnlineStore.Application;
using PRM392.OnlineStore.Application.FileUpload;
using PRM392.OnlineStore.Api.Services;
using PRM392.OnlineStore.Application.Common.Interfaces;
using PRM392.OnlineStore.Api.Filters;
using PRM392.OnlineStore.Api.Configuration;
using System.Text.Json.Serialization;
using PRM392.OnlineStore.Infrastructure;
using Net.payOS;

namespace PRM392.OnlineStore.Api.Installer
{
    public class SystemInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers(opt =>
            {
                opt.Filters.Add(typeof(ExceptionFilter));
            })
            .AddJsonOptions(options =>
            {
<<<<<<< Updated upstream
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
=======
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
>>>>>>> Stashed changes
                options.JsonSerializerOptions.WriteIndented = true;
            });

            services.AddSignalR();
            
            services.AddApplication(configuration);
            services.ConfigureApplicationSecurity(configuration);
            services.ConfigureProblemDetails();
            services.ConfigureApiVersioning();
            services.AddInfrastructure(configuration);
            services.ConfigureSwagger(configuration);
            services.AddScoped<IChatService, ChatService>();
            services.AddScoped<FileUploadService, FileUploadService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IStoreLocationService, StoreLocationService>();
<<<<<<< Updated upstream
            services.AddScoped<IStoreLocationRepository, StoreLocationRepository>();
=======
>>>>>>> Stashed changes
            // CORS policy
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                        .WithOrigins("https://localhost:7182")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials());
            });

            services.AddSingleton<PayOS>(provider =>
            {
                string clientId = configuration["PaymentEnvironment:PAYOS_CLIENT_ID"] ?? throw new Exception("Cannot find PAYOS_CLIENT_ID");
                string apiKey = configuration["PaymentEnvironment:PAYOS_API_KEY"] ?? throw new Exception("Cannot find PAYOS_API_KEY");
                string checksumKey = configuration["PaymentEnvironment:PAYOS_CHECKSUM_KEY"] ?? throw new Exception("Cannot find PAYOS_CHECKSUM_KEY");

                return new PayOS(clientId, apiKey, checksumKey);

            });

            // Register System.Text.Encoding
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        }
    }
}
