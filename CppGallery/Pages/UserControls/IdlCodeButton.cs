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
        private static List<string> KeyWordsBlue { get; } = new List<string>()
        {
            
            "default_interface",
            "namespace",
            "runtimeclass",
            
        };

        private static List<string> KeyWordsGreen { get; } = new List<string>()
        {
            "contentproperty",
            "Control",
            "IInspectable",
            "Int32",
            "ItemGrid",
            "String",
            "UserControl",
        };

        private static List<string> KeyWordsGlobal { get; } = new List<string>()
        {
            "Controls",
            
            "Microsoft",
            "UI",
            "Windows",
            "WindowsSample",
            "Xaml",
        };

        public IdlCodeButton()
        {
            cLanguage = false;
            Lan = "MIDL";
            KeyBlue.Add(KeyWordsBlue);
            KeyGreen.Add(KeyWordsGreen);
            KeyGlobal.Add(KeyWordsGlobal);

        }
    }
}
