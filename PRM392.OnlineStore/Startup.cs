using PRM392.OnlineStore.Application;
using PRM392.OnlineStore.Infrastructure;
using PRM392_OnlineStore.Api.Configuration;
using PRM392_OnlineStore.Api.Filters;
using Serilog;

namespace PRM392_OnlineStore.Api
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
            services.AddControllers(opt => opt.Filters.Add(typeof(ExceptionFilter)));

            services.AddSignalR();
            //services.AddScoped<OrderService>();
            services.AddApplication(Configuration);
            services.ConfigureApplicationSecurity(Configuration);
            services.ConfigureProblemDetails();
            services.ConfigureApiVersioning();
            services.AddInfrastructure(Configuration);
            services.ConfigureSwagger(Configuration);

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                        .WithOrigins("https://localhost:7024")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials());
            });

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            // Additional logging
            //    var firebaseSection = Configuration.GetSection("FirebaseConfig");
            //    if (!firebaseSection.Exists())
            //    {
            //        throw new ArgumentNullException("FirebaseConfig section does not exist in configuration.");
            //    }

            //    var firebaseConfig = firebaseSection.Get<FirebaseConfig>();
            //    if (firebaseConfig == null)
            //    {
            //        throw new ArgumentNullException(nameof(firebaseConfig), "FirebaseConfig section is missing in configuration.");
            //    }

            //    services.AddSingleton(firebaseConfig);
            //    services.AddSingleton<FileUploadService>();
            //}
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
                //endpoints.MapHub<NotificationHub>("/notificationHub");

            });

            app.UseSwashbuckle(Configuration);
        }
    }
}
