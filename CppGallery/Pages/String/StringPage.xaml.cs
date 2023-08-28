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
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace CppGallery.Pages.String
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class StringPage : Page
    {
        private int kisu3 = 8;
        private int kisu4 = 8;
        private int kisu5 = 8;
        private int kisu6 = 8;
        private int kisu7 = 8;

        public StringPage()
        {
            this.InitializeComponent();
        }

        private void Calc3()
        {
            FunctionExpander.Execute(StoIOut, new string[] { kisu3.ToString(), In3.Text });
        }

        private void Calc4()
        {
            FunctionExpander.Execute(StoLOut, new string[] { kisu4.ToString(), In4.Text });
        }

        private void Calc5()
        {
            FunctionExpander.Execute(StoLLOut, new string[] { kisu5.ToString(), In5.Text });
        }

        private void Calc6()
        {
            FunctionExpander.Execute(StoULOut, new string[] { kisu6.ToString(), In6.Text });
        }

        private void Calc7()
        {
            FunctionExpander.Execute(StoULLOut, new string[] { kisu7.ToString(), In7.Text });
        }

        private void Kisu3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (In3 == null) return;

            string st = e.AddedItems[0].ToString();
            if (st.Length == 1 || st.Length == 2)
            {
                kisu3 = int.Parse(st);
            }
            else
            {
                kisu3 = 0;
            }
            Calc3();
        }

        private void In3_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            Calc3();
        }

        private void Kisu5_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (In5 == null) return;

            string st = e.AddedItems[0].ToString();
            if (st.Length == 1 || st.Length == 2)
            {
                kisu5 = int.Parse(st);
            }
            else
            {
                kisu5 = 0;
            }
            Calc5();
        }

        private void In5_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            Calc5();
        }

        private void Kisu6_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (In6 == null) return;

            string st = e.AddedItems[0].ToString();
            if (st.Length == 1 || st.Length == 2)
            {
                kisu6 = int.Parse(st);
            }
            else
            {
                kisu6 = 0;
            }
            Calc6();
        }

        private void In6_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            Calc6();
        }

        private void Kisu7_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (In7 == null) return;

            string st = e.AddedItems[0].ToString();
            if (st.Length == 1 || st.Length == 2)
            {
                kisu7 = int.Parse(st);
            }
            else
            {
                kisu7 = 0;
            }
            Calc7();
        }

        private void In7_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            Calc7();
        }

        private void StoIOut_Loaded(object sender, RoutedEventArgs e)
        {
            Calc3();
        }

        private void StoLOut_Loaded(object sender, RoutedEventArgs e)
        {
            Calc4();
        }

        private void Kisu4_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (In4 == null) return;

            string st = e.AddedItems[0].ToString();
            if (st.Length == 1 || st.Length == 2)
            {
                kisu4 = int.Parse(st);
            }
            else
            {
                kisu4 = 0;
            }
            Calc4();
        }

        private void In4_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            Calc4();
        }

        private void StoLLOut_Loaded(object sender, RoutedEventArgs e)
        {
            Calc5();
        }

        private void StoULOut_Loaded(object sender, RoutedEventArgs e)
        {
            Calc6();
        }

        private void StoULLOut_Loaded(object sender, RoutedEventArgs e)
        {
            Calc7();
        }
    }
}
