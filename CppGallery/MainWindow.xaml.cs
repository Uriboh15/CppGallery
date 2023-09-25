// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI;           // Needed for WindowId.
using Microsoft.UI.Windowing; // Needed for AppWindow.
using WinRT.Interop;          // Needed for XAML/HWND interop.
using Windows.UI.ViewManagement;
using WinRT;
using Microsoft.UI.Composition.SystemBackdrops;
using System.Reflection.Metadata;
using System.Xml.Linq;
using CppGallery.Pages;
using System.Threading.Tasks;
using CppGallery.Pages.Settings;
using System.Diagnostics;
using CppGallery.Pages.UserControls;
using Windows.ApplicationModel.DataTransfer;
using DispatcherQueueHandler = Microsoft.UI.Dispatching.DispatcherQueueHandler;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace CppGallery
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>

    internal enum Monitor_DPI_Type : int
    {
        MDT_Effective_DPI = 0,
        MDT_Angular_DPI = 1,
        MDT_Raw_DPI = 2,
        MDT_Default = MDT_Effective_DPI
    }

    public sealed partial class MainWindow : Window
    {
        private static int CreateTabCount = 0;
        private IntPtr _hWnd { get; set; }
        public IntPtr HWnd => _hWnd;
        public IList<object> TabChildren => Tab.TabItems;
        public UIElementCollection Children => MainFrame.Children;
        double Scaling;
        private AppWindow OldAppWindow { get; set; }
        public static List<MainWindow> WindowList { get; } = new List<MainWindow>();


        [DllImport("User32.dll")]
        private static extern int IsZoomed(IntPtr hWnd);

        [DllImport("User32.dll")]
        private static extern int SetWindowLongPtrW(IntPtr hwnd, int nIndex, long dwNewLong);

        [DllImport("user32.dll")]
        static extern int GetDpiForWindow(IntPtr hWnd);

        [DllImport("User32.dll")]
        private static extern int GetSystemMetrics(int nIndex);
        private const int SM_CYSCREEN = 1;

        [DllImport("dwmapi.dll")]
        public static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);



        //mica
        WindowsSystemDispatcherQueueHelper wsdqHelper;
        MicaController micaController;
        SystemBackdropConfiguration configurationSource;
        DesktopAcrylicController m_acrylicController;

        public MainWindow(bool createTabContent = true)
        {
            this.InitializeComponent();

            //mica
            wsdqHelper = new WindowsSystemDispatcherQueueHelper();
            wsdqHelper.EnsureWindowsSystemDispatcherQueueController();
            this.Activated += Window_Activated;
            SetBackdrop();



            WindowList.Add(this);
            _hWnd = WindowNative.GetWindowHandle(this);

            SetIsCompact();
            OldAppWindow = AppWindow.GetFromWindowId(Win32Interop.GetWindowIdFromWindow(_hWnd));
            OldAppWindow.Title = "C++ Library Gallery";
            SetTitleBarColors(App.Theme);
            InitializeTheme();
            OldAppWindow.SetIcon("Pages/Assets/Square150x150Logo.scale-200.ico");
            
            this.SizeChanged += Window_SizeChanged;

            if (createTabContent) AddTab();
        }

        public static MainWindow GetParentMainWindow(UIElement element)
        {
            foreach(var window in WindowList)
            {
                if(element.XamlRoot == window.Content.XamlRoot)
                {
                    return window;
                }
            }

            throw new Exception("Element is not setted in a MainWindow.");
        }

        public void ShowCSampleTeaching()
        {
            CSampleTeaching.IsOpen = true;
        }



        private void SetIsCompact()
        {
            int dispx = GetSystemMetrics(SM_CYSCREEN);
            Scaling = GetDpiForWindow(_hWnd) / 96.0;

            if (App.IsFirstLaunch)
            {
                if (dispx / Scaling < 830.0)
                {
                    App.IsCompact = true;
                    Pages.Settings.SettingPage.IsCompact = true;
                }
            }
        }

        private void ToDark(AppWindowTitleBar titleBar)
        {

            titleBar.ButtonForegroundColor = Windows.UI.Color.FromArgb(0xFF, 0xFF, 0xFF, 0xFF);
            titleBar.ButtonHoverForegroundColor = Windows.UI.Color.FromArgb(0xFF, 0xFF, 0xFF, 0xFF);
            titleBar.ButtonPressedForegroundColor = Windows.UI.Color.FromArgb(0xFF, 0xFF, 0xFF, 0xFF);
            titleBar.ButtonInactiveForegroundColor = Windows.UI.Color.FromArgb(0xFF, 0x99, 0x99, 0x99);
            titleBar.ButtonHoverBackgroundColor = Windows.UI.Color.FromArgb(0xFF, 0x2D, 0x2D, 0x2D);
            titleBar.ButtonPressedBackgroundColor = Windows.UI.Color.FromArgb(0xFF, 0x29, 0x29, 0x29);
            Back.Background = new SolidColorBrush(Windows.UI.Color.FromArgb(0x08, 0xFF, 0xFF, 0xFF));

            if (App.Back == BackDrop.None)
            {
                MainFrame.Background = TmpGrid.Background;
                Dodai.Background = Data.DarkBackBrush;
            }
            else if (App.Back == BackDrop.FrostedGlass)
            {
                NanttyateMicaLite.Visibility = Visibility.Collapsed;
                NanttyateMicaDark.Visibility = Visibility.Visible;
            }
            else
            {
                Dodai.Background = null;
            }
#if false
            titleBar.BackgroundColor = Windows.UI.Color.FromArgb(0xFF, 0x20, 0x20, 0x20);
            titleBar.ButtonBackgroundColor = Windows.UI.Color.FromArgb(0xFF, 0x20, 0x20, 0x20);
            titleBar.ButtonHoverBackgroundColor = Windows.UI.Color.FromArgb(0xFF, 0x2D, 0x2D, 0x2D);
            titleBar.ButtonHoverForegroundColor = Windows.UI.Color.FromArgb(0xFF, 0xFF, 0xFF, 0xFF);
            titleBar.ButtonPressedForegroundColor = Windows.UI.Color.FromArgb(0xFF, 0xFF, 0xFF, 0xFF);
            titleBar.ButtonPressedBackgroundColor = Windows.UI.Color.FromArgb(0xFF, 0x29, 0x29, 0x29);
            titleBar.InactiveBackgroundColor = Windows.UI.Color.FromArgb(0xFF, 0x20, 0x20, 0x20);
            titleBar.ButtonInactiveBackgroundColor = Windows.UI.Color.FromArgb(0xFF, 0x20, 0x20, 0x20);
#endif
        }

        private void ToLight(AppWindowTitleBar titleBar)
        {

            titleBar.ButtonForegroundColor = Windows.UI.Color.FromArgb(0xFF, 0x00, 0x00, 0x00);
            titleBar.ButtonHoverForegroundColor = Windows.UI.Color.FromArgb(0xFF, 0x00, 0x00, 0x00);
            titleBar.ButtonPressedForegroundColor = Windows.UI.Color.FromArgb(0xFF, 0x00, 0x00, 0x00);
            titleBar.ButtonInactiveForegroundColor = Windows.UI.Color.FromArgb(0xFF, 0x9B, 0x9B, 0x9B);
            titleBar.ButtonHoverBackgroundColor = Windows.UI.Color.FromArgb(0xFF, 0xE9, 0xE9, 0xE9);
            titleBar.ButtonPressedBackgroundColor = Windows.UI.Color.FromArgb(0xFF, 0xED, 0xED, 0xED);
            Back.Background = new SolidColorBrush(Windows.UI.Color.FromArgb(0x74, 0xFF, 0xFF, 0xFF));

            if (App.Back == BackDrop.None)
            {
                Dodai.Background = Data.LightBackBrush;
                MainFrame.Background = TmpGrid.Background;
            }
            else if (App.Back == BackDrop.FrostedGlass)
            {
                NanttyateMicaLite.Visibility = Visibility.Visible;
                NanttyateMicaDark.Visibility = Visibility.Collapsed;
            }
            else
            {
                Dodai.Background = null;
            }
        }

        public void InitializeTheme()
        {

            var titleBar = OldAppWindow.TitleBar;
            titleBar.ExtendsContentIntoTitleBar = true;


            titleBar.ButtonBackgroundColor = Windows.UI.Color.FromArgb(0x00, 0x00, 0x00, 0x00);
            titleBar.ButtonInactiveBackgroundColor = Windows.UI.Color.FromArgb(0x00, 0x00, 0x00, 0x00);
            switch (Dodai.ActualTheme)
            {
                case ElementTheme.Light:
                    ToLight(titleBar);
                    break;

                case ElementTheme.Dark:
                    ToDark(titleBar);
                    break;
            }

            if (App.Win10)
            {
                ChangeBorder();
            }
        }


        public bool SetTitleBarColors(ElementTheme mode)
        {
            // Check to see if customization is supported.
            // Currently only supported on Windows 11.
            // Set active window colors
            switch (mode)
            {
                case ElementTheme.Default:
                    Dodai.RequestedTheme = ElementTheme.Default;
                    break;

                case ElementTheme.Light:
                    Dodai.RequestedTheme = ElementTheme.Light;
                    break;

                case ElementTheme.Dark:
                    Dodai.RequestedTheme = ElementTheme.Dark;
                    break;
            }

#if false
                titleBar.ForegroundColor = Colors.White;
                titleBar.BackgroundColor = Colors.Green;
                titleBar.ButtonForegroundColor = Colors.White;
                titleBar.ButtonBackgroundColor = Colors.SeaGreen;
                titleBar.ButtonHoverForegroundColor = Colors.Gainsboro;
                titleBar.ButtonHoverBackgroundColor = Colors.DarkSeaGreen;
                titleBar.ButtonPressedForegroundColor = Colors.Gray;
                titleBar.ButtonPressedBackgroundColor = Colors.LightGreen;

                // Set inactive window colors
                titleBar.InactiveForegroundColor = Colors.Gainsboro;
                titleBar.InactiveBackgroundColor = Colors.SeaGreen;
                titleBar.ButtonInactiveForegroundColor = Colors.Gainsboro;
                titleBar.ButtonInactiveBackgroundColor = Colors.SeaGreen;
#endif
            return true;
        }

        private void ContentFrame_ActualThemeChanged(FrameworkElement sender, object args)
        {

            var titleBar = OldAppWindow.TitleBar;
            switch (Dodai.ActualTheme)
            {
                case ElementTheme.Light:
                    if (App.Back != BackDrop.None && App.Back != BackDrop.FrostedGlass)
                    {
                        configurationSource.Theme = SystemBackdropTheme.Light;
                    }
                    ToLight(titleBar);
                    break;

                case ElementTheme.Dark:
                    if (App.Back != BackDrop.None && App.Back != BackDrop.FrostedGlass)
                    {

                        configurationSource.Theme = SystemBackdropTheme.Dark;

                    }
                    ToDark(titleBar);
                    break;
            }
        }

        private void Window_Closed(object sender, WindowEventArgs args)
        {
            WindowList.Remove(this);

            if(WindowList.Count == 0)
            {
                byte[] buf = new byte[App.SettingsLen];
                FileStream fs = new FileStream(App.SettingsFilePath, FileMode.Open, FileAccess.Write);
                buf[0] = (byte)App.Theme;
                buf[1] = (byte)App.Back;
                buf[2] = (byte)(Pages.Settings.SettingPage.IsCompact ? 1 : 0);
                buf[3] = (byte)App.Compiler;
                buf[4] = (byte)App.LibraryPageStyle;
                buf[5] = (byte)App.SourceCodeTheme;
                buf[6] = (byte)App.ResultTheme;
                buf[7] = (byte)App.ProcesserType;
                buf[8] = (byte)(App.UseCppInCSample ? 1 : 0);
                buf[9] = (byte)(App.IsShowReturnCode ? 1 : 0);
                buf[10] = (byte)App.CppVersion;
                buf[11] = (byte)App.CVersion;

                var tmp = BitConverter.GetBytes(App.WaitFor);
                for (int i = 0; i < tmp.Length; i++)
                {
                    buf[i + 12] = tmp[i];
                }

                fs.Write(buf, 0, App.SettingsLen);
                fs.Close();

                byte[] log = new byte[App.LogLen];
                fs = new FileStream(App.UserLogPath, FileMode.Open, FileAccess.Write);
                log[0] = (byte)(App.IsFirstCSampleOpened ? 1 : 0);

                fs.Write(log, 0, App.LogLen);
                fs.Close();
            }
            else
            {
                MainFrame.Children.Clear();
            }
            

            //mica
            if (micaController != null)
            {
                micaController.Dispose();
                micaController = null;
            }
            if (m_acrylicController != null)
            {
                m_acrylicController.Dispose();
                m_acrylicController = null;
            }
            this.Activated -= Window_Activated;
            configurationSource = null;

            Content = null;
        }

        public ElementTheme GetTheme()
        {
            return Dodai.ActualTheme;
        }


        //mica
        public void SetBackdrop()
        {
            if (micaController != null)
            {
                micaController.Dispose();
                micaController = null;
            }
            if (m_acrylicController != null)
            {
                m_acrylicController.Dispose();
                m_acrylicController = null;
            }
            configurationSource = null;
            Dodai.Background = null;

            MainFrame.Background = null;
            NanttyateMicaDark.Visibility = Visibility.Collapsed;
            NanttyateMicaLite.Visibility = Visibility.Collapsed;

            switch (App.Back)
            {
                case BackDrop.None:
                    MainFrame.Background = TmpGrid.Background;
                    if (Dodai.ActualTheme == ElementTheme.Light) Dodai.Background = Data.LightBackBrush;
                    else Dodai.Background = Data.DarkBackBrush;
                    break;

                case BackDrop.Mica: TrySetMicaBackdrop(); break;

                case BackDrop.FrostedGlass:
                    if (Dodai.ActualTheme == ElementTheme.Light) NanttyateMicaLite.Visibility = Visibility.Visible;
                    else NanttyateMicaDark.Visibility = Visibility.Visible;
                    break;

                case BackDrop.Acrylic: TryAcrylicBackdrop(); break;
            }
        }

        private bool TrySetMicaBackdrop()
        {
            if (MicaController.IsSupported())
            {
                configurationSource = new SystemBackdropConfiguration();

                configurationSource.IsInputActive = true;
                switch (Dodai.ActualTheme)
                {
                    case ElementTheme.Dark:
                        configurationSource.Theme = SystemBackdropTheme.Dark;

                        break;


                    case ElementTheme.Light:
                        configurationSource.Theme = SystemBackdropTheme.Light;
                        break;
                }

                micaController = new MicaController { Kind = MicaKind.BaseAlt };

                micaController.AddSystemBackdropTarget(this.As<Microsoft.UI.Composition.ICompositionSupportsSystemBackdrop>());
                micaController.SetSystemBackdropConfiguration(configurationSource);
                return true;
            }
            return false;
        }

        private bool TryAcrylicBackdrop()
        {
            if (DesktopAcrylicController.IsSupported())
            {
                wsdqHelper = new WindowsSystemDispatcherQueueHelper();
                wsdqHelper.EnsureWindowsSystemDispatcherQueueController();

                // Hooking up the policy object
                configurationSource = new SystemBackdropConfiguration();

                // Initial configuration state.
                configurationSource.IsInputActive = true;
                switch (Dodai.ActualTheme)
                {
                    case ElementTheme.Dark: configurationSource.Theme = SystemBackdropTheme.Dark; break;
                    case ElementTheme.Light: configurationSource.Theme = SystemBackdropTheme.Light; break;
                    case ElementTheme.Default: configurationSource.Theme = SystemBackdropTheme.Default; break;
                }

                m_acrylicController = new DesktopAcrylicController();


                // Enable the system backdrop.
                // Note: Be sure to have "using WinRT;" to support the Window.As<...>() call.
                m_acrylicController.AddSystemBackdropTarget(this.As<Microsoft.UI.Composition.ICompositionSupportsSystemBackdrop>());
                m_acrylicController.SetSystemBackdropConfiguration(configurationSource);
                return true; // succeeded
            }

            return false;
        }

        private void Window_Activated(object sender, WindowActivatedEventArgs args)
        {
            //背景更新
            if (App.Back != BackDrop.None && App.Back != BackDrop.FrostedGlass)
            {
                configurationSource.IsInputActive = args.WindowActivationState != WindowActivationState.Deactivated;


            }
            else if (App.Back == BackDrop.FrostedGlass)
            {
                if (args.WindowActivationState == WindowActivationState.Deactivated)
                {
                    TmpGrid.Visibility = Visibility.Visible;
                }
                else
                {
                    TmpGrid.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                if (args.WindowActivationState == WindowActivationState.Deactivated)
                {
                    if (Dodai.ActualTheme == ElementTheme.Dark)
                    {
                        Dodai.Background = Data.DarkDeactivatedBackBrush;
                    }
                    else
                    {
                        Dodai.Background = Data.LightDeactivatedBackBrush;
                    }
                }
                else
                {
                    if (Dodai.ActualTheme == ElementTheme.Dark)
                    {
                        Dodai.Background = Data.DarkBackBrush;
                    }
                    else
                    {
                        Dodai.Background = Data.LightBackBrush;
                    }
                }


            }



            //ほかのウィンドウで設定を変更したときにMainPageを更新
            if(args.WindowActivationState != WindowActivationState.Deactivated)
            {
                foreach(var element in MainFrame.Children)
                {
                    if(element is MainPage mainPage)
                    {
                        if(mainPage.Visibility == Visibility.Visible && !mainPage.Updated)
                        {
                            mainPage.UpdateSample();
                            break;
                        }
                    }
                }
            }
        }

        private void ChangeBorder()
        {
            if (IsZoomed(_hWnd) == 1)
            {
                Tab.Padding = new Thickness(0);
                TitleBarIcon.Margin = new Thickness(14, 0, 8, 0);
                if (App.Win10) Dodai.BorderThickness = new Thickness(0);
            }
            else
            {
                Tab.Padding = new Thickness(0, 8, 0, 0);
                TitleBarIcon.Margin = new Thickness(14, 4, 8, 0);
                if (App.Win10) Dodai.BorderThickness = new Thickness(0, 1, 0, 0);
            }
        }

        private void Window_SizeChanged(object sender, WindowSizeChangedEventArgs args)
        {
            ChangeBorder();
        }

        public void AddTab(string firstShowPageTag = "home")
        {
            var newTab = new TabViewItem { Tag = CreateTabCount };
            Tab.TabItems.Add(newTab);
            newTab.IconSource = new FontIconSource();
            MainFrame.Children.Add(new MainPage { Tag = CreateTabCount, FirstShowPageTag = firstShowPageTag });
            ++CreateTabCount;
            Tab.SelectedItem = newTab;
        }

        private void Tab_AddTabButtonClick(TabView sender, object args)
        {
            AddTab();
        }

        private void Tab_TabCloseRequested(TabView sender, TabViewTabCloseRequestedEventArgs args)
        {
            sender.TabItems.Remove(args.Tab);

            if (Tab.TabItems.Count == 0)
            {
                this.Close();
            }

            int count = (int)args.Tab.Tag;

            foreach (var element in MainFrame.Children)
            {
                var page = element as Page;

                if ((int)page.Tag == count)
                {
                    MainFrame.Children.Remove(page);
                    break;
                }
            }
            GC.Collect();
        }

        private void Tab_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0) return;

            int count = (int)(e.AddedItems[0] as TabViewItem).Tag;

            foreach (var element in MainFrame.Children)
            {
                var page = element as Page;

                if ((int)page.Tag == count)
                {
                    page.Visibility = Visibility.Visible;
                    if (page is MainPage mainPage)
                    {
                        if (!mainPage.Updated)
                        {
                            mainPage.UpdateSample();
                        }
                    }
                }
                else
                {
                    page.Visibility = Visibility.Collapsed;
                }
            }
        }




        private void SetDragRegionForCustomTitleBar(AppWindow appWindow)
        {
            // Check to see if customization is supported.
            // The method returns true on Windows 10 since Windows App SDK 1.2, and on all versions of
            // Windows App SDK on Windows 11.
            if (appWindow == null) return;

            double wid = Dodai.ActualWidth - 260.0;
            if (wid >= 0.0)
            {
                Tab.MaxWidth = wid;
            }

            double scaleAdjustment = UserAPI.GetScaleAdjustment(_hWnd);

            RightPaddingColumn.Width = new GridLength(appWindow.TitleBar.RightInset / scaleAdjustment);
            LeftPaddingColumn.Width = new GridLength(appWindow.TitleBar.LeftInset / scaleAdjustment);

            List<Windows.Graphics.RectInt32> dragRectsList = new();

            Windows.Graphics.RectInt32 dragRectL;
            dragRectL.X = (int)((LeftPaddingColumn.ActualWidth) * scaleAdjustment);
            dragRectL.Y = 0;
            dragRectL.Height = (int)(AppTitleBar.ActualHeight * scaleAdjustment);
            dragRectL.Width = (int)(LeftDragColumn.ActualWidth * scaleAdjustment);
            dragRectsList.Add(dragRectL);

            Windows.Graphics.RectInt32 dragRectR;
            dragRectR.X = (int)((LeftPaddingColumn.ActualWidth
                                + LeftDragColumn.ActualWidth
                                + TabColumn.ActualWidth) * scaleAdjustment);
            dragRectR.Y = 0;
            dragRectR.Height = (int)(AppTitleBar.ActualHeight * scaleAdjustment);
            dragRectR.Width = (int)(RightDragColumn.ActualWidth * scaleAdjustment);
            dragRectsList.Add(dragRectR);

            Windows.Graphics.RectInt32[] dragRects = dragRectsList.ToArray();

            appWindow.TitleBar.SetDragRectangles(dragRects);

        }

        private void DraggableArea_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            SetDragRegionForCustomTitleBar(OldAppWindow);
        }

        public static void OpenSettings(UIElement element)
        {
            //既に設定を開いている場合はそれを表示
            foreach(var window in WindowList)
            {
                for (int i = 0; i < window.Tab.TabItems.Count; ++i)
                {
                    if ((window.Tab.TabItems[i] as TabViewItem).Header as string == "設定")
                    {
                        window.Tab.SelectedItem = window.Tab.TabItems[i];
                        window.Activate();
                        return;
                    }
                }
            }

            var currentWindow = GetParentMainWindow(element);

            var newTab = new TabViewItem { Tag = CreateTabCount };
            currentWindow.Tab.TabItems.Add(newTab);
            newTab.IconSource = new SymbolIconSource { Symbol = Symbol.Setting };
            newTab.Header = "設定";
            var spage = SettingsParentPage.GetHandle();
            spage.Tag = CreateTabCount;
            currentWindow.MainFrame.Children.Add(spage);
            ++CreateTabCount;
            currentWindow.Tab.SelectedItem = newTab;
        }

        private void SettingButton_Click(object sender, RoutedEventArgs e)
        {
            OpenSettings(this.Content);
        }

        private async void Dodai_Loaded(object sender, RoutedEventArgs e)
        {
            if (App.IsFirstLaunch && CreateTabCount < 2)
            {
                DirectoryInfo di = new DirectoryInfo(Data.DefaultSampleExePath);
                FileInfo[] fiAlls = di.GetFiles();

                ContentDialog dialog = new ContentDialog
                {
                    XamlRoot = Content.XamlRoot,
                };
                var panel = new StackPanel
                {
                    VerticalAlignment = VerticalAlignment.Center,
                };
                panel.Children.Add(new TextBlock
                {
                    Text = "アプリの準備をしています...",
                    HorizontalAlignment = HorizontalAlignment.Center,
                });

                var textBlock2 = new TextBlock
                {
                    HorizontalAlignment = HorizontalAlignment.Center,
                };

                panel.Children.Add(textBlock2);

                var bar = new ProgressBar
                {
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                };

                panel.Children.Add(bar);

                dialog.Content = panel;

                dialog.Opened += async (s, e) =>
                {
                    int count = 0;

                    textBlock2.Text = count.ToString() + " / " + fiAlls.Length.ToString();

                    var plus = () =>
                    {
                        ++count;
                        textBlock2.Text = count.ToString() + " / " + fiAlls.Length.ToString();
                        bar.Value = 100.0 * count / fiAlls.Length;
                    };

                    foreach (FileInfo fi in fiAlls)
                    {
                        await CoAPI.ExecuteAsync(fi.FullName);
                        plus();

                        //Process.Start(fi.FullName);
                    }

                    await Task.Delay(800);

                    dialog.Hide();
                };

                await dialog.ShowAsync();


                //dll読み込み
                await Task.Run(() =>
                {
                    Func.SrandTest(0);
                    _ = Wcharh.WcsftimeTest();
                });



            }
#if false
            //設定の項目を先に読み込む
            await Task.Run(() =>
            {
                _ = SettingsParentPage.GetHandle();
                _ = SettingPage.GetHandle();
                _ = AboutAppPage.GetHandle();
                _ = ReleaseNote.GetHandle();
            });

#endif
        }

        private void Tab_TabDroppedOutside(TabView sender, TabViewTabDroppedOutsideEventArgs args)
        {
            //タブが1個の場合はウィンドウを移動しない
            if (Tab.TabItems.Count == 1) return;

            var selected = Tab.SelectedItem as TabViewItem;
            int index = (int)selected.Tag;

            Page page = null;

            foreach (var element in MainFrame.Children)
            {
                if ((int)(element as Page).Tag == index)
                {
                    page = element as Page;
                    MainFrame.Children.Remove(element);
                    Tab.TabItems.Remove(selected);
                    break;
                }
            }

            if (page == null) throw new Exception("Page not found.");

            var window = new MainWindow(false);

            var newTab = new TabViewItem { Tag = index, IconSource = selected.IconSource, Header = selected.Header };
            window.Tab.TabItems.Add(newTab);
            window.MainFrame.Children.Add(page);
            window.Tab.SelectedItem = newTab;

            window.Activate();
        }

        //staticにマークしてはいけない
        private void Tab_TabDragStarting(TabView sender, TabViewTabDragStartingEventArgs args)
        {
            // We can only drag one tab at a time, so grab the first one...

            // ... set the drag data to the tab...
            args.Data.Properties.Add("MainPage", sender.SelectedItem);

            // ... and indicate that we can move it
            args.Data.RequestedOperation = DataPackageOperation.Move;
        }

        private async void Tab_TabStripDrop(object sender, DragEventArgs e)
        {
            //なんかよくわからんやつ
            if (e.DataView.Properties.TryGetValue("MainPage", out object obj))
            {
                // Ensure that the obj property is set before continuing.
                if (obj == null)
                {
                    return;
                }

                var destinationTabView = sender as TabView;
                var destinationItems = destinationTabView.TabItems;

                if (destinationItems != null)
                {
                    // First we need to get the position in the List to drop to
                    var index = -1;

                    // Determine which items in the list our pointer is between.
                    for (int i = 0; i < destinationTabView.TabItems.Count; i++)
                    {
                        var item = destinationTabView.ContainerFromIndex(i) as TabViewItem;

                        if (e.GetPosition(item).X - item.ActualWidth < 0)
                        {
                            index = i;
                            break;
                        }
                    }

                    // The TabViewItem can only be in one tree at a time. Before moving it to the new TabView, remove it from the old.
                    // Note that this call can happen on a different thread if moving across windows. So make sure you call methods on
                    // the same thread as where the UI Elements were created.

                    var element = (obj as UIElement);

                    var taskCompletionSource = new TaskCompletionSource();

                    element.DispatcherQueue.TryEnqueue(
                        Microsoft.UI.Dispatching.DispatcherQueuePriority.Normal,
                        new DispatcherQueueHandler(() =>
                        {
                            var tabItem = obj as TabViewItem;
                            var destinationTabViewListView = (tabItem.Parent as TabViewListView);
                            destinationTabViewListView.Items.Remove(obj);

                            taskCompletionSource.SetResult();
                        }));

                    await taskCompletionSource.Task;

                    var tabItem = obj as TabViewItem;
                    int count = (int)tabItem.Tag;

                    Page page = null;

                    for (int j = 0; j < WindowList.Count; ++j)
                    {
                        var window = WindowList[j];
                        foreach (var uiElement in window.Children)
                        {
                            var subpage = uiElement as Page;

                            if ((int)subpage.Tag == count)
                            {
                                page = subpage;
                                window.Children.Remove(subpage);
                                if (window.Children.Count == 0)
                                {
                                    window.Close();
                                }
                                break;
                            }
                        }
                    }

                    MainFrame.Children.Add(page);

                    if (index < 0)
                    {
                        // We didn't find a transition point, so we're at the end of the list
                        destinationItems.Add(tabItem);
                    }
                    else if (index < destinationTabView.TabItems.Count)
                    {
                        // Otherwise, insert at the provided index.
                        destinationItems.Insert(index, tabItem);
                    }

                    // Select the newly dragged tab
                    destinationTabView.SelectedItem = tabItem;

                    
                }
            }
        }

        private void Tab_TabStripDragOver(object sender, DragEventArgs e)
        {
            if (e.DataView.Properties.ContainsKey("MainPage"))
            {
                e.AcceptedOperation = DataPackageOperation.Move;
            }
        }
    }
}