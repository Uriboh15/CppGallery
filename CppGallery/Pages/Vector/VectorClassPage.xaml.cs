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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace CppGallery.Pages.Vector
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class VectorClassPage : Page
    {
        public VectorClassPage()
        {
            this.InitializeComponent();
            Func.VectorClassClear();
        }

        private void CapacityButton1_Click(object sender, RoutedEventArgs e)
        {
            Func.VectorClassCapacityTest(true);
            uint size = Func.VectorClassCapacityTestGetSize();
            if (size == 0)
            {
                CapacityButton2.IsEnabled = false;
            }
            else
            {
                CapacityButton2.IsEnabled = true;
            }
            CapacityOut.Text = "capacity: " + Func.VectorClassCapacityTestGetCapacity().ToString() + '\n';
            CapacityOut.Text += "size: " + size.ToString();
        }

        private void CapacityButton2_Click(object sender, RoutedEventArgs e)
        {
            Func.VectorClassCapacityTest(false);
            uint size = Func.VectorClassCapacityTestGetSize();
            if (size == 0)
            {
                CapacityButton2.IsEnabled = false;
            }
            else
            {
                CapacityButton2.IsEnabled = true;
            }
            CapacityOut.Text = "capacity: " + Func.VectorClassCapacityTestGetCapacity().ToString() + '\n';
            CapacityOut.Text += "size: " + size.ToString();
        }

        private void CapacityOut_Loaded(object sender, RoutedEventArgs e)
        {
            CapacityOut.Text = "capacity: " + Func.VectorClassCapacityTestGetCapacity().ToString() + '\n';
            CapacityOut.Text += "size: " + Func.VectorClassCapacityTestGetSize().ToString();
        }
    }
}
