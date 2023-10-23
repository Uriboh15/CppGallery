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
            KeyClassTemplate.Add(KeywordsClassTemplate);
        }

        private static List<string> KeywordsGreen { get; } = new List<string>()
        {
            "auto_revoke_t",
            "Battery",
            "BatteryReport",
            "Brush",
            "Color",
            "ColorChangedEventArgs",
            "ColorHelper",
            "ColorPicker",
            "Colors",
            "ComboBox",
            "ContentDialog",
            "ContentDialogOpenedEventArgs",
            "CoreDispatcher",
            "DependencyObject",
            "DependencyProperty",
            "DeviceInformation",
            "DeviceInformationCollection",
            "DisplayRequest",
            "event_token",
            "FlyoutBase",
            "FileOpenPicker",
            "FolderPicker",
            "FontWeight",
            "FontWeights",
            "FrameworkElement",
            "Grid",
            "hstring",
            "IAsyncAction",
            "IInitializeWithWindow",
            "IInspectable",
            "InstalledDesktopApp",
            "IUnknown",
            "IWindowNative",
            "MainPage",
            "MainWindow",
            "Rectangle",
            "ReportUpdated_revoker",
            "RoutedEvent",
            "RoutedEventArgs",
            "SelectionChangedEventArgs",
            "SolidColorBrush",
            "StackPanel",
            "StorageFile",
            "StorageFolder",
            "TextBlock",
            "XamlReader",
        };

        private static List<string> KeywordsClassTemplate { get; } = new List<string>()
        {
            "array_view",
            "IAsyncOperation",
            "IIterable",
            "IIterator",
            "IKeyValuePair",
            "IMap",
            "IMapView",
            "IReference",
            "IVector",
            "IVectorView",
            "TypedEventHandler",
        };

        private static List<string> KeywordsEnum { get; } = new List<string>()
        {
            "BatteryStatus",
            "ContentDialogButton",
            "ElementTheme",
            "HorizontalAlignment",
            "PickerLocationId",
            "PickerViewMode",
            "TitleBarHeightOption",
            "VerticalAlignment",
        };

        private static List<string> KeywordsYellow { get; } = new List<string>()
        {
            "Battery_ReportUpdated",
        };

        private static List<string> KeywordsPurple { get; } = new List<string>()
        {
            "co_await",
        };

        private static List<string> KeywordsGlobal { get; } = new List<string>()
        {
            "Collections",
            "Controls",
            "Devices",
            "Display",
            "Enumeration",
            "Foundation",
            "implementation",
            "Inventory",
            "Markup",
            "Media",
            "Microsoft",
            "Pickers",
            "Power",
            "Shapes",
            "Storage",
            "System",
            "Text",
            "UI",
            "Windows",
            "winrt",
            "Xaml",
        };
    }
}