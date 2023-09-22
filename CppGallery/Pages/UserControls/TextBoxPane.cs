using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CppGallery.Pages.UserControls
{
    public class TextBoxPane : ControlGrid
    {
        private TextBox TextBox { get; set; } = new TextBox();

        public string Text
        {
            get { return TextBox.Text; }
            set { TextBox.Text = value; }
        }

        public TextBoxPane()
        {
            this.Content = TextBox;
            this.Loaded += Out_Loaded;
        }

        private void In_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            FunctionExpander.Execute(this, sender.Text);
        }

        private void Out_Loaded(object sender, RoutedEventArgs e)
        {
            Loaded -= Out_Loaded;

            FunctionExpander.Execute(this, this.Text);
            TextBox.TextChanging += In_TextChanging;
        }
    }
}
