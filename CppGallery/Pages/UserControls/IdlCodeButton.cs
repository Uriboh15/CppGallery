using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CppGallery.Pages.UserControls
{
    public class IdlCodeButton : CLanCodeButton
    {
        private static List<string> KeyWordsBlue = new List<string>()
        {
            "default_interface",
            "Int32",
            "namespace",
            "runtimeclass",
            "String",
        };

        private static List<string> KeyWordsGreen = new List<string>()
        {
            "ItemGrid",
            "UserControl",
        };

        private static List<string> KeyWordsGlobal = new List<string>()
        {
            "Controls",
            "Microsoft",
            "UI",
            "WinUI3App",
            "Xaml",
        };

        public IdlCodeButton() : base()
        {
            Lan = "MIDL";
            KeyBlue.Add(KeyWordsBlue);
            KeyGreen.Add(KeyWordsGreen);
            KeyGlobal.Add(KeyWordsGlobal);

        }
    }
}
