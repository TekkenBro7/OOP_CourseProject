using Manager.UI.Pages;

namespace Manager.UI
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            Routing.RegisterRoute(nameof(RegistrationPage), typeof(RegistrationPage));
            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(ProfilePage), typeof(ProfilePage));
            Routing.RegisterRoute(nameof(AddTaskPage), typeof(AddTaskPage));
            Routing.RegisterRoute(nameof(DetaliPage), typeof(DetaliPage));
            Routing.RegisterRoute(nameof(EditTaskPage), typeof(EditTaskPage));
            Routing.RegisterRoute(nameof(AddSubTaskPage), typeof(AddSubTaskPage));
            Routing.RegisterRoute(nameof(EditSubTaskPage), typeof(EditSubTaskPage));
            Routing.RegisterRoute(nameof(UserPage), typeof(UserPage));

            InitializeComponent();
        }
    }
}
