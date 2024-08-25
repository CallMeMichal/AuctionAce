using AuctionAce.Api.Hubs;
using AuctionAce.Application;
using AuctionAce.Application.Services;
using AuctionAce.Infrastructure;

namespace AuctionAce.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddApplication();
            builder.Services.AddSignalR();
            builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
            builder.Services.AddInfrastructure(builder.Configuration);
            builder.Services.AddDistributedMemoryCache();
            AuthenticationService.Initialize(builder.Configuration);
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(10000000);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            var app = builder.Build();
            app.UseSession();
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            builder.Services.AddDistributedMemoryCache();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.UseAuthentication();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapHub<ChatHub>("/chathub");
            app.MapHub<BidHub>("/bidhub");
            app.Run();
        }
    }
}