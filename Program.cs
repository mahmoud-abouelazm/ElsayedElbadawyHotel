using ElSayedHotel.IRepository;
using ElSayedHotel.Models;
using ElSayedHotel.Repository;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ElSayedHotel
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews().AddCookieTempDataProvider(options =>
    {
        options.Cookie.Name = "TempDataCookie";
    });

            builder.Services.AddSession(options =>
            {
                // Set a short timeout for easy testing.
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            builder.Services.AddScoped<IRoomRepository, RoomRepository>();
            builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
            builder.Services.AddScoped<IGuestRepository, GuestRepository>();
            builder.Services.AddScoped<IRoomRepository, RoomRepository>();
            builder.Services.AddDbContext<HotelElsayedContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("main")));
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<HotelElsayedContext>();
            
            
            var app = builder.Build();
            
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();
            app.UseSession();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
