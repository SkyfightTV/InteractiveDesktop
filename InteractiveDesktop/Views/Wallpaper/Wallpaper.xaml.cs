using Microsoft.UI.Xaml;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Interactive_Desktop.Views.Wallpaper
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Wallpaper : Window
    {
        public Wallpaper()
        {
            this.InitializeComponent();
            Inject();
            this.Activate();
        }

        private void Inject()
        {
            var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(this);
            var injecter = new Injecter.Injecter(hWnd);
            injecter.Attach();
        }
    }
}
