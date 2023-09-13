using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Documents;
using Microsoft.UI.Xaml.Media;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;

namespace CppGallery.Pages.UserControls
{
    public abstract class CLanCodeButton : CodeButtonBase
    {
        private SolidColorBrush blue;
        private SolidColorBrush green;
        private SolidColorBrush purple;
        private SolidColorBrush tyairo;
        private SolidColorBrush gray;
        private SolidColorBrush comment;
        private SolidColorBrush math;
        private SolidColorBrush yellow;
        private SolidColorBrush defined;
        private SolidColorBrush local;
        private SolidColorBrush global;

        private static List<SolidColorBrush> ColorsBlack { get; } = new List<SolidColorBrush>()
        {
            new SolidColorBrush(Windows.UI.Color.FromArgb(0XFF, 0x56, 0x9C, 0xD6)),
            new SolidColorBrush(Windows.UI.Color.FromArgb(0XFF, 0x4E, 0xC9, 0xB0)),
            new SolidColorBrush(Windows.UI.Color.FromArgb(0XFF, 0xD8, 0xA0, 0xDF)),
            new SolidColorBrush(Windows.UI.Color.FromArgb(0xFF, 0xD6, 0x9D, 0x85)),
            new SolidColorBrush(Windows.UI.Color.FromArgb(0xFF, 0x9B, 0x9B, 0x9B)),
            new SolidColorBrush(Windows.UI.Color.FromArgb(0XFF, 0X57, 0xA6, 0x4A)),
            new SolidColorBrush(Windows.UI.Color.FromArgb(0XFF, 0XB5, 0xCE, 0xA8)),
            new SolidColorBrush(Windows.UI.Color.FromArgb(0XFF, 0XDC, 0xDC, 0xAA)),
            new SolidColorBrush(Windows.UI.Color.FromArgb(0XFF, 0XBE, 0xB7, 0xFF)),
            new SolidColorBrush(Windows.UI.Color.FromArgb(0XFF, 0X9C, 0xDC, 0xFE)),
            new SolidColorBrush(Windows.UI.Color.FromArgb(0XFF, 0XB8, 0xD7, 0xA3)),
        };

        private static List<SolidColorBrush> ColorsWhite { get; } = new List<SolidColorBrush>()
        {
            new SolidColorBrush(Windows.UI.Color.FromArgb(0XFF, 0x00, 0x00, 0xFF)),
            new SolidColorBrush(Windows.UI.Color.FromArgb(0XFF, 0x2B, 0x91, 0xAF)),
            new SolidColorBrush(Windows.UI.Color.FromArgb(0XFF, 0x8F, 0x08, 0xC4)),
            new SolidColorBrush(Windows.UI.Color.FromArgb(0XFF, 0XA3, 0x15, 0x15)),
            new SolidColorBrush(Windows.UI.Color.FromArgb(0xFF, 0x80, 0x80, 0x80)),
            new SolidColorBrush(Windows.UI.Color.FromArgb(0XFF, 0X00, 0x80, 0x00)),
            new SolidColorBrush(Windows.UI.Color.FromArgb(0XFF, 0X00, 0x00, 0x00)),
            new SolidColorBrush(Windows.UI.Color.FromArgb(0XFF, 0X74, 0x53, 0x1F)),
            new SolidColorBrush(Windows.UI.Color.FromArgb(0XFF, 0X8A, 0x1B, 0xFF)),
            new SolidColorBrush(Windows.UI.Color.FromArgb(0XFF, 0X1F, 0x37, 0x7F)),
            new SolidColorBrush(Windows.UI.Color.FromArgb(0XFF, 0X2F, 0x4F, 0x4F))
        };

        protected List<List<string>> KeyBlue { get; } = new List<List<string>>();
        protected List<List<string>> KeyPurple { get; } = new List<List<string>>();
        protected List<List<string>> KeyGreen { get; } = new List<List<string>>();
        protected List<List<string>> KeyDefine { get; } = new List<List<string>>();
        protected List<List<string>> KeyFunc { get; } = new List<List<string>>();
        protected List<List<string>> KeyYellow { get; } = new List<List<string>>();
        protected List<List<string>> KeyUserFunction { get; } = new List<List<string>>();
        protected List<List<string>> KeyGlobal { get; } = new List<List<string>>();
        protected List<List<string>> KeyStatic { get; } = new List<List<string>>();
        protected List<List<string>> KeyClassTemplate { get; } = new List<List<string>>();
        protected List<List<string>> KeyEnum { get; } = new List<List<string>>();

