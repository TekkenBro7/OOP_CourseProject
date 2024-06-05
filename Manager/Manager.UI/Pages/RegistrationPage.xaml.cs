using Manager.UI.ViewModels;

namespace Manager.UI.Pages;

public partial class RegistrationPage : ContentPage
{
    private RegistrationPageViewModel _viewModel;
    public RegistrationPage(RegistrationPageViewModel viewModel)
	{
		InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }
}