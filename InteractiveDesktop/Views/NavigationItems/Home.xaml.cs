using Microsoft.UI.Xaml.Controls;

namespace Interactive_Desktop.Views.NavigationItems;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class Home : NavigationPage
{
    // ReSharper disable once ArrangeModifiersOrder
    private static new readonly NavigationViewItem NavigationViewItem = new()
    {
        Content = "Home",
        Icon = new SymbolIcon(Symbol.Home),
    };

    public Home()
        : base(NavigationViewItem)
    {
        this.InitializeComponent();
    }

    public override void Invoke() { }
}
