using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using WinRT.Interop;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace CppGallery.Pages.Columns.Column17
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CoenerWindow : Window
    {
        private new AppWindow AppWindow { get; set; }
        private IntPtr hWnd;

        [DllImport("dwmapi.dll")]
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);
        private const int DWMWA_USE_IMMERSIVE_DARK_MODE = 20;
        private const int DWMWA_WINDOW_CORNER_PREFERENCE = 33;
        private int Count { get; set; } = 0;


        public CoenerWindow()
        {
            this.InitializeComponent();
            hWnd = WindowNative.GetWindowHandle(this);
            AppWindow = AppWindow.GetFromWindowId(Win32Interop.GetWindowIdFromWindow(hWnd));
            AppWindow.Title = "äpä€ñ≥å¯âª  (C++ Library Gallery)";
            AppWindow.SetIcon("Pages/Assets/Square150x150Logo.scale-200.ico");
            CloseWindow();
        }

        private void SetTitleBar()
        {
            int value = (ModeText.ActualTheme == ElementTheme.Dark) ? 1 : 0;
            DwmSetWindowAttribute(hWnd, DWMWA_USE_IMMERSIVE_DARK_MODE, ref value, sizeof(int));
        }

        private void ChangeButton_Click(object sender, RoutedEventArgs e)
        {
            ++Count;
            int value = Count % 4;

            DwmSetWindowAttribute(hWnd, DWMWA_WINDOW_CORNER_PREFERENCE, ref value, sizeof(int));

            switch (value)
            {
                case 0:
                    ModeText.Text = "ÉVÉXÉeÉÄÇÃä˘íË";
                    break;

                case 1:
                    ModeText.Text = "äpä€Çñ≥å¯âª";
                    break;

                case 2:
                    ModeText.Text = "äpä€ÇóLå¯âª";
                    break;

                case 3:
                    ModeText.Text = "è¨Ç≥Ç»äpä€";
                    break;
            }
        }

        private void StackPanel_ActualThemeChanged(FrameworkElement sender, object args)
        {
            SetTitleBar();
        }

        private void StackPanel_Loaded(object sender, RoutedEventArgs e)
        {
            Panel.RequestedTheme = MainWindow.Handle.GetTheme();
            SetTitleBar();
        }

        private async void CloseWindow()
        {
            while (true)
            {
                await Task.Delay(100);
                if (MainWindow.WindowClosed)
                {
                    this.Close();
                    return;
                }
                if (Panel.RequestedTheme != MainWindow.Handle.GetTheme())
                {
                    Panel.RequestedTheme = MainWindow.Handle.GetTheme();
                }
            }
        }
    }
}
