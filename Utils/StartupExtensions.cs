using Homework.Data;
using Homework.Data.Entities;
using Homework.Services;
using Microsoft.EntityFrameworkCore;

namespace Homework.Utils
{
    public static partial class StartupExtensions
    {
        public static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder)
        {
            // Add services to the container.
            builder.Services.AddTransient<DatabaseDataFilterBuilder<QuestRoom>>();
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<QuestRoomContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
            });

            return builder;
        }

        public static async Task ConfigureAsync(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
                await QuestRoomDatabaseInitializer.InitializeAsync(scope.ServiceProvider);
            
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/QuestRoom/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=QuestRoom}/{action=Index}/{id:int?}");
        }
    }
}