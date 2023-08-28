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
using System.Runtime.InteropServices;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace CppGallery.Pages.Columns.Column4
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Column4Page : Page
    {

        public Column4Page()
        {
            this.InitializeComponent();
        }

        private void In1_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            Out1.Text = Marshal.PtrToStringAnsi(Column.C4(sender.Text));
        }

        private void Out1_Loaded(object sender, RoutedEventArgs e)
        {
            Out1.Text = Marshal.PtrToStringAnsi(Column.C4("11.2"));
        }
    }
}
