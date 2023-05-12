using Homework.Data;
using Homework.Data.Entities;
using Homework.Services;
using Homework.Services.Abstractions;
using Homework.Services.MapperProfiles;
using Microsoft.EntityFrameworkCore;

namespace Homework.Utils
{
    public static class StartupExtensions
    {
        public static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<QuestRoomContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("Azure"));
            });

            builder.Services.AddTransient<DatabaseDataFilterBuilder<QuestRoom>>()
                .AddTransient<IFileNameGenerator, UniqueFileNameGenerator>()
                .AddTransient<IFormImageProcessor, QuestRoomLogoImageSaver>();

            builder.Services.AddAutoMapper(profiles =>
            {
                profiles.AddProfile<QuestRoomProfile>();
            });
                            
            builder.Services.AddControllersWithViews();

            return builder;
        }

        public static void Configure(this WebApplication app)
        {
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