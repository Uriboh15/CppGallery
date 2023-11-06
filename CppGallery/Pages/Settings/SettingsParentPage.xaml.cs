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
using Microsoft.UI.Xaml.Media.Animation;
using CppGallery.Pages.UserControls;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace CppGallery.Pages.Settings
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SettingsParentPage : Page
    {
        private static SettingsParentPage Handle = null;
        private static bool IsFirstLoad = true;
        
        public static SettingsParentPage GetHandle()
        {
            if(Handle == null)
            {
                Handle = new SettingsParentPage();
            }
            return Handle;
        }

        private SettingsParentPage()
        {
            this.InitializeComponent();
            UserAPI.ToCompactForResources(this, NavView, SettingFrame);
            HeadText.FontSize = Data.NavigationViewHeaderFontSize;
        }

        private void NavView_Loaded(object sender, RoutedEventArgs e)
        {
            if (!IsFirstLoad) return;
            NavView.SelectedItem = NavView.MenuItems[0];
            SettingFrame.Content = SettingPage.GetHandle();
            IsFirstLoad = false;
        }

        private void NavView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            var content = (args.SelectedItem as NavigationViewItem).Content as string;

            switch (content)
            {
                case "設定": SettingFrame.Content = SettingPage.GetHandle(); break;
                case "アプリについて": SettingFrame.Content = AboutAppPage.GetHandle(); break;
                case "更新履歴": SettingFrame.Content = ReleaseNote.GetHandle(); break;
            }

            HeadText.Text = content;
        }
    }
}
