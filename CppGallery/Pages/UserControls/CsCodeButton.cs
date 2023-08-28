using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CppGallery.Pages.UserControls
{
    public class CsCodeButton : CLanCodeButton
    {
        private static List<string> KeywordsBlue = new List<string>()
        {
            "as",
            "base",
            "bool",
            "char",
            "class",
            "const",
            "extern",
            "false",
            "short",
            "int",
            "is",
            "long",
            "namespace",
            "new",
            "null",
            "pertial",
            "private",
            "protected",
            "public",
            "static",
            "string",
            "true",
            "var",
            "void",
            "uint",
            "ulong",
            "using",
        };

        private static List<string> KeywordsGreen = new List<string>()
        {
            "Console",
            "DllImport",
            "IntPtr",
            "Marshal",
            "UseCppDll",
        };

        private static List<string> KeywordsPurple = new List<string>()
        {
            "else",
            "for",
            "foreach",
            "if",
            "in",
        };

        private static List<string> KeywordsGlobal = new List<string>()
        {
            "Cryptography",
            "Diagnostics",
            "Globalization",
            "InteropServices",
            "Runtime",
            "Security",
            "System",
            "Uriboh",
        };

        public CsCodeButton() : base()
        {
            Lan = "C#";
            cLanguage = false;
            KeyBlue.Add(KeywordsBlue);
            KeyGreen.Add(KeywordsGreen);
            KeyPurple.Add(KeywordsPurple);
            KeyGlobal.Add(KeywordsGlobal);
        }
    }
}
