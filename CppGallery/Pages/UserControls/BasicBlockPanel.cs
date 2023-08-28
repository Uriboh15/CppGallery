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
        protected TextBlock _headText = new TextBlock();

        public string HeadText
        {
            get { return _headText.Text; }
            set { _headText.Text = value; }
        }

        public BasicBlockPanel()
        {
            ApplyIsCompact();
            this.Children.Add(_headText);
        }

        private void ApplyIsCompact()
        {
            _headText.FontWeight = new Windows.UI.Text.FontWeight(700);

            this.Spacing = Data.ResultPanelSpacing;
            _headText.FontSize = Data.TitleTextSize;
        }
    }
}
