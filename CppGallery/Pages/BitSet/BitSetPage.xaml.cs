// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using CppGallery.Pages.UserControls;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace CppGallery.Pages.BitSet
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BitSetPage : Page
    {
        public BitSetPage()
        {
            this.InitializeComponent();
        }

        private void In_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            FunctionExpander.Execute(sender, sender.Text);
        }

        private void Out_Loaded(object sender, RoutedEventArgs e)
        {
            FunctionExpander.Execute(sender, (sender as TextBox).Text);
        }

        private void OpeXORIn1_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            FunctionExpander.Execute(sender, new string[] { OpeXORIn1.Text, OpeXORIn2.Text });
        }

        private void OpeXOROut_Loaded(object sender, RoutedEventArgs e)
        {
            FunctionExpander.Execute(sender, new string[] { OpeXORIn1.Text, OpeXORIn2.Text });
        }

        private void OpeORIn1_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            FunctionExpander.Execute(sender, new string[] { OpeORIn1.Text, OpeORIn2.Text });
        }

        private void OpeOROut_Loaded(object sender, RoutedEventArgs e)
        {
            FunctionExpander.Execute(sender, new string[] { OpeORIn1.Text, OpeORIn2.Text });
        }

        private void OpeANDIn1_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            FunctionExpander.Execute(sender, new string[] { OpeANDIn1.Text, OpeANDIn2.Text });
        }

        private void OpeANDOut_Loaded(object sender, RoutedEventArgs e)
        {
            FunctionExpander.Execute(sender, new string[] { OpeANDIn1.Text, OpeANDIn2.Text });
        }
    }
}
