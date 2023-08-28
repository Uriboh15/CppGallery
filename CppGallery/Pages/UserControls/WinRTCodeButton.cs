using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CppGallery.Pages.UserControls
{
    public class WinRTCodeButton : CodeButton
    {
        public WinRTCodeButton() : base()
        {
            Lan = "C++/WinRT";
            KeyGreen.Add(KeywordsGreen);
            KeyFunc.Add(KeywordsYellow);
            KeyDefine.Add(KeywordsDefine);
            KeyGlobal.Add(KeywordsGlobal);
        }

        private static List<string> KeywordsGreen = new List<string>()
        {
            "BOOL",
            "ColorHelper",
            "DWORD",
            "DwmSetWindowAttributePtr",
            "ElementTheme",
            "FrameworkElement",
            "HMODULE",
            "HRESULT",
            "hstring",
            "HWND",
            "IInspectable",
            "ItemGrid",
            "ItemGridT",
            "IWindowNative",
            "LPCVOID",
            "MainWindow",
            "NTSTATUS",
            "RoutedEventArgs",
            "SelectionChangedEventArgs",
            "TitleBarHeightOption",
        };

        private static List<string> KeywordsYellow = new List<string>()
        {
            "check_bool",
                "ContentFrame",
                "ContentFrame_ActualThemeChanged",
                "ContentFrame_Loaded",
                "GetProcAddress",
                "get_WindowHandle",
                "InitializeComponent",
                "SetTitleBar",
                "try_as",
                "unbox_value",
                "LPCVOID",
        };

        private static List<string> KeywordsDefine = new List<string>()
        {
            "DWMWA_USE_IMMERSIVE_DARK_MODE",
                "FALSE",
                "LoadLibrary",
                "TRUE",
                "WINAPI",
                "_In_reads_bytes_",
        };

        private static List<string> KeywordsGlobal = new List<string>()
        {
            "Foundation",
            "implementation",
            "factory_implementation",
                "Microsoft",
                "UI",
                "Windows",
                "winrt",
                "Xaml",
        };
    }
}