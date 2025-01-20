using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace Interactive_Desktop.Views.NavigationItems.Home;

public abstract class AHomeButton : Page
{
    public abstract AppBarButton GetAppBarButton();

    public abstract void OnClick(object sender, RoutedEventArgs e);
}
