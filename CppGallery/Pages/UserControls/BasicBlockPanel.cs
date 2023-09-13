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

        public CppVersion TargetMinCppVersion { get; set; } = UserAPI.MinCppVersion;

        public string HeadText
        {
            get { return HeadTextBlock.Text; }
            set { HeadTextBlock.Text = value; }
        }

        public BasicBlockPanel()
        {
            ApplyIsCompact();
            this.Children.Add(HeadTextBlock);
            this.Loaded += BasicBlockPanel_Loaded;
        }

        private void BasicBlockPanel_Loaded(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {

            if (this.TargetMinCppVersion == UserAPI.MinCppVersion) return;

            foreach (var element in Children)
            {
                if (element is FunctionExpander expander)
                {
                    if (expander.TargetMinCppVersion < this.TargetMinCppVersion)
                    {
                        expander.TargetMinCppVersion = this.TargetMinCppVersion;
                    }
                }
            }
        }

        private void ApplyIsCompact()
        {
            HeadTextBlock.FontWeight = new Windows.UI.Text.FontWeight(700);

            this.Spacing = Data.ResultPanelSpacing;
            HeadTextBlock.FontSize = Data.TitleTextSize;
        }
    }
}
