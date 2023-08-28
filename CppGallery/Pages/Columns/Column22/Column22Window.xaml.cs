using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Windowing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using WinRT.Interop;
using System.Threading.Tasks;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace CppGallery.Pages.Columns.Column22
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Column22Window : Window
    {
        AppWindow AppWindow { get; set; }
        IntPtr hWnd { get; set; }
        private int Count = 0;

        private static string[] Names = new string[]
        {
            "システムの既定",
            "背景なし",
            "Mica",
            "アクリル",
            "MicaAlt",
        };

        public Column22Window()
        {
            this.InitializeComponent();
            Title = "タイトルバーの視覚効果を変更  (C++ Library Gallery)";
            hWnd = WindowNative.GetWindowHandle(this);
            AppWindow = AppWindow.GetFromWindowId(Win32Interop.GetWindowIdFromWindow(WindowNative.GetWindowHandle(this)));
            AppWindow.SetIcon("Pages/Assets/Square150x150Logo.scale-200.ico");
            CloseWindow();
        }

        private void ThemeSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            int value = ThemeSwitch.IsOn ? 1 : 0;
            ElementTheme theme = ThemeSwitch.IsOn ? ElementTheme.Dark : ElementTheme.Light;
            _ = MainWindow.DwmSetWindowAttribute(hWnd, 20, ref value, sizeof(int));
            OuterContent.RequestedTheme = theme;
        }

        private void ModeText_Loaded(object sender, RoutedEventArgs e)
        {
            ModeText.Text = Names[Count];
        }

        private void ChangeButton_Click(object sender, RoutedEventArgs e)
        {
            ++Count;
            int value = Count % 5;
            _ = MainWindow.DwmSetWindowAttribute(hWnd, 38, ref value, sizeof(int));
            ModeText.Text = Names[value];
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
            }
        }
    }
}
