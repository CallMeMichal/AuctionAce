using AuctionAce.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AuctionAce.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<UserService>();
            services.AddScoped<AuctionService>();
            services.AddScoped<LoginService>();
            services.AddScoped<AuthenticationService>();
            services.AddScoped<CalendarService>();
            services.AddScoped<ChatHistoryService>();
            //dowiedziec czym sie rozni scope,transit,singleton
            //services.AddScoped
            return services;
        }
    }
}