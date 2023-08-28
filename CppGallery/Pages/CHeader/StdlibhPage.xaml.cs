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
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace CppGallery.Pages.CHeader
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class StdlibhPage : Page
    {
        private int strtolKisu = 8;
        private int strtollKisu = 8;
        private int strtoulKisu = 8;
        private int strtoullKisu = 8;

        public StdlibhPage()
        {
            this.InitializeComponent();
            Func.SrandTest(0);
        }

        private void StrtolCalc()
        {
            FunctionExpander.Execute(StrtolIn, new string[] { strtolKisu.ToString(), StrtolIn.Text });
        }

        private void StrtollCalc()
        {
            FunctionExpander.Execute(StrtollIn, new string[] { strtollKisu.ToString(), StrtollIn.Text });
        }

        private void StrtoulCalc()
        {
            FunctionExpander.Execute(StrtoulIn, new string[] { strtoulKisu.ToString(), StrtoulIn.Text });
        }

        private void StrtoullCalc()
        {
            FunctionExpander.Execute(StrtoullIn, new string[] { strtoullKisu.ToString(), StrtoullIn.Text });
        }

        private void RandButton1_Click(object sender, RoutedEventArgs e)
        {
            Func.SrandTest(0);
            RandOut.Text = "n = " + Func.RandTest().ToString();
        }

        private void RandButton2_Click(object sender, RoutedEventArgs e)
        {
            RandOut.Text = "n = " + Func.RandTest().ToString();
        }

        private void StrtolKisu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (StrtolIn == null) return;
            string st = e.AddedItems[0].ToString();
            if (st.Length == 1 || st.Length == 2)
            {
                strtolKisu = int.Parse(st);
            }
            else
            {
                strtolKisu = 0;
            }
            StrtolCalc();
        }

        private void StrtolIn_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            StrtolCalc();
        }

        private void StrtollKisu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (StrtollIn != null)
            {
                string st = e.AddedItems[0].ToString();
                if (st.Length == 1 || st.Length == 2)
                {
                    strtollKisu = int.Parse(st);
                }
                else
                {
                    strtollKisu = 0;
                }
                StrtollCalc();
            }
        }

        private void StrtollIn_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            StrtollCalc();
        }

        private void StrtoulKisu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (StrtoulIn != null)
            {
                string st = e.AddedItems[0].ToString();
                if (st.Length == 1 || st.Length == 2)
                {
                    strtoulKisu = int.Parse(st);
                }
                else
                {
                    strtoulKisu = 0;
                }
                StrtoulCalc();
            }
        }

        private void StrtoulIn_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            StrtoulCalc();
        }

        private void StrtoullKisu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (StrtoullIn != null)
            {
                string st = e.AddedItems[0].ToString();
                if (st.Length == 1 || st.Length == 2)
                {
                    strtoullKisu = int.Parse(st);
                }
                else
                {
                    strtoullKisu = 0;
                }
                StrtoullCalc();
            }
        }

        private void StrtoullIn_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            StrtoullCalc();
        }

        private void ReallocButton1_Click(object sender, RoutedEventArgs e)
        {
            ReallocOut.Text = Marshal.PtrToStringAnsi(Func.ReallocTest(1));
            uint size = Func.ReallocTestGetSize();
            if (size == 0)
            {
                ReallocButton1.IsEnabled = false;
                ReallocButton2.IsEnabled = false;
            }
            else if (size == 1000)
            {
                ReallocButton1.IsEnabled = false;
            }
            else
            {
                ReallocButton1.IsEnabled = true;
                ReallocButton2.IsEnabled = true;
            }
        }

        private void ReallocButton2_Click(object sender, RoutedEventArgs e)
        {
            ReallocOut.Text = Marshal.PtrToStringAnsi(Func.ReallocTest(-1));
            uint size = Func.ReallocTestGetSize();
            if (size == 0)
            {
                ReallocButton1.IsEnabled = false;
                ReallocButton2.IsEnabled = false;
            }
            else if (size == 1)
            {
                ReallocButton2.IsEnabled = false;
            }
            else
            {
                ReallocButton1.IsEnabled = true;
                ReallocButton2.IsEnabled = true;
            }
        }

        private void ReallocGrid_Loaded(object sender, RoutedEventArgs e)
        {
            Func.ReallocTestInitialize();
            ReallocOut.Text = Marshal.PtrToStringAnsi(Func.ReallocTest(0));
        }

        private void CallocButton_Click(object sender, RoutedEventArgs e)
        {
            FunctionExpander.Execute(sender);
        }

        private void RandOut_Loaded(object sender, RoutedEventArgs e)
        {
            RandOut.Text = "n = " + Func.RandTest().ToString();
        }

        private void StrtolOut_Loaded(object sender, RoutedEventArgs e)
        {
            StrtolCalc();
        }

        private void StrtollOut_Loaded(object sender, RoutedEventArgs e)
        {
            StrtollCalc();
        }

        private void StrtoulOut_Loaded(object sender, RoutedEventArgs e)
        {
            StrtoulCalc();
        }

        private void StrtoullOut_Loaded(object sender, RoutedEventArgs e)
        {
            StrtoullCalc();
        }
    }
}
