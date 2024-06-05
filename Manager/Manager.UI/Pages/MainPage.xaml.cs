using Manager.UI.ViewModels;
using System.Collections.ObjectModel;

namespace Manager.UI.Pages;

public partial class MainPage : ContentPage
{
    private MainPageViewModel _viewModel;
    public MainPage(MainPageViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;


        
    }

    protected override bool OnBackButtonPressed()
    {
        // Return true to prevent back button 
        return true;
    }
}
