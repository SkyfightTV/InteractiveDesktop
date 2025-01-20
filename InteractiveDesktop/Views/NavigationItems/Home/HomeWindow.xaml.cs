using System.Collections.Generic;
using Interactive_Desktop.Views.NavigationItems.Home.Buttons;
using Microsoft.UI.Xaml.Controls;

namespace Interactive_Desktop.Views.NavigationItems.Home;

public sealed partial class HomeWindow : NavigationPage
{
    private static readonly List<NavigationPage> buttons = new() { new Screens() };

    // ReSharper disable once ArrangeModifiersOrder
    private static new readonly NavigationViewItem NavigationViewItem = new()
    {
        Content = "Home",
        Icon = new SymbolIcon(Symbol.Home),
    };

    public HomeWindow()
        : base(NavigationViewItem)
    {
        this.InitializeComponent();
        foreach (var button in buttons)
        {
            var appBarButton = new AppBarButton
            {
                Icon = button.NavigationViewItem.Icon,
                Label = button.NavigationViewItem.Content.ToString(),
            };

            TopBar.PrimaryCommands.Add(appBarButton);
        }
    }

    public override void Invoke() { }
}
