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
using Microsoft.VisualBasic;
using Windows.Services.Maps;

namespace CppGallery.Pages.UserControls
{
    

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

        public WinVer TargetMinWinVersion { get; set; } = WinVer.Win10;

        public CppVersion TargetMinCppVersion { get; set; } = UserAPI.MinCppVersion;
        public CVersion TargetMinCVersion { get; set; } = UserAPI.MinCVersion;

        public CppVersion TargetDeletedCppVersion { get; set; } = UserAPI.MaxCppVersion + 1;
        public CVersion TargetDeletedCVersion { get; set; } = UserAPI.MaxCVersion + 1;

        public string Text
        {
            get
            {
                return OutPut?.Text;
            }
            set
            {
                if (OutPut != null)
                {
                    OutPut.Text = value;
                }
            }
        }

        private static double TitleSize => Data.FunctionTitleTextSize;
        private static string LaunchAppMessage => "サンプルアプリを起動して動作確認";
        private static string LaunchButtonContent => "起動";
        private static string DefName => "定義";
        private static string Motion => "サンプル";
        private static string NoPrm => "なし(void)";
        private static string NoReturn => "なし(void)";
        private static string Prm => "パラメーター";
        private static string Results => "実行結果";
        private static string Returns => "戻り値";
        private string SampleName;
        private string[] Lines;

        //コンテンツはExpanderを展開したときにロードする
        private bool IsContentLoaded = false;

        private OutPuts OutPut = null;
        private string[] GCCPath;

        //サンプルコードのファイル名
        private string CodeName;
        private static string DeclName => "/Decl.txt";

        public FunctionExpander()
        {
            this.HorizontalAlignment = HorizontalAlignment.Stretch;
            this.HorizontalContentAlignment = HorizontalAlignment.Stretch;
            this.Loaded += FunctionExpander_Loaded;
            this.Expanding += FunctionExpander_Expanding;
            this.Padding = new Thickness(Data.FunctionExpanderPadding);
            this.MinHeight = Data.GalleryControlHeight;

        }

        private static InfoBar GetVersionErrorInfoBar(string language, int MinVersion = -1)
        {
            var infoBar = new InfoBar
            {
                IsClosable = false,
                IsOpen = true,
                Severity = InfoBarSeverity.Error,
                Message = "このサンプルを使用するには " + language + MinVersion.ToString() + (MinVersion < (int)UserAPI.MaxCppVersion ? " 以降" : " ") + "が必要です",
            };


            return infoBar;
        }

        private static InfoBar GetVersionErrorDeletedInfoBar(string language, int deleteVersion = -1)
        {
            var infoBar = new InfoBar
            {
                IsClosable = false,
                IsOpen = true,
                Severity = InfoBarSeverity.Error,
                Message = $"この項目は {language}{deleteVersion} で削除されました",
            };

            return infoBar;
        }

