using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Manager.Persistense;
using Manager.UI.Pages;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using Manager.Persistense.Data;
using Microsoft.EntityFrameworkCore;
using Manager.UI.ViewModels;
using CommunityToolkit.Maui;
using Manager.Persistense.Repository;
using Manager.Domain.Abstractions;
using SkiaSharp.Views.Maui.Controls.Hosting;

namespace Manager.UI
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .UseSkiaSharp()
                .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });
            builder.Services.
                RegisterPages().
                RegisterViewModels();
            ConfigureServices(builder.Services);

#if DEBUG
            builder.Logging.AddDebug();
#endif
            var app = builder.Build();

            return app;
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            string connectionString = "mongodb://localhost:27017";
            string databaseName = "ManagerDatabase";
            services.AddSingleton<MongoDbContext>(sp => new MongoDbContext(connectionString, databaseName));
            // Регистрация репозиториев и UnitOfWork
            services.AddScoped(typeof(IRepository<>), typeof(MongoRepository<>));
            services.AddScoped<IUnitOfWork, MongoUnitOfWork>();

            services.AddScoped<AuthManager>();

        }
    }
}