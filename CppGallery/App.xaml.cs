// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using CppGallery.Pages;
using CppGallery.Pages.UserControls;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Shapes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography;
using System.Text;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using static System.Runtime.InteropServices.JavaScript.JSType;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace CppGallery
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    /// 

    public enum Compiler
    {
        Clang,
        GCC,
        VC,
    }

    public enum LibraryPageStyle
    {
        Block,
        List,
    }

    public enum WinVer
    {
        Win10,
        Win11_21H2,
        Win11_22H2,
        Win11_23H2,
    }

    public enum ProcesserType
    {
        x86,
        x64,
    }

    public enum CppVersion
    {
        Cpp14 = 14,
        Cpp17 = 17,
        Cpp20 = 20,
        Cpp23 = 23,
    }

    public enum CVersion
    {
        C11 = 11,
        C17 = 17,
        C23 = 23,
    }

    public partial class App : Application
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        /// 



        private static WinVer _winver;
        public static ElementTheme Theme { get; set; }   //buf[0]
        public static BackDrop Back { get; set; }        //buf[1]
        public static bool IsCompact { get; set; }       //buf[2]
        public static Compiler Compiler { get; set; }                       //buf[3]
        public static LibraryPageStyle LibraryPageStyle { get; set; }       //buf[4]
        public static ElementTheme SourceCodeTheme { get; set; }            //buf[5]
        public static ElementTheme ResultTheme { get; set; }                //buf[6]
        public static ProcesserType ProcesserType { get; set; }             //buf[7]
        public static bool UseCppInCSample { get; set; }                    //buf[8]
        public static bool IsShowReturnCode { get; set; }                   //buf[9]
        public static CppVersion CppVersion { get; set; }                   //buf[10]
        public static CVersion CVersion { get; set; }                       //buf[11]
        public static uint WaitFor { get; set; }                            //buf[12, 13, 14, 15]


        public static bool IsFirstCSampleOpened { get; set; }       //log[0]


        public static string SettingsFilePath => @"Pages/settings.bin";
        public static string UserLogPath => @"Pages/log.bin";
        private static bool _isFirstLaunch = false;

        public static int SettingsLen => 16;
        public static int LogLen => 1;
        public static bool Win10 => _winver == WinVer.Win10;
        public static WinVer WinVer => _winver;
        public static bool IsFirstLaunch => _isFirstLaunch;

        public App()
        {
            this.InitializeComponent();
            InitializeOtherComponent();

            

            GetWinVer();
            ApplySettings();
            ApplyLog();
            Pages.Settings.SettingPage.IsCompact = IsCompact;
        }

        private static void InitializeOtherComponent()
        {
            //StreamReaderでShift-Jisを読み込めるようにする
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            MainPage.Initialize();
        }

        private static void ApplyTheme(byte x)
        {
            Theme = (ElementTheme)x;
        }

        private static void ApplyBackDrop(byte x)
        {
            if (Win10)
            {
                if (x == (byte)BackDrop.Mica)
                {
                    Back = BackDrop.None;
                }
                else
                {
                    Back = (BackDrop)x;
                }
            }
            else
            {
                Back = (BackDrop)x;
            }
        }

        private static void ApplyIsCompact(byte x)
        {
            IsCompact = x != 0;
        }

        private static void ApplyCompiler(byte x)
        {
            Compiler = (Compiler)x;
        }

        private static void ApplyLibraryPageStyle(byte x)
        {
            LibraryPageStyle = (LibraryPageStyle)x;
        }

        private static void ApplySourceTheme(byte x)
        {
            SourceCodeTheme = (ElementTheme)x;
        }

        private static void ApplyResultTheme(byte x)
        {
            ResultTheme = (ElementTheme)x;
        }

        private static void ApplyProcesserType(byte x)
        {
            ProcesserType = (ProcesserType)x;
        }

        private static void ApplyUseCppInCSample(byte x)
        {
            UseCppInCSample = x == 1;
        }

        private static void ApplyIsShowReturnCode(byte x)
        {
            IsShowReturnCode = x == 1;
        }

        private static void ApplyCppVersion(byte x)
        {
            CppVersion = (CppVersion)x;
        }

        private static void ApplyCVersion(byte x)
        {
            CVersion = (CVersion)x;
        }

        private static void ApplyWaitFor(byte[] arr)
        {
            WaitFor = BitConverter.ToUInt32(arr, 12);
        }

        private static void ApplySettings()
        {
            byte[] buf = new byte[SettingsLen];
            FileStream fs;
            if (File.Exists(SettingsFilePath))
            {
                fs = new FileStream(SettingsFilePath, FileMode.Open, FileAccess.ReadWrite);
            }
            else
            {
                fs = new FileStream(SettingsFilePath, FileMode.Create, FileAccess.ReadWrite);
            }
            
            if (fs.Length < SettingsLen)
            {
                buf[0] = (byte)ElementTheme.Default;
                buf[1] = (byte)BackDrop.FrostedGlass;
                buf[3] = (byte)Compiler.VC;
                buf[4] = (byte)LibraryPageStyle.Block;
                buf[5] = (byte)ElementTheme.Dark;
                buf[6] = (byte)ElementTheme.Dark;
                buf[7] = (byte)ProcesserType.x64;
                buf[8] = 1;
                buf[9] = 0;
                buf[10] = 17;
                buf[11] = 17;

                var tmp = BitConverter.GetBytes(400u);
                buf[12] = tmp[0];
                buf[13] = tmp[1];
                buf[14] = tmp[2];
                buf[15] = tmp[3];
                _isFirstLaunch = true;
            }
            else
            {
                fs.Read(buf, 0, SettingsLen);
            }
            

            ApplyTheme(buf[0]);
            ApplyBackDrop(buf[1]);
            ApplyIsCompact(buf[2]);
            ApplyCompiler(buf[3]);
            ApplyLibraryPageStyle(buf[4]);
            ApplySourceTheme(buf[5]);
            ApplyResultTheme(buf[6]);
            ApplyProcesserType(buf[7]);
            ApplyUseCppInCSample(buf[8]);
            ApplyIsShowReturnCode(buf[9]);
            ApplyCppVersion(buf[10]);
            ApplyCVersion(buf[11]);
            ApplyWaitFor(buf);

            fs.Close();
        }

        private static void ApplyLog()
        {
            byte[] buf = new byte[LogLen];

            FileStream fs;
            if (File.Exists(UserLogPath))
            {
                fs = new FileStream(UserLogPath, FileMode.Open, FileAccess.ReadWrite);
            }
            else
            {
                fs = new FileStream(UserLogPath, FileMode.Create, FileAccess.ReadWrite);
            }

            if(fs.Length < LogLen)
            {
                buf[0] = 1;
            }

            IsFirstCSampleOpened = buf[0] == 1;

            fs.Close();
        }

        private static void GetWinVer()
        {
            OperatingSystem os = Environment.OSVersion;

            var build = os.Version.Build;
            if (build < 22000)
            {
                _winver = WinVer.Win10;
            }
            else
            {
                if (build < 22500)
                {
                    _winver = WinVer.Win11_21H2;
                }
                else if (build < 22631)
                {
                    _winver = WinVer.Win11_22H2;
                }
                else
                {
                    _winver = WinVer.Win11_23H2;
                }
            }
        }

        /// <summary>
        /// Invoked when the application is launched.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            m_window = new MainWindow();
            m_window.Activate();
        }

        private Window m_window;


    }
}
