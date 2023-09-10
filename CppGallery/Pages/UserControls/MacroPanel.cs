using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Media.Animation;

namespace CppGallery.Pages.UserControls
{
    internal class MacroPanel : BlockPanel
    {
        public string Folder { get; set; } = string.Empty;

        public MacroPanel()
        {
            Header = PanelHeader.Macro;
            Loaded += MacroPanel_Loaded;
        }

        private async void MacroPanel_Loaded(object sender, RoutedEventArgs e)
        {
            await UserAPI.GetMacroValue(Children, Folder);

            
            if (Children.Count == 1)
            {

                var outerPanel = Parent as OuterPanel;

                outerPanel.Children.Remove(this);

                if(outerPanel.Children.Count == 0)
                {
                    outerPanel.SetAsNone(CodeLanguage.C);
                    

                }

                


            }

            //throw new Exception();
        }
    }
}
