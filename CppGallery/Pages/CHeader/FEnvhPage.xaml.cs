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
    public sealed partial class FEnvhPage : Page
    {
        public FEnvhPage()
        {
            this.InitializeComponent();
        }

        private void FsetRoundBox_Loaded(object sender, RoutedEventArgs e)
        {
            FsetRoundBox.SelectedItem = "ç≈Ç‡ãﬂÇ¢ílÇ÷ÇÃä€Çﬂ";
        }

        private void FsetRoundBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FunctionExpander.Execute(sender, new string[] { FsetRoundBox.SelectedIndex.ToString(), FsetRoundIn.Text });
        }

        private void FsetRoundIn_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            FunctionExpander.Execute(sender, new string[] { FsetRoundBox.SelectedIndex.ToString(), FsetRoundIn.Text });
        }
    }
}
