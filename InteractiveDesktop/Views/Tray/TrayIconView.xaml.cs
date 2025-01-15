using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using H.NotifyIcon;
using Microsoft.UI.Xaml.Controls;

namespace Interactive_Desktop.Views.Tray;

[ObservableObject]
#pragma warning disable MVVMTK0050
public sealed partial class TrayIconView : UserControl
#pragma warning restore MVVMTK0050
{
    [ObservableProperty]
#pragma warning disable MVVMTK0045
    private bool _isWindowVisible;
#pragma warning restore MVVMTK0045

    public TrayIconView()
    {
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