        private void FunctionExpander_Expanding(Expander sender, ExpanderExpandingEventArgs args)
        {
            if (!IsContentLoaded)
            {
                InfoBar infoBar = null;
                switch (CodeLanguage)
                {
                    //言語バージョン確認
                    case CodeLanguage.C:
                        if (App.UseCppInCSample)
                        {
                            if ((int)App.CppVersion < (int)TargetMinCVersion) infoBar = GetVersionErrorInfoBar("C++", (int)TargetMinCVersion);
                            else if((int)App.CppVersion >= (int)TargetDeletedCVersion) infoBar = GetVersionErrorDeletedInfoBar("C", (int)TargetDeletedCVersion);
                        }
                        else
                        {
                            if(App.CVersion < TargetMinCVersion) infoBar = GetVersionErrorInfoBar("C", (int)TargetMinCVersion);
                            else if(App.CVersion >= TargetDeletedCVersion) infoBar = GetVersionErrorDeletedInfoBar("C", (int)TargetDeletedCVersion);
                        }

                        break;

                    case CodeLanguage.Cpp:
                        if (App.CppVersion < TargetMinCppVersion) infoBar = GetVersionErrorInfoBar("C++", (int)TargetMinCppVersion);
                        else if (App.CppVersion >= TargetDeletedCppVersion) infoBar = GetVersionErrorDeletedInfoBar("C++", (int)TargetDeletedCppVersion);
                        break;
                }

                if(infoBar != null)
                {
                    base.Content = infoBar;
                    IsContentLoaded = true;
                    return;
                }

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
                            codeButton.FileName = string.Empty;

                            panel1.Children.Add(codeButton);
                            break;

                        case CodeLanguage.Cpp:
                            panel1.Children.Add(new CodeButton { Path = Data.DefaultSamplePath + Folder + CodeName, AutoFunc = this.AutoFunc, FileName = string.Empty });
                            break;

                        case CodeLanguage.CppWin32:
                            panel1.Children.Add(new Win32CodeButton { Path = Data.DefaultSamplePath + Folder + CodeName, AutoFunc = this.AutoFunc, FileName = string.Empty });
                            break;

                        case CodeLanguage.CppWinRT:
                            panel1.Children.Add(new WinRTCodeButton { Path = Data.DefaultSamplePath + Folder + CodeName, AutoFunc = this.AutoFunc, FileName = string.Empty });
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
                        BlockOldSystemPane pane = new BlockOldSystemPane { MinimumVersion = this.TargetMinWinVersion };
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

                            
                            OutPut = new OutPuts { Text = this.Text };
                            if (this.Content == null)
                            {
                                Execute(this);
                            }
                            else
                            {
                                panel1.Children.Add(this.Content);
                            }
                            panel1.Children.Add(OutPut);
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

        private StackPanel SetDefinition()
        {
            StackPanel panel = new StackPanel { Spacing = Data.CompactOuterPanelSpacing };

            int i;

            if (Lines.Length == 2)
            {
                return panel;
            }

            //関数定義ファイルがあればそれを表示
            if(File.Exists(Data.DefaultSamplePath + Folder + DeclName))
            {
                switch (this.CodeLanguage)
                {
                    case CodeLanguage.C:
                        if (App.UseCppInCSample)
                        {
                            panel.Children.Add(new CodeButton { Path = Data.DefaultSamplePath + Folder + DeclName, FileName = string.Empty });
                        }
                        else
                        {
                            panel.Children.Add(new CCodeButton { Path = Data.DefaultSamplePath + Folder + DeclName, FileName = string.Empty });
                        }


                        break;

                    case CodeLanguage.Cpp: panel.Children.Add(new CodeButton { Path = Data.DefaultSamplePath + Folder + DeclName, FileName = string.Empty }); break;
                    case CodeLanguage.CppWin32: panel.Children.Add(new Win32CodeButton { Path = Data.DefaultSamplePath + Folder + DeclName, FileName = string.Empty }); break;
                    case CodeLanguage.CppWinRT: panel.Children.Add(new WinRTCodeButton { Path = Data.DefaultSamplePath + Folder + DeclName, FileName = string.Empty }); break;
                }
            }

            
            
            //空回し
            for (i = 2; i < Lines.Length; ++i)
            {
                if (Lines[i].Length == 0)
                {
                    ++i;
                    break;
                }
            }

            if (i == Lines.Length)
            {
                return panel;
            }

            var resultPanel2 = new ResultsPanel();
            panel.Children.Add(resultPanel2);
            TextBlock title2 = new TextBlock();
            resultPanel2.Children.Add(title2);
            title2.FontWeight = new Windows.UI.Text.FontWeight(700);
            title2.FontSize = TitleSize;
            title2.Text = Prm;
            

            for (; i < Lines.Length; ++i)
            {
                if (Lines[i].Length == 0)
                {
                    ++i;
                    break;
                }
                string[] line = Lines[i].Split(": "); 

                if(line.Length == 2)
                {
                    resultPanel2.Children.Add(new DetailPane { Title = line[0], Detail = line[1] });
                }

            }

            if(resultPanel2.Children.Count == 1)
            {
                resultPanel2.Children.Add(new TextBlock { Text = NoPrm });
            }

            var resultPanel3 = new ResultsPanel();
            panel.Children.Add(resultPanel3);
            TextBlock title3 = new TextBlock();
            resultPanel3.Children.Add(title3);
            title3.Text = Returns;
            title3.FontSize = TitleSize;
            title3.FontWeight = new Windows.UI.Text.FontWeight(700);
            TextBlock sentence3 = new TextBlock();
            resultPanel3.Children.Add(sentence3);

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

            this.Header = new EHeader { Title = this.Title, Sentence = this.Sentence, Icon = UserAPI.GetIconSymbol(this.Icon) };

        }

        private void FunctionExpander_Loaded(object sender, RoutedEventArgs e)
        {

            SampleName = UserAPI.GetExeName(CodeLanguage == CodeLanguage.C);
            CodeName = App.UseCppInCSample && this.CodeLanguage == CodeLanguage.C ? "/CodeCpp.txt" : "/Code" + ((int)App.CVersion).ToString() + ".txt";

            if (CodeLanguage == CodeLanguage.C)
            {
                if (App.UseCppInCSample) CodeName = "/CodeCpp" + ((int)App.CppVersion).ToString() + ".txt";
                else CodeName = "/Code" + ((int)App.CVersion).ToString() + ".txt";
            }
            else
            {
                CodeName = "/Code" + ((int)App.CppVersion).ToString() + ".txt";
            }

            //各バージョン専用のソースコードが見つからない場合は別のバージョンのファイルを選択
            if (CodeLanguage == CodeLanguage.C)
            {
                if (App.UseCppInCSample)
                {
                    var cppversion = App.CppVersion;
                    while (cppversion > UserAPI.MinCppVersion)
                    {
                        if (File.Exists(Data.DefaultSamplePath + Folder + CodeName)) break;
                        --cppversion;
                        CodeName = "/CodeCpp" + ((int)cppversion).ToString() + ".txt";
                    }
                }
                else
                {
                    var cversion = App.CVersion;
                    while (cversion > UserAPI.MinCVersion)
                    {
                        if (File.Exists(Data.DefaultSamplePath + Folder + CodeName)) break;
                        --cversion;
                        CodeName = "/Code" + ((int)cversion).ToString() + ".txt";
                    }

                }

            }
            else
            {
                var version = App.CppVersion;
                while (version > UserAPI.MinCppVersion)
                {
                    if (File.Exists(Data.DefaultSamplePath + Folder + CodeName)) break;
                    --version;
                    CodeName = "/Code" + ((int)version).ToString() + ".txt";
                }


            }

            StreamReader sr = new StreamReader(Data.DefaultSamplePath + Folder + "/Def.txt");
            Lines = sr.ReadToEnd().Split(Environment.NewLine);
            sr.Close();
            Title = Lines[0];
            Sentence = Lines[1];

            SetHeader();
            GCCPath = new string[] { (Data.DefaultSampleExePath + SampleName.Replace("/", "")).Replace('/', '\\'), SplitByFront(Folder, '/'), SplitByBack(Folder, '/') };

            Loaded -= FunctionExpander_Loaded;
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

            var result = error ? string.Empty : sr.ReadToEnd();

            if(result.Length == 0)
            {
                sr?.Close();
                functionExpander.Text = errorMessage;
                return;
            }

            if (App.IsShowReturnCode)
            {
                functionExpander.Text = result + '\n';
                functionExpander.Text += "プログラムはコード " + rc.ToString() + " (0x" + rc.ToString("x") + ") で終了しました";
            }
            else
            {
                if (result.EndsWith(Environment.NewLine))
                {
                    functionExpander.Text = result.Substring(0, result.Length - Environment.NewLine.Length);
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

            GetOutput(fexpander, "接続がタイムアウトしました", rc);
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
                rc = CoAPI.Execute(fexpander.GCCPath[0] + ' ' + fexpander.GCCPath[1] + ' ' + fexpander.GCCPath[2] + " NoCmd", App.WaitFor);
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
                rc = CoAPI.Execute(fexpander.GCCPath[0] + ' ' + fexpander.GCCPath[1] + ' ' + fexpander.GCCPath[2] + " NoCmd", App.WaitFor);
            });

            GetOutput(fexpander, errorMessage, rc);

            Working = false;
        }
    }

}
