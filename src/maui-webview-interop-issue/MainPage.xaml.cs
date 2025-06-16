using System.Runtime.InteropServices;

namespace maui_webview_interop_issue
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
        }

        private void OpenWindowButton_Clicked(object sender, EventArgs e)
        {
            var window = new Window(new ContentPage() { Content = new WebView() { Source = "https://microsoft.com" } });
            Application.Current?.OpenWindow(window);
        }

        private void OpenChildWindowButton_Clicked(object sender, EventArgs e)
        {
            var window = new Window(new ContentPage() { Content = new WebView() { Source = "https://microsoft.com" } });
            var mainWindowHandle = (Application.Current?.MainPage.Window.Handler.PlatformView as MauiWinUIWindow).WindowHandle;

            Application.Current?.OpenWindow(window);
            var windowHandle = (window.Handler.PlatformView as MauiWinUIWindow).WindowHandle;

            NativeInterop.SetParent(windowHandle, mainWindowHandle);
        }
    }

    public static class NativeInterop
    {
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);
    }
}
