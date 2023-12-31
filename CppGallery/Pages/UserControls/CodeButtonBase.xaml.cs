// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

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
using Windows.ApplicationModel.DataTransfer;
using System.Xml.Linq;
using Windows.Devices.Enumeration;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace CppGallery.Pages.UserControls
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// 

    public abstract partial class CodeButtonBase : UserControl
    {
        public static readonly DependencyProperty PathProperty = DependencyProperty.Register(
            "Path",　// Max という名前の……
            typeof(string),　// int 型の CLR プロパティを……
            typeof(CodeButtonBase), // クラスに登録するやで―
            new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty LanProperty = DependencyProperty.Register(
            "Lan",　// Max という名前の……
            typeof(string),　// int 型の CLR プロパティを……
            typeof(CodeButtonBase), // クラスに登録するやで―
            new PropertyMetadata("C++"));

        public static readonly DependencyProperty FileNameProperty = DependencyProperty.Register(
            "FileName",　// Max という名前の……
            typeof(string),　// int 型の CLR プロパティを……
            typeof(CodeButtonBase), // クラスに登録するやで―
            new PropertyMetadata("サンプルコード"));

        public static readonly DependencyProperty SaveButtonVisibiltyProperty = DependencyProperty.Register(
            "SaveButtonVisibilty",　// Max という名前の……
            typeof(Visibility),　// int 型の CLR プロパティを……
            typeof(CodeButtonBase), // クラスに登録するやで―
            new PropertyMetadata(Visibility.Visible));

        public string Path
        {
            get { return (string)GetValue(PathProperty); }
            set { SetValue(PathProperty, value); }
        }

        public string Lan
        {
            get { return (string)GetValue(LanProperty); }
            set { SetValue(LanProperty, value); }
        }

        public string FileName
        {
            get { return (string)GetValue(FileNameProperty); }
            set { SetValue(FileNameProperty, value); }
        }

        public Visibility SaveButtonVisibilty
        {
            get { return (Visibility)GetValue(SaveButtonVisibiltyProperty); }
            set { SetValue(SaveButtonVisibiltyProperty, value); }
        }

        protected TextBlock Code => CodeText;

        private static DataPackage dataPackage { get; } = new DataPackage();

        //Panelの色
        private static SolidColorBrush DarkThemeBackground { get; } = new SolidColorBrush(Windows.UI.Color.FromArgb(0x60, 0x05, 0x05, 0x05));
        private static SolidColorBrush LightThemeBackground { get; } = new SolidColorBrush(Windows.UI.Color.FromArgb(0XFF, 0X20, 0x20, 0x20));        
        private static SolidColorBrush BorderColor { get; } = new SolidColorBrush(Windows.UI.Color.FromArgb(0xA0, 0x0D, 0x0D, 0x0D));

        public CodeButtonBase()
        {
            this.InitializeComponent();

            Panel.BorderBrush = BorderColor;

            if (App.IsCompact)
            {
                Code.FontFamily = new FontFamily("MS Gothic");

                Panel.Padding = new Thickness(16.0, 10.0, 16.0, 14.0);
                ButtonCopy.Margin = new Thickness(0.0, 6.0, 0.0, 0.0);
            }
        }

        private void ButtonCopy_Click(object sender, RoutedEventArgs e)
        {
            dataPackage.RequestedOperation = DataPackageOperation.Copy;
            dataPackage.SetText(Code.Text);
            Clipboard.SetContent(dataPackage);
        }

        protected abstract void LoadFile();

        private void TitleText_Loaded(object sender, RoutedEventArgs e)
        {
            if (TitleText.Text == string.Empty)
            {
                (TitleText.Parent as StackPanel).Children.Remove(TitleText);
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            SetThemeColor();
            LoadFile();

            Loaded -= UserControl_Loaded;
        }

        private void SetThemeColor()
        {
            if (this.ActualTheme == ElementTheme.Dark)
            {
                Panel.Background = DarkThemeBackground;
                
            }
            else
            {
                Panel.Background = LightThemeBackground;
            }
        }

        private void UserControl_ActualThemeChanged(FrameworkElement sender, object args)
        {
            SetThemeColor();
        }
    }
}
