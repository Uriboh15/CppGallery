// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace CppGallery.Pages.UserControls
{
    public sealed partial class OutPuts : UserControl
    {

        public string Text
        {
            get { return OutPut.Text; }
            set { OutPut.Text =  value; }
        }

        //PanelÇÃêF
        private static SolidColorBrush DarkThemeBackground { get; } = new SolidColorBrush(Windows.UI.Color.FromArgb(0xC0, 0x00, 0x00, 0x00));
        private static SolidColorBrush LightThemeBackground { get; } = new SolidColorBrush(Windows.UI.Color.FromArgb(0XFF, 0X0C, 0x0C, 0x0C));
        private static SolidColorBrush BorderColor { get; } = new SolidColorBrush(Windows.UI.Color.FromArgb(0xA0, 0x0D, 0x0D, 0x0D));

        public OutPuts()
        {
            this.InitializeComponent();

            if (App.IsCompact)
            {
                Viewer.Padding = new Thickness(Data.ControlGridPadding, Data.ControlGridPadding - 2.0, Data.ControlGridPadding, Data.ControlGridPadding - 2.0);
                Viewer.MaxHeight = Data.OutputsMaxHeight;

            }
            else
            {
                Viewer.Padding = new Thickness(Data.ControlGridPadding, Data.ControlGridPadding - 5.0, Data.ControlGridPadding, Data.ControlGridPadding - 5.0);
                Viewer.MaxHeight = Data.OutputsMaxHeight;
            }
            Viewer.MinHeight = Data.OutPutsMinHeight;
            Viewer.BorderBrush = BorderColor;
        }

        private void SetThemeColor()
        {
            if (this.ActualTheme == ElementTheme.Dark)
            {
                Viewer.Background = DarkThemeBackground;
            }
            else
            {
                Viewer.Background = LightThemeBackground;
            }
        }

        private void UserControl_ActualThemeChanged(FrameworkElement sender, object args)
        {
            SetThemeColor();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            SetThemeColor();
        }
    }
}
