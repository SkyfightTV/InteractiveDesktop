using System;
using H.NotifyIcon;
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Interactive_Desktop
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : Application
    {
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
        protected override async void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            _mWindow = new MainWindow();
            var appWindow = GetAppWindowForCurrentWindow(_mWindow);
            {
                var presenter = appWindow.Presenter as OverlappedPresenter;
                presenter!.IsAlwaysOnTop = true;

                appWindow.Closing += AppWindow_Closing;
                _mWindow.Hide();
            }
            _mWindow.Activate();

            //var injecter = new Injecter.Injecter(
            //    WinRT.Interop.WindowNative.GetWindowHandle(_mWindow)
            //);
            //injecter.Attach();
        }

        private static void AppWindow_Closing(AppWindow sender, AppWindowClosingEventArgs args)
        {
            sender.Hide();
            args.Cancel = true;
        }

        private static AppWindow GetAppWindowForCurrentWindow(Window window)
        {
            var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(window);
            var myWndId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(hWnd);
            return AppWindow.GetFromWindowId(myWndId);
        }

        private Window? _mWindow;
    }
}
