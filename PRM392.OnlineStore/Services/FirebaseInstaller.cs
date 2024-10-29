using PRM392.OnlineStore.Api.Installer;
using PRM392.OnlineStore.Application.FileUpload;

namespace PRM392.OnlineStore.Api.Services
{
    public class FirebaseInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
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
