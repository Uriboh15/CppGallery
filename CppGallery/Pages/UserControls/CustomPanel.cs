using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Media.Animation;

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

    public class InnerGrid : Grid
    {
        public InnerGrid()
        {
            this.Loaded += Grid_Loaded;
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            if (App.IsCompact)
            {
                this.Padding = new Thickness(Data.ControlGridPadding, Data.ControlGridPadding - 2.0, Data.ControlGridPadding, Data.ControlGridPadding - 2.0);
                this.MaxHeight = Data.OutputsMaxHeight;

            }
            else
            {
                this.Padding = new Thickness(Data.ControlGridPadding, Data.ControlGridPadding - 5.0, Data.ControlGridPadding, Data.ControlGridPadding - 5.0);
                this.MaxHeight = Data.OutputsMaxHeight;
            }
            this.MinHeight = Data.OutPutsMinHeight;

            if (App.ResultTheme != ElementTheme.Default)
            {
                if (App.ResultTheme != MainPage.Handle.ActualTheme)
                {
                    var panel = sender as Panel;
                    panel.RequestedTheme = App.SourceCodeTheme;
                    if (App.ResultTheme == ElementTheme.Dark)
                    {
                        panel.Background = new SolidColorBrush(Windows.UI.Color.FromArgb(0XFF, 0X20, 0x20, 0x20));
                    }
                    else
                    {
                        panel.Background = new SolidColorBrush(Windows.UI.Color.FromArgb(0XFF, 0XFD, 0xFD, 0xFD));
                    }
                }
            }
        }
    }

    public class ColumnButton : Button
    {
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(
            "Title",　// Max という名前の……
            typeof(string),　// int 型の CLR プロパティを……
            typeof(ColumnButton), // クラスに登録するやで―
            new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty IconProperty = DependencyProperty.Register(
            "Icon",　// Max という名前の……
            typeof(string),　// int 型の CLR プロパティを……
            typeof(ColumnButton), // クラスに登録するやで―
            new PropertyMetadata("\uF000"));

        public static readonly DependencyProperty PageTagProperty = DependencyProperty.Register(
           "PageTag", // Max という名前の……
           typeof(string), // int 型の CLR プロパティを……
           typeof(ColumnButton), // クラスに登録するやで―
           new PropertyMetadata(null));

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public string Icon
        {
            get { return (string)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        public string PageTag
        {
            get { return (string)GetValue(PageTagProperty); }
            set { SetValue(PageTagProperty, value); }
        }

        private static NavigationTransitionInfo Navigate { get; } = new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight };

        public ColumnButton()
        {
            this.Loaded += ColumnButton_Loaded;
            this.Click += ColumnButton_Click;
            this.MinHeight = 60.0;
            this.HorizontalAlignment = HorizontalAlignment.Stretch;
            this.HorizontalContentAlignment = HorizontalAlignment.Stretch;
        }

        private void ColumnButton_Click(object sender, RoutedEventArgs e)
        {
            if (PageTag != string.Empty)
            {
                foreach (var pair in MainPage.PageDictionary)
                {
                    if (pair.Value == PageTag)
                    {
                        var element = this.Parent;
                        while (element is not MainPage)
                        {
                            element = (element as FrameworkElement).Parent;
                        }
                        (element as MainPage).FrameHandle.Navigate(pair.Key, null, Navigate);
                    }
                }

            }
        }

        private void ColumnButton_Loaded(object sender, RoutedEventArgs e)
        {
            Grid outpanel = new Grid();
            FontIcon icon = new FontIcon { Glyph = this.Icon, FontFamily = new FontFamily("Segoe MDL2 Assets"), Margin = new Thickness(10.0, 0.0, 0.0, 0.0), HorizontalAlignment = HorizontalAlignment.Left };
            TextBlock textblock = new TextBlock { Text = this.Title, Margin = new Thickness(54.0, 0.0, 32.0, 0.0), VerticalAlignment = VerticalAlignment.Center };
            FontIcon toright = new FontIcon { Glyph = "\uE974", FontFamily = new FontFamily("Segoe Fluent Icons"), Margin = new Thickness(0.0, 0.0, 5.0, 0.0), HorizontalAlignment = HorizontalAlignment.Right, FontSize = 15.0 };
            if (App.Win10)
            {
                toright.FontFamily = new FontFamily("Segoe MDL2 Assets");
                toright.FontSize = 11;
                toright.Margin = new Thickness(0.0, 0.0, 7.0, 0.0);
            }
            if (!App.IsCompact)
            {
                outpanel.Padding = new Thickness(0.0, 7.0, 0.0, 7.0);
            }
            outpanel.Children.Add(icon);
            outpanel.Children.Add(textblock);
            outpanel.Children.Add(toright);

            this.Content = outpanel;
        }
    }
}
