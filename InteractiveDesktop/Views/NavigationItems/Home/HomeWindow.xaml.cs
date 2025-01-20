using System;
using System.Collections.Generic;
using Interactive_Desktop.Views.NavigationItems.Home.Buttons;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace Interactive_Desktop.Views.NavigationItems.Home;

public sealed partial class HomeWindow : ANavigationPage
{
    private static readonly List<AHomeButton> Buttons = [new Screens()];

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
        foreach (var button in Buttons)
        {
            TopBar.PrimaryCommands.Add(button.GetAppBarButton());
        }
    }

    public void CreateWallpaper(object sender, RoutedEventArgs e)
    {
        if (MainWindow.SelectedMonitor is not { } monitor)
        {
            Console.Error.WriteLine("NO SELECTED");
            return;
        }
        MainWindow.Wallpapers.Add(new Wallpaper.Wallpaper(monitor));
    }

    public override void Invoke() { }
}
