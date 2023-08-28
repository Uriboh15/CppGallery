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

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace CppGallery.Pages.UserControls
{
    public sealed partial class ColumnBlockPane : UserControl
    {
        public static readonly DependencyProperty LeftProperty = DependencyProperty.Register(
           "Left", // Max という名前の……
           typeof(string), // int 型の CLR プロパティを……
           typeof(ColumnBlockPane), // クラスに登録するやで―
           new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty RightProperty = DependencyProperty.Register(
           "Right", // Max という名前の……
           typeof(string), // int 型の CLR プロパティを……
           typeof(ColumnBlockPane), // クラスに登録するやで―
           new PropertyMetadata(string.Empty));

        public string Left
        {
            get { return (string)GetValue(LeftProperty); }
            set { SetValue(LeftProperty, value); }
        }

        public string Right
        {
            get { return (string)GetValue(RightProperty); }
            set { SetValue(RightProperty, value); }
        }


        public ColumnBlockPane()
        {
            this.InitializeComponent();
        }
    }
}
