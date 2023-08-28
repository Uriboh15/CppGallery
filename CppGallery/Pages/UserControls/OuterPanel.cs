using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CppGallery.Pages.UserControls
{
    public enum HeaderType
    {
        None,
        C,
        Cpp,
        Windows,
        WinRT,
        NonStandardC,
        NonStandardCpp,
    }

    public class OuterPanel : StackPanel
    {
        public HeaderType HeaderType { get; set; } = HeaderType.None;

        public OuterPanel()
        {
            this.Spacing = Data.OuterPanelSpacing;
            this.Padding = new Thickness(Data.OuterPanelPadding, 0, Data.OuterPanelPadding, Data.OuterPanelPadding);
            this.Loaded += OuterPanel_Loaded;
        }

        private void OuterPanel_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.HeaderType == HeaderType.Windows || this.HeaderType == HeaderType.WinRT)
            {
                if(App.Compiler != Compiler.VC)
                {
                    this.Children.Clear();
                    this.Spacing = Data.ResultPanelSpacing;
                    this.Padding = new Thickness(0);
                    this.HorizontalAlignment = HorizontalAlignment.Center;
                    this.VerticalAlignment = VerticalAlignment.Center;
                    this.Children.Add(new TextBlock
                    {
                        Text = "このヘッダーは Windows 固有のものです",
                        HorizontalAlignment = HorizontalAlignment.Center,
                    });
                    this.Children.Add(new TextBlock
                    {
                        Text = "Visual C++ をお使いください",
                        HorizontalAlignment = HorizontalAlignment.Center,
                    });
                    var button = new Button
                    {
                        Content = "設定へ移動",
                        HorizontalAlignment = HorizontalAlignment.Center,
                    };
                    button.Click += Button_Click;
                    this.Children.Add(button);
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Handle.OpenSettings();

        }
    }
}
