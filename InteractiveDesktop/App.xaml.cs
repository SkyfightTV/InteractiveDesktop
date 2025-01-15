using H.NotifyIcon;
using Interactive_Desktop.Views;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Windows.Globalization.DateTimeFormatting;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Interactive_Desktop;

/// <summary>
/// Provides application-specific behavior to supplement the default Application class.
/// </summary>
public partial class App : Application
{
    public static Window? MainWindow { get; private set; }
    public static bool HandleClosedEvents { get; set; } = true;

    /// <summary>
    /// Initializes the singleton application object.  This is the first line of authored code
    /// executed, and as such is the logical equivalent of main() or WinMain().
    /// </summary>
    public App()
    {
        this.InitializeComponent();
    }

    /// <summary>
    /// Invoked when the application is launched.
    /// </summary>
    /// <param name="args">Details about the launch request and process.</param>
    protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
    {
        MainWindow = new MainWindow();
        var appWindow = GetAppWindowForCurrentWindow(MainWindow);
        {
            var presenter = appWindow.Presenter as OverlappedPresenter;
            presenter!.IsAlwaysOnTop = true;

            appWindow.Closing += AppWindow_Closing;
            MainWindow.Hide();
        }
        MainWindow.Activate();
    }

    private static AppWindow GetAppWindowForCurrentWindow(Window window)
    {
        var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(window);
        var myWndId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(hWnd);
        return AppWindow.GetFromWindowId(myWndId);
    }

    private static void AppWindow_Closing(AppWindow sender, AppWindowClosingEventArgs args)
    {
        sender.Hide();
        args.Cancel = true;
    }
}
