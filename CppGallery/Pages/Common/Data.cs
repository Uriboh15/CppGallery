using Microsoft.UI.Xaml.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CppGallery
{
    public static class Data
    {
        //OuterPanel の Padding
        public static double NormalOuterPanelPadding => 40.0;
        public static double CompactOuterPanelPadding => 24.0;
        public static double OuterPanelPadding
        {
            get 
            {
                if (App.IsCompact)
                {
                    return CompactOuterPanelPadding;
                }
                else
                {
                    return NormalOuterPanelPadding;
                }
            }
        }

        //OuterPanel の Spacing
        public static double NormalOuterPanelSpacing => NormalOuterPanelPadding;
        public static double CompactOuterPanelSpacing => CompactOuterPanelPadding;
        public static double OuterPanelSpacing => OuterPanelPadding;

        //ControlGrid の Padding
        public static double NormalControlGridPadding => 24.0;
        public static double CompactControlGridPadding => 16.0;
        public static double ControlGridPadding
        {
            get
            {
                if (App.IsCompact)
                {
                    return CompactControlGridPadding;
                }
                else
                {
                    return NormalControlGridPadding;
                }
            }
        }

        //FunctionExpander の Padding
        public static double NormalFunctionExpanderPadding => 16.0;
        public static double CompactFunctionExpanderPadding => 8.0;
        public static double FunctionExpanderPadding
        {
            get
            {
                if (App.IsCompact)
                {
                    return CompactFunctionExpanderPadding;
                }
                else
                {
                    return NormalFunctionExpanderPadding;
                }
            }
        }

        //CppGarrery のコントロールの高さ
        public static double NormalGalleryControlHeight => 68.0;
        public static double CompactGalleryControlHeight => 60.0;
        public static double GalleryControlHeight
        {
            get
            {
                if (App.IsCompact)
                {
                    return CompactGalleryControlHeight;
                }
                else
                {
                    return NormalGalleryControlHeight;
                }
            }
        }

        //関数タイトルのテキストサイズ
        public static double NormalFunctionTitleTextSize => 18.0;
        public static double CompactFunctionTitleTextSize => 16.0;
        public static double FunctionTitleTextSize
        {
            get
            {
                if (App.IsCompact)
                {
                    return CompactFunctionTitleTextSize;
                }
                else
                {
                    return NormalFunctionTitleTextSize;
                }
            }
        }

        //タイトルのテキストサイズ
        public static double NormalTitleTextSize => 20.0;
        public static double CompactTitleTextSize => 18.0;
        public static double TitleTextSize
        {
            get
            {
                if (App.IsCompact)
                {
                    return CompactTitleTextSize;
                }
                else
                {
                    return NormalTitleTextSize;
                }
            }
        }

        //ResultPanel の Spacing
        public static double NormalResultPanelSpacing => 4.0;
        public static double CompactResultPanelSpacing => 0.0;
        public static double ResultPanelSpacing
        {
            get
            {
                if (App.IsCompact)
                {
                    return CompactResultPanelSpacing;
                }
                else
                {
                    return NormalResultPanelSpacing;
                }
            }
        }


        //サンプルがあるフォルダ
        public static string DefaultSamplePath => @"../data/";

        //サンプルの実行ファイルがあるフォルダ
        public static string DefaultSampleExePath => @"../sample/";

        //検索データのフォルダ
        public static string DefaultSearchingTextPath => @"../search/";

        public static double NormalNavigationViewHeaderFontSize => 28.0;
        public static double CompactNavigationViewHeaderFontSize => 24.0;
        public static double NavigationViewHeaderFontSize => App.IsCompact ? CompactNavigationViewHeaderFontSize : NormalNavigationViewHeaderFontSize;

        //ControlGridの高さ
        public static double NormalControlGridHeight => 60.0;
        public static double CompactControlGridHeight => 48.0;
        public static double ControlGridHeight => App.IsCompact ? CompactControlGridHeight : NormalControlGridHeight;

        //OutPuts の高さの最大値
        public static double NormalOutputsMaxHeight => 512.0;
        public static double CompactOutputsMaxHeight => 384.0;
        public static double OutputsMaxHeight => App.IsCompact ? CompactOutputsMaxHeight : NormalOutputsMaxHeight;

        //OutPutsの最小の高さ
        public static double NormalOutPutsMinHeight => NormalControlGridHeight;
        public static double CompactOutPutsMinHeight => CompactControlGridHeight;
        public static double OutPutsMinHeight => ControlGridHeight;

        //MainWindowの背景
        private static SolidColorBrush _lightBackBrush = new SolidColorBrush(Windows.UI.Color.FromArgb(0xFF, 0xE8, 0xE8, 0xE8));
        private static SolidColorBrush _darkBackBrush = new SolidColorBrush(Windows.UI.Color.FromArgb(0xFF, 0x18, 0x18, 0x18));
        private static SolidColorBrush _lightDeactivatedBackBrush = new SolidColorBrush(Windows.UI.Color.FromArgb(0xFF, 0xF8, 0xF8, 0xF8));
        private static SolidColorBrush _darkDeactivatedBackBrush = new SolidColorBrush(Windows.UI.Color.FromArgb(0xFF, 0x2B, 0x2B, 0x2B));

        public static SolidColorBrush LightBackBrush => _lightBackBrush;
        public static SolidColorBrush DarkBackBrush => _darkBackBrush;
        public static SolidColorBrush LightDeactivatedBackBrush => _lightDeactivatedBackBrush;
        public static SolidColorBrush DarkDeactivatedBackBrush => _darkDeactivatedBackBrush;

    }
}
