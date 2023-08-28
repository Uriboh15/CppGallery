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
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace CppGallery.Pages.CHeader
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TimehPage : Page
    {
        public TimehPage()
        {
            this.InitializeComponent();
        }

        private async void ClockOut_Loaded(object sender, RoutedEventArgs e)
        {
            var parent = (sender as Grid).Parent as Panel;

            parent.Children.Remove(sender as UIElement);
            while (true)
            {
                ClockOut.Text = Func.ClockTest().ToString() + "É~Éäïb";
                await Task.Delay(10);
            }
        }

        private async void LocaltimeOut_Loaded(object sender, RoutedEventArgs e)
        {
            var parent = (sender as Grid).Parent as Panel;

            parent.Children.Remove(sender as UIElement);
            while (true)
            {
                LocaltimeOut.Text = Marshal.PtrToStringUni(Func.LocaltimeTest());
                await Task.Delay(10);
            }
        }

        private async void TimeOut_Loaded(object sender, RoutedEventArgs e)
        {
            var parent = (sender as Grid).Parent as Panel;

            parent.Children.Remove(sender as UIElement);
            while (true)
            {
                TimeOut.Text = "åªç›ÇÃéûçèÅF" + Marshal.PtrToStringUni(Func.TimeTest());
                await Task.Delay(10);
            }
        }

        private async void CtimeOut_Loaded(object sender, RoutedEventArgs e)
        {
            var parent = (sender as Grid).Parent as Panel;

            parent.Children.Remove(sender as UIElement);

            while (true)
            {
                CtimeOut.Text = Marshal.PtrToStringAnsi(Func.CtimeTest());
                await Task.Delay(10);
            }
        }

        private async void GmtimeOut_Loaded(object sender, RoutedEventArgs e)
        {
            var parent = (sender as Grid).Parent as Panel;

            parent.Children.Remove(sender as UIElement);
            while (true)
            {
                GmtimeOut.Text = Marshal.PtrToStringAnsi(Func.GmtimeTest());
                await Task.Delay(10);
            }
        }

        private async void StrftimeOut_Loaded(object sender, RoutedEventArgs e)
        {
            var parent = (sender as Grid).Parent as Panel;

            parent.Children.Remove(sender as UIElement);
            while (true)
            {
                StrftimeOut.Text = Marshal.PtrToStringAnsi(Func.StrftimeTest());
                await Task.Delay(10);
            }
        }

        private async void Asctime_Loaded(object sender, RoutedEventArgs e)
        {
            var parent = (sender as Grid).Parent as Panel;

            parent.Children.Remove(sender as UIElement);

            while (true)
            {
                Asctime.Text = Marshal.PtrToStringAnsi(Func.CtimeTest());
                await Task.Delay(30);
            }
        }
    }
}
