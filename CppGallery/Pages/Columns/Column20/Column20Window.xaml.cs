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
using Microsoft.UI;
using WinRT.Interop;
using System.Threading.Tasks;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace CppGallery.Pages.Columns.Column20
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Column20Window : Window
    {
        private new AppWindow AppWindow { get; set; }

        public Column20Window()
        {
            this.InitializeComponent();
            Title = "タイトルバーのカスタマイズ  (C++ Library Gallery)";
            AppWindow = AppWindow.GetFromWindowId(Win32Interop.GetWindowIdFromWindow(WindowNative.GetWindowHandle(this)));
            AppWindow.SetIcon("Pages/Assets/Square150x150Logo.scale-200.ico");
            AppWindow.TitleBar.ExtendsContentIntoTitleBar = true;
            AppWindow.TitleBar.ButtonBackgroundColor = Windows.UI.Color.FromArgb(0x00, 0x00, 0x00, 0x00);
            AppWindow.TitleBar.ButtonInactiveBackgroundColor = Windows.UI.Color.FromArgb(0x00, 0x00, 0x00, 0x00);
            SetTitleBar();
            CloseWindow();
        }

        private void ToLight()
        {
            AppWindow.TitleBar.ButtonForegroundColor = Windows.UI.Color.FromArgb(0xFF, 0x00, 0x00, 0x00);
            AppWindow.TitleBar.ButtonHoverForegroundColor = Windows.UI.Color.FromArgb(0xFF, 0x00, 0x00, 0x00);
            AppWindow.TitleBar.ButtonPressedForegroundColor = Windows.UI.Color.FromArgb(0xFF, 0x00, 0x00, 0x00);
            AppWindow.TitleBar.ButtonInactiveForegroundColor = Windows.UI.Color.FromArgb(0xFF, 0x9B, 0x9B, 0x9B);
            AppWindow.TitleBar.ButtonHoverBackgroundColor = Windows.UI.Color.FromArgb(0xFF, 0xE9, 0xE9, 0xE9);
            AppWindow.TitleBar.ButtonPressedBackgroundColor = Windows.UI.Color.FromArgb(0xFF, 0xED, 0xED, 0xED);
        }

        private void ToDark()
        {
            AppWindow.TitleBar.ButtonForegroundColor = Windows.UI.Color.FromArgb(0xFF, 0xFF, 0xFF, 0xFF);
            AppWindow.TitleBar.ButtonHoverForegroundColor = Windows.UI.Color.FromArgb(0xFF, 0xFF, 0xFF, 0xFF);
            AppWindow.TitleBar.ButtonPressedForegroundColor = Windows.UI.Color.FromArgb(0xFF, 0xFF, 0xFF, 0xFF);
            AppWindow.TitleBar.ButtonInactiveForegroundColor = Windows.UI.Color.FromArgb(0xFF, 0x99, 0x99, 0x99);
            AppWindow.TitleBar.ButtonHoverBackgroundColor = Windows.UI.Color.FromArgb(0xFF, 0x2D, 0x2D, 0x2D);
            AppWindow.TitleBar.ButtonPressedBackgroundColor = Windows.UI.Color.FromArgb(0xFF, 0x29, 0x29, 0x29);
        }

        private void SetTitleBar()
        {
            if (ExtendSwitch.IsOn)
            {
                if (MainPanel.ActualTheme == ElementTheme.Dark)
                {
                    ToDark();
                }
                else
                {
                    ToLight();
                }
            }
            
        }

        private void MainPanel_ActualThemeChanged(FrameworkElement sender, object args)
        {
            SetTitleBar();
        }

        private void ExtendSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            if(AppWindow != null)
            {
                if (ExtendSwitch.IsOn)
                {
                    AppWindow.TitleBar.ExtendsContentIntoTitleBar = true;
                    AppWindow.TitleBar.PreferredHeightOption = TallSwitch.IsOn ? TitleBarHeightOption.Tall : TitleBarHeightOption.Standard;
                    AppWindow.TitleBar.ButtonBackgroundColor = Windows.UI.Color.FromArgb(0x00, 0x00, 0x00, 0x00);
                    AppWindow.TitleBar.ButtonInactiveBackgroundColor = Windows.UI.Color.FromArgb(0x00, 0x00, 0x00, 0x00);
                    SetTitleBar();

                }
                else
                {
                    AppWindow.TitleBar.ResetToDefault();
                }
            }
            
        }

        private void TallSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            if (TallSwitch.IsOn)
            {
                if (ExtendSwitch.IsOn)
                {
                    AppWindow.TitleBar.PreferredHeightOption = TitleBarHeightOption.Tall;
                }
                
                TitleBarIcon.Margin = new Thickness(18, 0, 0, 0);
                TitleTextBlock.Margin = new Thickness(50, 0, 0, 0);
                MainFrame.Margin = new Thickness(0, 48, 0, 0);
                AppTitleBar.Height = 48;
                AccentGrid.Height = 48;
            }
            else
            {
                if (ExtendSwitch.IsOn)
                {
                    AppWindow.TitleBar.PreferredHeightOption = TitleBarHeightOption.Standard;
                }
                    
                TitleBarIcon.Margin = new Thickness(8, 0, 0, 0);
                TitleTextBlock.Margin = new Thickness(30, 0, 0, 0);
                MainFrame.Margin = new Thickness(0, 32, 0, 0);
                AppTitleBar.Height = 32;
                AccentGrid.Height = 32;
            }
        }

        private void DefaultButton_Checked(object sender, RoutedEventArgs e)
        {
            MainPanel.RequestedTheme = ElementTheme.Default;
        }

        private void lightButton_Checked(object sender, RoutedEventArgs e)
        {
            MainPanel.RequestedTheme = ElementTheme.Light;
        }

        private void DarkButton_Checked(object sender, RoutedEventArgs e)
        {
            MainPanel.RequestedTheme = ElementTheme.Dark;
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

        private void AccentSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            if(AccentSwitch.IsOn)
            {
                AccentGrid.Visibility = Visibility.Visible;
            }
            else
            {
                AccentGrid.Visibility = Visibility.Collapsed;
            }
        }
    }
}
