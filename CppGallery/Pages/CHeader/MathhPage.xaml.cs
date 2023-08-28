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
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using CppGallery.Pages.UserControls;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace CppGallery.Pages.CHeader
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MathhPage : Page
    {
        public MathhPage()
        {
            this.InitializeComponent();
        }

        private void FmaOut_Loaded(object sender, RoutedEventArgs e)
        {
            FunctionExpander.Execute(sender, new string[] { FmaIn1.Text, FmaIn2.Text, FmaIn3.Text });
        }

        private void FmaIn_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            FunctionExpander.Execute(sender, new string[] { FmaIn1.Text, FmaIn2.Text, FmaIn3.Text });
        }

        private void FmalIn_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            FunctionExpander.Execute(sender, new string[] { FmalIn1.Text, FmalIn2.Text, FmalIn3.Text });
        }

        private void FmalOut_Loaded(object sender, RoutedEventArgs e)
        {
            FunctionExpander.Execute(sender, new string[] { FmalIn1.Text, FmalIn2.Text, FmalIn3.Text });
        }

        private void FmafOut_Loaded(object sender, RoutedEventArgs e)
        {
            FunctionExpander.Execute(sender, new string[] { FmafIn1.Text, FmafIn2.Text, FmafIn3.Text });
        }

        private void FmafIn_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            FunctionExpander.Execute(sender, new string[] { FmafIn1.Text, FmafIn2.Text, FmafIn3.Text });
        }
    }
}
