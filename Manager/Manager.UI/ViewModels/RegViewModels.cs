using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Manager.UI.Pages;

namespace Manager.UI.ViewModels
{
    public static class DependencyInjection
    {
        public static IServiceCollection RegisterPages(this IServiceCollection services)
        {
            services.
                AddTransient<MainPage>()
                .AddTransient<RegistrationPage>()
                .AddTransient<LoginPage>()
                .AddTransient<ProfilePage>()
                .AddTransient<AddTaskPage>()
                .AddTransient<DetaliPage>()
                .AddTransient<EditTaskPage>()
                .AddTransient<AddSubTaskPage>()
                .AddTransient<EditSubTaskPage>()
                .AddTransient<UserPage>();

            return services;
        }
        public static IServiceCollection RegisterViewModels(this IServiceCollection services)
        {
            services.
                AddTransient<MainPageViewModel>()
                .AddTransient<LoginPageViewModel>()
                .AddTransient<RegistrationPageViewModel>()
                .AddTransient<ProfilePageViewModel>()
                .AddTransient<AddTaskPageViewModel>()
                .AddTransient<DetaliPageViewModel>()
                .AddTransient<EditTaskPageViewModel>()
                .AddTransient<AddSubTaskPageViewModel>()
                .AddTransient<EditSubTaskPageViewModel>()
                .AddTransient<UserPageViewModel>();

            return services;
        }
    }
}
