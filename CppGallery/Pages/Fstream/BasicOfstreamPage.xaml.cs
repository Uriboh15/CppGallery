// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

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
using WinRT.Interop;
using CppGallery.Pages.UserControls;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace CppGallery.Pages.Fstream
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BasicOfstreamPage : Page
    {
        public BasicOfstreamPage()
        {
            this.InitializeComponent();
        }

        bool BasicOFStreamWorking = false;

        private async void BasicOfstreamButton_Click(object sender, RoutedEventArgs e)
        {
            if (BasicOFStreamWorking) return;
            BasicOFStreamWorking = true;
            var savepicker = new Windows.Storage.Pickers.FileSavePicker();
            savepicker.SuggestedFileName = "sample";

            savepicker.FileTypeChoices.Add("�e�L�X�g�t�@�C��", new List<string>() { ".txt" });

            InitializeWithWindow.Initialize(savepicker, WindowNative.GetWindowHandle(MainWindow.GetParentMainWindow(this)));

            Windows.Storage.StorageFile savefile = await savepicker.PickSaveFileAsync();
            if (savefile != null)
            {
                FunctionExpander.Execute(sender, new string[] { savefile.Path, BasicOfstreamIn.Text });
            }
            BasicOFStreamWorking = false;
        }
    }
}
