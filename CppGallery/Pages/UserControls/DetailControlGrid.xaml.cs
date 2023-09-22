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

namespace CppGallery.Pages.UserControls
{
    public sealed partial class DetailControlGrid : UserControl
    {
        public static readonly DependencyProperty MessageProperty = DependencyProperty.Register(
            "Message",　// Max という名前の……
            typeof(string),　// int 型の CLR プロパティを……
            typeof(ControlGrid), // クラスに登録するやで―
            new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty DetailProperty = DependencyProperty.Register(
            "Detail",　// Max という名前の……
            typeof(string),　// int 型の CLR プロパティを……
            typeof(ControlGrid), // クラスに登録するやで―
            new PropertyMetadata(string.Empty));

        public string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }

        public string Detail
        {
            get { return (string)GetValue(DetailProperty); }
            set { SetValue(DetailProperty, value); }
        }

        public new UIElement Content { get; set; }

        public DetailControlGrid()
        {
            this.InitializeComponent();

            Height = Data.GalleryControlHeight;
            Panel.Padding = new Thickness(Data.ControlGridPadding, 0.0, Data.ControlGridPadding, 0.0);
            InPanel.Margin = new Thickness(0, 0, Data.ControlGridPadding, 0.0);
        }


    }
}
