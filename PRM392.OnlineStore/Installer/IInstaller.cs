namespace PRM392.OnlineStore.Api.Installer
{
    public interface IInstaller
    {
        void InstallServices(IServiceCollection services, IConfiguration configuration);


    }
}
