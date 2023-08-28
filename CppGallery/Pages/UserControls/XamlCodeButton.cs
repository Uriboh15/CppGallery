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
    public class XamlCodeButton : CodeButtonBase
    {
        private static SolidColorBrush TypeName;
        private static SolidColorBrush Value;
        private static SolidColorBrush Property;

        public XamlCodeButton() : base()
        {
            Lan = "XAML";
            ChangeTheme(App.SourceCodeTheme);
        }

        public static void ChangeTheme(ElementTheme theme)
        {
            if (theme == ElementTheme.Default) theme = MainPage.Handle.ActualTheme;

            if (theme == ElementTheme.Dark)
            {
                TypeName = new SolidColorBrush(Windows.UI.Color.FromArgb(0XFF, 0x56, 0x9C, 0xD6));
                Value = new SolidColorBrush(Windows.UI.Color.FromArgb(0xFF, 0xD6, 0x9D, 0x85));
                Property = new SolidColorBrush(Windows.UI.Color.FromArgb(0XFF, 0X9C, 0xDC, 0xFE));
            }
            else
            {
                TypeName = new SolidColorBrush(Windows.UI.Color.FromArgb(0XFF, 0x00, 0x00, 0xFF));
                Value = new SolidColorBrush(Windows.UI.Color.FromArgb(0XFF, 0XA3, 0x15, 0x15));
                Property = new SolidColorBrush(Windows.UI.Color.FromArgb(0XFF, 0X1F, 0x37, 0x7F));
            }
        }

        bool CanUseName(char ch)
        {
            if ('0' <= ch && ch <= '9') return true;
            if ('A' <= ch && ch <= 'Z') return true;
            if ('a' <= ch && ch <= 'z') return true;
            if (ch == ':') return true;
            if (ch == '.') return true;

            return false;
        }

        protected override void Root_Loaded(object sender, RoutedEventArgs e)
        {
            if (File.Exists(Path) == false)
            {
                Code.Text = Path + " is not found.";
                return;
            }

            StreamReader sr = new StreamReader(Path);
            bool In = false;
            string mae = string.Empty;
            while (sr.EndOfStream == false)
            {
                int i = 0;
                string st = sr.ReadLine();
                
                while (i < st.Length)
                {
                    Run run = new Run();
                    string tmp = string.Empty;

                    if(mae == "<")
                    {
                        In = true;
                        for(; i < st.Length; ++i)
                        {
                            if (!CanUseName(st[i]))
                            {
                                --i;
                                break;
                            }
                            tmp += st[i];
                        }
                        run.Foreground = TypeName;
                        mae = string.Empty;
                    }
                    else if (st[i] == '<')
                    {
                        tmp += '<';
                        if (i + 1 < st.Length)
                        {
                            if (st[i + 1] == '/')
                            {
                                tmp += '/';
                                ++i;
                            }
                        }
                        mae = "<";
                    }
                    else if (st[i] == '\"')
                    {
                        tmp += '\"';

                        for (++i; i < st.Length; ++i)
                        {
                            tmp += st[i];
                            if (st[i] == '\"') break;
                        }
                        run.Foreground = Value;
                    }
                    else if (st[i] == '>')
                    {
                        In = false;
                        tmp += '>';
                    }
                    else
                    {
                        if (In)
                        {
                            if (!CanUseName(st[i]))
                            {
                                tmp += st[i];
                            }
                            else
                            {
                                for (; i < st.Length; ++i)
                                {

                                    if (!CanUseName(st[i]))
                                    {
                                        --i;
                                        break;
                                    }
                                    tmp += st[i];
                                }
                                run.Foreground = Property;
                            }
                            
                        }
                        else
                        {
                            tmp += st[i];
                        }
                    }

                    run.Text = tmp;
                    Code.Inlines.Add(run);
                    ++i;
                }

                if (sr.EndOfStream == false)
                {
                    Code.Inlines.Add(new Run { Text = "\n" });
                }
            }

            sr.Close();
        }
    }
}
