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
    public sealed partial class DetailPane : UserControl
    {
        public string Title { get; set; }
        public string Detail { get; set; }

        public DetailPane()
        {
            this.InitializeComponent();

            this.MinHeight = Data.GalleryControlHeight;
            InPanel.Padding = new Thickness(Data.ControlGridPadding, 0.0, Data.ControlGridPadding, 0.0);
        }
    }
}
