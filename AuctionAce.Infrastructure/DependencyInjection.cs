using AuctionAce.Infrastructure.Data.Models;
using AuctionAce.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AuctionAce.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configurationManager)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(
            configurationManager.GetConnectionString("DefaultConnection"),
            b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
            services.AddScoped<UserRepository>();
            services.AddScoped<AuctionRespository>();
            services.AddScoped<ChatHistoryRepostiory>();
            services.AddScoped<BidHistoryRepostiory>();
            services.AddScoped<UserBoughtItemsRepostiory>();

            return services;
        }
    }
}