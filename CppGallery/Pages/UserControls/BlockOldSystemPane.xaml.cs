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

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace CppGallery.Pages.UserControls
{
    public sealed partial class BlockOldSystemPane : UserControl
    {
        public static readonly new DependencyProperty ContentProperty = DependencyProperty.Register(
            "Content",�@// Max �Ƃ������O�́c�c
            typeof(UIElement),�@// int �^�� CLR �v���p�e�B���c�c
            typeof(ToRightButton), // �N���X�ɓo�^�����Ł\
            new PropertyMetadata(null));

        public static readonly DependencyProperty MinimumVersionProperty = DependencyProperty.Register(
            "MinimumVersion",�@// Max �Ƃ������O�́c�c
            typeof(WinVer),�@// int �^�� CLR �v���p�e�B���c�c
            typeof(ToRightButton), // �N���X�ɓo�^�����Ł\
            new PropertyMetadata(WinVer.Win10));

        public new UIElement Content
        {
            get { return (UIElement)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        public WinVer MinimumVersion
        {
            get { return (WinVer)GetValue(MinimumVersionProperty); }
            set { SetValue(MinimumVersionProperty, value); }
        }

        public BlockOldSystemPane()
        {
            this.InitializeComponent();
        }

        private static string GetErrorMessage(WinVer winVer)
        {
            string strVer;
            switch (winVer)
            {
                case WinVer.Win11_21H2:
                    strVer = "Windows 11";
                    break;

                case WinVer.Win11_22H2:
                    strVer = "Windows 11 Version 22H2 �ȍ~";
                    break;

                    case WinVer.Win11_23H2:
                    strVer = "Windows 11 Version 23H2";
                    break;

                default:
                    strVer = "Windows 10";
                    break;

            }

            return "���̃T���v�����N������ɂ� " + strVer + " ���K�v�ł�";
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if(App.WinVer < MinimumVersion)
            {
                ErrorInfo.Message = GetErrorMessage(MinimumVersion);
                Content = null;
            }
            else
            {
                Panel.Children.Clear();
                Panel.Children.Add(Content);
            }
        }
    }
}