        private List<string> AddedDefine { get; } = new List<string>();

        protected bool cLanguage = true;

        public static readonly DependencyProperty AutoFuncProperty = DependencyProperty.Register(
            "AutoFunc",　// Max という名前の……
            typeof(bool),　// int 型の CLR プロパティを……
            typeof(CLanCodeButton), // クラスに登録するやで―
            new PropertyMetadata(true));

        public bool AutoFunc
        {
            get { return (bool)GetValue(AutoFuncProperty); }
            set { SetValue(AutoFuncProperty, value); }
        }

        public CLanCodeButton() : base()
        {
            ChangeTheme(App.SourceCodeTheme);
            KeyDefine.Add(AddedDefine);
        }

        private static bool Contains(List<List<string>> lists, string str)
        {
            foreach (var list in lists)
            {
                if (list.Contains(str)) return true;
            }
            return false;
        }

        public void ChangeTheme(ElementTheme theme)
        {
            if (theme == ElementTheme.Default) theme = MainWindow.Handle.GetTheme();

            if (theme == ElementTheme.Dark)
            {
                blue = ColorsBlack[0];
                green = ColorsBlack[1];
                purple = ColorsBlack[2];
                tyairo = ColorsBlack[3];
                gray = ColorsBlack[4];
                comment = ColorsBlack[5];
                math = ColorsBlack[6];
                yellow = ColorsBlack[7];
                defined = ColorsBlack[8];
                local = ColorsBlack[9];
                global = ColorsBlack[10];
            }
            else
            {
                blue = ColorsWhite[0];
                green = ColorsWhite[1];
                purple = ColorsWhite[2];
                tyairo = ColorsWhite[3];
                gray = ColorsWhite[4];
                comment = ColorsWhite[5];
                math = ColorsWhite[6];
                yellow = ColorsWhite[7];
                defined = ColorsWhite[8];
                local = ColorsWhite[9];
                global = ColorsWhite[10];
            }
        }

        private static bool StKey(string str, int i)
        {
            if (i + 1 < str.Length)
            {
                return !Alpha(str[i + 1]);
            }
            return true;
        }

        private static bool Alpha(char c)
        {
            return ('0' <= c && c <= '9') || ('A' <= c && c <= 'Z') || ('a' <= c && c <= 'z') || (c == '_' || c == '.');
        }

        private static bool IsDigit(char c)
        {
            return '0' <= c && c <= '9';
        }

