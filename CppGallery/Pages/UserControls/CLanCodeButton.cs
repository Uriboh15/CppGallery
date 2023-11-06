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

        private string Mae = string.Empty;

        private static List<SolidColorBrush> ColorsDarkTheme { get; } = new List<SolidColorBrush>()
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
        protected List<List<string>> KeyConcept { get; } = new List<List<string>>();
        protected List<List<string>> KeyFunctionMacro { get; } = new List<List<string>>();

        private List<string> AddedDefine { get; } = new List<string>();
        private List<string> AddedGreen { get; } = new List<string>();
        protected List<string> DeletedMacro { get; } = new List<string>();

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

        public CLanCodeButton()
        {
            ChangeTheme();
            KeyDefine.Add(AddedDefine);
            KeyGreen.Add(AddedGreen);
        }

        private bool Contains(List<List<string>> lists, string str, bool isMacro = false)
        {
            //#undefで削除されたマクロ
            if (isMacro && DeletedMacro.Contains(str))
            {
                return false;
            }
            foreach (var list in lists)
            {
                if (list.Contains(str)) return true;
            }
            return false;
        }

        public void ChangeTheme()
        {
            blue = ColorsDarkTheme[0];
            green = ColorsDarkTheme[1];
            purple = ColorsDarkTheme[2];
            tyairo = ColorsDarkTheme[3];
            gray = ColorsDarkTheme[4];
            comment = ColorsDarkTheme[5];
            math = ColorsDarkTheme[6];
            yellow = ColorsDarkTheme[7];
            defined = ColorsDarkTheme[8];
            local = ColorsDarkTheme[9];
            global = ColorsDarkTheme[10];

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
            if (Contains(KeyDefine, tmp, true))
            {
                run.Foreground = defined;
                return true;
            }

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
            if (Contains(KeyConcept, tmp))
            {
                if (Code.Inlines.Count > 2 && (Code.Inlines[^2] as Run).Text != "requires")
                {
                    Mae = "Concept";
                }
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
                int i = 0;
                string st = sr.ReadLine();
                while (i < st.Length)
                {
                    Run run = new Run();
                    string tmp = string.Empty;
                    if (Mae == "include")
                    {
                        for (; i < st.Length; ++i)
                        {
                            tmp += st[i];
                        }
                        Mae = string.Empty;
                        run.Text = tmp;
                        run.Foreground = tyairo;
                        Code.Inlines.Add(run);
                        ++i;
                        continue;
                    }
                    else if (Mae == "define")
                    {
                        for (; i < st.Length && Alpha(st[i]); ++i)
                        {
                            tmp += st[i];
                        }
                        Mae = string.Empty;
                        AddedDefine.Add(tmp);
                        run.Text = tmp;
                        run.Foreground = defined;
                        Code.Inlines.Add(run);
                        continue;
                    }
                    else if (Mae == "ifdef")
                    {
                        for (; i < st.Length && Alpha(st[i]); ++i)
                        {
                            tmp += st[i];
                        }
                        if (Contains(KeyDefine, tmp, true))
                        {
                            run.Foreground = defined;
                        }
                        run.Text = tmp;
                        Mae = string.Empty;
                        Code.Inlines.Add(run);
                        continue;
                    }
                    else if (Mae == "pragma")
                    {
                        for (; i < st.Length; ++i)
                        {
                            tmp += st[i];
                        }
                        Mae = string.Empty;
                        run.Text = tmp;
                        run.Foreground = gray;
                        Code.Inlines.Add(run);
                        ++i;
                        continue;
                    }
                    else if(Mae == "undef")
                    {
                        for (; i < st.Length; ++i)
                        {
                            tmp += st[i];
                        }

                        DeletedMacro.Add(tmp);

                        Mae = string.Empty;
                        run.Text = tmp;
                        Code.Inlines.Add(run);
                        ++i;
                        continue;
                    }
                    else if (Mae == "comment")
                    {
                        for (; !tmp.Contains("*/") && i < st.Length; ++i)
                        {
                            tmp += st[i];
                        }
                        if (tmp.Contains("*/"))
                        {
                            Mae = string.Empty;
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
                                    Mae = "include";
                                }
                                else if (tmp == "#pragma ")
                                {
                                    Mae = "pragma";
                                }
                                else if (tmp == "#define ")
                                {
                                    Mae = "define";
                                }
                                else if (tmp == "#ifdef " || tmp == "#ifndef ")
                                {
                                    Mae = "ifdef";
                                }
                                else if(tmp == "#undef ")
                                {
                                    Mae = "undef";
                                }

                            }
                            else if (st[i] == '{')
                            {
                                tmp += '{';
                                Mae = string.Empty;
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
                                        if (tmp.Contains("*/"))
                                        {
                                            --i;
                                        }
                                        else
                                        {
                                            Mae = "comment";
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
                                        Mae = "::";
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
                                    // (ObjectPointer)->(FieldName)
                                    else if (st[i + 1] == '>')
                                    {
                                        Mae = ".";
                                    }
                                }
                                tmp += '-';
                            }
                            else if (st[i] == ';')
                            {
                                Mae = string.Empty;
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
                            //マクロが最優先
                            if (Contains(KeyDefine, tmp, true))
                            {
                                run.Foreground = defined;
                                Mae = string.Empty;
                                break;
                            }

                            //関数マクロ
                            if (i + 1 < st.Length && st[i + 1] == '(' && Contains(KeyFunctionMacro, tmp, true))
                            {
                                run.Foreground = defined;
                                Mae = string.Empty;
                                break;
                            }

                            //次にキーワード
                            if (Contains(KeyBlue, tmp))
                            {
                                run.Foreground = blue;
                                Mae = string.Empty;
                                break;
                            }

                            if (Mae == "." || Mae == "::")
                            {
                                if (i + 1 < st.Length)
                                {

                                    if (Mae == "::")
                                    {
                                        if (Contains(KeyGreen, tmp))
                                        {
                                            if (st[i + 1] == '(')
                                            {
                                                //run.Foreground = ((Code.Inlines[^2] as Run).Foreground == global) ? green : yellow;
                                                run.Foreground = green;
                                            }
                                            else
                                            {
                                                run.Foreground = green;
                                            }
                                            Mae = string.Empty;
                                            break;
                                        }
                                        else if ((Code.Inlines[^2] as Run).Foreground == green)
                                        {
                                            if (st[i + 1] == '(' || st[i + 1] == '<')
                                            {
                                                Mae = string.Empty;
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
                                                Mae = string.Empty;
                                                
                                                break;
                                            }
                                        }
                                        else
                                        {
                                            Mae = string.Empty;
                                            if((Code.Inlines[^2] as Run).Foreground == global)
                                            {
                                                if (SetColor(run, tmp, st, i)) break;
                                            }
                                            if (st[i + 1] == '(')
                                            {
                                                Mae = string.Empty;
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
                                        Mae = string.Empty;
                                        run.Foreground = yellow;
                                        break;
                                    }
                                    run.Foreground = local;
                                    Mae = string.Empty;
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
                                    Mae = string.Empty;

                                    break;
                                }
                            }

                            if (SetColor(run, tmp, st, i)) break;

                            if (Code.Inlines.Count >= 2)
                            {
                                if(Code.Inlines[^2].Foreground == blue)
                                {
                                    string tmp1 = (Code.Inlines[^2] as Run).Text;

                                    if (tmp1 == "typename" || tmp1 == "class" || tmp1 == "struct" || tmp1 == "using" || tmp1 == "enum" || tmp1 == "union")
                                    {
                                        AddedGreen.Add(tmp);

                                        run.Foreground = green;
                                        break;
                                    }
                                }
                                if(Code.Inlines[^2].Foreground == green && Contains(KeyConcept,(Code.Inlines[^2] as Run).Text))
                                {
                                    AddedGreen.Add(tmp);

                                    run.Foreground = green;
                                    break;
                                }
                            }

                            if(Mae == "Concept")
                            {
                                run.Foreground = green;
                                AddedGreen.Add(tmp);
                                if ((Code.Inlines[^1] as Run).Text == " ")
                                {
                                    Mae = string.Empty;
                                }
                                
                                break;
                            }

                            if (!AutoFunc)
                            {
                                if (Contains(KeyFunc, tmp))
                                {
                                    if (Mae == "::")
                                    {
                                        Mae = string.Empty;
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
                                        Mae = string.Empty;
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
                                Mae = cLanguage ? "." : "::";
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
                                        if (Mae == ".")
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
