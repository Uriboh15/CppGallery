using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CppGallery.Pages.UserControls
{
    internal class DefinePane : UserControl
    {
        private TextBlock DefineTextBlock { get; } = new TextBlock { IsTextSelectionEnabled = true };
        private TextBlock ParameterTextBlock { get; } = new TextBlock();
        private TextBlock ReturnsTextBlock { get; } = new TextBlock();

        public string Definision
        {
            get { return DefineTextBlock.Text; }
            set { DefineTextBlock.Text = value.Replace(@"'\r\n' ", "\n"); }
        }


        public string Parameter
        {
            get { return ParameterTextBlock.Text; }
            set { ParameterTextBlock.Text = value.Replace(@"'\r\n' ", "\n"); }
        }
        public string Returns
        {
            get { return ReturnsTextBlock.Text; }
            set { ReturnsTextBlock.Text = value.Replace(@"'\r\n' ", "\n"); }
        }

        public DefinePane() 
        {
            var rpanel = new ResultsPanel();
            var panel = new InnerPanel();

            panel.Children.Add(new TextBlock { Text = "定義", FontWeight = new Windows.UI.Text.FontWeight(700) });
            panel.Children.Add(DefineTextBlock);

            panel.Children.Add(new TextBlock { Text = "パラメーター", FontWeight = new Windows.UI.Text.FontWeight(700), Margin = new Microsoft.UI.Xaml.Thickness { Top = 14.0 } });
            panel.Children.Add(ParameterTextBlock);

            panel.Children.Add(new TextBlock { Text = "戻り値", FontWeight = new Windows.UI.Text.FontWeight(700), Margin = new Microsoft.UI.Xaml.Thickness { Top = 14.0 } });
            panel.Children.Add(ReturnsTextBlock);
            //Parameter

            rpanel.Children.Add(new TextBlock { Text = "定義", FontWeight = new Windows.UI.Text.FontWeight(700), FontSize = Data.TitleTextSize });
            rpanel.Children.Add(panel);

            Content = rpanel;
        }
    }
}
