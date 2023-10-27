using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CppGallery.Pages.UserControls
{
    public class DelegatePane : UserControl
    {
        public CodeLanguage CodeLanguage { get; set; }
        public string Folder { get; set; }


        public DelegatePane()
        {
            this.Loaded += DelegatePane_Loaded;
        }

        private void DelegatePane_Loaded(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            this.Loaded -= DelegatePane_Loaded;

            //ソースを読み込み
            string path = Data.DefaultSamplePath + Folder + "/Delegate.txt";

            CLanCodeButton cLanCodeButton = CodeLanguage switch
            {
                CodeLanguage.C => new CCodeButton { Path = path },
                CodeLanguage.Cpp => new CodeButton { Path = path },
                CodeLanguage.CppWin32 => new Win32CodeButton { Path = path },
                CodeLanguage.CppWinRT => new WinRTCodeButton { Path = path },
                _ => throw new NotImplementedException(),
            };

            BlockPanel blockPanel = new BlockPanel
            {
                Header = "デリゲート実装例",
            };

            blockPanel.Children.Add(cLanCodeButton);

            Content = blockPanel;
        }
    }
}
