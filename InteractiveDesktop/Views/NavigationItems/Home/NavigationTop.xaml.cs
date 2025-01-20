using System.Collections.Generic;
using System.Linq;
using Interactive_Desktop.Views.NavigationItems.Home.Buttons;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Interactive_Desktop.Views.NavigationItems.Home;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class NavigationTop : Page
{
    private static HashSet<NavigationPage> NavigationPages { get; } = [new Screens()];

    public NavigationTop()
    {
        this.InitializeComponent();
    }

    private void ItemInvoke(NavigationView sender, NavigationViewItemInvokedEventArgs args)
    {
        var options = new FrameNavigationOptions
        {
            TransitionInfoOverride = args.RecommendedNavigationTransitionInfo,
        };
        var invokedItemContent = (string)args.InvokedItem;
        var page = NavigationPages.FirstOrDefault(page =>
            invokedItemContent == page.NavigationViewItem.Content.ToString()
        );

        if (page == null)
        {
            return;
        }
        page.Invoke();
    }
}
