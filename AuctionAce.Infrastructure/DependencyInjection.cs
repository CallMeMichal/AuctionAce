using AuctionAce.Infrastructure.Data.AuctionAceDbContext;
using AuctionAce.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AuctionAce.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<AuctionAceContext>(options =>
            options.UseSqlServer(
            configuration.GetConnectionString("DefaultConnection"),
            b => b.MigrationsAssembly(typeof(AuctionAceContext).Assembly.FullName)));

            services.AddScoped<UserRepository> ();

            return services;
        }




    }
}
