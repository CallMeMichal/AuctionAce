﻿using AuctionAce.Application.Services;
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
            services.AddScoped<BidHistoryService>();
            services.AddScoped<UserBoughtItemsService>();
            services.AddScoped<CategoryService>();
            services.AddScoped<WishlistService>();
            //dowiedziec czym sie rozni scope,transit,singleton
            //services.AddScoped
            return services;
        }
    }
}