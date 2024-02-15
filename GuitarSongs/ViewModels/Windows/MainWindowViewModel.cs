
using GuitarSongs.Views.Pages;
using System.Configuration;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using Wpf.Ui.Controls;

namespace GuitarSongs.ViewModels.Windows;
public partial class MainWindowViewModel : ObservableObject
{
    [ObservableProperty]
    private string _applicationTitle = "Гитарный песенник";
    [ObservableProperty]
    private ICollection<object> _menuItems = new ObservableCollection<object>
    {
        
        
        new NavigationViewItem("Песни", SymbolRegular.MusicNote220, typeof(DescriptionPage)),
        new NavigationViewItem("Аккорды", SymbolRegular.TextNumberListLtr16, typeof(DescriptionPage)),
        

    };

    [ObservableProperty]
    private ICollection<object> _footerMenuItems = new ObservableCollection<object>()
    {

        new NavigationViewItem("Настройки", SymbolRegular.Settings24, typeof(DescriptionPage)),
        new NavigationViewItem("О проекте", SymbolRegular.Info24, typeof(DescriptionPage)),
    };

    [ObservableProperty]
    private ObservableCollection<Wpf.Ui.Controls.MenuItem> _trayMenuItems =
        new()
        {
            new Wpf.Ui.Controls.MenuItem { Header = "Home", Tag = "tray_home" },
            new Wpf.Ui.Controls.MenuItem { Header = "Close", Tag = "tray_close" }
        };
}

