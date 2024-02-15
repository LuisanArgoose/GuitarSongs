using Wpf.Ui.Controls;
using GuitarSongs.Services.Contracts;
using GuitarSongs.ViewModels.Windows;
using GuitarSongs.Views.Pages;

namespace GuitarSongs.Views.Windows;

public partial class MainWindow : IWindow
{
    public MainWindow(
        MainWindowViewModel viewModel,
        INavigationService navigationService,
        IServiceProvider serviceProvider,
        ISnackbarService snackbarService,
        IContentDialogService contentDialogService
    )
    {
        SystemThemeWatcher.Watch(this);

        ViewModel = viewModel;
        DataContext = this;

        InitializeComponent();

        snackbarService.SetSnackbarPresenter(SnackbarPresenter);
        navigationService.SetNavigationControl(NavigationView);
        contentDialogService.SetContentPresenter(RootContentDialog);

        NavigationView.SetServiceProvider(serviceProvider);
    }

    public MainWindowViewModel ViewModel { get; }

    private void OnNavigationSelectionChanged(object sender, RoutedEventArgs e)
    {
        if (sender is not NavigationView navigationView)
        {
            return;
        }

        NavigationView.HeaderVisibility =
            navigationView.SelectedItem?.TargetPageType != typeof(DescriptionPage)
                ? Visibility.Visible
                : Visibility.Collapsed;
    }
    
}