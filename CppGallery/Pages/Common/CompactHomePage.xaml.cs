// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Collections.Generic;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace CppGallery.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CompactHomePage : Page
    {
        public CompactHomePage()
        {
            this.InitializeComponent();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            if(App.LibraryPageStyle == LibraryPageStyle.Block)
            {
                MoveChildren(CppLibraryList, CppLibraryBlock);
                MoveChildren(CLibraryList, CLibraryBlock);
                MoveChildren(WindowsLibraryList, WindowsLibraryBlock);
                ListStylePanel.Visibility = Visibility.Collapsed;
                BlockStylePanel.Visibility = Visibility.Visible;

                BlockStylePanel.Padding = new Thickness(Data.OuterPanelPadding, 0, 0, Data.OuterPanelPadding);
                BlockStylePanel.Spacing = Data.OuterPanelSpacing;
            }
        }

        private static void MoveChildren(StackPanel oldpanel, StackPanel newpanel)
        {
            List<UIElement> tmplib = new List<UIElement>();
            int i;
            for (i = 1; i < oldpanel.Children.Count; ++i)
            {
                tmplib.Add(oldpanel.Children[i]);
            }

            oldpanel.Children.Clear();

            var panel = new VariableSizedWrapGrid();
            panel.Orientation = Orientation.Horizontal;
            if(App.IsCompact)
            {
                panel.ItemWidth = 300;
                panel.ItemHeight = 85;
            }
            else
            {
                panel.ItemWidth = 372;
                panel.ItemHeight = 102;
            }
            

            for (i = 0; i < tmplib.Count; ++i)
            {
                panel.Children.Add(tmplib[i]);
            }

            newpanel.Children.Add(panel);
        }
    }
}
