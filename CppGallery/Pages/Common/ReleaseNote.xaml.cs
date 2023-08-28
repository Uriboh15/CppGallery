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

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace CppGallery.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ReleaseNote : Page
    {
        private static ReleaseNote Handle = null;

        public static ReleaseNote GetHandle()
        {
            if(Handle == null)
            {
                Handle = new ReleaseNote();
            }
            return Handle;
        }

        private ReleaseNote()
        {
            this.InitializeComponent();
        }

        private void CustomPanel_Loaded(object sender, RoutedEventArgs e)
        {
            var panel = sender as StackPanel;
            foreach (var control in panel.Children)
            {
                if (control is Expander expander)
                {
                    expander.MinHeight = Data.GalleryControlHeight;
                    expander.Padding = new Thickness(Data.NormalControlGridPadding, Data.NormalControlGridPadding - 5.0, Data.NormalControlGridPadding, Data.NormalControlGridPadding - 5.0);
                }

            }

            OutPanel.Loaded -= CustomPanel_Loaded;
        }
    }
}
