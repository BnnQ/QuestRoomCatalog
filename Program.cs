using Homework.Utils;

namespace Homework
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.ConfigureServices();
            var app = builder.Build();

            await app.ConfigureAsync();
            app.Run();
        }
    }
}