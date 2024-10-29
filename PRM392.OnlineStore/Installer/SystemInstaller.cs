
using PRM392_OnlineStore.Api.Configuration;
using PRM392_OnlineStore.Api.Filters;

namespace PRM392.OnlineStore.Api.Installer
{
    public class SystemInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers(opt => opt.Filters.Add(typeof(ExceptionFilter)));

            services.AddSignalR();
            
            //services.AddApplication(configuration);
            services.ConfigureApplicationSecurity(configuration);
            services.ConfigureProblemDetails();
            services.ConfigureApiVersioning();
            //services.AddInfrastructure(configuration);
            services.ConfigureSwagger(configuration);

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
        }
    }
}
