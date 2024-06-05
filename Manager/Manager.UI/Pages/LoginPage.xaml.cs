using Manager.UI.ViewModels;

namespace Manager.UI.Pages
{
    public partial class LoginPage : ContentPage
    {
        private LoginPageViewModel _viewModel;
        public LoginPage(LoginPageViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            BindingContext = _viewModel;
        }
    }
}