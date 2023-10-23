using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CppGallery.Pages.UserControls
{
    public class GrammarPane : UserControl
    {
        public CodeLanguage CodeLanguage { get; set; }
        public string Folder {  get; set; }

        public GrammarPane() 
        {
            this.Loaded += GrammarPane_Loaded;
        }

        private void GrammarPane_Loaded(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            this.Loaded -= GrammarPane_Loaded;

            //ソースを読み込み
            string path = Data.DefaultSamplePath + Folder + "/Grm.txt";

            CLanCodeButton cLanCodeButton = CodeLanguage switch
            {
                CodeLanguage.C => new CCodeButton { Path = path },
                CodeLanguage.Cpp => new CodeButton { Path = path },
                CodeLanguage.CppWin32 => new Win32CodeButton { Path = path },
                CodeLanguage.CppWinRT => new WinRTCodeButton { Path = path },
                _ => throw new NotImplementedException(),
            };

            SymbolBlockPanel symbolBlockPanel = new SymbolBlockPanel
            {
                Symbol = PanelHeader.TemplateGrammer,
            };

            symbolBlockPanel.Children.Add(cLanCodeButton);

            Content = symbolBlockPanel;
        }
    }
}
