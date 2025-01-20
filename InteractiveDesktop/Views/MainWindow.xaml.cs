using System;
using System.Collections.Generic;
using Microsoft.UI.Xaml;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Interactive_Desktop.Views;

/// <summary>
/// An empty window that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class MainWindow : Window
{
    public static List<Wallpaper.Wallpaper> Wallpapers = [];
    public static IntPtr? SelectedMonitor = null;

    public MainWindow()
    {
        InitializeComponent();
    }
}
