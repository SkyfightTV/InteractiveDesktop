using Microsoft.UI.Xaml.Controls;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Interactive_Desktop.Views.NavigationItems.Home.Buttons;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class Screens : NavigationPage
{
    private static new readonly NavigationViewItem NavigationViewItem = new()
    {
        Content = "Screens",
        Icon = new SymbolIcon(Symbol.FullScreen),
    };

    public Screens()
        : base(NavigationViewItem)
    {
        this.InitializeComponent();
    }

    public override void Invoke() { }
}
