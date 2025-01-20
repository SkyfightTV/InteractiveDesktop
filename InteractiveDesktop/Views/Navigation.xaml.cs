using System;
using System.Collections.Generic;
using System.Linq;
using Interactive_Desktop.Views.NavigationItems.Home;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace Interactive_Desktop.Views;

public sealed partial class Navigation : Page
{
    private static HashSet<ANavigationPage> NavigationPages { get; } = [new HomeWindow()];

    public Navigation()
    {
        this.InitializeComponent();

        NavigationView.ItemInvoked += ItemInvoke;
        foreach (var page in NavigationPages)
        {
            NavigationView.MenuItems.Add(page.NavigationViewItem);
        }
        NavigationView.SelectedItem = NavigationView.MenuItems.First();
    }

    private void ItemInvoke(NavigationView sender, NavigationViewItemInvokedEventArgs args)
    {
        var options = new FrameNavigationOptions
        {
            TransitionInfoOverride = args.RecommendedNavigationTransitionInfo,
            IsNavigationStackEnabled = true,
        };

        var invokedItemContent = (string)args.InvokedItem;
        var page = NavigationPages.FirstOrDefault(page =>
            invokedItemContent == page.NavigationViewItem.Content.ToString()
        );
        if (page == null)
        {
            return;
        }
        _ = NavigationViewFrame.NavigateToType(page.GetType(), null, options);
        page.Invoke();
    }
}
