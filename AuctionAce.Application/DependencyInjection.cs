using AuctionAce.Application.Interfaces;
using AuctionAce.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionAce.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<UserService>();
            services.AddScoped<AuctionService>();
            services.AddScoped<LoginService>();
            //dowiedziec czym sie rozni scope,transit,singleton
            //services.AddScoped
            return services;
        }


    }
}
