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
    public partial class ControlGrid : UserControl
    {
        public static readonly DependencyProperty MessageProperty = DependencyProperty.Register(
            "Message",　// Max という名前の……
            typeof(string),　// int 型の CLR プロパティを……
            typeof(ControlGrid), // クラスに登録するやで―
            new PropertyMetadata(string.Empty));

        public string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }

        private bool _isCardStyle { get; set; }

        private static CornerRadius Curve { get; } = new CornerRadius(4);
        private static CornerRadius Kakukaku { get; } = new CornerRadius(0);
        private static Thickness CurveT { get; } = new Thickness(1);
        private static Thickness KakukakuT { get; } = new Thickness(0);

        public bool IsCardStyle
        {
            get => _isCardStyle;
            set
            {
                if (value)
                {
                    Panel.CornerRadius = Kakukaku;
                    Panel.BorderThickness = KakukakuT;
                }
                else
                {
                    Panel.CornerRadius = Curve;
                    Panel.BorderThickness = CurveT;
                }
            }
        }

        public new UIElement Content { get; set; }

        public ControlGrid()
        {
            this.Loaded += ControlGrid_Loaded;
            
            Height = Data.ControlGridHeight;
            
        }

        private void ControlGrid_Loaded(object sender, RoutedEventArgs e)
        {
            this.Loaded -= ControlGrid_Loaded;
            this.InitializeComponent();
            Panel.Padding = new Thickness(Data.ControlGridPadding, 0.0, Data.ControlGridPadding, 0.0);
            MesText.Margin = new Thickness(0, 0, Data.ControlGridPadding, 0.0);
        }
    }
}
