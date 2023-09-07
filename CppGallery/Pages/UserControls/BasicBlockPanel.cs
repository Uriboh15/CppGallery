using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CppGallery.Pages.UserControls
{
    public class BasicBlockPanel : ResultsPanel
    {
        private TextBlock HeadTextBlock { get; } = new TextBlock();

        public string HeadText
        {
            get { return HeadTextBlock.Text; }
            set { HeadTextBlock.Text = value; }
        }

        public BasicBlockPanel()
        {
            ApplyIsCompact();
            this.Children.Add(HeadTextBlock);
        }

        private void ApplyIsCompact()
        {
            HeadTextBlock.FontWeight = new Windows.UI.Text.FontWeight(700);

            this.Spacing = Data.ResultPanelSpacing;
            HeadTextBlock.FontSize = Data.TitleTextSize;
        }
    }
}
