using PRM392.OnlineStore.Api.Configuration;
using PRM392.OnlineStore.Api.Installer;
using PRM392.OnlineStore.Api.Services;
using PRM392.OnlineStore.Application;
using PRM392.OnlineStore.Domain.Entities.Repositories.PRM392.OnlineStore.Domain.Entities.Repositories;
using PRM392.OnlineStore.Infrastructure;
using PRM392.OnlineStore.Api.Filters;
using Serilog;
using Firebase.Database;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;

namespace PRM392.OnlineStore.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.InstallerServicesInAssembly(Configuration);
            FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile("C:\\Users\\Asus\\Downloads\\prm392-11333-fcb968f30232.json")
            });

            services.AddSingleton(new FirebaseClient("https://prm392-11333-default-rtdb.firebaseio.com/"));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseCors("CorsPolicy");
            app.UseSerilogRequestLogging();
            app.UseExceptionHandler();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<ChatHub>("/chatHub");
                //endpoints.MapHub<NotificationHub>("/notificationHub");

            });

            app.UseSwashbuckle(Configuration);
        }
    }
}
