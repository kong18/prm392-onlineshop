using PRM392.OnlineStore.Application;
using PRM392.OnlineStore.Infrastructure;
using PRM392.OnlineStore.Application.FileUpload;
using PRM392.OnlineStore.Api.Services;
using PRM392.OnlineStore.Domain.Entities.Repositories.PRM392.OnlineStore.Domain.Entities.Repositories;
using PRM392.OnlineStore.Domain.Entities.Repositories;
using PRM392.OnlineStore.Application.Common.Interfaces;
using PRM392.OnlineStore.Infrastructure.Repositories;
using PRM392.OnlineStore.Api.Filters;
using PRM392.OnlineStore.Api.Configuration;
using System.Text.Json.Serialization;
using Net.payOS;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.Extensions.Options;
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
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;  // Avoids $id and $values
    options.JsonSerializerOptions.WriteIndented = true;
});

            //services.AddSignalR();
            services.AddSignalR().AddAzureSignalR(options =>
            {
                options.ConnectionString = configuration["AzureSignalRConnectionString"];
            });

            services.AddApplication(configuration);
            services.ConfigureApplicationSecurity(configuration);
            services.ConfigureProblemDetails();
            services.ConfigureApiVersioning();
            services.AddInfrastructure(configuration);
            services.ConfigureSwagger(configuration);
            services.AddScoped<IChatService, ChatService>();
            services.AddScoped<IChatMessageRepository, ChatMessageRepository>();
            services.AddScoped<FileUploadService, FileUploadService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddScoped<IStoreLocationService, StoreLocationService>();
            services.AddScoped<IStoreLocationRepository, StoreLocationRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<ICartItemRepository, CartItemRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            
                     // CORS policy
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                        .WithOrigins("https://localhost:7182", "https://prm392onlinestoreapi20241106130615.azurewebsites.net")
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

            services.AddSingleton<FirebaseApp>(provider =>
            {
                var firebaseApp = FirebaseApp.Create(new AppOptions()
                {
                    Credential = GoogleCredential.FromJson(configuration["FirebaseConfig"]),
                });

                return firebaseApp;
            });

            // Register System.Text.Encoding
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        }
    }
}
