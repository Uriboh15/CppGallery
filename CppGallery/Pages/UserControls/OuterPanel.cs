using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CppGallery.Pages.UserControls
{
    public enum HeaderType
    {
        None,
        C,
        Cpp,
        Windows,
        WinRT,
        NonStandardC,
        NonStandardCpp,
        WindowsFunction,
    }

    public class OuterPanel : StackPanel
    {
        public HeaderType HeaderType { get; set; } = HeaderType.None;
        public CVersion TargetMinCVersion { get; set; } = UserAPI.MinCVersion;
        public CppVersion TargetMinCppVersion { get; set; }  = UserAPI.MinCppVersion;
        public CodeLanguage HeaderLanguage { get; set; } = CodeLanguage.Cpp;

        public OuterPanel()
        {
            this.Spacing = Data.OuterPanelSpacing;
            this.Padding = new Thickness(Data.OuterPanelPadding, 0, Data.OuterPanelPadding, Data.OuterPanelPadding);
            this.Loading += OuterPanel_Loading;
        }

        private void OuterPanel_Loading(FrameworkElement sender, object args)
        {
            this.Loading -= OuterPanel_Loading;

            //Windowsヘッダーの時
            if (this.HeaderType == HeaderType.Windows || this.HeaderType == HeaderType.WinRT)
            {
                if (App.Compiler != Compiler.VC)
                {
                    this.Children.Clear();
                    this.Spacing = Data.ResultPanelSpacing;
                    this.Padding = new Thickness(0);
                    this.HorizontalAlignment = HorizontalAlignment.Center;
                    this.VerticalAlignment = VerticalAlignment.Center;
                    this.Children.Add(new TextBlock
                    {
                        Text = "このヘッダーは Windows 固有のものです",
                        HorizontalAlignment = HorizontalAlignment.Center,
                    });
                    this.Children.Add(new TextBlock
                    {
                        Text = "Visual C++ をお使いください",
                        HorizontalAlignment = HorizontalAlignment.Center,
                    });
                    var button = new Button
                    {
                        Content = "設定へ移動",
                        HorizontalAlignment = HorizontalAlignment.Center,
                    };
                    button.Click += Button_Click;
                    this.Children.Add(button);
                    return;
                }
            }

            //C++/WInRTならC++17以降が必要
            if(this.HeaderType == HeaderType.WinRT)
            {
                if(this.TargetMinCppVersion < CppVersion.Cpp17)
                {
                    this.TargetMinCppVersion = CppVersion.Cpp17;
                }
            }

            //言語バージョンの確認
            if (HeaderLanguage == CodeLanguage.C)
            {
                if (App.UseCppInCSample)
                {
                    if ((int)App.CppVersion < (int)TargetMinCVersion) SetAsNone(CodeLanguage.C, (int)TargetMinCVersion);
                }
                else
                {
                    if (App.CVersion < TargetMinCVersion) SetAsNone(CodeLanguage.C, (int)TargetMinCVersion);
                }
            }
            else
            {
                if (App.CppVersion < TargetMinCppVersion) SetAsNone(CodeLanguage.Cpp, (int)TargetMinCppVersion);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.OpenSettings(this);

        }

        public void SetAsNone(CodeLanguage language, int MinVersion = -1)
        {
            this.Children.Clear();
            this.Spacing = Data.ResultPanelSpacing;
            this.Padding = new Thickness(0);
            this.HorizontalAlignment = HorizontalAlignment.Center;
            this.VerticalAlignment = VerticalAlignment.Center;

            string message;

            if(MinVersion != -1)
            {
                if (language == CodeLanguage.C)
                {
                    if (App.UseCppInCSample) message = "このヘッダーを使用するには C++" + MinVersion.ToString() + (MinVersion < (int)UserAPI.MaxCppVersion ? " 以降" : " ") + "が必要です";
                    else message = "このヘッダーを使用するには C" + MinVersion.ToString() + (MinVersion < (int)UserAPI.MaxCVersion ? " 以降" : " ") + "が必要です";
                }
                else
                {
                    message = "このヘッダーを使用するには C++" + MinVersion.ToString() + (MinVersion < (int)UserAPI.MaxCppVersion ? " 以降" : " ") + "が必要です";
                }
            }
            else
            {
                if (language == CodeLanguage.C)
                {
                    if (App.UseCppInCSample) message = "C++" + ((int)App.CppVersion).ToString() + " では何も定義されていません";
                    else message = "C" + ((int)App.CVersion).ToString() + " では何も定義されていません";
                }
                else
                {
                    message = "C++" + ((int)App.CppVersion).ToString() + " では何も定義されていません";
                }
            }

            

            this.Children.Add(new TextBlock
            {
                Text = message,
                HorizontalAlignment = HorizontalAlignment.Center,
            });

            var button = new Button
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                Content = "設定へ移動",
            };
            this.Children.Add(button);

            button.Click += Button_Click;
        }
    }
}
