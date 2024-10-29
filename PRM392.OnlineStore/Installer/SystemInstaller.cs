
using PRM392_OnlineStore.Api.Configuration;
using PRM392_OnlineStore.Api.Filters;
using PRM392.OnlineStore.Application;
using PRM392.OnlineStore.Infrastructure;
using PRM392.OnlineStore.Application.FileUpload;
using PRM392.OnlineStore.Api.Services;
using PRM392.OnlineStore.Domain.Entities.Repositories.PRM392.OnlineStore.Domain.Entities.Repositories;
namespace PRM392.OnlineStore.Api.Installer
{
    public class SystemInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers(opt => opt.Filters.Add(typeof(ExceptionFilter)));

            services.AddSignalR();
            
            services.AddApplication(configuration);
            services.ConfigureApplicationSecurity(configuration);
            services.ConfigureProblemDetails();
            services.ConfigureApiVersioning();
            services.AddInfrastructure(configuration);
            services.ConfigureSwagger(configuration);
            services.AddScoped<IChatService, ChatService>();
            services.AddScoped<IChatMessageRepository, ChatMessageRepository>();
            // CORS policy
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                        .WithOrigins("https://starotvn.com", "https://localhost:7024", "http://localhost:7024")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials());
            });

            // Register System.Text.Encoding
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            var firebaseSection = configuration.GetSection("FirebaseConfig");
            if (!firebaseSection.Exists())
            {
                throw new ArgumentNullException("FirebaseConfig section does not exist in configuration.");
            }

            var firebaseConfig = firebaseSection.Get<FirebaseConfig>();
            if (firebaseConfig == null)
            {
                throw new ArgumentNullException(nameof(firebaseConfig), "FirebaseConfig section is missing in configuration.");
            }
            services.AddSingleton(firebaseConfig);
            services.AddSingleton<FileUploadService>();
        }
    }
}
