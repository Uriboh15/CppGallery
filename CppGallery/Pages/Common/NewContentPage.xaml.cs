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
using System.Diagnostics;
using Microsoft.UI.Xaml.Media.Animation;
using System.Runtime.InteropServices;
using WinRT.Interop;
using System.Threading.Tasks;
using System.Threading;
using CppGallery.Pages.UserControls;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace CppGallery.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NewContentPage : Page
    {

        public NewContentPage()
        {
            this.InitializeComponent();
        }

        private void Panel_Loaded(object sender, RoutedEventArgs e)
        {
            if (Panel.Children.Count != 0)
            {
                ErrorText.Visibility = Visibility.Collapsed;
            }
        }


        bool SearchPathWorking = false;

        private async void SearchPathButton_Click(object sender, RoutedEventArgs e)
        {
            if (SearchPathWorking) return;
            SearchPathWorking = true;

            Windows.Storage.Pickers.FolderPicker Picker = new Windows.Storage.Pickers.FolderPicker();
            Picker.FileTypeFilter.Add("*");
            InitializeWithWindow.Initialize(Picker, WindowNative.GetWindowHandle(MainWindow.Handle));

            Windows.Storage.StorageFolder folder = await Picker.PickSingleFolderAsync();

            if (folder != null)
            {
                SearchPathTextBlock.Text = folder.Path;
            }

            SearchPathWorking = false;
        }

        private void GetEnvironmentVariableStartButton_Click(object sender, RoutedEventArgs e)
        {
            FunctionExpander.Execute(sender, new string[] { SearchPathTextBlock.Text, SearchPathFileNameTextBox.Text, SearchPathExtensionTextBox.Text });
        }
    }
}
