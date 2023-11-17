using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CppGallery.Pages.UserControls
{
    public class WinRTCodeButton : Win32CodeButton
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
            "BatteryStatusChanged_revoker",
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
            "EnergySaverStatusChanged_revoker",
            "event_token",
            "FlyoutBase",
            "FileOpenPicker",
            "FolderPicker",
            "FontFamily",
            "FontStretch",
            "FontWeight",
            "FontWeights",
            "FrameworkElement",
            "Grid",
            "guid",
            "HResult",
            "hstring",
            "IAsyncAction",
            "IInitializeWithWindow",
            "IInspectable",
            "Inline",
            "InstalledDesktopApp",
            "IVectorChangedEventArgs",
            "IUnknown",
            "IWindowNative",
            "JapanesePhoneme",
            "JapanesePhoneticAnalyzer",
            "LanguageFont",
            "LanguageFontGroup",
            "LineBreak",
            "MainPage",
            "MainWindow",
            "MapChanged_revoker",
            "MulticastDelegate",
            "NumeralSystemIdentifiers",
            "NumeralSystemTranslator",
            "Point",
            "PowerManager",
            "PowerSupplyStatusChanged_revoker",
            "Rect",
            "Rectangle",
            "RemainingChargePercentChanged_revoker",
            "RemainingDischargeTimeChanged_revoker",
            "ReportUpdated_revoker",
            "RoutedEvent",
            "RoutedEventArgs",
            "Run",
            "SelectionChangedEventArgs",
            "SolidColorBrush",
            "StackPanel",
            "StorageFile",
            "StorageFolder",
            "TextBlock",
            "TextPointer",
            "TimeSpan",
            "VectorChanged_revoker",
            "XamlReader",
            "XamlRoot",
        };

        private static List<string> KeywordsClassTemplate { get; } = new List<string>()
        {
            "array_view",
            "com_array",
            "EventHandler",
            "IAsyncOperation",
            "IIterable",
            "IIterator",
            "IKeyValuePair",
            "IMap",
            "IMapChangedEventArgs",
            "IMapView",
            "IObservableMap",
            "IObservableVector",
            "IPropertyValue",
            "IReference",
            "IVector",
            
            "IVectorView",
            "MapChangedEventHandler",
            "TypedEventHandler",
            "VectorChangedEventHandler",
        };

        private static List<string> KeywordsEnum { get; } = new List<string>()
        {
            "AsyncStatus",
            "BatteryStatus",
            "CollectionChange",
            "ContentDialogButton",
            "CoreDispatcherPriority",
            "ElementTheme",
            "EnergySaverStatus",
            "HorizontalAlignment",
            "PickerLocationId",
            "PickerViewMode",
            "PowerSupplyStatus",
            "PropertyType",
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
            "Core",
            "Devices",
            "Display",
            "Documents",
            "Enumeration",
            "Fonts",
            "Foundation",
            "Globalization",
            "implementation",
            "Inventory",
            "Markup",
            "Media",
            "Microsoft",
            "NumberFormatting",
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