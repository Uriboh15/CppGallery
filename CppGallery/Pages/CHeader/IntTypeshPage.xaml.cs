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
using CppGallery.Pages.UserControls;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace CppGallery.Pages.CHeader
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class IntTypeshPage : Page
    {
        public IntTypeshPage()
        {
            this.InitializeComponent();
        }

        private static string GetRadix(string value)
        {
            if (value == "0 (Ž©“®)") return "0";
            return value;
        }

        private void StrtoImaxKisu_Loaded(object sender, RoutedEventArgs e)
        {
            (sender as ComboBox).SelectedItem = "8";
        }

        private void StrtoImaxKisu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FunctionExpander.Execute(sender, new string[] { GetRadix(StrtoImaxKisu.SelectedItem as string), StrtoImaxIn.Text });
        }

        

        private void StrtoImaxIn_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            FunctionExpander.Execute(sender, new string[] { GetRadix(StrtoImaxKisu.SelectedItem as string), StrtoImaxIn.Text });
        }

        private void StrtoUmaxKisu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FunctionExpander.Execute(sender, new string[] { GetRadix(StrtoUmaxKisu.SelectedItem as string), StrtoUmaxIn.Text });
        }

        private void StrtoUmaxIn_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            FunctionExpander.Execute(sender, new string[] { GetRadix(StrtoUmaxKisu.SelectedItem as string), StrtoUmaxIn.Text });
        }

        private void WcstoImaxKisu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FunctionExpander.Execute(sender, new string[] { GetRadix(WcstoImaxKisu.SelectedItem as string), WcstoImaxIn.Text });
        }

        private void WcstoImaxIn_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            FunctionExpander.Execute(sender, new string[] { GetRadix(WcstoImaxKisu.SelectedItem as string), WcstoImaxIn.Text });
        }

        private void WcstoUmaxKisu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FunctionExpander.Execute(sender, new string[] { GetRadix(WcstoUmaxKisu.SelectedItem as string), WcstoUmaxIn.Text });
        }

        private void WcstoUmaxIn_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            FunctionExpander.Execute(sender, new string[] { GetRadix(WcstoUmaxKisu.SelectedItem as string), WcstoUmaxIn.Text });
        }

        private void ImaxDivOut_Loaded(object sender, RoutedEventArgs e)
        {
            FunctionExpander.Execute(sender, new string[] { ImaxDivIn1.Text, ImaxDivIn2.Text });
        }

        private void ImaxDivIn1_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            FunctionExpander.Execute(sender, new string[] { ImaxDivIn1.Text, ImaxDivIn2.Text });
        }
    }
}
