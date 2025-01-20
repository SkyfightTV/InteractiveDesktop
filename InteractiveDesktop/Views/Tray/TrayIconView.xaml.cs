using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using H.NotifyIcon;
using Microsoft.UI.Xaml.Controls;

namespace Interactive_Desktop.Views.Tray;

[ObservableObject]
public sealed partial class TrayIconView : UserControl
{
    [ObservableProperty]
    private bool _isWindowVisible;

    public TrayIconView(bool isWindowVisible)
    {
        _isWindowVisible = isWindowVisible;
        InitializeComponent();
    }

    [RelayCommand]
    private void ShowHideWindow()
    {
        var window = App.MainWindow;
        if (window == null)
        {
            return;
        }

        if (window.Visible)
        {
            window.Hide();
        }
        else
        {
            window.Show();
        }
        IsWindowVisible = window.Visible;
    }

    [RelayCommand]
    private void ExitApplication()
    {
        App.MainWindow?.Hide();
        TrayIcon.Dispose();
        App.MainWindow?.Close();
    }
}
