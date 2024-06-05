using Manager.UI.ViewModels;

namespace Manager.UI.Pages;

public partial class EditSubTaskPage : ContentPage
{
	EditSubTaskPageViewModel _viewModel;
	public EditSubTaskPage(EditSubTaskPageViewModel viewModel)
	{
		InitializeComponent();
		_viewModel = viewModel;
		BindingContext = _viewModel;
	}
}