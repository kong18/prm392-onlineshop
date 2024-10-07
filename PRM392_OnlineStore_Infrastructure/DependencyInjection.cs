using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PRM392.OnlineStore.Infrastructure.Persistence;
using PRM392_OnlineStore_Domain.Interfaces;

namespace PRM392.OnlineStore.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>((sp, options) =>
            {
                options.UseSqlServer(
                    configuration.GetConnectionString("local"),
                    b =>
                    {
                        b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
                        b.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                    });
                options.UseLazyLoadingProxies();
            });

            services.AddScoped<IUnitOfWork>(provider => provider.GetRequiredService<ApplicationDbContext>());

            //services.AddTransient<IUserRepository, UserRepository>();
            //services.AddTransient<IOrderRepository, OrderRepository>();
            //services.AddTransient<IReaderRepository,ReaderRepository>();
            //services.AddTransient<IOrderDetailRepository, OrderDetailRepository>();
            //services.AddTransient<IProductRepository, ProductRepository>();
            //services.AddTransient<ITarotCardRepository, TarotCardRepository>();
            //services.AddTransient<IFavoriteProductRepository, FavoriteProductRepository>();
            //services.AddTransient<IPackageQuestionRepository, PackageQuestionRepository>();
            //services.AddTransient<IFeedBackRepository, FeedBackRepository>();
            services.AddAutoMapper(typeof(ApplicationDbContext));

            return services;
        }
    }
}
