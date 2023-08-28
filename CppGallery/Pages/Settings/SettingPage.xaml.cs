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
    public sealed partial class SettingPage : Page
    {
        private bool BackCLoaded = false;
        private bool LibraryCLoaded = false;
        private bool SourceThemeLoaded = false;
        private bool ResultThemeLoaded = false;
        private bool PlatCLoaded = false;
        private bool CompilerLoaded = false;
        private bool UseCppLoaded = false;
        private bool IsShowReturnCodeTLoaded = false;
        public static bool IsCompact { get; set; }

        private static SettingPage Handle = null;

        public static SettingPage GetHandle()
        {
            if (Handle == null)
            {
                Handle = new SettingPage();
            }
            return Handle;
        }

        private SettingPage()
        {
            this.InitializeComponent();
        }

        private void ThemeC_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string tmp = e.AddedItems[0].ToString();
            if (tmp == "ダーク")
            {
                App.Theme = ElementTheme.Dark;
            }
            else if (tmp == "ライト")
            {
                App.Theme = ElementTheme.Light;
            }
            else
            {
                App.Theme = ElementTheme.Default;
            }
            MainWindow.Handle.SetTitleBarColors(App.Theme);
        }

        private void ThemeC_Loaded(object sender, RoutedEventArgs e)
        {
            switch (App.Theme)
            {
                case ElementTheme.Dark:
                    ThemeC.SelectedItem = "ダーク";
                    break;


                case ElementTheme.Light:
                    ThemeC.SelectedItem = "ライト";
                    break;

                case ElementTheme.Default:
                    ThemeC.SelectedItem = "システムの既定";
                    break;

            }

        }

        private void BackC_Loaded(object sender, RoutedEventArgs e)
        {

            if (App.Win10)
            {
                BackC.ItemsSource = new string[] { "アプリ内すりガラス", "アクリル", "オフ" };
            }

            switch (App.Back)
            {
                case BackDrop.None: BackC.SelectedItem = "オフ"; break;
                case BackDrop.Mica: BackC.SelectedItem = "Mica"; break;
                case BackDrop.FrostedGlass: BackC.SelectedItem = "アプリ内すりガラス"; break;
                case BackDrop.Acrylic: BackC.SelectedItem = "アクリル"; break;
            }

            BackC.Loaded -= BackC_Loaded;
        }

        private void BackC_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (BackCLoaded)
            {
                string AddedItem = e.AddedItems[0].ToString();
                switch (AddedItem)
                {
                    case "オフ": App.Back = BackDrop.None; break;
                    case "Mica": App.Back = BackDrop.Mica; break;
                    case "アプリ内すりガラス": App.Back = BackDrop.FrostedGlass; break;
                    case "アクリル": App.Back = BackDrop.Acrylic; break;
                }


                MainWindow.Handle.SetBackdrop();
                return;
            }
            BackCLoaded = true;
        }

        private void CompactT_Toggled(object sender, RoutedEventArgs e)
        {
            if (SText != null)
            {
                if (CompactT.IsOn)
                {
                    SText.Text = "オン";
                    IsCompact = true;
                }
                else
                {
                    SText.Text = "オフ";
                    IsCompact = false;
                }
            }
        }

        private void CompactT_Loaded(object sender, RoutedEventArgs e)
        {
            if (IsCompact)
            {
                CompactT.IsOn = true;
                SText.Text = "オン";
            }
            else
            {
                CompactT.IsOn = false;
                SText.Text = "オフ";
            }

            CompactT.Loaded -= CompactT_Loaded;
        }

        private void ComC_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CompilerLoaded)
            {
                switch (e.AddedItems[0].ToString())
                {
                    case "Clang":
                        App.Compiler = Compiler.Clang;
                        break;

                    case "GCC":
                        App.Compiler = Compiler.GCC;
                        break;

                    case "Visual C++":
                        App.Compiler = Compiler.VC;
                        break;
                }

                UpdateOtherPage();
            }

            CompilerLoaded = true;
        }

        private void ComC_Loaded(object sender, RoutedEventArgs e)
        {
            switch (App.Compiler)
            {
                case Compiler.Clang:
                    ComC.SelectedItem = "Clang";
                    break;

                case Compiler.GCC:
                    ComC.SelectedItem = "GCC";
                    break;

                case Compiler.VC:
                    ComC.SelectedItem = "Visual C++";
                    break;
            }
            ComC.Loaded -= ComC_Loaded;
        }

        private void Beta1_Loaded(object sender, RoutedEventArgs e)
        {
#if !DEBUG
            var grid = sender as FrameworkElement;
            (grid.Parent as Panel).Children.Remove(grid);
#endif


        }

        private void LibraryC_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LibraryCLoaded)
            {
                string AddedItem = e.AddedItems[0].ToString();
                switch (AddedItem)
                {
                    case "ブロック": App.LibraryPageStyle = LibraryPageStyle.Block; break;
                    case "リスト": App.LibraryPageStyle = LibraryPageStyle.List; break;
                }
                return;
            }
            LibraryCLoaded = true;
        }

        private void LibraryC_Loaded(object sender, RoutedEventArgs e)
        {
            switch (App.LibraryPageStyle)
            {
                case LibraryPageStyle.Block:
                    LibraryC.SelectedItem = "ブロック";
                    break;

                case LibraryPageStyle.List:
                    LibraryC.SelectedItem = "リスト";
                    break;
            }

            LibraryC.Loaded -= LibraryC_Loaded;
        }

        private void ThemeSC_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SourceThemeLoaded)
            {
                string AddedItem = e.AddedItems[0].ToString();
                switch (AddedItem)
                {
                    case "アプリのテーマと同期": App.SourceCodeTheme = ElementTheme.Default; break;
                    case "ライト": App.SourceCodeTheme = ElementTheme.Light; break;
                    case "ダーク": App.SourceCodeTheme = ElementTheme.Dark; break;
                }
            }
            SourceThemeLoaded = true;
        }

        private void ThemeSC_Loaded(object sender, RoutedEventArgs e)
        {
            switch (App.SourceCodeTheme)
            {
                case ElementTheme.Default:
                    ThemeSC.SelectedItem = "アプリのテーマと同期";
                    break;

                case ElementTheme.Light:
                    ThemeSC.SelectedItem = "ライト";
                    break;

                case ElementTheme.Dark:
                    ThemeSC.SelectedItem = "ダーク";
                    break;
            }

            ThemeSC.Loaded -= ThemeSC_Loaded;
        }

        private void ThemeRC_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ResultThemeLoaded)
            {
                string AddedItem = e.AddedItems[0].ToString();
                switch (AddedItem)
                {
                    case "アプリのテーマと同期": App.ResultTheme = ElementTheme.Default; break;
                    case "ライト": App.ResultTheme = ElementTheme.Light; break;
                    case "ダーク": App.ResultTheme = ElementTheme.Dark; break;
                }
            }
            ResultThemeLoaded = true;
        }

        private void ThemeRC_Loaded(object sender, RoutedEventArgs e)
        {
            switch (App.ResultTheme)
            {
                case ElementTheme.Default:
                    ThemeRC.SelectedItem = "アプリのテーマと同期";
                    break;

                case ElementTheme.Light:
                    ThemeRC.SelectedItem = "ライト";
                    break;

                case ElementTheme.Dark:
                    ThemeRC.SelectedItem = "ダーク";
                    break;
            }

            ThemeRC.Loaded -= ThemeRC_Loaded;
        }

        private void PlatC_Loaded(object sender, RoutedEventArgs e)
        {
            switch (App.ProcesserType)
            {
                case ProcesserType.x86:
                    PlatC.SelectedItem = "x86";
                    break;

                case ProcesserType.x64:
                    PlatC.SelectedItem = "x64";
                    break;
            }

            PlatC.Loaded -= PlatC_Loaded;
        }

        private static void UpdateOtherPage()
        {
            MainPage.RequestUpdate();
        }

        private void PlatC_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PlatCLoaded)
            {
                string AddedItem = e.AddedItems[0].ToString();

                switch (AddedItem)
                {
                    case "x86":
                        App.ProcesserType = ProcesserType.x86;
                        break;

                    case "x64":
                        App.ProcesserType = ProcesserType.x64;
                        break;
                }

                UpdateOtherPage();


            }
            PlatCLoaded = true;
        }

        private void UseCppT_Toggled(object sender, RoutedEventArgs e)
        {
            if (UseCppT.IsOn)
            {
                UseCppText.Text = "オン";
                App.UseCppInCSample = true;
            }
            else
            {
                UseCppText.Text = "オフ";
                App.UseCppInCSample = false;
                App.IsFirstCSampleOpened = false;
            }

            if (UseCppLoaded) UpdateOtherPage();

        }

        private void UseCppT_Loaded(object sender, RoutedEventArgs e)
        {
            UseCppT.IsOn = App.UseCppInCSample;
            UseCppLoaded = true;
            UseCppT.Loaded -= UseCppT_Loaded;
        }

        private void IsShowReturnCodeT_Toggled(object sender, RoutedEventArgs e)
        {
            if(IsShowReturnCodeT.IsOn)
            {
                App.IsShowReturnCode = true;
                IsShowReturnCodeText.Text = "オン";
            }
            else
            {
                App.IsShowReturnCode = false;
                IsShowReturnCodeText.Text = "オフ";
            }
            if (IsShowReturnCodeTLoaded) UpdateOtherPage();
        }

        private void IsShowReturnCodeT_Loaded(object sender, RoutedEventArgs e)
        {
            IsShowReturnCodeT.IsOn = App.IsShowReturnCode;
            IsShowReturnCodeTLoaded = true;
            IsShowReturnCodeT.Loaded -= IsShowReturnCodeT_Loaded;
        }

        private void ControlGrid_Loaded(object sender, RoutedEventArgs e)
        {
#if !DEBUG
            var grid = sender as ControlGrid;

            (grid.Parent as Panel).Children.Remove(grid);

#endif

        }

        private void Expander_Loaded(object sender, RoutedEventArgs e)
        {
            var expander = sender as Expander;
            var textBlock = expander.Header as TextBlock;
            if (App.IsCompact)
            {

                textBlock.Padding = new Thickness(0);
                
            }
            expander.MinHeight = Data.ControlGridHeight;
            expander.Loaded -= Expander_Loaded;
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            var grid = sender as Grid;
            grid.Padding = new Thickness(Data.ControlGridPadding, 0.0, Data.ControlGridPadding, 0.0);
            grid.Height = Data.GalleryControlHeight;

            grid.Loaded -= Grid_Loaded;
        }
    }
}
