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

namespace CppGallery.Pages.UserControls
{
    public sealed partial class GCCSourceFilePane : Grid
    {
        TextBlock FileNameText { get; } = new TextBlock();

        public string FileName
        {
            get {  return FileNameText.Text; }
            set { FileNameText.Text = value; }
        }


        public GCCSourceFilePane()
        {
            this.InitializeComponent();
            FileNameText.HorizontalAlignment = HorizontalAlignment.Left;
            FileNameText.VerticalAlignment = VerticalAlignment.Center;
            this.Padding = new Thickness(Data.ControlGridPadding, 0, Data.ControlGridPadding, 0);
            this.Children.Add(this.FileNameText);
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if(this.Parent is ContentControl control)
            {
                control.Content = null;
                return;
            }

            if(this.Parent is Panel panel)
            {
                panel.Children.Remove(this);
                
                if(panel.Children.Count > 0)
                {
                    (panel.Children[0] as Grid).BorderThickness = new Thickness(1);
                }
                else
                {
                    if(panel.Parent is Expander expander)
                    {
                        expander.IsExpanded = false;
                    }
                }
                return;
            }
        }
    }
}
