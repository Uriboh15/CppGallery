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
    public sealed partial class ItemGrid : UserControl
    {
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(
            "Title",　// Max という名前の……
            typeof(string),　// int 型の CLR プロパティを……
            typeof(ItemGrid), // クラスに登録するやで―
            new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty SentenceProperty = DependencyProperty.Register(
            "Sentence",　// Max という名前の……
            typeof(string),　// int 型の CLR プロパティを……
            typeof(ItemGrid), // クラスに登録するやで―
            new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty IconProperty = DependencyProperty.Register(
           "Icon", // Max という名前の……
           typeof(string), // int 型の CLR プロパティを……
           typeof(ItemGrid), // クラスに登録するやで―
           new PropertyMetadata("\uEA86"));

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
           "Value", // Max という名前の……
           typeof(string), // int 型の CLR プロパティを……
           typeof(ItemGrid), // クラスに登録するやで―
           new PropertyMetadata(string.Empty));


        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public string Sentence
        {
            get { return (string)GetValue(SentenceProperty); }
            set { SetValue(SentenceProperty, value); }
        }

        public string Icon
        {
            get { return (string)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        public string Value
        {
            get { return (string)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public CppVersion TargetMinCppVersion { get; set; } = UserAPI.MinCppVersion;

        public ItemGrid()
        {
            this.InitializeComponent();
            MinHeight = Data.GalleryControlHeight;
            FIcon.Margin = new Thickness((Data.GalleryControlHeight - 24.0) / 2.0, 0, (Data.GalleryControlHeight - 24.0) / 2.0, 0);
            ValueTextBlock.Margin = new Thickness(Data.ControlGridPadding, 0, Data.ControlGridPadding, 0);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if(App.CppVersion < TargetMinCppVersion)
            {
                if(this.Parent is ContentControl contentControl)
                {
                    contentControl.Content = null;
                }
                else if(this.Parent is Panel panel)
                {
                    panel.Children.Remove(this);
                }
                return;
            }
            if (Sentence == string.Empty)
            {
                this.MinHeight = Data.CompactGalleryControlHeight;
                var textblock = sender as TextBlock;
                textblock.Visibility = Visibility.Collapsed;
            }
        }
    }
}
