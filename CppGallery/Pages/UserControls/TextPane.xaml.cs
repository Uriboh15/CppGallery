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
using Microsoft.UI.Xaml.Documents;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace CppGallery.Pages.UserControls
{
    public sealed partial class TextPane : UserControl
    {
        public string Text
        {
            get => InTextBlock.Text;
            set => InTextBlock.Text = value.Replace("'b", "\n");
        }

        public TextPane()
        {
            this.InitializeComponent();

            if (App.IsCompact)
            {
                Viewer.Padding = new Thickness(Data.ControlGridPadding, Data.ControlGridPadding - 2.0, Data.ControlGridPadding, Data.ControlGridPadding - 2.0);
                Viewer.MaxHeight = Data.OutputsMaxHeight;

            }
            else
            {
                Viewer.Padding = new Thickness(Data.ControlGridPadding, Data.ControlGridPadding - 5.0, Data.ControlGridPadding, Data.ControlGridPadding - 5.0);
                Viewer.MaxHeight = Data.OutputsMaxHeight;
            }

            Viewer.MinHeight = Data.OutPutsMinHeight;
        }
    }
}
