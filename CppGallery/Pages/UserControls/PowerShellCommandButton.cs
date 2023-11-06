using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Documents;
using Microsoft.UI.Xaml.Media;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CppGallery.Pages.UserControls
{
    public class PowerShellCommandButton : CodeButtonBase
    {
        private static SolidColorBrush blue = new SolidColorBrush(Windows.UI.Color.FromArgb(0XFF, 0x3A, 0x96, 0xDD));
        private static SolidColorBrush green = new SolidColorBrush(Windows.UI.Color.FromArgb(0XFF, 0x16, 0xC6, 0x0C));
        private static SolidColorBrush gray = new SolidColorBrush(Windows.UI.Color.FromArgb(0xFF, 0x76, 0x76, 0x76));
        private static SolidColorBrush yellow = new SolidColorBrush(Windows.UI.Color.FromArgb(0XFF, 0XF9, 0xF1, 0xA5));

        public PowerShellCommandButton() : base()
        {
            Lan = "PowerShell";
            FileName = string.Empty;
        }

        public static bool IsDigit(char c)
        {
            if ('0' <= c && c <= '9') return true;

            return false;
        }

        public void OverrideText(string text)
        {
            Code.Text = string.Empty;
            string[] code = text.Split('\n');

            for (int i = 0; i < code.Length; ++i)
            {
                SetCode(code[i]);

                if (i < code.Length - 1)
                {
                    Code.Inlines.Add(new Run { Text = "\n" });
                }
            }

        }

        private void SetCode(string st)
        {
            int i = 0;

            while (i < st.Length)
            {
                Run run = new Run();
                string tmp = string.Empty;

                if (i == 0)
                {
                    if (IsDigit(st[0]))
                    {
                        for (; i < st.Length; ++i)
                        {

                            tmp += st[i];
                            if (st[i] == ' ') break;
                        }
                    }
                    else
                    {
                        for (; i < st.Length; ++i)
                        {

                            tmp += st[i];
                            if (st[i] == ' ') break;
                        }
                        run.Foreground = yellow;
                    }
                }
                else
                {
                    if (st[i] == ' ')
                    {
                        tmp = " ";
                    }
                    else if (st[i] == '\"')
                    {
                        tmp += "\"";
                        ++i;
                        for (; i < st.Length; ++i)
                        {

                            tmp += st[i];
                            if (st[i] == '\"') break;
                        }
                        run.Foreground = blue;
                    }
                    else if (st[i] == '-')
                    {
                        for (; i < st.Length; ++i)
                        {

                            tmp += st[i];
                            if (st[i] == ' ') break;
                        }
                        run.Foreground = gray;
                    }
                    else
                    {
                        for (; i < st.Length; ++i)
                        {

                            tmp += st[i];
                            if (st[i] == ' ') break;
                        }
                    }
                }
                run.Text = tmp;
                Code.Inlines.Add(run);
                ++i;
            }

            
        }

        protected override void LoadFile()
        {
            if (File.Exists(Path) == false)
            {
                Code.Text = Path + " is not found.";
                return;
            }

            StreamReader sr = new StreamReader(Path);

            while (sr.EndOfStream == false)
            {
                SetCode(sr.ReadLine());

                if (sr.EndOfStream == false)
                {
                    Code.Inlines.Add(new Run { Text = "\n" });
                }
            }

            sr.Close();
        }
    }
}
