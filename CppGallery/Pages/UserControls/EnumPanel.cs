using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CppGallery.Pages.UserControls
{
    public class EnumPanel : ResultsPanel
    {
        public EnumPanel() 
        {
            this.Children.Add(new TextBlock { Text = "メンバー"});
        }
    }
}
