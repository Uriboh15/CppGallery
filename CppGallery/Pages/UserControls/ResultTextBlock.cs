using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CppGallery.Pages.UserControls
{
    public class ResultTextBlock : UserControl
    {
        private TextBlock Result;

        public string Text
        {
            get { return Result.Text; }
            set { Result.Text = value; }
        }

        public ResultTextBlock()
        {
            this.Resources["ContentControlThemeFontFamily"] = new FontFamily("Yu Gothic");
            Result = new TextBlock();
            this.Content = Result;
            this.Loaded += ResultTextBlock_Loaded;
        }

        private void ResultTextBlock_Loaded(object sender, RoutedEventArgs e)
        {
#if false
            Result.FontFamily = new FontFamily("Cascadia Mono");

#endif
        }
    }
}
