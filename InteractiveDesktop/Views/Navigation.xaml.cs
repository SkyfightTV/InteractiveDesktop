using System.Collections.Generic;
using System.Linq;
using Interactive_Desktop.Views.NavigationItems;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Interactive_Desktop.Views;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class Navigation : Page
{
    private static HashSet<NavigationPage> NavigationPages { get; } = [new Home()];

    public Navigation()
    {
        this.InitializeComponent();

        NavigationView.ItemInvoked += ItemInvoke;
        foreach (var page in NavigationPages)
        {
            NavigationView.MenuItems.Add(page.NavigationViewItem);
        }
    }

    private void ItemInvoke(NavigationView sender, NavigationViewItemInvokedEventArgs args)
    {
        var options = new FrameNavigationOptions
        {
            TransitionInfoOverride = args.RecommendedNavigationTransitionInfo,
        };
        var page = NavigationPages.First(page =>
            ((string)args.InvokedItem) == page.NavigationViewItem.Content.ToString()
        );
        {
            _ = NavigationViewFrame.NavigateToType(page.GetType(), null, options);
            page.Invoke();
        }
    }
}
