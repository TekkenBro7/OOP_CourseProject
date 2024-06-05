using Manager.UI.ViewModels;

namespace Manager.UI.Pages;

public partial class DetaliPage : ContentPage
{
	DetaliPageViewModel _viewModel;
	public DetaliPage(DetaliPageViewModel viewModel)
	{
		InitializeComponent();
		_viewModel = viewModel;
		BindingContext = _viewModel;
	}
}