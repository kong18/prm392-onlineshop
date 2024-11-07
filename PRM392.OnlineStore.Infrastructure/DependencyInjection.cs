using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PRM392.OnlineStore.Domain.Common.Interfaces;
using PRM392.OnlineStore.Domain.Entities.Repositories;
using PRM392.OnlineStore.Infrastructure.Persistence;
using PRM392.OnlineStore.Infrastructure.Repositories;

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

                        b.EnableRetryOnFailure(
                            maxRetryCount: 5, // Number of retry attempts
                            maxRetryDelay: TimeSpan.FromSeconds(30), // Max delay between retries
                            errorNumbersToAdd: null); // Optional: specify SQL error numbers to retry on
                    });
                options.UseLazyLoadingProxies();
            });

            services.AddScoped<IUnitOfWork>(provider => provider.GetRequiredService<ApplicationDbContext>());
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ICartRepository, CartRepository>();
            services.AddTransient<ICartItemRepository, CartItemRepository>();
            services.AddTransient<IPaymentRepository, PaymentRepository>();
        
            services.AddTransient<INotificationRepository, NotificationRepository>();
           
            return services;
        }

    }
}
