using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CppGallery.Pages.UserControls
{
    internal class NumberBoxPane : ControlGrid
    {
        private NumberBox NumberBox { get; } = new NumberBox
        {
            SmallChange = 1,
            LargeChange = 10,
            SpinButtonPlacementMode = NumberBoxSpinButtonPlacementMode.Inline,
        };

        public double Value
        {
            get { return NumberBox.Value; }
            set { NumberBox.Value = value; }
        }

        public double SmallChange
        {
            get { return NumberBox.SmallChange; }
            set { NumberBox.SmallChange = value; }
        }

        public double LargeChange
        {
            get { return NumberBox.LargeChange; }
            set { NumberBox.LargeChange = value; }
        }

        public double Minimum
        {
            get { return NumberBox.Minimum; }
            set { NumberBox.Minimum = value; }
        }

        public double Maximum
        {
            get { return NumberBox.Maximum; }
            set { NumberBox.Maximum = value; }
        }

        public NumberBoxPane() 
        {
            this.Content = NumberBox;
            this.Loaded += NumberBoxPane_Loaded;
            
        }

        private void NumberBoxPane_Loaded(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            NumberBox.ValueChanged += NumberBox_ValueChanged;
            FunctionExpander.Execute(this, Value.ToString());
        }

        private void NumberBox_ValueChanged(NumberBox sender, NumberBoxValueChangedEventArgs args)
        {
            FunctionExpander.Execute(this, Value.ToString());
        }
    }
}
