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

namespace CppGallery.Pages.Columns.Column14
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Column14Page : Page
    {
        public Column14Page()
        {
            this.InitializeComponent();
        }

        private void DefaultButton_Click(object sender, RoutedEventArgs e)
        {
            var window = new Column14Window(ElementTheme.Default);
            MainPage.ActivateWindow(window);
        }

        private void LightButton_Click(object sender, RoutedEventArgs e)
        {
            var window = new Column14Window(ElementTheme.Light);
            MainPage.ActivateWindow(window);
        }

        private void DarkButton_Click(object sender, RoutedEventArgs e)
        {
            var window = new Column14Window(ElementTheme.Dark);
            MainPage.ActivateWindow(window);
        }
    }
}
