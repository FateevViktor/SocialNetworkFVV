using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SocialNetworkFVV.Models;
using SocialNetworkFVV.Services;

namespace SocialNetworkFVV
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            //Настройки подключения к БД
            var connection = builder.Configuration.GetConnectionString("DefaultConnection");
            //Контекст БД
            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connection));
            //Добавляем сервис аутентификации
            builder.Services.AddAuthentication();
            //определить нашу модель работы с пользователями
            builder.Services.AddIdentity<User, IdentityRole>(opts => {
                opts.Password.RequiredLength = 5; // Минимальная длина — 5 символов
                opts.Password.RequireNonAlphanumeric = false; // Не требуют спецсимволы
                opts.Password.RequireLowercase = false; // Не требуют строчные буквы
                opts.Password.RequireUppercase = false; // Не требуют прописные буквы
                opts.Password.RequireDigit = false; // Не требуют цифр
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();
            app.UseAuthentication();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
