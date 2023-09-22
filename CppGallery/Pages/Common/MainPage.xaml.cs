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
            {typeof(NewContentPage), "New"},
            {typeof(UpdatedPage), "Updated"},
            {typeof(ReleaseNote), "Release"},
            {typeof(BaseTypePage), "BaseType"},
            {typeof(OperatorPage), "BaseOperator" },
            {typeof(GCCCommandPage), "GCCCommand"},
            { typeof(CompactHomePage), "home"},
            {typeof(CHeader.AsserthPage), "0asserth" },
            {typeof(CHeader.ComplexhPage), "0complexh"},
            {typeof(CHeader.CtypehPage), "0ctypeh"},
            {typeof(CHeader.ErrnohPage), "0errnoh"},
            {typeof(CHeader.FEnvhPage), "0fenvh"},
            {typeof(CHeader.FloathPage), "0floath"},
            {typeof(CHeader.IntTypeshPage), "0inttypesh"},
            {typeof(CHeader.Iso646hPage), "0iso646h"},
            {typeof(CHeader.LimitsPage), "0limitsh"},
            {typeof(CHeader.LocalehPage), "0localeh"},
            {typeof(CHeader.MathhPage), "0mathh"},
            {typeof(CHeader.StdAlignPage), "0stdalignh"},
            {typeof(CHeader.StdboolhPage), "0stdboolh"},
            {typeof(CHeader.StdintPage), "0stdinth"},
            {typeof(CHeader.StdiohPage), "0stdioh"},
            {typeof(CHeader.StdlibhPage), "0stdlibh"},
            {typeof(CHeader.StdNoReturnhPage), "0stdnoreturnh"},
            {typeof(CHeader.StringhPage), "0stringh"},
            {typeof(CHeader.TgMathhPage), "0tgmathh"},
            {typeof(CHeader.TimehPage), "0timeh"},
            {typeof(CHeader.UcharhPage), "0ucharh"},
            {typeof(CHeader.WcharhPage), "0wcharh"},
            {typeof(CHeader.WctypehPage), "0wctypeh"},
            {typeof(String.StringPage), "1stringc"},    //未完成
            {typeof(String.BasicStringPage), "1basic_string0"},    //未完成
            {typeof(String.CharTraitsPage), "1char_traits0"},    //未完成
            {typeof(Bit.BitPage), "2bit"},    //未完成
            {typeof(Limits.LimitsPage), "3limits"},    //未完成
            {typeof(Limits.NumericLimitsPage), "3numeric_limits0"},    //未完成
            {typeof(WindowsHeader.ConiohPage), "4conioh"},    //未完成
            {typeof(WindowsHeader.ConsoleApiPage), "4ConsoleApi" },
            {typeof(WindowsHeader.ConsoleApi2Page), "4ConsoleApi2h" },
            {typeof(WindowsHeader.ConsoleApi3Page), "4ConsoleApi3h" },
            {typeof(WindowsHeader.FIleApiPage), "4fileapi" },
            {typeof(WindowsHeader.ProcessEnvPage), "4ProcessEnv" },
            {typeof(WindowsHeader.WindowshPage), "4Windows" },
            {typeof(WindowsHeader.WinUserPage), "4WinUser" },
            {typeof(BitSet.BitSetPage), "5bitset"},    //未完成
            {typeof(BitSet.BitSetClassPage), "5bitset0"},    //未完成
            {typeof(StrStream.StrStreamPage), "6strstream"},    //未完成
            {typeof(StrStream.OstrStreamPage), "6ostrstream0"},    //未完成
            {typeof(Array.ArrayPage), "7array"},
            {typeof(Array.ArrayClassPage), "7array0"},
            {typeof(Iostream.IostreamPage), "9iostream"},
            {typeof(Ios.IosPage), "aios"},
            {typeof(Ios.BasicIosPage), "abasic_ios0"},
            {typeof(Ios.IosBasePage), "aios_base0"},
            {typeof(IosFwd.IosFwdPage), "8iosfwd"},
            {typeof(IosFwd.FposPage), "8fpos0"},
            {typeof(Iomanip.IomanipPage), "biomanip"},
            {typeof(List.ListPage), "clist"},
            {typeof(List.ListClassPage), "clist0"},
            {typeof(Istream.IstreamPage), "distream"},
            {typeof(Istream.BasicIostreamPage), "dbasic_iostream0"},
            {typeof(Istream.BasicIstreamPage), "dbasic_istream0"},
            {typeof(Map.MapPage), "emap"},
            {typeof(Map.MapClassPage), "emap0"},
            {typeof(Ostream.OstreamPage), "fostream"},
            {typeof(Ostream.BasicOstreamPage), "fbasic_ostream0"},
            {typeof(StreamBuf.StreamBufPage), "gstreambuf"},
            {typeof(StreamBuf.BasicStreamBufPage), "gbasic_streambuf0"},
            {typeof(Vector.VectorPage), "hvector"},
            {typeof(Vector.VectorClassPage), "hvector0"},
            {typeof(Fstream.FstreamPage), "ifstream"},
            {typeof(Fstream.BasicFilebufPage), "ibasic_filebuf0"},
            {typeof(Fstream.BasicFstreamPage), "ibasic_fstream0"},
            {typeof(Fstream.BasicIfstreamPage), "ibasic_ifstream0"},
            {typeof(Fstream.BasicOfstreamPage), "ibasic_ofstream0"},
            {typeof(ValArray.ValArrayPage), "jvalarray"},
            {typeof(ValArray.SlicePage), "jslice0"},
            {typeof(ValArray.ValArrayClassPage), "jvalarray0"},
            {typeof(InitializerList.InitializerListPage), "kinitializer_list" },
            {typeof(InitializerList.InitializerListClassPage), "kinitializer_list0" },
            {typeof(SStream.SStreamPage), "lsstream"},
            {typeof(SStream.BasicStringBufPage), "lbasic_stringbuf0"},
            {typeof(SStream.BasicIstringStreamPage), "lbasic_istringstream0"},
            {typeof(SStream.BasicOstringStreamPage), "lbasic_ostringstream0"},
            {typeof(SStream.BasicStringStreamPage), "lbasic_stringstream0"},
            {typeof(New.NewPage), "mnew"},
            {typeof(Random.RandomPage), "nrandom"},
            {typeof(Random.LinearCongruentialEnginePage), "nlinear_congruential_engine0"},
            {typeof(Random.MersenneTwisterEngineClassPage), "nmersenne_twister_engine0" },
            {typeof(Complex.ComplexPage), "ocomplex"},
            {typeof(Complex.ComplexCalssPage), "ocomplex0"},
            {typeof(Stack.StackPage), "pstack"},
            {typeof(Stack.StackClassPage), "pstack0"},
            {typeof(Queue.QueuePage), "qqueue"},
            {typeof(Queue.QueueClassPage), "qqueue0"},
            {typeof(Any.AnyPage), "rany" },
            {typeof(Any.AnyClassPage), "rany0" },
            {typeof(StdExcept.StdExceptPage), "sstdexcept" },
            {typeof(StringView.StringViewPage), "tstring_view" },
            {typeof(StringView.BasicStringViewClassPage), "tbasic_string_view0" },
            {typeof(TypeTraits.TypeTraitsPage), "utype_traits" },
            {typeof(Span.SpanPage), "vspan" },
            {typeof(Span.SpanClassPage), "vspan0" },
            {typeof(FileSystem.FileSystemPage), "wfilesystem" },
            {typeof(FileSystem.FileStatusPage), "wfile_status0" },
            {typeof(FileSystem.PathPage), "wpath0" },
            {typeof(FileSystem.DirectoryIteratorPage), "wdirectory_iterator0" },
            {typeof(ForwardList.ForwardListPage), "xforward_list" },
            {typeof(ForwardList.ForwardListClassPage), "xforward_list0" },
            {typeof(Deque.DequePage), "ydeque" },
            {typeof(Deque.DequeClassPage), "ydeque0" },
            {typeof(CharConv.CharConvPage), "zcharconv" },
            {typeof(Set.SetPage), "Aset" },
            {typeof(Set.SetClassPage), "Aset0" },
            {typeof(Set.MultiSetClassPage), "Amultiset0" },
            {typeof(Numeric.NumericPage), "Bnumeric" },
            {typeof(Ranges.RangesPage), "Cranges" },
            {typeof(Numbers.NumbersPage), "Dnumbers" },
            {typeof(SourceLocation.SourceLocationPage), "Esource_location" },
            {typeof(SourceLocation.SourceLocationClassPage), "Esource_location0" },
            {typeof(Concepts.ConceptsPage), "Fconcepts" },
            {typeof(TypeInfo.TypeInfoPage), "Gtypeinfo" },
            {typeof(TypeInfo.TypeInfoClassPage), "Gtype_info0" },
            {typeof(WindowsHeader.DwmAPI.DwmAPIPage), "Hdwmapi" },
            {typeof(WindowsHeader.DwmAPI.DwmSetWindowAttributePage), "HDwmSetWindowAttribute0" },
            {typeof(Print.PrintPage), "IPrint" },
            {typeof(Format.FormatPage), "Jformat" },
            {typeof(Format.BasicFormatStringPage), "Jbasic_format_string0" },
           {typeof(Algorithm.AlgorithmPage), "Kalgorithm" },
            {typeof(Optional.OptionalPage), "Loptional" },
            {typeof(Optional.OptionalClassPage), "Loptional0" },
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
            {"0asserth", "<assert.h>" },
            {"0complexh", "<complex.h>"},
            {"0ctypeh", "<ctype.h>"},
            {"0errnoh", "<errno.h>"},
            {"0fenvh", "<fenv.h>"},
            {"0floath", "<float.h>"},
            {"0inttypesh", "<inttypes.h>"},
            {"0iso646h", "<iso646.h>"},
            {"0limitsh", "<limits.h>"},
            {"0localeh", "<locale.h>"},
            {"0mathh", "<math.h>"},
            {"0stdalignh", "<stdalign.h>"},
            {"0stdboolh", "<stdbool.h>"},
            {"0stdinth", "<stdint.h>"},
            {"0stdioh", "<stdio.h>"},
            {"0stdlibh", "<stdlib.h>"},
            {"0stdnoreturnh", "<stdnoreturn.h>"},
            {"0stringh", "<string.h>"},
            {"0tgmathh", "<tgmath.h>"},
            {"0timeh", "<time.h>"},
            {"0ucharh", "<uchar.h>"},
            {"0wcharh", "<wchar.h>"},
            {"0wctypeh", "<wctype.h>"},
            {"1stringc", "<string>"},
            {"1basic_string0", "basic_string クラス"},
            {"1char_traits0", "char_traits 構造体"},
            {"2bit", "<bit>"},
            {"3limits", "<limits>"},
            {"3numeric_limits0", "numeric_limits クラス"},
            {"4conioh", "<conio.h>"},
            {"4ConsoleApi", "<ConsoleApi.h>"},
            {"4ConsoleApi2h", "<ConsoleApi2.h>"},
            {"4ConsoleApi3h", "<ConsoleApi3.h>"},
            {"4fileapi", "<fileapi.h>"},
            {"4ProcessEnv", "<ProcessEnv.h>" },
            {"4Windows", "<Windows.h>" },
            {"4WinUser", "<WinUser.h>" },
            {"5bitset", "<bitset>"},
            {"5bitset0", "bitset クラス"},
            {"6strstream", "<strstream>"},
            {"6ostrstream0", "ostrstream クラス"},
            {"7array", "<array>" },
            {"7array0", "array クラス" },
            {"8iosfwd", "<iosfwd>"},
            {"8fpos0", "fpos クラス"},
            {"9iostream", "<iostream>"},
            {"aios", "<ios>"},
            {"abasic_ios0", "basic_ios クラス"},
            {"aios_base0", "ios_base クラス"},
            {"biomanip", "<iomanip>"},
            {"clist", "<list>"},
            {"clist0", "list クラス"},
            {"distream", "<istream>"},
            {"dbasic_iostream0", "basic_iostream クラス"},
            {"dbasic_istream0", "basic_istream クラス"},
            {"emap", "<map>"},
            {"emap0", "map クラス"},
            {"fostream", "<ostream>"},
            {"fbasic_ostream0", "basic_ostream クラス"},
            {"gstreambuf", "<streambuf>"},
            {"gbasic_streambuf0", "basic_streambuf クラス"},
            {"hvector", "<vector>"},
            {"hvector0", "vector クラス"},
            {"ifstream", "<fstream>"},
            {"ibasic_filebuf0", "basic_filebuf クラス"},
            {"ibasic_fstream0", "basic_fstream クラス"},
            {"ibasic_ifstream0", "basic_ifstream クラス"},
            {"ibasic_ofstream0", "basic_ofstream クラス"},
            {"jvalarray", "<valarray>"},
            {"jslice0", "slice クラス"},
            {"jvalarray0", "valarray クラス"},
            {"kinitializer_list", "<initializer_list>"},
            {"kinitializer_list0", "initializer_list クラス"},
            {"lsstream", "<sstream>"},
            {"lbasic_stringbuf0", "basic_stringbuf クラス"},
            {"lbasic_istringstream0", "basic_istringstream クラス"},
            {"lbasic_ostringstream0", "basic_ostringstream クラス"},
            {"lbasic_stringstream0", "basic_stringstream クラス"},
            {"mnew", "<new>"},
            {"nrandom", "<random>"},
            {"nlinear_congruential_engine0", "linear_congruential_engine クラス"},
            {"nmersenne_twister_engine0", "mersenne_twister_engine クラス" },
            {"ocomplex", "<complex>"},
            {"ocomplex0", "complex クラス"},
            {"pstack", "<stack>" },
            {"pstack0", "stack クラス" },
            {"qqueue", "<queue>" },
            {"qqueue0", "queue クラス" },
            {"rany", "<any>" },
            {"rany0", "any クラス" },
            {"sstdexcept", "<stdexcept>" },
            {"tstring_view", "<string_view>" },
            {"tbasic_string_view0", "basic_string_view クラス" },
            {"utype_traits", "<type_traits>" },
            {"vspan", "<span>" },
            {"vspan0", "span クラス" },
            {"wfilesystem", "<filesystem>" },
            {"wfile_status0", "file_status クラス" },
            {"wpath0", "path クラス" },
            {"wdirectory_iterator0", "directory_iterator クラス" },
            {"xforward_list", "<forward_list>" },
            {"xforward_list0", "forward_list クラス" },
            {"ydeque", "<deque>" },
            {"ydeque0", "deque クラス" },
            {"zcharconv", "<charconv>" },
            {"Aset", "<set>" },
            {"Aset0", "set クラス" },
            {"Amultiset0", "multiset クラス" },
            {"Bnumeric", "<numeric>" },
            {"Cranges", "<ranges>" },
            {"Dnumbers", "<numbers>" },
            {"Esource_location", "<source_location>" },
            {"Esource_location0", "source_location クラス" },
            {"Fconcepts", "<concepts>" },
            {"Gtypeinfo", "<typeinfo>" },
            {"Gtype_info0", "type_info クラス" },
            {"Hdwmapi", "<dwmapi.h>" },
            {"HDwmSetWindowAttribute0", "DwmSetWindowAttribute() 関数" },
            {"IPrint", "<print>" },
            {"Jformat", "<format>" },
            {"Jbasic_format_string0", "basic_format_string 構造体" },
            {"Kalgorithm", "<algorithm>" },
            {"Loptional", "<optional>" },
            {"Loptional0", "optional クラス" },
        };


        private static SortedDictionary<string, string> SuggestDictionary { get; } = new SortedDictionary<string, string>();
        private static SortedDictionary<string, string> UseCppSuggestDictionary { get; } = new SortedDictionary<string, string>();
        private static SortedDictionary<string, string> CurrentSuggestDictionary => App.UseCppInCSample ? UseCppSuggestDictionary : SuggestDictionary;

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

                    if (!name.StartsWith('<'))
                    {
                        name = '(' + name + ')';
                        useCppName = '(' + useCppName + ')';
                    }

                    while (!streamReader.EndOfStream)
                    {
                        string readed = streamReader.ReadLine();
                        SuggestDictionary.Add(readed + "  " + name, fileName);
                        UseCppSuggestDictionary.Add(readed + "  " + useCppName, fileName);
                    }
                }
            }
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


        private void Suggest_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            var dictionary = App.UseCppInCSample ? UseCppSuggestDictionary : SuggestDictionary;
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                var suitableItems = new List<string>();
                var splitText = sender.Text.ToLower();
                foreach (var cat in dictionary)
                {
                    if (cat.Key.ToLower().StartsWith(splitText))
                    {
                        suitableItems.Add(cat.Key);
                    }
                }
                foreach (var cat in dictionary)
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
            var dictionary = App.UseCppInCSample ? UseCppSuggestDictionary : SuggestDictionary;
            ChosedItemsTag = dictionary[(string)args.SelectedItem];
            
        }

        private void ContentFrame_Navigated(object sender, NavigationEventArgs e)
        {
            var tag = PageDictionary[ContentFrame.SourcePageType];
            if (tag[tag.Length - 1] == '0')
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

        //ほかのウィンドうで設定を変更した時用
        private void Page_GettingFocus(UIElement sender, GettingFocusEventArgs args)
        {
#if false
            if (!_updated)
            {
                UpdateSample();
            }
#endif
        }
    }
}
