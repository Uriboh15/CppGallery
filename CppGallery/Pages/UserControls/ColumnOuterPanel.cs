using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CppGallery.Pages.UserControls
{
    public class ColumnOuterPanel : StackPanel
    {
        public ColumnOuterPanel()
        {
            this.Spacing = Data.ColumnOuterPanelSpacing;
            this.Padding = new Thickness(Data.ColumnOuterPanelPadding, 0, Data.ColumnOuterPanelPadding, Data.ColumnOuterPanelPadding);

        }
    }
}
