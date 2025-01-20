using System;
using System.Collections.Generic;
using System.Linq;
using Interactive_Desktop.Injecter;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Interactive_Desktop.Views.NavigationItems.Home.Buttons;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class Screens : AHomeButton
{
    private readonly AppBarButton _appBarButton;
    private Flyout? _flyout = null;

    public Screens()
    {
        this.InitializeComponent();
        _appBarButton = new AppBarButton
        {
            Icon = new SymbolIcon(Symbol.FullScreen),
            Label = "Screens",
        };
        _appBarButton.Click += OnClick;
    }

    private static IntPtr[] GetAllMonitors()
    {
        var monitors = new List<IntPtr>();
        var result = NativeFunctions.EnumDisplayMonitors(
            IntPtr.Zero,
            IntPtr.Zero,
            (IntPtr monitor, IntPtr _, ref NativeFunctions.Rect _, IntPtr _) =>
            {
                monitors.Add(monitor);
                return true;
            },
            IntPtr.Zero
        );
        if (!result)
        {
            throw new Exception("Failed to enumerate display monitors.");
        }
        return monitors.ToArray();
    }

    public override AppBarButton GetAppBarButton()
    {
        return _appBarButton;
    }

    public override void OnClick(object sender, RoutedEventArgs e)
    {
        if (_flyout != null)
        {
            _flyout.ShowAt(_appBarButton);
            return;
        }
        _flyout = new Flyout { Content = this };
        var monitors = GetAllMonitors();

        ScreensView.ItemsSource = monitors.Select(monitor => new ScreenDataObject(monitor));
        _flyout.ShowAt(_appBarButton);
    }

    public void ScreensView_Click(object sender, ItemClickEventArgs itemClickEventArgs)
    {
        var screen = (ScreenDataObject)itemClickEventArgs.ClickedItem;
        MainWindow.SelectedMonitor = screen.Monitor;
        _flyout?.Hide();
    }
}
