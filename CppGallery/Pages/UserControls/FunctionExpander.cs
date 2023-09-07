using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Reflection.PortableExecutable;
using Windows.Devices.Enumeration;

namespace CppGallery.Pages.UserControls
{
    public enum HeaderIcon
    {
        Function,
        Literal,
        Macro,
        Object,
        Operator,
    }

    public enum CodeLanguage
    {
        C,
        Cpp,
        CppWin32,
        CppWinRT,
    }

    public enum SampleType
    {
        Default,
        None,
        ConsoleOnly,
        GUIApp,
    }

    public class FunctionExpander : Expander
    {

        public static readonly DependencyProperty IconProperty = DependencyProperty.Register(
           "Icon", // Max という名前の……
           typeof(HeaderIcon), // int 型の CLR プロパティを……
           typeof(FunctionExpander), // クラスに登録するやで―
           new PropertyMetadata(HeaderIcon.Function));

        public static readonly DependencyProperty InfoProperty = DependencyProperty.Register(
           "Info", // Max という名前の……
           typeof(InfoBar), // int 型の CLR プロパティを……
           typeof(FunctionExpander), // クラスに登録するやで―
           new PropertyMetadata(null));

        public static readonly DependencyProperty FolderProperty = DependencyProperty.Register(
           "Folder", // Max という名前の……
           typeof(string), // int 型の CLR プロパティを……
           typeof(FunctionExpander), // クラスに登録するやで―
           new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty CodeLanguageProperty = DependencyProperty.Register(
           "CodeLanguage", // Max という名前の……
           typeof(string), // int 型の CLR プロパティを……
           typeof(CodeLanguage), // クラスに登録するやで―
           new PropertyMetadata(CodeLanguage.Cpp));

        public static new readonly DependencyProperty ContentProperty = DependencyProperty.Register(
           "Content", // Max という名前の……
           typeof(UIElement), // int 型の CLR プロパティを……
           typeof(FunctionExpander), // クラスに登録するやで―
           new PropertyMetadata(null));

        public static readonly DependencyProperty AutoFuncProperty = DependencyProperty.Register(
            "AutoFunc",　// Max という名前の……
            typeof(bool),　// int 型の CLR プロパティを……
            typeof(FunctionExpander), // クラスに登録するやで―
            new PropertyMetadata(true));

        public static readonly DependencyProperty ConsoleButtonVisibilityProperty = DependencyProperty.Register(
            "ConsoleButtonVisibility",　// Max という名前の……
            typeof(Visibility),　// int 型の CLR プロパティを……
            typeof(FunctionExpander), // クラスに登録するやで―
            new PropertyMetadata(Visibility.Visible));

        public bool AutoFunc
        {
            get { return (bool)GetValue(AutoFuncProperty); }
            set { SetValue(AutoFuncProperty, value); }
        }

        public Visibility ConsoleButtonVisibility
        {
            get { return (Visibility)GetValue(ConsoleButtonVisibilityProperty); }
            set { SetValue(ConsoleButtonVisibilityProperty, value); }
        }

        private string Title;

        private string Sentence;

        public HeaderIcon Icon
        {
            get { return (HeaderIcon)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        public string Folder
        {
            get { return (string)GetValue(FolderProperty); }
            set { SetValue(FolderProperty, value); }
        }

        public CodeLanguage CodeLanguage
        {
            get { return (CodeLanguage)GetValue(CodeLanguageProperty); }
            set { SetValue(CodeLanguageProperty, value); }
        }

        public new UIElement Content
        {
            get { return (UIElement)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        public SampleType SampleType { get; set; }

        public InfoBar Info
        {
            get { return (InfoBar)GetValue(InfoProperty); }
            set { SetValue(InfoProperty, value); }
        }

        public WinVer TargetMinVersion { get; set; } = WinVer.Win10;

        private string _text;

        public string Text
        {
            get
            {
                return _text;
            }
            set
            {
                _text = value;
                if (outPut != null)
                {

                    outPut.Text = value;
                }
            }
        }

        private double TitleSize;
        private string LaunchAppMessage;
        private string LaunchButtonContent;
        private string CodeName;
        private string DefName;
        private string Motion;
        private string NoPrm;
        private string NoReturn;
        private string OverLoad;
        private string Prm;
        private string Results;
        private string Returns;
        private string SampleName;
        private string[] Lines;
        private bool IsContentLoaded = false;
        private OutPuts outPut = null;
        private string[] GCCPath;

        public FunctionExpander()
        {
            this.HorizontalAlignment = HorizontalAlignment.Stretch;
            this.HorizontalContentAlignment = HorizontalAlignment.Stretch;
            this.Loaded += FunctionExpander_Loaded;
            this.Expanding += FunctionExpander_Expanding;
            this.Padding = new Thickness(Data.FunctionExpanderPadding);
            this.MinHeight = Data.GalleryControlHeight;
            this.TitleSize = Data.FunctionTitleTextSize;
            DefName = "定義";
            LaunchAppMessage = "サンプルアプリを起動して動作確認";
            LaunchButtonContent = "起動";
            Motion = "動作";
            NoPrm = "なし(void)\n";
            NoReturn = "なし(void)";
            OverLoad = "オーバーロード";
            Prm = "パラメーター";
            Results = "実行結果";
            Returns = "戻り値";

            
        }

        private string ParseIcon()
        {
            switch (this.Icon)
            {
                case HeaderIcon.Function:
                    return "\uF158";

                case HeaderIcon.Literal:
                    return "\uEA3A";

                case HeaderIcon.Macro:
                    return "\uE71A";

                case HeaderIcon.Object:
                    return "\uEA86";

                case HeaderIcon.Operator:
                    return "\uE710";

                default:
                    return "\uF158";
            }
        }

        private void FunctionExpander_Expanding(Expander sender, ExpanderExpandingEventArgs args)
        {
            if (!IsContentLoaded)
            {
                ResultsPanel panel1 = new ResultsPanel();
                TextBlock title1 = new TextBlock();
                var defpanel = SetDefinition();
                if (Info != null)
                {
                    panel1.Children.Add(Info);
                    title1.Margin = new Thickness(0, 30, 0, 0);
                }
                if (defpanel.Children.Count != 0)
                {
                    title1.Text = DefName;
                    title1.FontSize = TitleSize;
                    title1.FontWeight = new Windows.UI.Text.FontWeight(700);
                    panel1.Children.Add(title1);
                    panel1.Children.Add(defpanel);


                }
                if (File.Exists(Data.DefaultSamplePath + Folder + CodeName))
                {
                    if (defpanel.Children.Count != 0)
                    {
                        TextBlock title2 = new TextBlock();
                        title2.Margin = new Thickness(0, 30, 0, 0);
                        title2.Text = Motion;
                        title2.FontSize = TitleSize;
                        title2.FontWeight = new Windows.UI.Text.FontWeight(700);
                        panel1.Children.Add(title2);
                    }

                    switch (this.CodeLanguage)
                    {
                        case CodeLanguage.C:
                            CCodeButton codeButton = App.UseCppInCSample ? new CodeButton() : new CCodeButton();
                            codeButton.Path = Data.DefaultSamplePath + Folder + CodeName;
                            codeButton.AutoFunc = this.AutoFunc;

                            panel1.Children.Add(codeButton);
                            break;

                        case CodeLanguage.Cpp:
                            panel1.Children.Add(new CodeButton { Path = Data.DefaultSamplePath + Folder + CodeName, AutoFunc = this.AutoFunc });
                            break;

                        case CodeLanguage.CppWin32:
                            panel1.Children.Add(new Win32CodeButton { Path = Data.DefaultSamplePath + Folder + CodeName, AutoFunc = this.AutoFunc });
                            break;

                        case CodeLanguage.CppWinRT:
                            panel1.Children.Add(new WinRTCodeButton { Path = Data.DefaultSamplePath + Folder + CodeName, AutoFunc = this.AutoFunc });
                            break;
                    }

                    var consoleAndGUI = () =>
                    {

                        panel1.Children.Add(new TextBlock { Text = Results, Margin = new Thickness(0, 30, 0, 0) });
                        Button launchButton = new Button();
                        launchButton.Content = LaunchButtonContent;
                        launchButton.MinWidth = 65;
                        launchButton.Click += ExeButton_Click;
                        ControlGrid cgrid = new ControlGrid { Content = launchButton };
                        cgrid.Message = LaunchAppMessage;
                        BlockOldSystemPane pane = new BlockOldSystemPane { MinimumVersion = this.TargetMinVersion };
                        pane.Content = cgrid;

                        panel1.Children.Add(pane);
                    };

                    switch (this.SampleType)
                    {
                        case SampleType.Default:
                            Grid grid = new Grid();
                            grid.Margin = new Thickness(0, 16, 0, 0);
                            TextBlock output = new TextBlock();
                            output.Text = Results;
                            output.HorizontalAlignment = HorizontalAlignment.Left;
                            output.VerticalAlignment = VerticalAlignment.Bottom;
                            grid.Children.Add(output);

                            Button ExeButton = new Button();
                            ExeButton.Content = new FontIcon { Glyph = "\uE756", FontFamily = new Microsoft.UI.Xaml.Media.FontFamily("Segoe MDL2 Assets") };
                            ExeButton.HorizontalAlignment = HorizontalAlignment.Right;
                            ExeButton.IsEnabled = this.ConsoleButtonVisibility == Visibility.Visible;
                            grid.Children.Add(ExeButton);
                            ExeButton.Click += ExeButton_Click;


                            panel1.Children.Add(grid);

                            
                            outPut = new OutPuts { Text = this.Text };
                            if (this.Content == null)
                            {
                                Execute(this);
                            }
                            else
                            {
                                panel1.Children.Add(this.Content);
                            }
                            panel1.Children.Add(outPut);
                            break;

                        case SampleType.ConsoleOnly:
                            consoleAndGUI();

                            break;

                        case SampleType.GUIApp:
                            GCCPath[0] = "WindowsSample" + (App.ProcesserType == ProcesserType.x86 ? "-x86" : string.Empty);
                            consoleAndGUI();

                            break;


                        case SampleType.None:

                            break;

                    }
                }

                base.Content = panel1;

                IsContentLoaded = true;
            }
        }

        private InnerPanel SetDefinition()
        {
            InnerPanel panel = new InnerPanel();

            int i;

            if (Lines.Length == 2)
            {
                return panel;
            }
            TextBlock title1 = new TextBlock();
            panel.Children.Add(title1);
            title1.FontWeight = new Windows.UI.Text.FontWeight(700);
            TextBlock sentence1 = new TextBlock();
            panel.Children.Add(sentence1);
            sentence1.IsTextSelectionEnabled = true;

            for (i = 2; i < Lines.Length; ++i)
            {
                if (Lines[i].Length == 1)
                {
                    ++i;
                    break;
                }
                sentence1.Text += Lines[i] + '\n';
            }

            if (i > 4)
            {
                title1.Text = OverLoad;
            }
            else
            {
                title1.Text = DefName;
            }

            if (i == Lines.Length)
            {
                sentence1.Text = sentence1.Text.Substring(0, sentence1.Text.Length - 1);
                return panel;
            }
            TextBlock title2 = new TextBlock();
            panel.Children.Add(title2);
            title2.FontWeight = new Windows.UI.Text.FontWeight(700);
            title2.Text = Prm;
            TextBlock sentence2 = new TextBlock();
            panel.Children.Add(sentence2);

            for (; i < Lines.Length; ++i)
            {
                if (Lines[i].Length == 1)
                {
                    ++i;
                    break;
                }
                sentence2.Text += Lines[i] + '\n';
            }

            if (sentence2.Text.Length < 2)
            {
                sentence2.Text = NoPrm;
            }

            TextBlock title3 = new TextBlock();
            panel.Children.Add(title3);
            title3.Text = Returns;
            title3.FontWeight = new Windows.UI.Text.FontWeight(700);
            TextBlock sentence3 = new TextBlock();
            panel.Children.Add(sentence3);

            for (; i < Lines.Length; ++i)
            {
                if (i != Lines.Length - 1)
                {
                    sentence3.Text += Lines[i] + '\n';
                }
                else
                {
                    sentence3.Text += Lines[i];
                }
            }

            if (sentence3.Text.Length == 0)
            {
                sentence3.Text = NoReturn;
            }

            return panel;
        }

        private void SetHeader()
        {

            this.Header = new EHeader { Title = this.Title, Sentence = this.Sentence, Icon = ParseIcon() };

        }

        private void FunctionExpander_Loaded(object sender, RoutedEventArgs e)
        {
            SampleName = UserAPI.GetExeName(CodeLanguage == CodeLanguage.C);
            CodeName = App.UseCppInCSample && this.CodeLanguage == CodeLanguage.C ? "/CodeCpp.txt" : "/Code.txt";

            //すべての項目がC++に対応したら消す
#if true
            if(CodeName == "/CodeCpp.txt")
            {
                if(!File.Exists(Data.DefaultSamplePath + Folder + CodeName))
                {
                    CodeName = "/Code.txt";
                }
            }

#endif

            StreamReader sr = new StreamReader(Data.DefaultSamplePath + Folder + "/Def.txt");
            Lines = sr.ReadToEnd().Split('\n');
            sr.Close();
            if (Lines.Length == 2)
            {
                Title = Lines[0].Substring(0, Lines[0].Length - 1);
                Sentence = Lines[1];
            }
            else
            {
                Title = Lines[0].Substring(0, Lines[0].Length - 1);
                Sentence = Lines[1].Substring(0, Lines[1].Length - 1);
            }

            SetHeader();
            GCCPath = new string[] { (Data.DefaultSampleExePath + SampleName.Replace("/", "")).Replace('/', '\\'), SplitByFront(Folder, '/'), SplitByBack(Folder, '/') };
        }

        private static string SplitByBack(string text, char separator)
        {
            int i = text.Length - 1;

            for (; i >= 0; --i)
            {
                if (text[i] == separator)
                {
                    ++i;
                    break;
                }
            }

            return text.Substring(i, text.Length - i);
        }

        private static string SplitByFront(string text, char separator)
        {
            int i = 0;

            for (; i < text.Length; ++i)
            {
                if (text[i] == separator)
                {
                    break;
                }
            }

            return text.Substring(0, i);
        }

        private string Replace(string text)
        {
            int i = text.Length - 1;

            while (i >= 0)
            {
                if (text[i] == '/')
                {
                    break;
                }
                --i;
            }

            return text.Substring(0, i) + SampleName;
        }

        private void ExeButton_Click(object sender, RoutedEventArgs e)
        {
            if(this.SampleType == SampleType.GUIApp)
            {
                ProcessHost.WinStart(GCCPath);
                return;
            }
            ProcessHost.Start(GCCPath);
        }

        private static bool Working = false;

        private static void GetOutput(FunctionExpander functionExpander, string errorMessage, uint rc)
        {
            StreamReader sr = null;
            bool error = false;

            try
            {
                sr = new StreamReader(CoAPI.GetOutputFileName(), Encoding.GetEncoding("sjis"));
            }
            catch
            {
                error = true;
            }

            var result = error ? errorMessage : sr.ReadToEnd();

            if (App.IsShowReturnCode)
            {
                functionExpander.Text = result + '\n';
                functionExpander.Text += "プログラムはコード " + rc.ToString() + " (0x" + rc.ToString("x") + ") で終了しました";
            }
            else
            {
                if (result.EndsWith("\r\n"))
                {
                    functionExpander.Text = result.Substring(0, result.Length - 2);
                }
                else
                {
                    functionExpander.Text = result;
                }
            }

            

            sr?.Close();
        }

        public static async void Execute(object element)
        {
            object tmp = element;

            //親のFucntionExpnaderを探す
            while (tmp is not FunctionExpander)
            {
                tmp = (tmp as FrameworkElement).Parent;
            }

            FunctionExpander fexpander = tmp as FunctionExpander;

            uint rc = await CoAPI.ExecuteAsync(fexpander.GCCPath[0] + ' ' + fexpander.GCCPath[1] + ' ' + fexpander.GCCPath[2]);

            GetOutput(fexpander, "一時的なエラーのため、結果を表示できません", rc);
        }

        public static async void Execute(object element, string cmd, string errorMessage = "接続がタイムアウトしました")
        {
            if (Working)
            {
                return;
            }
            Working = true;
            object tmp = element;

            //親のFucntionExpnaderを探す
            while (tmp is not FunctionExpander)
            {
                tmp = (tmp as FrameworkElement).Parent;
            }

            FunctionExpander fexpander = tmp as FunctionExpander;
            uint rc = uint.MaxValue;

            await Task.Run(() =>
            {
                using (var sw = new StreamWriter(CoAPI.GetInputFileName(), false, Encoding.GetEncoding("sjis")))
                {
                    sw.WriteLine(cmd);
                }
                rc = CoAPI.Execute(fexpander.GCCPath[0] + ' ' + fexpander.GCCPath[1] + ' ' + fexpander.GCCPath[2] + " NoCmd");
            });

            GetOutput(fexpander, errorMessage, rc);

            Working = false;
        }

        public async static void Execute(object element, string[] cmd, string errorMessage = "接続がタイムアウトしました")
        {
            if (Working)
            {
                return;
            }
            Working = true;
            object tmp = element;

            //親のFucntionExpnaderを探す
            while (tmp is not FunctionExpander)
            {
                tmp = (tmp as FrameworkElement).Parent;
            }

            FunctionExpander fexpander = tmp as FunctionExpander;

            uint rc = uint.MaxValue;

            await Task.Run(() =>
            {
                using (var sw = new StreamWriter(CoAPI.GetInputFileName(), false, Encoding.GetEncoding("sjis")))
                {
                    foreach (string str in cmd)
                    {
                        sw.WriteLine(str);
                    }
                }
                rc = CoAPI.Execute(fexpander.GCCPath[0] + ' ' + fexpander.GCCPath[1] + ' ' + fexpander.GCCPath[2] + " NoCmd");
            });

            GetOutput(fexpander, errorMessage, rc);

            Working = false;
        }
    }

}
