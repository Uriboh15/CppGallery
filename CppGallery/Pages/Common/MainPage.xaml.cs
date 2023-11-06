// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Input;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Windowing;
using System.Threading.Tasks;
using CppGallery.Pages.Common;
using CppGallery.Pages.UserControls;
using System.Reflection;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace CppGallery.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public string openedst = "ホーム";
        public Frame FrameHandle => ContentFrame;
        private string ChosedItemsTag;
        public string FirstShowPageTag = "home";
        private bool CurrentUseCppSetting = false;

        //コンテントダイアログが表示されている間はtrue
        private bool UpdateWorking = false;

        private bool _updated = true;
        public bool Updated => _updated;

        public static Dictionary<Type, string> PageDictionary { get; } = new Dictionary<Type, string>()
        {
            { typeof(NewContentPage), "New"},
            { typeof(UpdatedPage), "Updated"},
            { typeof(ReleaseNote), "Release"},
            { typeof(BaseTypePage), "BaseType"},
            { typeof(OperatorPage), "BaseOperator" },
            { typeof(GCCCommandPage), "GCCCommand"},
            { typeof(CompactHomePage), "home"},
            { typeof(CHeader.AsserthPage), "0asserth" },
            { typeof(CHeader.ComplexhPage), "0complexh"},
            { typeof(CHeader.CtypehPage), "0ctypeh"},
            { typeof(CHeader.ErrnohPage), "0errnoh"},
            { typeof(CHeader.FEnvhPage), "0fenvh"},
            { typeof(CHeader.FloathPage), "0floath"},
            { typeof(CHeader.IntTypeshPage), "0inttypesh"},
            { typeof(CHeader.Iso646hPage), "0iso646h"},
            { typeof(CHeader.LimitsPage), "0limitsh"},
            { typeof(CHeader.LocalehPage), "0localeh"},
            { typeof(CHeader.MathhPage), "0mathh"},
            { typeof(CHeader.StdAlignPage), "0stdalignh"},
            { typeof(CHeader.StdboolhPage), "0stdboolh"},
            { typeof(CHeader.StdintPage), "0stdinth"},
            { typeof(CHeader.StdiohPage), "0stdioh"},
            { typeof(CHeader.StdlibhPage), "0stdlibh"},
            { typeof(CHeader.StdNoReturnhPage), "0stdnoreturnh"},
            { typeof(CHeader.StringhPage), "0stringh"},
            { typeof(CHeader.TgMathhPage), "0tgmathh"},
            { typeof(CHeader.TimehPage), "0timeh"},
            { typeof(CHeader.UcharhPage), "0ucharh"},
            { typeof(CHeader.WcharhPage), "0wcharh"},
            { typeof(CHeader.WctypehPage), "0wctypeh"},
            { typeof(String.StringPage), "1stringc"},    //未完成
            { typeof(String.BasicStringPage), "1basic_string0"},    //未完成
            { typeof(String.CharTraitsPage), "1char_traits0"},    //未完成
            { typeof(Bit.BitPage), "2bit"},    //未完成
            { typeof(Limits.LimitsPage), "3limits"},    //未完成
            { typeof(Limits.NumericLimitsPage), "3numeric_limits0"},    //未完成
            { typeof(WindowsHeader.ConiohPage), "4conioh"},    //未完成
            { typeof(WindowsHeader.ConsoleApiPage), "4ConsoleApi" },
            { typeof(WindowsHeader.ConsoleApi2Page), "4ConsoleApi2h" },
            { typeof(WindowsHeader.ConsoleApi3Page), "4ConsoleApi3h" },
            { typeof(WindowsHeader.FIleApiPage), "4fileapi" },
            { typeof(WindowsHeader.MinWinDefPage), "4Minwindef" },
            { typeof(WindowsHeader.ProcessEnvPage), "4ProcessEnv" },
            { typeof(WindowsHeader.WindowshPage), "4Windows" },
            { typeof(WindowsHeader.WinUserPage), "4WinUser" },
            { typeof(BitSet.BitSetPage), "5bitset"},    //未完成
            { typeof(BitSet.BitSetClassPage), "5bitset0"},    //未完成
            { typeof(StrStream.StrStreamPage), "6strstream"},    //未完成
            { typeof(StrStream.OstrStreamPage), "6ostrstream0"},    //未完成
            { typeof(Array.ArrayPage), "7array"},
            { typeof(Array.ArrayClassPage), "7array0"},
            { typeof(Iostream.IostreamPage), "9iostream"},
            { typeof(Ios.IosPage), "aios"},
            { typeof(Ios.BasicIosPage), "abasic_ios0"},
            { typeof(Ios.IosBasePage), "aios_base0"},
            { typeof(IosFwd.IosFwdPage), "8iosfwd"},
            { typeof(IosFwd.FposPage), "8fpos0"},
            { typeof(Iomanip.IomanipPage), "biomanip"},
            { typeof(List.ListPage), "clist"},
            { typeof(List.ListClassPage), "clist0"},
            { typeof(Istream.IstreamPage), "distream"},
            { typeof(Istream.BasicIostreamPage), "dbasic_iostream0"},
            { typeof(Istream.BasicIstreamPage), "dbasic_istream0"},
            { typeof(Map.MapPage), "emap"},
            { typeof(Map.MapClassPage), "emap0"},
            { typeof(Ostream.OstreamPage), "fostream"},
            { typeof(Ostream.BasicOstreamPage), "fbasic_ostream0"},
            { typeof(StreamBuf.StreamBufPage), "gstreambuf"},
            { typeof(StreamBuf.BasicStreamBufPage), "gbasic_streambuf0"},
            { typeof(Vector.VectorPage), "hvector"},
            { typeof(Vector.VectorClassPage), "hvector0"},
            { typeof(Fstream.FstreamPage), "ifstream"},
            { typeof(Fstream.BasicFilebufPage), "ibasic_filebuf0"},
            { typeof(Fstream.BasicFstreamPage), "ibasic_fstream0"},
            { typeof(Fstream.BasicIfstreamPage), "ibasic_ifstream0"},
            { typeof(Fstream.BasicOfstreamPage), "ibasic_ofstream0"},
            { typeof(ValArray.ValArrayPage), "jvalarray"},
            { typeof(ValArray.SlicePage), "jslice0"},
            { typeof(ValArray.ValArrayClassPage), "jvalarray0"},
            { typeof(InitializerList.InitializerListPage), "kinitializer_list" },
            { typeof(InitializerList.InitializerListClassPage), "kinitializer_list0" },
            { typeof(SStream.SStreamPage), "lsstream"},
            { typeof(SStream.BasicStringBufPage), "lbasic_stringbuf0"},
            { typeof(SStream.BasicIstringStreamPage), "lbasic_istringstream0"},
            { typeof(SStream.BasicOstringStreamPage), "lbasic_ostringstream0"},
            { typeof(SStream.BasicStringStreamPage), "lbasic_stringstream0"},
            { typeof(New.NewPage), "mnew"},
            { typeof(Random.RandomPage), "nrandom"},
            { typeof(Random.LinearCongruentialEnginePage), "nlinear_congruential_engine0"},
            { typeof(Random.MersenneTwisterEngineClassPage), "nmersenne_twister_engine0" },
            { typeof(Complex.ComplexPage), "ocomplex"},
            { typeof(Complex.ComplexCalssPage), "ocomplex0"},
            { typeof(Stack.StackPage), "pstack"},
            { typeof(Stack.StackClassPage), "pstack0"},
            { typeof(Queue.QueuePage), "qqueue"},
            { typeof(Queue.QueueClassPage), "qqueue0"},
            { typeof(Any.AnyPage), "rany" },
            { typeof(Any.AnyClassPage), "rany0" },
            { typeof(StdExcept.StdExceptPage), "sstdexcept" },
            { typeof(StringView.StringViewPage), "tstring_view" },
            { typeof(StringView.BasicStringViewClassPage), "tbasic_string_view0" },
            { typeof(TypeTraits.TypeTraitsPage), "utype_traits" },
            { typeof(Span.SpanPage), "vspan" },
            { typeof(Span.SpanClassPage), "vspan0" },
            { typeof(FileSystem.FileSystemPage), "wfilesystem" },
            { typeof(FileSystem.FileStatusPage), "wfile_status0" },
            { typeof(FileSystem.PathPage), "wpath0" },
            { typeof(FileSystem.DirectoryIteratorPage), "wdirectory_iterator0" },
            { typeof(ForwardList.ForwardListPage), "xforward_list" },
            { typeof(ForwardList.ForwardListClassPage), "xforward_list0" },
            { typeof(Deque.DequePage), "ydeque" },
            { typeof(Deque.DequeClassPage), "ydeque0" },
            { typeof(CharConv.CharConvPage), "zcharconv" },
            { typeof(Set.SetPage), "Aset" },
            { typeof(Set.SetClassPage), "Aset0" },
            { typeof(Set.MultiSetClassPage), "Amultiset0" },
            { typeof(Numeric.NumericPage), "Bnumeric" },
            { typeof(Ranges.RangesPage), "Cranges" },
            { typeof(Numbers.NumbersPage), "Dnumbers" },
            { typeof(SourceLocation.SourceLocationPage), "Esource_location" },
            { typeof(SourceLocation.SourceLocationClassPage), "Esource_location0" },
            { typeof(Concepts.ConceptsPage), "Fconcepts" },
            { typeof(TypeInfo.TypeInfoPage), "Gtypeinfo" },
            { typeof(TypeInfo.TypeInfoClassPage), "Gtype_info0" },
            { typeof(WindowsHeader.DwmAPI.DwmAPIPage), "Hdwmapi" },
            { typeof(WindowsHeader.DwmAPI.DwmSetWindowAttributePage), "HDwmSetWindowAttribute0" },
            { typeof(Print.PrintPage), "IPrint" },
            { typeof(Format.FormatPage), "Jformat" },
            { typeof(Format.BasicFormatStringPage), "Jbasic_format_string0" },
            { typeof(Algorithm.AlgorithmPage), "Kalgorithm" },
            { typeof(Optional.OptionalPage), "Loptional" },
            { typeof(Optional.OptionalClassPage), "Loptional0" },
            { typeof(TypeIndex.TypeIndexPage), "Mtypeindex" },
            { typeof(TypeIndex.TypeIndexClassPage), "Mtype_index0" },
            { typeof(Compare.ComparePage), "Ncompare" },
            { typeof(Compare.StrongOrderingClassPage), "Nstrong_ordering0" },
            { typeof(Iterator.IteratorPage), "Oiterator" },
            { typeof(WinRT.WindowsUIXamlControls.WindowsUIXamlControlsPage), "W00WindowsUIXamlControls" },
            { typeof(WinRT.WindowsUIXamlControls.ContentDialogPage), "W00ContentDialog0" },
            { typeof(WinRT.WindowsUIXaml.WindowsUIXamlPage), "W01WindowsUIXaml" },
            { typeof(WinRT.WindowsUIXaml.DependencyObjectPage), "W01DependencyObject0" },
            { typeof(WinRT.WindowsUIXaml.UIElementPage), "W01UIElement0" },
            { typeof(WinRT.BasehPage), "W02base" },
            { typeof(WinRT.IInspectablePage), "W02IInspectable0" },
            { typeof(WinRT.IUnknownPage), "W02IUnknown0" },
            { typeof(WinRT.WindowsUIText.WindowsUITextPage), "W03WindowsUIText" },
            { typeof(WinRT.WindowsUIText.FontStretchPage), "W03FontStretch0" },
            { typeof(WinRT.WindowsUIText.FontStylePage), "W03FontStyle0" },
            { typeof(WinRT.WindowsUIText.FontWeightPage), "W03FontWeight0" },
            { typeof(WinRT.WindowsUIText.FontWeightsPage), "W03FontWeights0" },
            { typeof(WinRT.WindowsUIText.LetterCasePage), "W03LetterCase0" },
            { typeof(WinRT.WindowsUI.WindowsUIPage), "W04WindowsUI" },
            { typeof(WinRT.WindowsUI.ColorPage), "W04Color0" },
            { typeof(WinRT.WindowsUI.ColorHelperPage), "W04ColorHelper0" },
            { typeof(WinRT.WindowsUI.ColorsPage), "W04Colors0" },
            { typeof(WinRT.WindowsUIXamlMarkup.WindowsUIXamlMarkupPage), "W05WindowsUIXamlMarkup" },
            { typeof(WinRT.WindowsUIXamlMarkup.ContentPropertyAttributePage), "W05ContentPropertyAttribute0" },
            { typeof(WinRT.WindowsUIXamlMarkup.XamlReaderPage), "W05XamlReader0" },
            { typeof(WinRT.WindowsStoragePickers.WindowsStoragePickersPage), "W06WindowsStoragePickers" },
            { typeof(WinRT.WindowsStoragePickers.FileExtensionVectorPage), "W06FileExtensionVector0" },
            { typeof(WinRT.WindowsStoragePickers.FileOpenPickerPage), "W06FileOpenPicker0" },
            { typeof(WinRT.WindowsStoragePickers.FolderPickerPage), "W06FolderPicker0" },
            { typeof(WinRT.WindowsStoragePickers.PickerLocationIdPage), "W06PickerLocationId0" },
            { typeof(WinRT.WindowsStoragePickers.PickerViewModePage), "W06PickerViewMode0" },
            { typeof(WinRT.WindowsDevicesPower.WindowsDevicesPowerPage), "W07WindowsDevicesPower" },
            { typeof(WinRT.WindowsDevicesPower.BatteryPage), "W07Battery0" },
            { typeof(WinRT.WindowsDevicesPower.BatteryReportPage), "W07BatteryReport0" },
            { typeof(WinRT.WindowsSystemDisplay.WindowsSystemDisplayPage), "W08WindowsSystemDisplay" },
            { typeof(WinRT.WindowsSystemDisplay.DisplayRequestPage), "W08DisplayRequest0" },
            { typeof(WinRT.WindowsSystemInventory.WindowsSystemInventoryPage), "W09WindowsSystemInventory" },
            { typeof(WinRT.WindowsSystemInventory.InstalledDesktopAppPage), "W09InstalledDesktopApp0" },
            { typeof(WinRT.WindowsFoundationCollections.WindowsFoundationCollectionsPage), "W0AWindowsFoundationCollections" },
            { typeof(WinRT.WindowsFoundationCollections.CollectionChangePage), "W0ACollectionChange0" },
            { typeof(WinRT.WindowsFoundationCollections.IIterablePage), "W0AIIterable0" },
            { typeof(WinRT.WindowsFoundationCollections.IIteratorPage), "W0AIIterator0" },
            { typeof(WinRT.WindowsFoundationCollections.IKeyValuePairPage), "W0AIKeyValuePair0" },
            { typeof(WinRT.WindowsFoundationCollections.IMapPage), "W0AIMap0" },
            { typeof(WinRT.WindowsFoundationCollections.IMapChangedEventArgsPage), "W0AIMapChangedEventArgs0" },
            { typeof(WinRT.WindowsFoundationCollections.IMapViewPage), "W0AIMapView0" },
            { typeof(WinRT.WindowsFoundationCollections.IObservableMapPage), "W0AIObservableMap0" },
            { typeof(WinRT.WindowsFoundationCollections.IObservableVectorPage), "W0AIObservableVector0" },
            { typeof(WinRT.WindowsFoundationCollections.IPropertySetPage), "W0AIPropertySet0" },
            { typeof(WinRT.WindowsFoundationCollections.IVectorPage), "W0AIVector0" },
            { typeof(WinRT.WindowsFoundationCollections.IVectorChangedEventArgsPage), "W0AIVectorChangedEventArgs0" },
            { typeof(WinRT.WindowsFoundationCollections.IVectorViewPage), "W0AIVectorView0" },
            { typeof(WinRT.WindowsFoundationCollections.MapChangedEventHandlerPage), "W0AMapChangedEventHandler0" },
            { typeof(WinRT.WindowsFoundationCollections.VectorChangedEventHandlerPage), "W0AVectorChangedEventHandler0" },
            { typeof(WinRT.WindowsSystemPower.WindowsSystemPowerPage), "W0BWindowsSystemPower" },
            { typeof(WinRT.WindowsSystemPower.BatteryStatusPage), "W0BBatteryStatus0" },
            { typeof(WinRT.WindowsSystemPower.EnergySaverStatusPage), "W0BEnergySaverStatus0" },
            { typeof(WinRT.WindowsSystemPower.PowerManagerPage), "W0BPowerManager0" },
            { typeof(WinRT.WindowsSystemPower.PowerSupplyStatusPage), "W0BPowerSupplyStatus0" },
            { typeof(WinRT.WindowsUIXamlDocuments.WindowsUIXamlDocumentsPage), "W0CWindowsUIXamlDocuments" },
            { typeof(WinRT.WindowsUIXamlDocuments.InlinePage), "W0CInline0" },
            { typeof(WinRT.WindowsUIXamlDocuments.LineBreakPage), "W0CLineBreak0" },
            { typeof(WinRT.WindowsUIXamlDocuments.RunPage), "W0CRun0" },
            { typeof(WinRT.WindowsUIXamlDocuments.TextElementPage), "W0CTextElement0" },
            { typeof(WinRT.WindowsGlobalization.WindowsGlobalizationPage), "W0DWindowsGlobalization" },
            { typeof(WinRT.WindowsGlobalization.DayOfWeekPage), "W0DDayOfWeek0" },
            { typeof(WinRT.WindowsGlobalization.JapanesePhonemePage), "W0DJapanesePhoneme0" },
            { typeof(WinRT.WindowsGlobalization.JapanesePhoneticAnalyzerPage), "W0DJapanesePhoneticAnalyzer0" },
            { typeof(WinRT.WindowsGlobalization.LanguageLayoutDirectionPage), "W0DLanguageLayoutDirection0" },
            { typeof(WinRT.WindowsGlobalization.NumeralSystemIdentifiersPage), "W0DNumeralSystemIdentifiers0" },
            { typeof(WinRT.WindowsGlobalizationFonts.WindowsGlobalizationFontsPage), "W0EWindowsGlobalizationFonts" },
            { typeof(WinRT.WindowsGlobalizationFonts.LanguageFontPage), "W0ELanguageFont0" },
            { typeof(WinRT.WindowsGlobalizationFonts.LanguageFontGroupPage), "W0ELanguageFontGroup0" },
            { typeof(WinRT.WindowsFoundation.WindowsFoundationPage), "W0FWindowsFoundation" },
            { typeof(WinRT.WindowsFoundation.IClosablePage), "W0FIClosable0" },
            { typeof(WinRT.WindowsFoundation.IStringablePage), "W0FIStringable0" },
        };

        private static Dictionary<string, string> AMD { get; } = new Dictionary<string, string>()
        {
            {"home", "ライブラリ 一覧"},
            {"New", "新規追加項目"},
            {"Updated", "更新された項目"},
            {"Release", "リリースノート"},
            {"BaseType", "基本型"},
            {"BaseOperator", "基本演算子" },
            {"GCCCommand", "GCC コマンド生成" },
        };


        private static SortedDictionary<string, string> SuggestDictionary { get; } = new SortedDictionary<string, string>();
        private static SortedDictionary<string, string> UseCppSuggestDictionary { get; } = new SortedDictionary<string, string>();
        private static SortedDictionary<string, string> SuggestMemberDisabledDictionary { get; } = new SortedDictionary<string, string>();
        private static SortedDictionary<string, string> UseCppSuggestMemberDisabledDictionary { get; } = new SortedDictionary<string, string>();
        private static SortedDictionary<string, string> CurrentSuggestDictionary
        {
            get
            {
                if (App.UseCppInCSample)
                {
                    return App.IsSearchClassMemberEnabled ? UseCppSuggestDictionary : UseCppSuggestMemberDisabledDictionary;
                }
                else
                {
                    return App.IsSearchClassMemberEnabled ? SuggestDictionary : SuggestMemberDisabledDictionary;
                }
            }
        }

        public MainPage()
        {
            this.InitializeComponent();

            UserAPI.ToCompactForResources(this, NavView, ContentFrame);

            if (App.UseCppInCSample) ChangeCHeaderName();
            CurrentUseCppSetting = App.UseCppInCSample;
        }

        public static void Initialize()
        {
            if (UseCppSuggestDictionary.Count != 0) return;

            DirectoryInfo di = new DirectoryInfo(Data.DefaultSearchingTextPath);
            FileInfo[] fiAlls = di.GetFiles();

            foreach (FileInfo fileInfo in fiAlls)
            {
                using (StreamReader streamReader = new StreamReader(fileInfo.FullName))
                {
                    string name = streamReader.ReadLine();
                    string useCppName;
                    string fileName = fileInfo.Name.Replace(".txt", "");

                    //CHeader
                    if (fileName[0] == '0')
                    {
                        useCppName = name.Replace("<", "<c").Replace(".h>", ">");
                    }
                    else
                    {
                        useCppName = name;
                    }

                    SuggestDictionary.Add(name, fileName);
                    UseCppSuggestDictionary.Add(useCppName, fileName);
                    SuggestMemberDisabledDictionary.Add(name, fileName);
                    UseCppSuggestMemberDisabledDictionary.Add(useCppName, fileName);
                    AMD.Add(fileName, name);
                    

                    if (!name.StartsWith('<'))
                    {
                        name = '(' + name + ')';
                        useCppName = '(' + useCppName + ')';
                    }


                    bool IsClassMember = false;
                    while (!streamReader.EndOfStream)
                    {
                        string readed = streamReader.ReadLine();

                        if(readed == "?")
                        {
                            IsClassMember = true;
                        }

                        if (!IsClassMember)
                        {
                            if (!fileName.EndsWith("0"))
                            {
                                SuggestMemberDisabledDictionary.Add(readed + "  " + name, fileName);
                                UseCppSuggestMemberDisabledDictionary.Add(readed + "  " + useCppName, fileName);
                            }
                            else
                            {
                                SuggestMemberDisabledDictionary.Add(readed, fileName);
                                UseCppSuggestMemberDisabledDictionary.Add(readed, fileName);
                            }
                            
                        }

                        SuggestDictionary.Add(readed + "  " + name, fileName);
                        UseCppSuggestDictionary.Add(readed + "  " + useCppName, fileName);
                    }
                }
            }

            //ヘッダー名変更
            AMD["HDwmSetWindowAttribute0"] = "DwmSetWindowAttribute()";
        }

        public void ChangeCHeaderName()
        {
            foreach(var item in NavView.MenuItems)
            {
                if (item is NavigationViewItem nitem)
                {
                    var str = nitem.Content as string;
                    if ((nitem.Tag as string)[0] == '0')
                    {
                        if (App.UseCppInCSample)
                        {
                            nitem.Content = str.Replace("<", "<c").Replace(".h>", ">");
                        }
                        else
                        {
                            nitem.Content = str.Replace("<c", "<").Replace(">", ".h>");
                        }
                    }
                }
            }
        }

        private void SetHeader(string tag)
        {
            //NavView.Header = AMD[tag];
            openedst = tag;
            if (App.UseCppInCSample)
            {
                if (tag.StartsWith("0"))
                {
                    HeadBar.ItemsSource = new string[] { AMD[tag].Replace("<", "<c").Replace(".h>", ">") };

                    if (App.IsFirstCSampleOpened)
                    {
                        App.IsFirstCSampleOpened = false;
                        MainWindow.GetParentMainWindow(this).ShowCSampleTeaching();
                    }

                    return;
                }
            }
            HeadBar.ItemsSource = new string[] { AMD[tag] };
        }

        private void SetHeader(string folder, string tag)
        {
            //NavView.Header = AMD[tag];
            HeadBar.ItemsSource = new string[] { folder, AMD[tag] };
            openedst = tag;
        }

        private NavigationViewItem GetNavigationViewItem(string tag)
            => NavView.MenuItems.OfType<NavigationViewItem>().First(i => i.Tag.ToString() == tag);

        private NavigationViewItem StringItem(string tag)
            => stringc.MenuItems.OfType<NavigationViewItem>().First(i => i.Tag.ToString() == tag);

        private NavigationViewItem ListItem(string tag)
            => list.MenuItems.OfType<NavigationViewItem>().First(i => i.Tag.ToString() == tag);

        private NavigationViewItem IstreamItem(string tag)
            => istream.MenuItems.OfType<NavigationViewItem>().First(i => i.Tag.ToString() == tag);

        private NavigationViewItem OstreamItem(string tag)
            => ostream.MenuItems.OfType<NavigationViewItem>().First(i => i.Tag.ToString() == tag);

        private NavigationViewItem Initializer_listItem(string tag)
            => initializer_list.MenuItems.OfType<NavigationViewItem>().First(i => i.Tag.ToString() == tag);

        private NavigationViewItem StreambufItem(string tag)
            => streambuf.MenuItems.OfType<NavigationViewItem>().First(i => i.Tag.ToString() == tag);

        private NavigationViewItem SstreamItem(string tag)
            => sstream.MenuItems.OfType<NavigationViewItem>().First(i => i.Tag.ToString() == tag);

        private NavigationViewItem FstreamItem(string tag)
            => fstream.MenuItems.OfType<NavigationViewItem>().First(i => i.Tag.ToString() == tag);

        private NavigationViewItem IosItem(string tag)
            => ios.MenuItems.OfType<NavigationViewItem>().First(i => i.Tag.ToString() == tag);

        private NavigationViewItem ComplexItem(string tag)
            => complex.MenuItems.OfType<NavigationViewItem>().First(i => i.Tag.ToString() == tag);

        private NavigationViewItem MapItem(string tag)
            => map.MenuItems.OfType<NavigationViewItem>().First(i => i.Tag.ToString() == tag);

        private NavigationViewItem VectorItem(string tag)
            => vector.MenuItems.OfType<NavigationViewItem>().First(i => i.Tag.ToString() == tag);

        private NavigationViewItem ValarrayItem(string tag)
            => valarray.MenuItems.OfType<NavigationViewItem>().First(i => i.Tag.ToString() == tag);

        private NavigationViewItem RandomItem(string tag)
           => random.MenuItems.OfType<NavigationViewItem>().First(i => i.Tag.ToString() == tag);

        private NavigationViewItem LimitsItem(string tag)
           => limits.MenuItems.OfType<NavigationViewItem>().First(i => i.Tag.ToString() == tag);

        private NavigationViewItem BitSetItem(string tag)
           => bitset.MenuItems.OfType<NavigationViewItem>().First(i => i.Tag.ToString() == tag);

        private NavigationViewItem StrStreamItem(string tag)
           => strstream.MenuItems.OfType<NavigationViewItem>().First(i => i.Tag.ToString() == tag);

        private NavigationViewItem ArrayItem(string tag)
           => array.MenuItems.OfType<NavigationViewItem>().First(i => i.Tag.ToString() == tag);

        private NavigationViewItem IosFwdItem(string tag)
           => iosfwd.MenuItems.OfType<NavigationViewItem>().First(i => i.Tag.ToString() == tag);

        private static NavigationViewItem GetItem(IList<object> menuItems, string tag)
        {
            foreach (var item in menuItems)
            {
                var navItem = item as NavigationViewItem;
                if((string)navItem.Tag == tag)
                {
                    return navItem;
                }
            }

            throw new NotImplementedException();
        }

        //staticにマークしてはいけない
        private void Suggest_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                var suitableItems = new List<string>();
                var splitText = sender.Text.ToLower();
                foreach (var cat in CurrentSuggestDictionary)
                {
                    if (cat.Key.ToLower().StartsWith(splitText))
                    {
                        suitableItems.Add(cat.Key);
                    }
                }
                foreach (var cat in CurrentSuggestDictionary)
                {
                    if (cat.Key.ToLower().Contains(splitText))
                    {
                        if (!suitableItems.Contains(cat.Key))
                        {
                            suitableItems.Add(cat.Key);
                            
                        }
                        continue;
                    }
                }
                if (suitableItems.Count == 0)
                {
                    suitableItems.Add("結果が見つかりませんでした");
                }
                sender.ItemsSource = suitableItems;
            }

        }

        private void Suggest_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            ChosedItemsTag = CurrentSuggestDictionary[(string)args.SelectedItem];
            
        }

        private void SetItem(NavigationViewItem item, ref string folder, string tag)
        {
            item.IsExpanded = true;
            folder = AMD[item.Tag as string];
            NavView.SelectedItem = GetItem(item.MenuItems, tag);
        }

        private void ContentFrame_Navigated(object sender, NavigationEventArgs e)
        {
            var tag = PageDictionary[ContentFrame.SourcePageType];
            if (tag[^1] == '0')
            {
                string folder = string.Empty;
                switch (tag[0])
                {
                    case '1':
                        stringc.IsExpanded = true;
                        NavView.SelectedItem = StringItem(tag);
                        folder = AMD["1stringc"];
                        break;

                    case '3':
                        limits.IsExpanded = true;
                        NavView.SelectedItem = LimitsItem(tag);
                        folder = AMD["3limits"];
                        break;

                    case '5':
                        bitset.IsExpanded = true;
                        NavView.SelectedItem = BitSetItem(tag);
                        folder = AMD["5bitset"];
                        break;

                    case '6':
                        strstream.IsExpanded = true;
                        NavView.SelectedItem = StrStreamItem(tag);
                        folder = AMD["6strstream"];
                        break;

                    case '7':
                        array.IsExpanded = true;
                        NavView.SelectedItem = ArrayItem(tag);
                        folder = AMD["7array"];
                        break;

                    case '8':
                        iosfwd.IsExpanded = true;
                        NavView.SelectedItem = IosFwdItem(tag);
                        folder = AMD["8iosfwd"];
                        break;

                    case 'a':
                        ios.IsExpanded = true;
                        NavView.SelectedItem = IosItem(tag);
                        folder = AMD["aios"];
                        break;

                    case 'c':
                        list.IsExpanded = true;
                        NavView.SelectedItem = ListItem(tag);
                        folder = AMD["clist"];
                        break;

                    case 'd':
                        istream.IsExpanded = true;
                        NavView.SelectedItem = IstreamItem(tag);
                        folder = AMD["distream"];
                        break;

                    case 'e':
                        map.IsExpanded = true;
                        NavView.SelectedItem = MapItem(tag);
                        folder = AMD["emap"];
                        break;

                    case 'f':
                        ostream.IsExpanded = true;
                        NavView.SelectedItem = OstreamItem(tag);
                        folder = AMD["fostream"];
                        break;

                    case 'g':
                        streambuf.IsExpanded = true;
                        NavView.SelectedItem = StreambufItem(tag);
                        folder = AMD["gstreambuf"];
                        break;

                    case 'h':
                        vector.IsExpanded = true;
                        NavView.SelectedItem = VectorItem(tag);
                        folder = AMD["hvector"];
                        break;

                    case 'i':
                        fstream.IsExpanded = true;
                        NavView.SelectedItem = FstreamItem(tag);
                        folder = AMD["ifstream"];
                        break;

                    case 'j':
                        valarray.IsExpanded = true;
                        NavView.SelectedItem = ValarrayItem(tag);
                        folder = AMD["jvalarray"];
                        break;

                    case 'k':
                        initializer_list.IsExpanded = true;
                        NavView.SelectedItem = Initializer_listItem(tag);
                        folder = AMD["kinitializer_list"];
                        break;

                    case 'l':
                        sstream.IsExpanded = true;
                        NavView.SelectedItem = SstreamItem(tag);
                        folder = AMD["lsstream"];
                        break;

                    case 'n':
                        random.IsExpanded = true;
                        NavView.SelectedItem = RandomItem(tag);
                        folder = AMD["nrandom"];
                        break;

                    case 'o':
                        complex.IsExpanded = true;
                        NavView.SelectedItem = ComplexItem(tag);
                        folder = AMD["ocomplex"];
                        break;

                    case 'p':
                        stack.IsExpanded = true;
                        NavView.SelectedItem = GetItem(stack.MenuItems, tag);
                        folder = AMD["pstack"];
                        break;

                    case 'q':
                        queue.IsExpanded = true;
                        NavView.SelectedItem = GetItem(queue.MenuItems, tag);
                        folder = AMD["qqueue"];
                        break;

                    case 'r':
                        any.IsExpanded = true;
                        NavView.SelectedItem = GetItem(any.MenuItems, tag);
                        folder = AMD["rany"];
                        break;

                    case 't':
                        string_view.IsExpanded = true;
                        NavView.SelectedItem = GetItem(string_view.MenuItems, tag);
                        folder = AMD["tstring_view"];
                        break;

                    case 'v':
                        span.IsExpanded = true;
                        NavView.SelectedItem = GetItem(span.MenuItems, tag);
                        folder = AMD["vspan"];
                        break;

                    case 'w':
                        filesystem.IsExpanded = true;
                        NavView.SelectedItem = GetItem(filesystem.MenuItems, tag);
                        folder = AMD["wfilesystem"];
                        break;

                    case 'x':
                        forward_list.IsExpanded = true;
                        NavView.SelectedItem = GetItem(forward_list.MenuItems, tag);
                        folder = AMD["xforward_list"];
                        break;

                    case 'y':
                        deque.IsExpanded = true;
                        NavView.SelectedItem = GetItem(deque.MenuItems, tag);
                        folder = AMD["ydeque"];
                        break;

                    case 'A':
                        set.IsExpanded = true;
                        NavView.SelectedItem = GetItem(set.MenuItems, tag);
                        folder = AMD["Aset"];
                        break;

                    case 'E':
                        SourceLocation.IsExpanded = true;
                        folder = AMD["Esource_location"];
                        NavView.SelectedItem = GetItem(SourceLocation.MenuItems, tag);
                        break;

                    case 'G':
                        typeinfo.IsExpanded = true;
                        folder = AMD["Gtypeinfo"];
                        NavView.SelectedItem = GetItem(typeinfo.MenuItems, tag);
                        break;

                    case 'H':
                        folder = AMD["Hdwmapi"];
                        NavView.SelectedItem = GetNavigationViewItem("Hdwmapi");
                        break;

                    case 'J':
                        format.IsExpanded = true;
                        folder = AMD["Jformat"];
                        NavView.SelectedItem = GetItem(format.MenuItems, tag);
                        break;

                    case 'L':
                        optional.IsExpanded = true;
                        folder = AMD["Loptional"];
                        NavView.SelectedItem = GetItem(optional.MenuItems, tag);
                        break;

                    case 'M':
                        typeindex.IsExpanded = true;
                        folder = AMD["Mtypeindex"];
                        NavView.SelectedItem = GetItem(typeindex.MenuItems, tag);
                        break;

                    case 'N':
                        compare.IsExpanded = true;
                        folder = AMD["Ncompare"];
                        NavView.SelectedItem = GetItem(compare.MenuItems, tag);
                        break;

                    //C++/WinRT
                    case 'W':
                        switch (tag[1])
                        {

                            case '0':
                                switch (tag[2])
                                {
                                    case '0':
                                        WindowsUIXamlControls.IsExpanded = true;
                                        folder = AMD["W00WindowsUIXamlControls"];
                                        NavView.SelectedItem = GetItem(WindowsUIXamlControls.MenuItems, tag);

                                        break;

                                    case '1':
                                        WindowsUIXaml.IsExpanded = true;
                                        folder = AMD["W01WindowsUIXaml"];
                                        NavView.SelectedItem = GetItem(WindowsUIXaml.MenuItems, tag);

                                        break;

                                    case '2':
                                        SetItem(Baseh, ref folder, tag);

                                        break;

                                    case '3':
                                        SetItem(WindowsUIText, ref folder, tag);
                                        break;

                                    case '4':
                                        SetItem(WindowsUI, ref folder, tag);
                                        break;

                                    case '5':
                                        SetItem(WindowsUIXamlMarkup, ref folder, tag);
                                        break;

                                    case '6':
                                        SetItem(WindowsStoragePickers, ref folder, tag);
                                        break;

                                    case '7':
                                        SetItem(WindowsDevicesPower, ref folder, tag);
                                        break;

                                    case '8': SetItem(WindowsSystemDisplay, ref folder, tag); break;
                                    case '9': SetItem(WindowsSystemInventory, ref folder, tag); break;
                                    case 'A': SetItem(WindowsFoundationCollections, ref folder, tag); break;
                                    case 'B': SetItem(WindowsSystemPower, ref folder, tag); break;
                                    case 'C': SetItem(WindowsUIXamlDocuments, ref folder, tag); break;
                                    case 'D': SetItem(WindowsGlobalization, ref folder, tag); break;
                                    case 'E': SetItem(WindowsGlobalizationFonts, ref folder, tag); break;
                                    case 'F': SetItem(WindowsFoundation, ref folder, tag); break;
                                }

                                break;
                        }

                        break;

                }
                SetHeader(folder, tag);
            }
            else
            {
                NavView.SelectedItem = GetNavigationViewItem(tag);
                SetHeader(tag);

            }

            int count = (int)this.Tag;

            foreach (var element in MainWindow.GetParentMainWindow(this).TabChildren)
            {
                var item = element as TabViewItem;

                if ((int)item.Tag == count)
                {
                    if (App.UseCppInCSample && tag[0] == '0') item.Header = AMD[tag].Replace("<", "<c").Replace(".h>", ">");
                    else item.Header = AMD[tag];
                    item.IconSource = new FontIconSource { Glyph = ((NavView.SelectedItem as NavigationViewItem).Icon as FontIcon).Glyph };
                    break;
                }
            }

            NavView.IsBackEnabled = ContentFrame.CanGoBack;
            GC.Collect();
        }

        private void NavView_Loaded(object sender, RoutedEventArgs e)
        {
            foreach(var element in NavView.MenuItems)
            {
                if(element is NavigationViewItem item)
                {
                    item.ContextFlyout = UserAPI.GetMenuFlyout(item.Tag as string, this);
                    item.RightTapped += (s, e) =>
                    {
                        (s as NavigationViewItem).ContextFlyout.ShowAt(s as FrameworkElement);
                    };
                    foreach (var menuItem in item.MenuItems)
                    {
                        (menuItem as NavigationViewItem).ContextFlyout = UserAPI.GetMenuFlyout((menuItem as NavigationViewItem).Tag as string, this);
                        (menuItem as NavigationViewItem).ContextRequested += (s, e) =>
                        {
                            (s as NavigationViewItem).ContextFlyout.ShowAt(s as FrameworkElement);
                        };
                    }
                }
            }
            NavView_Navigate(FirstShowPageTag);

            NavView.Loaded -= NavView_Loaded;
        }

        private void NavView_Navigate(string tag, bool left = false)
        {
            if (tag != openedst)
            {
                var targetType = PageDictionary.First(c => c.Value.Equals(tag)).Key;
                if (openedst == "ホーム")
                {
                    ContentFrame.Navigate(targetType, null, new DrillInNavigationTransitionInfo());
                }
                else
                {
                    if (left)
                    {
                        ContentFrame.Navigate(targetType, null, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromLeft });
                    }
                    else
                    {
                        ContentFrame.Navigate(targetType, null);
                    }
                }

            }
            NavView.IsBackEnabled = ContentFrame.CanGoBack;
            GC.Collect();
        }

        private void NavView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            string str = args.InvokedItemContainer.Tag.ToString();
            NavView_Navigate(str, false);

        }

        private void NavView_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {
            ContentFrame.GoBack();
        }

        private void HeadBar_ItemClicked(BreadcrumbBar sender, BreadcrumbBarItemClickedEventArgs args)
        {
            string head = args.Item.ToString();
            foreach (var a in AMD)
            {
                if (a.Value == head)
                {
                    NavView_Navigate(a.Key, true);
                }
            }
        }

        private void Suggest_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            if (args.ChosenSuggestion != null)
            {
                NavView_Navigate(ChosedItemsTag, false);
            }
                
        }

        //マウスのサイドボタンでページを戻る
        private void NavView_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            PointerPoint ptrPt = e.GetCurrentPoint(sender as UIElement);
            if (ptrPt.Properties.IsXButton1Pressed)
            {
                if (ContentFrame.CanGoBack)
                {
                    ContentFrame.GoBack();
                }
            }
        }

        public async void UpdateSample()
        {
            //Cヘッダーの名前変更
            //<cstdio> <=> <stdio.h>
            if(CurrentUseCppSetting != App.UseCppInCSample)
            {
                CurrentUseCppSetting = App.UseCppInCSample;
                ChangeCHeaderName();
            }

            if (UpdateWorking) return;
            UpdateWorking = true;

            ContentDialog dialog = new ContentDialog
            {
                RequestedTheme = App.Theme,
                XamlRoot = this.XamlRoot,
                Title = "再読み込みが必要です",
                Content = "\n設定の変更を反映するには項目を再読み込みする必要があります",
                PrimaryButtonText = "再読み込み",
                SecondaryButtonText = "無視",
                DefaultButton = ContentDialogButton.Primary,
            };

            var resylt = await dialog.ShowAsync();

            if(resylt == ContentDialogResult.Primary)
            {
                ContentFrame.Navigate(ContentFrame.SourcePageType);
            }

            UpdateWorking = false;
            _updated = true;
        }

        public static void RequestUpdate()
        {
            foreach(var window in MainWindow.WindowList)
            {
                foreach (var element in window.Children)
                {
                    if (element is MainPage mainPage)
                    {
                        mainPage._updated = false;
                    }
                }
            }
        }
    }
}
