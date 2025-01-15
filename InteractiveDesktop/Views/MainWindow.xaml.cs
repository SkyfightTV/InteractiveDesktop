using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Interactive_Desktop.Views;

/// <summary>
/// An empty window that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class MainWindow : Window
{
    public MainWindow()
    {
        this.InitializeComponent();
    }

    //private void NavigationView_ItemInvoked(
    //    NavigationView sender,
    //    NavigationViewItemInvokedEventArgs args
    //)
    //{
    //    var options = new FrameNavigationOptions
    //    {
    //        TransitionInfoOverride = args.RecommendedNavigationTransitionInfo,
    //    };

    //    switch (((string)args.InvokedItem))
    //    {
    //        case "Notifications":
    //            _ = NavigationViewFrame.NavigateToType(typeof(NotificationView), null, options);
    //            ((NotificationView)NavigationViewFrame.Content).TrayIcon = TrayIconView.TrayIcon;
    //            break;
    //    }
    //}
}
