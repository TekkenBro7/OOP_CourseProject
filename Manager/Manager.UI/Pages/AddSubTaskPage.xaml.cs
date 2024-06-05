using Manager.UI.ViewModels;

namespace Manager.UI.Pages;

public partial class AddSubTaskPage : ContentPage
{
    AddSubTaskPageViewModel _viewModel;
	public AddSubTaskPage(AddSubTaskPageViewModel viewModel)
	{
		InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }
}