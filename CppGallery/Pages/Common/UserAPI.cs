using Microsoft.UI.Windowing;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using CppGallery.Pages.UserControls;

namespace CppGallery
{
    public enum HeaderIcon
    {
        Error,
        Event,
        Function,
        Literal,
        Macro,
        Object,
        Operator,
    }

    internal static class UserAPI
    {
        [DllImport("Shcore.dll", SetLastError = true)]
        internal static extern int GetDpiForMonitor(IntPtr hmonitor, Monitor_DPI_Type dpiType, out uint dpiX, out uint dpiY);

        public static CppVersion MaxCppVersion => CppVersion.Cpp23;
        public static CppVersion MinCppVersion => CppVersion.Cpp14;
        public static CVersion MaxCVersion => CVersion.C23;
        public static CVersion MinCVersion => CVersion.C11;

        public static void ToCompactForResources(Page page, NavigationView navView, FrameworkElement frame)
        {
            if (App.IsCompact)
            {
                navView.OpenPaneLength = 300;
                navView.ExpandedModeThresholdWidth = 800;
                page.Resources["NavigationViewContentMargin"] = new Thickness(0);
                page.Resources["NavigationViewHeaderMargin"] = new Thickness(24, 16, 0, 0);
                page.Resources["BreadcrumbBarItemThemeFontSize"] = 24.0;
                if (App.Win10)
                {
                    page.Resources["BreadcrumbBarChevronFontSize"] = 9.5;
                }
                else
                {
                    page.Resources["BreadcrumbBarChevronFontSize"] = 14.0;
                }

                page.Resources["BreadcrumbBarChevronPadding"] = new Thickness(8, 0, 10, 0);
                
                if(frame != null)
                {
                    frame.Margin = new Thickness(0, Data.CompactOuterPanelPadding, 0, 0);
                }
            }
            else
            {
                page.Resources["NavigationViewHeaderMargin"] = new Thickness(Data.NormalOuterPanelPadding, Data.NormalOuterPanelPadding - 14, 0, 0);
                if (frame != null)
                {
                    frame.Margin = new Thickness(0, Data.NormalOuterPanelPadding - 10, 0, 0);
                }
                    
                if (App.Win10)
                {
                    page.Resources["BreadcrumbBarChevronFontSize"] = 11.0;
                }
            }
        }

        public static string GetExeName(string exeName, bool isCSample)
        {
            if (!isCSample || App.UseCppInCSample) exeName += "-Cpp" + ((int)App.CppVersion).ToString();
            else exeName += "-C" + ((int)App.CVersion).ToString();
            if (App.Compiler == Compiler.GCC) exeName += "-GCC";
            if (App.ProcesserType == ProcesserType.x86) exeName += "-x86";

            return exeName;
        }

        public static string GetExeName(bool isCSample)
        {
            return GetExeName("/Sample", isCSample);
        }


        public static async Task GetMacroValue(UIElementCollection collection, string folder, string exeName, int offset)
        {

            await CoAPI.ExecuteAsync(Data.DefaultSampleExePath + GetExeName(exeName, true) + ' ' + folder + " macro");
            

            using (var sr = new StreamReader(CoAPI.GetOutputFileName()))
            {
                var output = sr.ReadToEnd().Split(Environment.NewLine);

                int i = offset;
                int deleted = 0;
                while (i < collection.Count)
                {
                    if (collection[i] is ItemGrid itemGrid)
                    {
                        if (itemGrid.Title == output[i - offset + deleted])
                        {
                            collection.Remove(itemGrid);
                            ++deleted;
                            continue;
                        }

                        itemGrid.Value = output[i - offset + deleted];
                        itemGrid.Icon = HeaderIcon.Macro;
                    }
                    else
                    {
                        --deleted;
                    }
                    

                    ++i;
                }
            }

            //throw new Exception();
        }

        public static async Task GetMacroValue(UIElementCollection collection, string folder)
        {
            await GetMacroValue(collection, folder, "/Sample", 1);
        }

        public static async Task GetMacroValue(UIElementCollection collection, string folder, int offset)
        {
            await GetMacroValue(collection, folder, "/Sample", offset);
        }

        public static async Task GetMacroValue(UIElementCollection collection, string folder, string exeName)
        {
            await GetMacroValue(collection, folder, exeName, 1);
        }

        public static MenuFlyout GetMenuFlyout(string newPageTag, UIElement element)
        {
            MenuFlyout menuFlyout = new MenuFlyout();
            MenuFlyoutItem item1 = new MenuFlyoutItem { Text = "新しいタブで開く", Icon = new FontIcon { Glyph = "\uE72D" } };

            item1.Click += (s, e) =>
            {
                MainWindow.GetParentMainWindow(element).AddTab(newPageTag);
            };

            MenuFlyoutItem item2 = new MenuFlyoutItem { Text = "新しいウィンドウで開く", Icon = new FontIcon { Glyph = "\uE8A7" } };

            item2.Click += (s, e) =>
            {
                var window = new MainWindow(false);

                window.AddTab(newPageTag);

                window.Activate();
            };

            menuFlyout.Items.Add(item1);
            menuFlyout.Items.Add(item2);

            return menuFlyout;
        }

        public static string GetIconSymbol(HeaderIcon symbol)
        {
            return symbol switch
            {
                HeaderIcon.Error => "\uEB90",
                HeaderIcon.Event => "\uEA86",
                HeaderIcon.Function => "\uF158",
                HeaderIcon.Literal => "\uEA3A",
                HeaderIcon.Macro => "\uE71A",
                HeaderIcon.Object => "\uEA86",
                HeaderIcon.Operator => "\uE710",
                _ => "\uF158",
            };
        }

        public static double GetScaleAdjustment(IntPtr hWnd)
        {
            WindowId wndId = Win32Interop.GetWindowIdFromWindow(hWnd);
            DisplayArea displayArea = DisplayArea.GetFromWindowId(wndId, DisplayAreaFallback.Primary);
            IntPtr hMonitor = Win32Interop.GetMonitorFromDisplayId(displayArea.DisplayId);

            // Get DPI.
            int result = GetDpiForMonitor(hMonitor, Monitor_DPI_Type.MDT_Default, out uint dpiX, out uint _);
            if (result != 0)
            {
                throw new Exception("Could not get DPI for monitor.");
            }

            uint scaleFactorPercent = (uint)(((long)dpiX * 100 + (96 >> 1)) / 96);
            return scaleFactorPercent / 100.0;
        }
    }
}
