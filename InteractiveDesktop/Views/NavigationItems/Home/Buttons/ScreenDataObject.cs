using System;
using System.Text.RegularExpressions;

namespace Interactive_Desktop.Views.NavigationItems.Home.Buttons;

public class ScreenDataObject(IntPtr monitor)
{
    public IntPtr Monitor { get; set; } = monitor;
    public string DisplayName => Monitor.ToString();
}