        private bool SetColor(Run run, string tmp, string st, int i)
        {
            if (Contains(KeyBlue, tmp))
            {
                run.Foreground = blue;
                return true;

            }
            if (Contains(KeyGlobal, tmp))
            {
                run.Foreground = global;
                return true;
            }
            if (Contains(KeyGreen, tmp))
            {

                run.Foreground = green;
                return true;

            }
            if (Contains(KeyEnum, tmp))
            {

                run.Foreground = green;
                return true;

            }
            if (Contains(KeyClassTemplate, tmp))
            {
                if(i + 1 < st.Length)
                {
                    if (st[i + 1] == '<')
                    {
                        run.Foreground = green;
                        return true;
                    }
                }
                if(i + 2 < st.Length)
                {
                    if (st[i + 1] == ':' && st[i + 2] == ':')
                    {
                        run.Foreground = green;
                        return true;
                    }
                }
            }
            if (Contains(KeyPurple, tmp))
            {

                run.Foreground = purple;
                return true;

            }
            if (Contains(KeyDefine, tmp))
            {

                run.Foreground = defined;
                return true;

            }
            if (Contains(KeyYellow, tmp))
            {

                run.Foreground = yellow;
                return true;

            }
            if (IsDigit(tmp[0]))
            {
                run.Foreground = math;
                return true;
            }
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
            string mae = string.Empty;
            while (sr.EndOfStream == false)
            {
                int i = 0;
                string st = sr.ReadLine();
                while (i < st.Length)
                {
                    Run run = new Run();
                    string tmp = string.Empty;
                    if (mae == "include")
                    {
                        for (; i < st.Length; ++i)
                        {
                            tmp += st[i];
                        }
                        mae = string.Empty;
                        run.Text = tmp;
                        run.Foreground = tyairo;
                        Code.Inlines.Add(run);
                        ++i;
                        continue;
                    }
                    else if (mae == "define")
                    {
                        for (; i < st.Length && Alpha(st[i]); ++i)
                        {
                            tmp += st[i];
                        }
                        mae = string.Empty;
                        AddedDefine.Add(tmp);
                        run.Text = tmp;
                        run.Foreground = defined;
                        Code.Inlines.Add(run);
                        continue;
                    }
                    else if (mae == "ifdef")
                    {
                        for (; i < st.Length && Alpha(st[i]); ++i)
                        {
                            tmp += st[i];
                        }
                        if (Contains(KeyDefine, tmp))
                        {
                            run.Foreground = defined;
                        }
                        run.Text = tmp;
                        mae = string.Empty;
                        Code.Inlines.Add(run);
                        continue;
                    }
                    else if (mae == "pragma")
                    {
                        for (; i < st.Length; ++i)
                        {
                            tmp += st[i];
                        }
                        mae = string.Empty;
                        run.Text = tmp;
                        run.Foreground = gray;
                        Code.Inlines.Add(run);
                        ++i;
                        continue;
                    }
                    else if (mae == "comment")
                    {
                        for (; !tmp.Contains("*/") && i < st.Length; ++i)
                        {
                            tmp += st[i];
                        }
                        if (tmp.Contains("*/"))
                        {
                            mae = string.Empty;
                        }
                        run.Foreground = comment;
                        run.Text = tmp;
                        Code.Inlines.Add(run);
                        continue;
                    }
                    while (i < st.Length)
                    {
                        if (Alpha(st[i]) == false)
                        {
                            if (st[i] == '#')
                            {
                                tmp += st[i];
                                for (++i; i < st.Length; ++i)
                                {
                                    tmp += st[i];
                                    if (st[i] == ' ') break;

                                }
                                run.Foreground = gray;
                                if (tmp == "#include ")
                                {
                                    mae = "include";
                                }
                                else if (tmp == "#pragma ")
                                {
                                    mae = "pragma";
                                }
                                else if (tmp == "#define ")
                                {
                                    mae = "define";
                                }
                                else if (tmp == "#ifdef " || tmp == "#ifndef ")
                                {
                                    mae = "ifdef";
                                }

                            }
                            else if (st[i] == '{')
                            {
                                tmp += '{';
                                mae = string.Empty;
                            }
                            else if (st[i] == '/')
                            {
                                tmp += st[i];
                                if (i + 1 < st.Length)
                                {
                                    if (st[i + 1] == '/')
                                    {
                                        for (++i; i < st.Length; ++i)
                                        {
                                            tmp += st[i];
                                        }
                                        run.Foreground = comment;
                                    }
                                    else if (st[i + 1] == '*')
                                    {
                                        for (++i; !tmp.Contains("*/") && i < st.Length; ++i)
                                        {
                                            tmp += st[i];
                                        }
                                        if (!tmp.Contains("*/"))
                                        {
                                            mae = "comment";
                                        }

                                        run.Foreground = comment;
                                    }
                                }
                            }
                            else if (st[i] == '\"')
                            {
                                tmp += st[i];
                                for (++i; i < st.Length; ++i)
                                {
                                    tmp += st[i];
                                    if (st[i] == '\"' && st[i - 1] != '\\')
                                    {
                                        break;
                                    }
                                }
                                run.Foreground = tyairo;
                                if (i < st.Length - 1)
                                {
                                    while (Alpha(st[++i]) && st[i] != '.')
                                    {
                                        tmp += st[i];
                                        if (i == st.Length - 1) break;
                                    }
                                    --i;
                                }


                            }
                            else if (st[i] == '\'')
                            {
                                tmp += st[i];
                                for (++i; i < st.Length; ++i)
                                {
                                    tmp += st[i];
                                    if (st[i] == '\'' && st[i - 1] != '\\')
                                    {
                                        break;
                                    }
                                }
                                run.Foreground = tyairo;
                            }
                            else if (st[i] == ':')
                            {
                                if (i + 1 < st.Length)
                                {
                                    if (st[i + 1] == ':')
                                    {
                                        mae = "::";
                                        tmp += "::";
                                        ++i;
                                        break;
                                    }
                                }
                                tmp += st[i];
                            }
                            else if (st[i] == '-')
                            {
                                if (i + 1 < st.Length)
                                {
                                    if (IsDigit(st[i + 1]))
                                    {
                                        run.Foreground = math;
                                    }
                                    else if (st[i + 1] == '>')
                                    {
                                        mae = ".";
                                    }
                                }
                                tmp += '-';
                            }
                            else if (st[i] == ';')
                            {
                                mae = string.Empty;
                                tmp += ";";
                            }
                            else
                            {
                                tmp += st[i];
                            }
                            break;
                        }
                        tmp += st[i];
                        if (StKey(st, i))
                        {
                            if (mae == "." || mae == "::")
                            {
                                if (i + 1 < st.Length)
                                {

                                    if (mae == "::")
                                    {
                                        if (Contains(KeyGreen, tmp))
                                        {
                                            if (st[i + 1] == '(')
                                            {
                                                run.Foreground = ((Code.Inlines[^2] as Run).Foreground == global) ? green : yellow;
                                            }
                                            else
                                            {
                                                run.Foreground = green;
                                            }
                                            mae = string.Empty;
                                            break;
                                        }
                                        else if ((Code.Inlines[^2] as Run).Foreground == green)
                                        {
                                            if (st[i + 1] == '(' || st[i + 1] == '<')
                                            {
                                                mae = string.Empty;
                                                run.Foreground = yellow;
                                                break;
                                            }
                                            else
                                            {
                                                if (Contains(KeyEnum, (Code.Inlines[^2] as Run).Text))
                                                {
                                                    run.Foreground = global;
                                                }
                                                else
                                                {
                                                    run.Foreground = local;
                                                }
                                                mae = string.Empty;
                                                
                                                break;
                                            }
                                        }
                                        else
                                        {
                                            mae = string.Empty;
                                            if((Code.Inlines[^2] as Run).Foreground == global)
                                            {
                                                if (SetColor(run, tmp, st, i)) break;
                                            }
                                            if (st[i + 1] == '(')
                                            {
                                                mae = string.Empty;
                                                run.Foreground = this.AutoFunc ? yellow : local;
                                                break;
                                            }
                                            else if (st[i + 1] == '<')
                                            {
                                                run.Foreground = yellow;
                                                break;
                                            }
                                            else
                                            {
                                                run.Foreground = local;
                                            }
                                            break;
                                        }

                                    }
                                    if (st[i + 1] == '(' || st[i + 1] == '<')
                                    {
                                        mae = string.Empty;
                                        run.Foreground = yellow;
                                        break;
                                    }
                                    run.Foreground = local;
                                    mae = string.Empty;
                                    break;


                                }
                            }

                            if (SetColor(run, tmp, st, i)) break;

                            if (!AutoFunc)
                            {
                                if (Contains(KeyFunc, tmp))
                                {
                                    if (mae == "::")
                                    {
                                        mae = string.Empty;
                                        if (Contains(KeyStatic, tmp))
                                        {
                                            break;
                                        }
                                    }

                                    run.Foreground = yellow;
                                    break;
                                }
                                else if (Contains(KeyUserFunction, tmp))
                                {
                                    run.Foreground = yellow;
                                    break;
                                }

                            }

                            if (AutoFunc)
                            {
                                if (i + 1 < st.Length)
                                {
                                    if (st[i + 1] == '(' || st[i + 1] == '<')
                                    {
                                        run.Foreground = yellow;
                                        mae = string.Empty;
                                        break;
                                    }
                                }
                            }

                            if (i + 1 < st.Length)
                            {
                                if (st[i + 1] == '\"' || st[i + 1] == '\'')
                                {
                                    run.Foreground = tyairo;
                                    break;
                                }
                            }
                            run.Foreground = local;
                            break;
                        }
                        else
                        {
                            if (tmp == ".")
                            {
                                if (i + 2 < st.Length)
                                {
                                    if (st[i + 1] == '.' && st[i + 2] == '.')
                                    {
                                        tmp = "...";
                                        i += 2;
                                        break;
                                    }
                                }
                                mae = ".";
                                break;
                            }
                            if (i + 1 < st.Length)
                            {
                                if (st[i + 1] == '.')
                                {
                                    if (!cLanguage)
                                    {
                                        if (Contains(KeyGreen, tmp))
                                        {
                                            run.Foreground = green;
                                            break;
                                        }
                                    }
                                    if (!IsDigit(tmp[0]))
                                    {
                                        if (mae == ".")
                                        {
                                            run.Foreground = local;
                                            break;
                                        }
                                        if (Contains(KeyGlobal, tmp))
                                        {
                                            run.Foreground = global;
                                        }
                                        else
                                        {
                                            if (i + 3 < st.Length)
                                            {
                                                if (st[i + 2] == '.' && st[i + 3] == '.')
                                                {
                                                    SetColor(run, tmp, st, i);
                                                    break;
                                                }
                                            }
                                            run.Foreground = local;
                                        }
                                        break;
                                    }
                                }
                            }
                        }
                        ++i;
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
