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

        private int kisu3 = 8;
        private int kisu4 = 8;
        private int kisu5 = 8;
        private int kisu6 = 8;
        private int kisu7 = 8;

        private int strtolKisu = 8;
        private int strtollKisu = 8;
        private int strtoulKisu = 8;
        private int strtoullKisu = 8;

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

        private void FsetRoundBox_Loaded(object sender, RoutedEventArgs e)
        {
            FsetRoundBox.SelectedItem = "最も近い値への丸め";
        }

        private void FsetRoundBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FunctionExpander.Execute(sender, new string[] { FsetRoundBox.SelectedIndex.ToString(), FsetRoundIn.Text });
        }

        private void FsetRoundIn_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            FunctionExpander.Execute(sender, new string[] { FsetRoundBox.SelectedIndex.ToString(), FsetRoundIn.Text });
        }

        private static string GetRadix(string value)
        {
            if (value == "0 (自動)") return "0";
            return value;
        }

        private void StrtoImaxKisu_Loaded(object sender, RoutedEventArgs e)
        {
            (sender as ComboBox).SelectedItem = "8";
        }

        private void StrtoImaxKisu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FunctionExpander.Execute(sender, new string[] { GetRadix(StrtoImaxKisu.SelectedItem as string), StrtoImaxIn.Text });
        }



        private void StrtoImaxIn_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            FunctionExpander.Execute(sender, new string[] { GetRadix(StrtoImaxKisu.SelectedItem as string), StrtoImaxIn.Text });
        }

        private void StrtoUmaxKisu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FunctionExpander.Execute(sender, new string[] { GetRadix(StrtoUmaxKisu.SelectedItem as string), StrtoUmaxIn.Text });
        }

        private void StrtoUmaxIn_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            FunctionExpander.Execute(sender, new string[] { GetRadix(StrtoUmaxKisu.SelectedItem as string), StrtoUmaxIn.Text });
        }

        private void WcstoImaxKisu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FunctionExpander.Execute(sender, new string[] { GetRadix(WcstoImaxKisu.SelectedItem as string), WcstoImaxIn.Text });
        }

        private void WcstoImaxIn_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            FunctionExpander.Execute(sender, new string[] { GetRadix(WcstoImaxKisu.SelectedItem as string), WcstoImaxIn.Text });
        }

        private void WcstoUmaxKisu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FunctionExpander.Execute(sender, new string[] { GetRadix(WcstoUmaxKisu.SelectedItem as string), WcstoUmaxIn.Text });
        }

        private void WcstoUmaxIn_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            FunctionExpander.Execute(sender, new string[] { GetRadix(WcstoUmaxKisu.SelectedItem as string), WcstoUmaxIn.Text });
        }

        private void ImaxDivOut_Loaded(object sender, RoutedEventArgs e)
        {
            FunctionExpander.Execute(sender, new string[] { ImaxDivIn1.Text, ImaxDivIn2.Text });
        }

        private void ImaxDivIn1_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            FunctionExpander.Execute(sender, new string[] { ImaxDivIn1.Text, ImaxDivIn2.Text });
        }

        private async void FprintfButton_Click(object sender, RoutedEventArgs e)
        {
            var savepicker = new Windows.Storage.Pickers.FileSavePicker();
            savepicker.SuggestedFileName = "sample";

            savepicker.FileTypeChoices.Add("テキストファイル", new List<string>() { ".txt" });

            InitializeWithWindow.Initialize(savepicker, WindowNative.GetWindowHandle(MainWindow.Handle));

            Windows.Storage.StorageFile savefile = await savepicker.PickSaveFileAsync();
            if (savefile != null)
            {
                FunctionExpander.Execute(sender, new string[] { savefile.Path, FprintfIn.Text });
            }
        }

        private void TmpNamButton_Click(object sender, RoutedEventArgs e)
        {
            TmpnamOut.Text = Marshal.PtrToStringAnsi(Func.TmpnamTest());
        }

        private void TmpnamOut_Loaded(object sender, RoutedEventArgs e)
        {
            TmpnamOut.Text = Marshal.PtrToStringAnsi(Func.TmpnamTest());
        }

        private void CallocButton_Click(object sender, RoutedEventArgs e)
        {
            FunctionExpander.Execute(sender);
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
