using Manager.UI.ViewModels;

namespace Manager.UI.Pages;

public partial class ProfilePage : ContentPage
{
    private ProfilePageViewModel _viewModel;
    public ProfilePage(ProfilePageViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }
}