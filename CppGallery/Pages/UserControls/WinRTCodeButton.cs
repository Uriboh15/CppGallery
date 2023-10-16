using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CppGallery.Pages.UserControls
{
    public class WinRTCodeButton : CodeButton
    {
        public WinRTCodeButton()
        {
            Lan = "C++/WinRT";
            KeyGreen.Add(KeywordsGreen);
            KeyFunc.Add(KeywordsYellow);
            KeyPurple.Add(KeywordsPurple);
            KeyGlobal.Add(KeywordsGlobal);
            KeyEnum.Add(KeywordsEnum);
        }

        private static List<string> KeywordsGreen { get; } = new List<string>()
        {
            "Brush",
            "ColorChangedEventArgs",
            "ColorHelper",
            "ColorPicker",
            "ComboBox",
            "ContentDialog",
            "ContentDialogOpenedEventArgs",
            "CoreDispatcher",
            "DependencyObject",
            "DependencyProperty",
            "FlyoutBase",
            "FontWeight",
            "FontWeights",
            "FrameworkElement",
            
            "hstring",
            "IAsyncAction",
            "IInspectable",
            "IUnknown",
            "IWindowNative",
            "MainPage",
            "MainWindow",
            "RoutedEvent",
            "RoutedEventArgs",
            "SelectionChangedEventArgs",
            "SolidColorBrush",
            "StackPanel",
            "TextBlock",
        };

        private static List<string> KeywordsEnum { get; } = new List<string>()
        {
            "ContentDialogButton",
            "ElementTheme",
            "TitleBarHeightOption",
        };

        private static List<string> KeywordsYellow { get; } = new List<string>()
        {
            "check_bool",
            "GetProcAddress",
            "get_WindowHandle",
            "InitializeComponent",
            "SetTitleBar",
            "try_as",
            "unbox_value",
        };

        private static List<string> KeywordsPurple { get; } = new List<string>()
        {
            "co_await",
        };

        private static List<string> KeywordsGlobal { get; } = new List<string>()
        {
            "Controls",
            "Foundation",
            "implementation",
            "Media",
            "Microsoft",
            "Text",
            "UI",
            "Windows",
            "winrt",
            "Xaml",
        };
    }
}