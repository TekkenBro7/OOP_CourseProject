using Manager.UI.ViewModels;

namespace Manager.UI.Pages;

public partial class EditTaskPage : ContentPage
{
	EditTaskPageViewModel _viewModel;
	public EditTaskPage(EditTaskPageViewModel viewModel)
	{
		InitializeComponent();
		_viewModel = viewModel;
		BindingContext = _viewModel;
	}
}