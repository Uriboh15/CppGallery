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
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using WinRT.Interop;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace CppGallery.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class UpdatedPage : Page
    {

        public UpdatedPage()
        {
            this.InitializeComponent();
        }

        private void Panel_Loaded(object sender, RoutedEventArgs e)
        {
            if(Panel.Children.Count != 0)
            {
                ErrorText.Visibility = Visibility.Collapsed;
            }
        }

        
    }
}
