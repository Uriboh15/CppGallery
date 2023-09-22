using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CppGallery.Pages.UserControls
{
    internal class LinkButtons : UserControl
    {
        private HyperlinkButton MSButton { get; } = new HyperlinkButton { Content = "Microsoft リファレンス" };
        private HyperlinkButton CppJPButton { get; } = new HyperlinkButton { Content = "C++日本語リファレンス" };

        private SymbolBlockPanel BlockPanel { get; } = new SymbolBlockPanel
        {
            Symbol = PanelHeader.ReferenceURL,
        };

        private StackPanel MSPanel { get; } = new StackPanel { Orientation = Orientation.Horizontal, Spacing = 8 };
        private StackPanel CppJPPanel { get; } = new StackPanel { Orientation = Orientation.Horizontal, Spacing = 8 };

        public Uri MSURL
        {
            get
            {
                return MSButton.NavigateUri;
            }
            set
            {
                MSButton.NavigateUri = value;
                SetNavigateUri();
            }
        }

        public Uri CppJPURL
        {
            get
            {
                return CppJPButton.NavigateUri;
            }
            set
            {
                CppJPButton.NavigateUri = value;
                SetNavigateUri();
            }
        }

        private void SetNavigateUri()
        {
            for (int i = BlockPanel.Children.Count - 1; i >= 1; --i)
            {
                BlockPanel.Children.Remove(BlockPanel.Children[i]);
            }

            if (CppJPURL != null)
            {
                BlockPanel.Children.Add(CppJPPanel);
            }

            if (MSURL != null)
            {
                BlockPanel.Children.Add(MSPanel);
            }
        }

        public LinkButtons()
        {
            Content = BlockPanel;
            MSPanel.Children.Add(new FontIcon { Glyph = "\uE774", FontSize = 16, });
            MSPanel.Children.Add(MSButton);
            CppJPPanel.Children.Add(new FontIcon { Glyph = "\uE774", FontSize = 16, });
            CppJPPanel.Children.Add(CppJPButton);
        }
    }
}
