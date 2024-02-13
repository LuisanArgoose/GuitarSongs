
using GuitarSongs.Views.Pages;
using System.Configuration;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using Wpf.Ui.Controls;

namespace GuitarSongs.ViewModels.Windows;
public partial class MainWindowViewModel : ObservableObject
{
    [ObservableProperty]
    private ICollection<object> _menuItems = new ObservableCollection<object>
    {
        new NavigationViewItem("Home", SymbolRegular.Home24, typeof(DashboardPage)),
    };



    [ObservableProperty]
    private ObservableCollection<Wpf.Ui.Controls.MenuItem> _trayMenuItems =
        new()
        {
            new Wpf.Ui.Controls.MenuItem { Header = "Home", Tag = "tray_home" },
            new Wpf.Ui.Controls.MenuItem { Header = "Close", Tag = "tray_close" }
        };
}

