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
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace CppGallery.Pages.CharConv
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CharConvPage : Page
    {
        public CharConvPage()
        {
            this.InitializeComponent();
        }

        private void FromCharsKisu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FunctionExpander.Execute(sender, new string[] { FromCharsKisu.SelectedItem as string, FromCharsIn.Text });
        }

        private void FromCharsKisu_Loaded(object sender, RoutedEventArgs e)
        {
            FromCharsKisu.SelectedItem = "10";
        }

        private void FromCharsIn_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            FunctionExpander.Execute(sender, new string[] { FromCharsKisu.SelectedItem as string, FromCharsIn.Text  });
        }
    }
}
