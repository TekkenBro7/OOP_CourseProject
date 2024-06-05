using Manager.Persistense.Data;
using Manager.UI.Pages;
using Manager.UI.ViewModels;
using Manager.Persistense.Data;

namespace Manager.UI
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
         
            MainPage = new AppShell();
        }
        protected override Window CreateWindow(IActivationState activationState)
        {
            Window window = base.CreateWindow(activationState);
            const int newWidth = 425;
            const int newHeight = 600;
            window.Width = newWidth;
            window.Height = newHeight;
            window.MinimumHeight = newHeight;
            window.MinimumWidth = newWidth;

            return window;
        }
    }
}
