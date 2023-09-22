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
        private static SolidColorBrush blue;
        private static SolidColorBrush green;
        private static SolidColorBrush gray;
        private static SolidColorBrush yellow;

        public PowerShellCommandButton() : base()
        {
            Lan = "PowerShell";
            FileName = string.Empty;
            ChangeTheme(App.SourceCodeTheme);
        }

        public void ChangeTheme(ElementTheme theme)
        {
            if (theme == ElementTheme.Default) theme = ActualTheme;

            if (theme == ElementTheme.Dark)
            {
                blue = new SolidColorBrush(Windows.UI.Color.FromArgb(0XFF, 0x3A, 0x96, 0xDD));
                green = new SolidColorBrush(Windows.UI.Color.FromArgb(0XFF, 0x16, 0xC6, 0x0C));
                gray = new SolidColorBrush(Windows.UI.Color.FromArgb(0xFF, 0x76, 0x76, 0x76));
                yellow = new SolidColorBrush(Windows.UI.Color.FromArgb(0XFF, 0XF9, 0xF1, 0xA5));
            }
            else
            {
                blue = new SolidColorBrush(Windows.UI.Color.FromArgb(0XFF, 0x00, 0x37, 0xDA));
                green = new SolidColorBrush(Windows.UI.Color.FromArgb(0XFF, 0x13, 0xA1, 0x0E));
                gray = new SolidColorBrush(Windows.UI.Color.FromArgb(0xFF, 0x76, 0x76, 0x76));
                yellow = new SolidColorBrush(Windows.UI.Color.FromArgb(0XFF, 0XC1, 0x9C, 0x00));
            }
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
