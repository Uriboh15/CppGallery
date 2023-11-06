using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Media.Animation;
using Windows.Devices.Enumeration;

namespace CppGallery.Pages.UserControls
{
    public class CustomPanel : StackPanel
    {

        public CustomPanel()
        {
            this.Padding = new Thickness(Data.OuterPanelPadding, 0, Data.OuterPanelPadding, Data.OuterPanelPadding);
            this.Spacing = Data.ResultPanelSpacing;
        }
    }

    public class CustomExpander : Expander
    {
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(
            "Title",　// Max という名前の……
            typeof(string),　// int 型の CLR プロパティを……
            typeof(CustomExpander), // クラスに登録するやで―
            new PropertyMetadata(null));

        public static readonly DependencyProperty SentenceProperty = DependencyProperty.Register(
            "Sentence",　// Max という名前の……
            typeof(string),　// int 型の CLR プロパティを……
            typeof(CustomExpander), // クラスに登録するやで―
            new PropertyMetadata("aaa"));

        public static readonly DependencyProperty IconProperty = DependencyProperty.Register(
           "Icon", // Max という名前の……
           typeof(string), // int 型の CLR プロパティを……
           typeof(CustomExpander), // クラスに登録するやで―
           new PropertyMetadata("\uF158"));

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public string Sentence
        {
            get { return (string)GetValue(SentenceProperty); }
            set { SetValue (SentenceProperty, value); }
        }

        public string Icon
        {
            get { return (string)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }


        public CustomExpander()
        {
            this.Loaded += CustomExpander_Loaded;
            this.HorizontalAlignment = HorizontalAlignment.Stretch;
            this.HorizontalContentAlignment = HorizontalAlignment.Stretch;
            this.Padding = new Thickness(Data.FunctionExpanderPadding);
            this.MinHeight = Data.GalleryControlHeight;
        }

        private void CustomExpander_Loaded(object sender, RoutedEventArgs e)
        {
            Loaded -= CustomExpander_Loaded;

            if (this.Title != null)
            {
                this.Header = new EHeader { Title = this.Title, Sentence = this.Sentence, Icon = this.Icon };
            }

        }
    }

    public class InnerPanel : StackPanel
    {
        public InnerPanel()
        {
            this.Padding = new Thickness(Data.ControlGridPadding);
        }
    }
}
