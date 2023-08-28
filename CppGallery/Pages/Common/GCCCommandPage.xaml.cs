using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using CppGallery.Pages.UserControls;
using WinRT.Interop;
using Windows.Storage;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace CppGallery.Pages.Common
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    

    
    public sealed partial class GCCCommandPage : Page
    {
        private string LanguageOptionOutput = string.Empty;
        private string CppVersionOutput = string.Empty;
        private string CVersionOutput = string.Empty;
        private BuildOption CurrentBuildOption;
        private string PlatformOptionOutput = string.Empty;

        private bool Working = false;

        enum BuildOption
        {
            Console,
            Desctop,
            Dll,
            Lib,
            Object,
            Assembly,
        }

        public GCCCommandPage()
        {
            this.InitializeComponent();
            SourceFileExpander.MinHeight = Data.ControlGridHeight;
        }

        private void GenerateCommand()
        {
            string arg = LanguageOptionOutput;
            arg += arg == "g++" ? SetArg(CppVersionOutput) : SetArg(CVersionOutput);

            //BuildOption

            arg += SetArg(PlatformOptionOutput);

            switch (CurrentBuildOption)
            {
                case BuildOption.Console:
                    arg += GetFileName();
                    break;

                case BuildOption.Desctop:
                    arg += GetFileName();
                    break;

                case BuildOption.Dll:
                    arg += " -c";
                    arg += GetFileName();
                    arg += "\n";
                    arg += LanguageOptionOutput;
                    arg += GetObjectFileName();
                    break;

                case BuildOption.Lib:
                    arg += GetFileName();
                    break;


                case BuildOption.Object:
                    arg += " -c";
                    arg += GetFileName();
                    break;

                case BuildOption.Assembly:
                    arg += " -S";
                    arg += GetFileName();
                    break;
            }
            

            if(AssemblyName.Text.Length != 0 && (CurrentBuildOption == BuildOption.Console) || (CurrentBuildOption == BuildOption.Desctop))
            {
                arg += " -o \"" + AssemblyName.Text + '\"';
            }

            if(CurrentBuildOption == BuildOption.Dll)
            {
                if(!AssemblyName.Text.EndsWith(".dll"))
                {
                    if(AssemblyName.Text.Length == 0)
                    {
                        arg += " -o \"sample.dll\"";
                    }
                    else
                    {
                        arg += " -o \"" + AssemblyName.Text + ".dll\"";
                    }
                }
                else
                {
                    arg += " -o \"" + AssemblyName.Text + '\"';
                }

                arg += " -shared";
            }

            CommandOutput.OverrideText(arg);
        }

        private string GetFileName()
        {
            string arg = string.Empty;
            if (SourceFilePane.Children.Count == 0)
            {
                arg += LanguageOptionOutput == "g++" ? " source.cpp" : " source.c";
            }
            else
            {
                foreach (var element in SourceFilePane.Children)
                {
                    arg += " \"" + (element as GCCSourceFilePane).FileName + '\"';
                }
            }

            return arg;
        }

        private string GetObjectFileName()
        {
            string arg = string.Empty;
            if (SourceFilePane.Children.Count == 0)
            {
                arg += " source.o";
            }
            else
            {
                foreach (var element in SourceFilePane.Children)
                {
                    string tmp = (element as GCCSourceFilePane).FileName;
                    int endIndex;
                    int startIndex;
                    for(endIndex = tmp.Length - 1;  endIndex >= 0; --endIndex)
                    {
                        if (tmp[endIndex] == '.') break;
                    }

                    for(startIndex = endIndex - 1;  startIndex >= 0; --startIndex)
                    {
                        if (tmp[startIndex] == '\\')
                        {
                            ++startIndex;
                            break;
                        }
                    }

                    tmp = tmp.Substring(startIndex, endIndex - startIndex);

                    arg += " \"" + tmp + ".o\"";
                }
            }

            return arg;
        }

        private string SetArg(string arg)
        {
            return arg.Length != 0 ? ' ' +  arg : string.Empty;
        }

        private void CommandOutput_Loaded(object sender, RoutedEventArgs e)
        {
            GenerateCommand();
        }

        private void LanguageOption_Loaded(object sender, RoutedEventArgs e)
        {
            LanguageOption.SelectionChanged += Option_SelectionChanged;
        }

        private void Option_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GenerateCommand();
        }

        private void CppVersion_Loaded(object sender, RoutedEventArgs e)
        {
            CppVersion.SelectionChanged += Option_SelectionChanged;
        }

        private void CVersion_Loaded(object sender, RoutedEventArgs e)
        {
            CVersion.SelectionChanged += Option_SelectionChanged;
        }

        private void BuildSelect_Loaded(object sender, RoutedEventArgs e)
        {
            BuildSelect.SelectionChanged += Option_SelectionChanged;
        }

        private void PlatformSelect_Loaded(object sender, RoutedEventArgs e)
        {
            PlatformSelect.SelectionChanged += Option_SelectionChanged;
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            if (App.IsCompact)
            {
                (sender as Grid).Margin = new Thickness(0);
            }
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Working) return;
            Working = true;
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.FileTypeFilter.Add(".c");
            picker.FileTypeFilter.Add(".cpp");

            InitializeWithWindow.Initialize(picker, WindowNative.GetWindowHandle(MainWindow.Handle));

            IReadOnlyList<StorageFile> files = await picker.PickMultipleFilesAsync();

            if (files.Count != 0)
            {
                foreach(var file in files)
                {
                    SourceFilePane.Children.Add(new GCCSourceFilePane { FileName = file.Path });
                }
                (SourceFilePane.Children[0] as Grid).BorderThickness = new Thickness(1);
                SourceFileExpander.IsExpanded = true;
                //GenerateCommand();
            }
            Working = false;
        }

        private void LanguageOption_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedItem = e.AddedItems[0].ToString();

            switch(selectedItem)
            {
                case "C言語":
                    LanguageOptionOutput = "gcc";
                    break;

                case "C++":
                    LanguageOptionOutput = "g++";
                    break;
            }

        }

        private void CppVersion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedItem = e.AddedItems[0].ToString();

            switch (selectedItem)
            {
                case "既定値":
                    CppVersionOutput = string.Empty;
                    break;

                case "C++11":
                    CppVersionOutput = "-std=c++11";
                    break;

                case "C++14":
                    CppVersionOutput = "-std=c++14";
                    break;

                case "C++17":
                    CppVersionOutput = "-std=c++17";
                    break;

                case "C++20":
                    CppVersionOutput = "-std=c++20";
                    break;
            }
        }

        private void CVersion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedItem = e.AddedItems[0].ToString();

            switch (selectedItem)
            {
                case "既定値":
                    CVersionOutput = string.Empty;
                    break;

                case "C11":
                    CVersionOutput = "-std=c11";
                    break;

                case "C17":
                    CVersionOutput = "-std=c17";
                    break;
            }
        }

        private void BuildSelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedItem = e.AddedItems[0].ToString();

            switch (selectedItem)
            {
                case "コンソールアプリ (.exe)":
                    CurrentBuildOption = BuildOption.Console;
                    break;

                case "Windows デスクトップアプリ (.exe)":
                    CurrentBuildOption = BuildOption.Desctop;
                    break;

                case "ダイナミックライブラリ (.dll)":
                    CurrentBuildOption = BuildOption.Dll;
                    break;

                case "スタティックライブラリ (.lib)":
                    CurrentBuildOption = BuildOption.Lib;
                    break;

                case "オブジェクトファイル (.o)":
                    CurrentBuildOption = BuildOption.Object;
                    break;

                case "アセンブリファイル (.s)":
                    CurrentBuildOption = BuildOption.Assembly;
                    break;
            }
        }

        private void PlatformSelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedItem = e.AddedItems[0].ToString();

            switch (selectedItem)
            {
                case "x86":
                    PlatformOptionOutput = "-m32";
                    break;

                case "x64":
                    PlatformOptionOutput = string.Empty;
                    break;
            }
        }

        private void AssemblyName_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            GenerateCommand();
        }

        private void AssemblyName_Loaded(object sender, RoutedEventArgs e)
        {
            AssemblyName.TextChanging += AssemblyName_TextChanging;
        }

        private void SourceFilePane_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            GenerateCommand();
        }

        private void SourceFileExpander_Expanding(Expander sender, ExpanderExpandingEventArgs args)
        {
            if(SourceFilePane.Children.Count == 0) SourceFileExpander.IsExpanded = false;
        }
    }
}
