using Manager.UI.ViewModels;

namespace Manager.UI.Pages;

public partial class AddTaskPage : ContentPage
{
    AddTaskPageViewModel _viewModel;
	public AddTaskPage(AddTaskPageViewModel viewModel)
	{
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    private void dataPicker_DateSelected(object sender, DateChangedEventArgs e)
    {

    }
}