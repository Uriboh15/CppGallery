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
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TitleBarSettingsButton : Button
    {
        public TitleBarSettingsButton()
        {
            this.InitializeComponent();
        }

        private void Button_LostFocus(object sender, RoutedEventArgs e)
        {
            Background = null;
        }

        private void Button_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            Background = Resources["AppBarButtonBackgroundPressed"] as Brush;
        }

        private void Button_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            Background = null;
        }

        private void Button_GotFocus(object sender, RoutedEventArgs e)
        {
            Background = Resources["AppBarButtonBackgroundPointerOver"] as Brush;
        }

        private void Button_PointerCaptureLost(object sender, PointerRoutedEventArgs e)
        {
            Background = null;
        }
    }
}
