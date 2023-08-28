// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Windowing;
using System.Runtime.InteropServices;
using System.Reflection.Metadata;
using WinRT.Interop;
using Microsoft.UI;
using System.Threading.Tasks;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace CppGallery.Pages.Columns.Column14
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Column14Window : Window
    {
        private new AppWindow AppWindow { get; set; }
        private IntPtr hWnd;

        [DllImport("dwmapi.dll")]
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);
        private const int DWMWA_USE_IMMERSIVE_DARK_MODE = 20;

        public ElementTheme RequestedTheme;

        public Column14Window(ElementTheme requestedTheme)
        {
            this.InitializeComponent();
            hWnd = WindowNative.GetWindowHandle(this);
            AppWindow = AppWindow.GetFromWindowId(Win32Interop.GetWindowIdFromWindow(hWnd));
            AppWindow.Title = "ダークモード対応  (C++ Library Gallery)";
            AppWindow.SetIcon("Pages/Assets/Square150x150Logo.scale-200.ico");
            RequestedTheme = requestedTheme;
            CloseWindow();
        }

        private void ModeText_Loaded(object sender, RoutedEventArgs e)
        {
            SetTitleBar();
        }

        private void ModeText_ActualThemeChanged(FrameworkElement sender, object args)
        {
            SetTitleBar();
        }

        private void SetTitleBar()
        {
            int value = (ModeText.ActualTheme == ElementTheme.Dark) ? 1 : 0;
            DwmSetWindowAttribute(hWnd, DWMWA_USE_IMMERSIVE_DARK_MODE, ref value, sizeof(int));
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            switch (RequestedTheme)
            {
                case ElementTheme.Dark:
                    Panel.RequestedTheme = ElementTheme.Dark;
                    ModeText.Text = "ダークモード";
                    break;

                    case ElementTheme.Light:
                    Panel.RequestedTheme = ElementTheme.Light;
                    ModeText.Text = "ライトモード";
                    break;

                case ElementTheme.Default:
                    ModeText.Text = "システムの既定";
                    break;
            }
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
