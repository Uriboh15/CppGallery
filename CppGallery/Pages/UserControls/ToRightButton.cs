using Microsoft.UI.Input;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Animation;
using Windows.Graphics.Holographic;
using Microsoft.UI.Xaml.Controls.Primitives;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace CppGallery.Pages.UserControls
{
    public class ToRightButton : Button
    {
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(
            "Title",　// Max という名前の……
            typeof(string),　// int 型の CLR プロパティを……
            typeof(ToRightButton), // クラスに登録するやで―
            new PropertyMetadata("aaa"));

        public static readonly DependencyProperty SentenceProperty = DependencyProperty.Register(
            "Sentence",　// Max という名前の……
            typeof(string),　// int 型の CLR プロパティを……
            typeof(ToRightButton), // クラスに登録するやで―
            new PropertyMetadata("aaa"));

        public static readonly DependencyProperty IconProperty = DependencyProperty.Register(
           "Icon", // Max という名前の……
           typeof(string), // int 型の CLR プロパティを……
           typeof(ToRightButton), // クラスに登録するやで―
           new PropertyMetadata("\uEA86"));

        public static readonly DependencyProperty InLibraryPageProperty = DependencyProperty.Register(
           "InLibraryPage", // Max という名前の……
           typeof(bool), // int 型の CLR プロパティを……
           typeof(ToRightButton), // クラスに登録するやで―
           new PropertyMetadata(false));

        public static readonly DependencyProperty PageTagProperty = DependencyProperty.Register(
           "PageTag", // Max という名前の……
           typeof(string), // int 型の CLR プロパティを……
           typeof(ToRightButton), // クラスに登録するやで―
           new PropertyMetadata(null));

        


        public string Title
        {
            get {
                if(PageTag == null) return (string)GetValue(TitleProperty);
                if (App.UseCppInCSample && PageTag[0] == '0') return ((string)GetValue(TitleProperty)).Replace("<", "<c").Replace(".h>", ">");
                return (string)GetValue(TitleProperty);
            }
            set { SetValue(TitleProperty, value); }
        }

        public string Sentence
        {
            get { return (string)GetValue(SentenceProperty); }
            set { SetValue(SentenceProperty, value); }
        }

        public string Icon
        {
            get { return (string)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        public bool InLibraryPage
        {
            get { return (bool)GetValue(InLibraryPageProperty); }
            set { SetValue(InLibraryPageProperty, value); }
        }

        public string PageTag
        {
            get { return (string)GetValue(PageTagProperty); }
            set {  SetValue(PageTagProperty, value); }
        }

        private NavigationTransitionInfo navigate = new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight };

        public ToRightButton()
        {
            
            this.Loaded += ToRightButton_Loaded;
            this.Click += ToRightButton_Click;
            this.ContextRequested += ToRightButton_ContextRequested;
            this.PointerPressed += ToRightButton_PointerPressed;
            
        }

        private void ToRightButton_PointerPressed(object sender, Microsoft.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            PointerPoint ptrPt = e.GetCurrentPoint(this);
            if (ptrPt.Properties.IsMiddleButtonPressed)
            {
                MainWindow.GetParentMainWindow(this).AddTab(PageTag);
            }
        }

        private void ToRightButton_ContextRequested(UIElement sender, Microsoft.UI.Xaml.Input.ContextRequestedEventArgs args)
        {
            //FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
            if(ContextFlyout == null)
            {
                this.ContextFlyout = UserAPI.GetMenuFlyout(PageTag, this);
            }
            ContextFlyout.ShowAt(this);
        }

        private void ToRightButton_Click(object sender, RoutedEventArgs e)
        {
            if (PageTag != string.Empty)
            {
                foreach (var pair in MainPage.PageDictionary)
                {
                    if(pair.Value == PageTag)
                    {
                        var element = this.Parent;
                        while(element is not MainPage)
                        {
                            element = (element as FrameworkElement).Parent;
                        }
                        (element as MainPage).FrameHandle.Navigate(pair.Key, null, navigate);
                    }
                }
                
            }
        }

        private void ToRightButton_Loaded(object sender, RoutedEventArgs e)
        {
            Loaded -= ToRightButton_Loaded;

            this.ContextFlyout = UserAPI.GetMenuFlyout(PageTag, this);
            if (App.LibraryPageStyle == LibraryPageStyle.Block && this.InLibraryPage)
            {
                double width;
                double height;
                double panelWidth;
                double panelHeight;
                double iconSize;
                double innerPanelSpacing;
                Thickness innerPanelMargin;
                double innerPanelMaxWidth;
                if (App.IsCompact)
                {
                    width = 290;
                    height = 75;
                    panelWidth = 240;
                    panelHeight = 50;
                    iconSize = 27;
                    innerPanelSpacing = 0;
                    innerPanelMargin = new Thickness(17, 0, 0, 0);
                    innerPanelMaxWidth = 198;
                }
                else
                {
                    width = 360;
                    height = 90;
                    panelWidth = 300;
                    panelHeight = 60;
                    iconSize = 32;
                    innerPanelSpacing = 5;
                    innerPanelMargin = new Thickness(20, 0, 0, 0);
                    innerPanelMaxWidth = 250;
                }
                this.Width = width;
                this.Height = height;
                StackPanel panel = new StackPanel();
                panel.Orientation = Orientation.Horizontal;
                panel.Width = panelWidth;
                panel.Height = panelHeight;
                panel.Children.Add(new FontIcon { FontFamily = new FontFamily("Segoe MDL2 Assets"), Glyph = this.Icon, FontSize = iconSize });
                StackPanel innerpanel = new StackPanel();
                innerpanel.Spacing = innerPanelSpacing;
                innerpanel.Margin = innerPanelMargin;
                innerpanel.MaxWidth = innerPanelMaxWidth;
                innerpanel.Children.Add(new TextBlock { Text = this.Title });
                innerpanel.Children.Add(new SentenceTextBlock { Text = this.Sentence });
                panel.Children.Add(innerpanel);
                this.Content = panel;
                navigate = new DrillInNavigationTransitionInfo();
            }
            else
            {
                this.HorizontalAlignment = HorizontalAlignment.Stretch;
                this.HorizontalContentAlignment = HorizontalAlignment.Stretch;
                this.Padding = new Thickness(0);
                MinHeight = Data.GalleryControlHeight;
                Grid grid = new Grid();
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
                grid.ColumnDefinitions.Add(new ColumnDefinition());
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
                FontIcon icon = new FontIcon();
                icon.FontSize = 24.0;
                icon.Margin = new Thickness((Data.GalleryControlHeight - 24.0) / 2.0, 0.0, (Data.GalleryControlHeight - 24.0) / 2.0, 0.0);
                icon.HorizontalAlignment = HorizontalAlignment.Left;
                icon.FontFamily = new FontFamily("Segoe MDL2 Assets");
                icon.Glyph = Icon;
                
                StackPanel subPanel = new StackPanel();
                TextBlock title = new TextBlock();
                title.Text = Title;
                title.TextWrapping = TextWrapping.Wrap;
                var sentence = new SentenceTextBlock();
                sentence.Text = Sentence;
                subPanel.Children.Add(title);
                subPanel.Children.Add(sentence);
                FontIcon right = new FontIcon();
                right.Glyph = "\uE974";
                right.HorizontalAlignment = HorizontalAlignment.Right;
                right.Margin = new Thickness(Data.ControlGridPadding, 0, 0, 0);
                if (App.Win10)
                {
                    right.FontSize = 11.0;
                    right.Margin = new Thickness(0, 0, 18, 0);

                }
                else
                {
                    right.FontSize = 15.0;
                    right.Margin = new Thickness(0, 0, 16, 0);
                }

                grid.Children.Add(icon);
                grid.Children.Add(subPanel);
                grid.Children.Add(right);

                Grid.SetColumn(icon, 0);
                Grid.SetColumn(subPanel, 1);
                Grid.SetColumn(right, 2);


                this.Content = grid;
            }
            
        }
    }
}
