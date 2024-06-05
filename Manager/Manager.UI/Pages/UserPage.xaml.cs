using Manager.UI.ViewModels;

namespace Manager.UI.Pages;

public partial class UserPage : ContentPage
{
    private UserPageViewModel _viewModel;
    public UserPage(UserPageViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }
}