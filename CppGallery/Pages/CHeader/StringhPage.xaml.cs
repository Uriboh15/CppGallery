// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using CppGallery.Pages.UserControls;
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
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using WinRT.Interop;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace CppGallery.Pages.CHeader
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class StringhPage : Page
    {
        public StringhPage()
        {
            this.InitializeComponent();
        }

        private async void StrtokButton_Click(object sender, RoutedEventArgs e)
        {
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.FileTypeFilter.Add(".csv");

            InitializeWithWindow.Initialize(picker, WindowNative.GetWindowHandle(MainWindow.GetParentMainWindow(this)));

            Windows.Storage.StorageFile savefile = await picker.PickSingleFileAsync();
            if (savefile != null)
            {
                FunctionExpander.Execute(sender, savefile.Path);
            }
        }

        private void StrCmpOut_Loaded(object sender, RoutedEventArgs e)
        {
            FunctionExpander.Execute(sender, new string[] { StrCmpIn1.Text, StrCmpIn2.Text });
        }

        private void StrCmpIn1_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            FunctionExpander.Execute(sender, new string[] { StrCmpIn1.Text, StrCmpIn2.Text });
        }
    }
}
