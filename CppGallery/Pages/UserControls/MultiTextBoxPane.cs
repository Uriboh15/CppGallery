using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CppGallery.Pages.UserControls
{
    public class MultiTextBoxPane : ResultsPanel
    {
        private ControlGrid ControlGrid1 { get; } = new ControlGrid();
        private ControlGrid ControlGrid2 { get; } = new ControlGrid();
        private TextBox TextBox1 { get; } = new TextBox();
        private TextBox TextBox2 { get; } = new TextBox();

        public string Text1
        {
            get => TextBox1.Text;
            set => TextBox1.Text = value;
        }

        public string Text2
        {
            get => TextBox2.Text;
            set => TextBox2.Text = value;
        }

        public string Message1
        {
            get => ControlGrid1.Message;
            set => ControlGrid1.Message = value;
        }

        public string Message2
        {
            get => ControlGrid2?.Message;
            set => ControlGrid2.Message = value;
        }

        public MultiTextBoxPane()
        {
            ControlGrid1.Content = TextBox1;
            ControlGrid2.Content = TextBox2;

            Children.Add(ControlGrid1);
            Children.Add(ControlGrid2);
            
            Loaded += MultiTextBoxPane_Loaded;
        }

        private void MultiTextBoxPane_Loaded(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            FunctionExpander.Execute(sender, new string[] { Text1, Text2 });
            TextBox1.TextChanging += TextBox1_TextChanging;
            TextBox2.TextChanging += TextBox1_TextChanging;
        }

        private void TextBox1_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            FunctionExpander.Execute(sender, new string[] { Text1, Text2 });
        }
    }
}
