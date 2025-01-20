using Microsoft.UI.Xaml.Controls;

namespace Interactive_Desktop.Views;

public abstract class ANavigationPage(NavigationViewItem navigationViewItem) : Page
{
    public readonly NavigationViewItem NavigationViewItem = navigationViewItem;

    public abstract void Invoke();
}
