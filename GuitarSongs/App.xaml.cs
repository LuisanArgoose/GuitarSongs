using GuitarSongs.DependencyModel;
using GuitarSongs.Services;
using GuitarSongs.Services.Contracts;
using GuitarSongs.ViewModels.Pages;
using GuitarSongs.ViewModels.Windows;
using GuitarSongs.Views.Pages;
using GuitarSongs.Views.Windows;

namespace GuitarSongs;

public partial class App : Application
{
    // The .NET Generic Host provides dependency injection, configuration, logging, and other services.
    // https://docs.microsoft.com/dotnet/core/extensions/generic-host
    // https://docs.microsoft.com/dotnet/core/extensions/dependency-injection
    // https://docs.microsoft.com/dotnet/core/extensions/configuration
    // https://docs.microsoft.com/dotnet/core/extensions/logging
    private static readonly IHost _host = Host.CreateDefaultBuilder()
        .ConfigureAppConfiguration(c =>
        {
            c.SetBasePath(AppContext.BaseDirectory);
        })
        .ConfigureServices(
            (_, services) =>
            {
                // App Host
                services.AddHostedService<ApplicationHostService>();

                // Main window container with navigation
                services.AddSingleton<IWindow, MainWindow>();
                services.AddSingleton<MainWindowViewModel>();
                services.AddSingleton<INavigationService, NavigationService>();
                services.AddSingleton<ISnackbarService, SnackbarService>();
                services.AddSingleton<IContentDialogService, ContentDialogService>();
                services.AddSingleton<WindowsProviderService>();

                // Top-level pages
                services.AddSingleton<DescriptionPage>();
                services.AddSingleton<DescriptionViewModel>();
                //services.AddSingleton<AllControlsPage>();
                //services.AddSingleton<AllControlsViewModel>();
                //services.AddSingleton<SettingsPage>();
                //services.AddSingleton<SettingsViewModel>();

                // All other pages and view models
                services.AddTransientFromNamespace("GuitarSongs.Views", GuitarSongsAssembly.Asssembly);
                services.AddTransientFromNamespace("GuitarSongs.ViewModels", GuitarSongsAssembly.Asssembly);
            }
        )
        .Build();

    /// <summary>
    /// Gets registered service.
    /// </summary>
    /// <typeparam name="T">Type of the service to get.</typeparam>
    /// <returns>Instance of the service or <see langword="null"/>.</returns>
    public static T GetRequiredService<T>()
        where T : class
    {
        return _host.Services.GetRequiredService<T>();
    }

    /// <summary>
    /// Occurs when the application is loading.
    /// </summary>
    private void OnStartup(object sender, StartupEventArgs e)
    {
        _host.Start();
    }

    /// <summary>
    /// Occurs when the application is closing.
    /// </summary>
    private void OnExit(object sender, ExitEventArgs e)
    {
        _host.StopAsync().Wait();

        _host.Dispose();
    }
}

