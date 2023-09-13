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
        public string openedst = "�z�[��";
        public static MainPage Handle { get; set; }
        public Frame FrameHandle => ContentFrame;
        private string ChosedItemsTag;
        public string FirstShowPageTag = "home";
        private bool CurrentUseCppSetting = false;

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
            {typeof(String.StringPage), "1stringc"},    //������
            {typeof(String.BasicStringPage), "1basic_string0"},    //������
            {typeof(String.CharTraitsPage), "1char_traits0"},    //������
            {typeof(Bit.BitPage), "2bit"},    //������
            {typeof(Limits.LimitsPage), "3limits"},    //������
            {typeof(Limits.NumericLimitsPage), "3numeric_limits0"},    //������
            {typeof(WindowsHeader.ConiohPage), "4conioh"},    //������
            {typeof(WindowsHeader.ConsoleApiPage), "4ConsoleApi" },
            {typeof(WindowsHeader.ConsoleApi2Page), "4ConsoleApi2h" },
            {typeof(WindowsHeader.ConsoleApi3Page), "4ConsoleApi3h" },
            {typeof(WindowsHeader.ProcessEnvPage), "4ProcessEnv" },
            {typeof(WindowsHeader.WindowshPage), "4Windows" },
            {typeof(WindowsHeader.WinUserPage), "4WinUser" },
            {typeof(BitSet.BitSetPage), "5bitset"},    //������
            {typeof(BitSet.BitSetClassPage), "5bitset0"},    //������
            {typeof(StrStream.StrStreamPage), "6strstream"},    //������
            {typeof(StrStream.OstrStreamPage), "6ostrstream0"},    //������
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
           {typeof(Algorithm.AlgorithmPage), "Kalgorithm" },
        };

        private static Dictionary<string, string> AMD { get; } = new Dictionary<string, string>()
        {
            {"home", "���C�u���� �ꗗ"},
            {"New", "�V�K�ǉ�����"},
            {"Updated", "�X�V���ꂽ����"},
            {"Release", "�����[�X�m�[�g"},
            {"BaseType", "��{�^"},
            {"BaseOperator", "��{���Z�q" },
            {"GCCCommand", "GCC �R�}���h����" },
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
            {"1basic_string0", "basic_string �N���X"},
            {"1char_traits0", "char_traits �\����"},
            {"2bit", "<bit>"},
            {"3limits", "<limits>"},
            {"3numeric_limits0", "numeric_limits �N���X"},
            {"4conioh", "<conio.h>"},
            {"4ConsoleApi", "<ConsoleApi.h>"},
            {"4ConsoleApi2h", "<ConsoleApi2.h>"},
            {"4ConsoleApi3h", "<ConsoleApi3.h>"},
            {"4ProcessEnv", "<ProcessEnv.h>" },
            {"4Windows", "<Windows.h>" },
            {"4WinUser", "<WinUser.h>" },
            {"5bitset", "<bitset>"},
            {"5bitset0", "bitset �N���X"},
            {"6strstream", "<strstream>"},
            {"6ostrstream0", "ostrstream �N���X"},
            {"7array", "<array>" },
            {"7array0", "array �N���X" },
            {"8iosfwd", "<iosfwd>"},
            {"8fpos0", "fpos �N���X"},
            {"9iostream", "<iostream>"},
            {"aios", "<ios>"},
            {"abasic_ios0", "basic_ios �N���X"},
            {"aios_base0", "ios_base �N���X"},
            {"biomanip", "<iomanip>"},
            {"clist", "<list>"},
            {"clist0", "list �N���X"},
            {"distream", "<istream>"},
            {"dbasic_iostream0", "basic_iostream �N���X"},
            {"dbasic_istream0", "basic_istream �N���X"},
            {"emap", "<map>"},
            {"emap0", "map �N���X"},
            {"fostream", "<ostream>"},
            {"fbasic_ostream0", "basic_ostream �N���X"},
            {"gstreambuf", "<streambuf>"},
            {"gbasic_streambuf0", "basic_streambuf �N���X"},
            {"hvector", "<vector>"},
            {"hvector0", "vector �N���X"},
            {"ifstream", "<fstream>"},
            {"ibasic_filebuf0", "basic_filebuf �N���X"},
            {"ibasic_fstream0", "basic_fstream �N���X"},
            {"ibasic_ifstream0", "basic_ifstream �N���X"},
            {"ibasic_ofstream0", "basic_ofstream �N���X"},
            {"jvalarray", "<valarray>"},
            {"jslice0", "slice �N���X"},
            {"jvalarray0", "valarray �N���X"},
            {"kinitializer_list", "<initializer_list>"},
            {"kinitializer_list0", "initializer_list �N���X"},
            {"lsstream", "<sstream>"},
            {"lbasic_stringbuf0", "basic_stringbuf �N���X"},
            {"lbasic_istringstream0", "basic_istringstream �N���X"},
            {"lbasic_ostringstream0", "basic_ostringstream �N���X"},
            {"lbasic_stringstream0", "basic_stringstream �N���X"},
            {"mnew", "<new>"},
            {"nrandom", "<random>"},
            {"nlinear_congruential_engine0", "linear_congruential_engine �N���X"},
            {"ocomplex", "<complex>"},
            {"ocomplex0", "complex �N���X"},
            {"pstack", "<stack>" },
            {"pstack0", "stack �N���X" },
            {"qqueue", "<queue>" },
            {"qqueue0", "queue �N���X" },
            {"rany", "<any>" },
            {"rany0", "any �N���X" },
            {"sstdexcept", "<stdexcept>" },
            {"tstring_view", "<string_view>" },
            {"tbasic_string_view0", "basic_string_view �N���X" },
            {"utype_traits", "<type_traits>" },
            {"vspan", "<span>" },
            {"vspan0", "span �N���X" },
            {"wfilesystem", "<filesystem>" },
            {"wfile_status0", "file_status �N���X" },
            {"wpath0", "path �N���X" },
            {"wdirectory_iterator0", "directory_iterator �N���X" },
            {"xforward_list", "<forward_list>" },
            {"xforward_list0", "forward_list �N���X" },
            {"ydeque", "<deque>" },
            {"ydeque0", "deque �N���X" },
            {"zcharconv", "<charconv>" },
            {"Aset", "<set>" },
            {"Aset0", "set �N���X" },
            {"Amultiset0", "multiset �N���X" },
            {"Bnumeric", "<numeric>" },
            {"Cranges", "<ranges>" },
            {"Dnumbers", "<numbers>" },
            {"Esource_location", "<source_location>" },
            {"Esource_location0", "source_location �N���X" },
            {"Fconcepts", "<concepts>" },
            {"Gtypeinfo", "<typeinfo>" },
            {"Gtype_info0", "type_info �N���X" },
            {"Hdwmapi", "<dwmapi.h>" },
            {"HDwmSetWindowAttribute0", "DwmSetWindowAttribute() �֐�" },
            {"IPrint", "<print>" },
            {"Jformat", "<format>" },
            {"Kalgorithm", "<algorithm>" },
        };


        private static SortedDictionary<string, string> SuggestDictionary { get; } = new SortedDictionary<string, string>()
        {
            {"<assert.h>", "0asserth" },
            {"assert()  <assert.h>", "0asserth" },
            { "<complex.h>", "0complexh"},
            { "<ctype.h>", "0ctypeh"},
            { "isalnum()  <ctype.h>", "0ctypeh"},
            { "isalpha()  <ctype.h>", "0ctypeh"},
            { "isblank()  <ctype.h>", "0ctypeh"},
            { "iscntrl()  <ctype.h>", "0ctypeh"},
            { "isdigit()  <ctype.h>", "0ctypeh"},
            { "isgraph()  <ctype.h>", "0ctypeh"},
            { "islower()  <ctype.h>", "0ctypeh"},
            { "isprint()  <ctype.h>", "0ctypeh"},
            { "ispunct()  <ctype.h>", "0ctypeh"},
            { "isspace()  <ctype.h>", "0ctypeh"},
            { "isupper()  <ctype.h>", "0ctypeh"},
            { "isxdigit()  <ctype.h>", "0ctypeh"},
            { "tolower()  <ctype.h>", "0ctypeh"},
            { "toupper()  <ctype.h>", "0ctypeh"},
            { "<errno.h>", "0errnoh"},
            { "<fenv.h>", "0fenvh"},
            { "feclearexcept()  <fenv.h>", "0fenvh"},
            { "feholdexcept()  <fenv.h>", "0fenvh"},
            { "fegetenv()  <fenv.h>", "0fenvh"},
            { "fegetexceptflag()  <fenv.h>", "0fenvh"},
            { "fegetround()  <fenv.h>", "0fenvh"},
            { "feraiseexcept()  <fenv.h>", "0fenvh"},
            { "fesetenv()  <fenv.h>", "0fenvh"},
            { "fesetexceptflag()  <fenv.h>", "0fenvh"},
            { "fesetround()  <fenv.h>", "0fenvh"},
            { "fetestexcept()  <fenv.h>", "0fenvh"},
            { "feupdateenv()  <fenv.h>", "0fenvh"},
            { "<float.h>", "0floath"},
            { "<inttypes.h>", "0inttypesh"},
            { "imaxabs()  <inttypes.h>", "0inttypesh"},
            { "imaxdiv()  <inttypes.h>", "0inttypesh"},
            { "strtoimax()  <inttypes.h>", "0inttypesh"},
            { "strtoumax()  <inttypes.h>", "0inttypesh"},
            { "wcstoimax()  <inttypes.h>", "0inttypesh"},
            { "wcstoumax()  <inttypes.h>", "0inttypesh"},
            { "<iso646.h>", "0iso646h"},
            { "<limits.h>", "0limitsh"},
            { "<locale.h>", "0localeh"},
            { "localeconv()  <locale.h>", "0localeh"},
            { "setlocale()  <locale.h>", "0localeh"},
            { "<math.h>", "0mathh"},
            { "abs()  <math.h>", "0mathh"},
            { "acos()  <math.h>", "0mathh"},
            { "acosf()  <math.h>", "0mathh"},
            { "acosh()  <math.h>", "0mathh"},
            { "acoshf()  <math.h>", "0mathh"},
            { "acoshl()  <math.h>", "0mathh"},
            { "acosl()  <math.h>", "0mathh"},
            { "asin()  <math.h>", "0mathh"},
            { "asinf()  <math.h>", "0mathh"},
            { "asinh()  <math.h>", "0mathh"},
            { "asinhf()  <math.h>", "0mathh"},
            { "asinhl()  <math.h>", "0mathh"},
            { "asinl()  <math.h>", "0mathh"},
            { "atan()  <math.h>", "0mathh"},
            { "atanf()  <math.h>", "0mathh"},
            { "atanh()  <math.h>", "0mathh"},
            { "atanhf()  <math.h>", "0mathh"},
            { "atanhl()  <math.h>", "0mathh"},
            { "atanl()  <math.h>", "0mathh"},
            { "atan2()  <math.h>", "0mathh"},
            { "atan2f()  <math.h>", "0mathh"},
            { "atan2l()  <math.h>", "0mathh"},
            { "cbrt()  <math.h>", "0mathh"},
            { "cbrtf()  <math.h>", "0mathh"},
            { "cbrtl()  <math.h>", "0mathh"},
            { "ceil()  <math.h>", "0mathh"},
            { "ceilf()  <math.h>", "0mathh"},
            { "ceill()  <math.h>", "0mathh"},
            { "copysign()  <math.h>", "0mathh"},
            { "copysignf()  <math.h>", "0mathh"},
            { "copysignl()  <math.h>", "0mathh"},
            { "cos()  <math.h>", "0mathh"},
            { "cosf()  <math.h>", "0mathh"},
            { "cosh()  <math.h>", "0mathh"},
            { "coshf()  <math.h>", "0mathh"},
            { "coshl()  <math.h>", "0mathh"},
            { "cosl()  <math.h>", "0mathh"},
            { "erf()  <math.h>", "0mathh"},
            { "erfc()  <math.h>", "0mathh"},
            { "erfcf()  <math.h>", "0mathh"},
            { "erfcl()  <math.h>", "0mathh"},
            { "erff()  <math.h>", "0mathh"},
            { "erfl()  <math.h>", "0mathh"},
            { "exp()  <math.h>", "0mathh"},
            { "exp2()  <math.h>", "0mathh"},
            { "exp2f()  <math.h>", "0mathh"},
            { "exp2l()  <math.h>", "0mathh"},
            { "expf()  <math.h>", "0mathh"},
            { "expl()  <math.h>", "0mathh"},
            { "expm1()  <math.h>", "0mathh"},
            { "expm1f()  <math.h>", "0mathh"},
            { "expm1l()  <math.h>", "0mathh"},
            { "fabs()  <math.h>", "0mathh"},
            { "fabsf()  <math.h>", "0mathh"},
            { "fabsl()  <math.h>", "0mathh"},
            { "fdim()  <math.h>", "0mathh"},
            { "fdimf()  <math.h>", "0mathh"},
            { "fdiml()  <math.h>", "0mathh"},
            { "floor()  <math.h>", "0mathh"},
            { "floorf()  <math.h>", "0mathh"},
            { "floorl()  <math.h>", "0mathh"},
            { "fma()  <math.h>", "0mathh"},
            { "fmaf()  <math.h>", "0mathh"},
            { "fmal()  <math.h>", "0mathh"},
            { "fmax()  <math.h>", "0mathh"},
            { "fmaxf()  <math.h>", "0mathh"},
            { "fmaxl()  <math.h>", "0mathh"},
            { "fmin()  <math.h>", "0mathh"},
            { "fminf()  <math.h>", "0mathh"},
            { "fminl()  <math.h>", "0mathh"},
            { "fmod()  <math.h>", "0mathh"},
            { "fmodf()  <math.h>", "0mathh"},
            { "fmodl()  <math.h>", "0mathh"},
            { "fpclassify()  <math.h>", "0mathh"},
            { "frexp()  <math.h>", "0mathh"},
            { "frexpf()  <math.h>", "0mathh"},
            { "frexpl()  <math.h>", "0mathh"},
            { "hypot()  <math.h>", "0mathh"},
            { "hypotf()  <math.h>", "0mathh"},
            { "hypotl()  <math.h>", "0mathh"},
            { "ilogb()  <math.h>", "0mathh"},
            { "ilogbf()  <math.h>", "0mathh"},
            { "ilogbl()  <math.h>", "0mathh"},
            { "isfinite()  <math.h>", "0mathh"},
            { "isgreater()  <math.h>", "0mathh"},
            { "isgreaterequal()  <math.h>", "0mathh"},
            { "isinf()  <math.h>", "0mathh"},
            { "isless()  <math.h>", "0mathh"},
            { "islessqual()  <math.h>", "0mathh"},
            { "islessgreater()  <math.h>", "0mathh"},
            { "isnan()  <math.h>", "0mathh"},
            { "isnormal()  <math.h>", "0mathh"},
            { "isundered()  <math.h>", "0mathh"},
            { "labs()  <math.h>", "0mathh"},
            { "ldexp()  <math.h>", "0mathh"},
            { "ldexpf()  <math.h>", "0mathh"},
            { "ldexpl()  <math.h>", "0mathh"},
            { "lgamma()  <math.h>", "0mathh"},
            { "lgammaf()  <math.h>", "0mathh"},
            { "lgammal()  <math.h>", "0mathh"},
            { "llabs()  <math.h>", "0mathh"},
            { "llrint()  <math.h>", "0mathh"},
            { "llrintf()  <math.h>", "0mathh"},
            { "llrintl()  <math.h>", "0mathh"},
            { "llround()  <math.h>", "0mathh"},
            { "llroundf()  <math.h>", "0mathh"},
            { "llroundl()  <math.h>", "0mathh"},
            { "log()  <math.h>", "0mathh"},
            { "log10()  <math.h>", "0mathh"},
            { "log10f()  <math.h>", "0mathh"},
            { "log10l()  <math.h>", "0mathh"},
            { "log1p()  <math.h>", "0mathh"},
            { "log1pf()  <math.h>", "0mathh"},
            { "log1pl()  <math.h>", "0mathh"},
            { "log2()  <math.h>", "0mathh"},
            { "log2f()  <math.h>", "0mathh"},
            { "log2l()  <math.h>", "0mathh"},
            { "logb()  <math.h>", "0mathh"},
            { "logbf()  <math.h>", "0mathh"},
            { "logbl()  <math.h>", "0mathh"},
            { "logf()  <math.h>", "0mathh"},
            { "logl()  <math.h>", "0mathh"},
            { "lrint()  <math.h>", "0mathh"},
            { "lrintf()  <math.h>", "0mathh"},
            { "lrintl()  <math.h>", "0mathh"},
            { "lround()  <math.h>", "0mathh"},
            { "lroundf()  <math.h>", "0mathh"},
            { "lroundl()  <math.h>", "0mathh"},
            { "modf()  <math.h>", "0mathh"},
            { "modff()  <math.h>", "0mathh"},
            { "modfl()  <math.h>", "0mathh"},
            { "nan()  <math.h>", "0mathh"},
            { "nanf()  <math.h>", "0mathh"},
            { "nanl()  <math.h>", "0mathh"},
            { "nearbyint()  <math.h>", "0mathh"},
            { "nearbyintf()  <math.h>", "0mathh"},
            { "nearbyintl()  <math.h>", "0mathh"},
            { "nextafter()  <math.h>", "0mathh"},
            { "nextafterf()  <math.h>", "0mathh"},
            { "nextafterl()  <math.h>", "0mathh"},
            { "nexttoward()  <math.h>", "0mathh"},
            { "nexttowardf()  <math.h>", "0mathh"},
            { "nexttowardl()  <math.h>", "0mathh"},
            { "pow()  <math.h>", "0mathh"},
            { "powf()  <math.h>", "0mathh"},
            { "powl()  <math.h>", "0mathh"},
            { "remainder()  <math.h>", "0mathh"},
            { "remainderf()  <math.h>", "0mathh"},
            { "remainderl()  <math.h>", "0mathh"},
            { "remquo()  <math.h>", "0mathh"},
            { "remquof()  <math.h>", "0mathh"},
            { "remquol()  <math.h>", "0mathh"},
            { "rint()  <math.h>", "0mathh"},
            { "rintf()  <math.h>", "0mathh"},
            { "rintl()  <math.h>", "0mathh"},
            { "round()  <math.h>", "0mathh"},
            { "roundf()  <math.h>", "0mathh"},
            { "roundl()  <math.h>", "0mathh"},
            { "scalbln()  <math.h>", "0mathh"},
            { "scalblnf()  <math.h>", "0mathh"},
            { "scalblnl()  <math.h>", "0mathh"},
            { "scalbn()  <math.h>", "0mathh"},
            { "scalbnf()  <math.h>", "0mathh"},
            { "scalbnl()  <math.h>", "0mathh"},
            { "signbit()  <math.h>", "0mathh"},
            { "sin()  <math.h>", "0mathh"},
            { "sinf()  <math.h>", "0mathh"},
            { "sinh()  <math.h>", "0mathh"},
            { "sinhf()  <math.h>", "0mathh"},
            { "sinhl()  <math.h>", "0mathh"},
            { "sinl()  <math.h>", "0mathh"},
            { "sqrt()  <math.h>", "0mathh"},
            { "sqrtf()  <math.h>", "0mathh"},
            { "sqrtl()  <math.h>", "0mathh"},
            { "tan()  <math.h>", "0mathh"},
            { "tanf()  <math.h>", "0mathh"},
            { "tanh()  <math.h>", "0mathh"},
            { "tanhf()  <math.h>", "0mathh"},
            { "tanhl()  <math.h>", "0mathh"},
            { "tanl()  <math.h>", "0mathh"},
            { "tgamma()  <math.h>", "0mathh"},
            { "tgammaf()  <math.h>", "0mathh"},
            { "tgammal()  <math.h>", "0mathh"},
            { "trunc()  <math.h>", "0mathh"},
            { "truncf()  <math.h>", "0mathh"},
            { "truncl()  <math.h>", "0mathh"},
            { "<stdalign.h>", "0stdalignh"},
            { "<stdbool.h>", "0stdboolh"},
            { "<stdint.h>", "0stdinth"},
            { "<stdio.h>", "0stdioh"},
            { "clarerr()  <stdio.h>", "0stdioh"},
            { "fclose()  <stdio.h>", "0stdioh"},
            { "feof()  <stdio.h>", "0stdioh"},
            { "ferror()  <stdio.h>", "0stdioh"},
            { "fflush()  <stdio.h>", "0stdioh"},
            { "fgetc()  <stdio.h>", "0stdioh"},
            { "fgetpos()  <stdio.h>", "0stdioh"},
            { "fgets()  <stdio.h>", "0stdioh"},
            { "fopen()  <stdio.h>", "0stdioh"},
            { "fopen_s()  <stdio.h>", "0stdioh"},
            { "fprintf()  <stdio.h>", "0stdioh"},
            { "fputc()  <stdio.h>", "0stdioh"},
            { "fputs()  <stdio.h>", "0stdioh"},
            { "fread()  <stdio.h>", "0stdioh"},
            { "freopen()  <stdio.h>", "0stdioh"},
            { "fscanf()  <stdio.h>", "0stdioh"},
            { "fseek()  <stdio.h>", "0stdioh"},
            { "fsetpos()  <stdio.h>", "0stdioh"},
            { "ftell()  <stdio.h>", "0stdioh"},
            { "fwrite()  <stdio.h>", "0stdioh"},
            { "getc()  <stdio.h>", "0stdioh"},
            { "getchar()  <stdio.h>", "0stdioh"},
            { "gets()  <stdio.h>", "0stdioh"},
            { "perror()  <stdio.h>", "0stdioh"},
            { "printf()  <stdio.h>", "0stdioh"},
            { "putchar()  <stdio.h>", "0stdioh"},
            { "putc()  <stdio.h>", "0stdioh"},
            { "puts()  <stdio.h>", "0stdioh"},
            { "remove()  <stdio.h>", "0stdioh"},
            { "rewind()  <stdio.h>", "0stdioh"},
            { "scanf()  <stdio.h>", "0stdioh"},
            { "setbuf()  <stdio.h>", "0stdioh"},
            { "setvbuf()  <stdio.h>", "0stdioh"},
            { "snprintf()  <stdio.h>", "0stdioh"},
            { "sprintf()  <stdio.h>", "0stdioh"},
            { "sscanf()  <stdio.h>", "0stdioh"},
            { "tmpfile()  <stdio.h>", "0stdioh"},
            { "tmpnam()  <stdio.h>", "0stdioh"},
            { "ungetc()  <stdio.h>", "0stdioh"},
            { "vfprintf()  <stdio.h>", "0stdioh"},
            { "vfscanf()  <stdio.h>", "0stdioh"},
            { "vprintf()  <stdio.h>", "0stdioh"},
            { "vscanf()  <stdio.h>", "0stdioh"},
            { "vsnprintf()  <stdio.h>", "0stdioh"},
            { "vsprintf()  <stdio.h>", "0stdioh"},
            { "vsscanf()  <stdio.h>", "0stdioh"},
            { "<stdlib.h>", "0stdlibh"},
            { "abort()  <stdlib.h>", "0stdlibh"},
            { "abs()  <stdlib.h>", "0stdlibh"},
            { "atexit()  <stdlib.h>", "0stdlibh"},
            { "atof()  <stdlib.h>", "0stdlibh"},
            { "atoi()  <stdlib.h>", "0stdlibh"},
            { "atol()  <stdlib.h>", "0stdlibh"},
            { "atoll()  <stdlib.h>", "0stdlibh"},
            { "calloc()  <stdlib.h>", "0stdlibh"},
            { "div()  <stdlib.h>", "0stdlibh"},
            { "exit()  <stdlib.h>", "0stdlibh"},
            { "free()  <stdlib.h>", "0stdlibh"},
            { "getenv()  <stdlib.h>", "0stdlibh"},
            { "labs()  <stdlib.h>", "0stdlibh"},
            { "ldiv()  <stdlib.h>", "0stdlibh"},
            { "llabs()  <stdlib.h>", "0stdlibh"},
            { "lldiv()  <stdlib.h>", "0stdlibh"},
            { "malloc()  <stdlib.h>", "0stdlibh"},
            { "mblen()  <stdlib.h>", "0stdlibh"},
            { "mbstowcs()  <stdlib.h>", "0stdlibh"},
            { "mbtowc()  <stdlib.h>", "0stdlibh"},
            { "rand()  <stdlib.h>", "0stdlibh"},
            { "realloc()  <stdlib.h>", "0stdlibh"},
            { "srand()  <stdlib.h>", "0stdlibh"},
            { "strtod()  <stdlib.h>", "0stdlibh"},
            { "strtof()  <stdlib.h>", "0stdlibh"},
            { "strtol()  <stdlib.h>", "0stdlibh"},
            { "strtold()  <stdlib.h>", "0stdlibh"},
            { "strtoll()  <stdlib.h>", "0stdlibh"},
            { "strtoul()  <stdlib.h>", "0stdlibh"},
            { "strtoull()  <stdlib.h>", "0stdlibh"},
            { "system()  <stdlib.h>", "0stdlibh"},
            { "wcstombs()  <stdlib.h>", "0stdlibh"},
            { "wctomb()  <stdlib.h>", "0stdlibh"},
            { "<stdnoreturn.h>", "0stdnoreturnh"},
            { "<string.h>", "0stringh"},
            { "memchr()  <string.h>", "0stringh"},
            { "memcmp()  <string.h>", "0stringh"},
            { "memcpy()  <string.h>", "0stringh"},
            { "memmove()  <string.h>", "0stringh"},
            { "memset()  <string.h>", "0stringh"},
            { "strcat()  <string.h>", "0stringh"},
            { "strchr()  <string.h>", "0stringh"},
            { "strcmp()  <string.h>", "0stringh"},
            { "strcoll()  <string.h>", "0stringh"},
            { "strcpy()  <string.h>", "0stringh"},
            { "strcspn()  <string.h>", "0stringh"},
            { "_strdup()  <string.h>", "0stringh"},
            { "strerror()  <string.h>", "0stringh"},
            { "strlen()  <string.h>", "0stringh"},
            { "strncat()  <string.h>", "0stringh"},
            { "strncmp()  <string.h>", "0stringh"},
            { "strncpy()  <string.h>", "0stringh"},
            { "strndup()  <string.h>", "0stringh"},
            { "strpbrk()  <string.h>", "0stringh"},
            { "strrchr()  <string.h>", "0stringh"},
            { "strspn()  <string.h>", "0stringh"},
            { "strstr()  <string.h>", "0stringh"},
            { "strtok()  <string.h>", "0stringh"},
            { "strxfrm()  <string.h>", "0stringh"},
            { "<tgmath.h>", "0tgmathh"},
            { "<time.h>", "0timeh"},
            { "asctime()  <time.h>", "0timeh"},
            { "clock()  <time.h>", "0timeh"},
            { "ctime()  <time.h>", "0timeh"},
            { "difftime()  <time.h>", "0timeh"},
            { "gmtime()  <time.h>", "0timeh"},
            { "localtime()  <time.h>", "0timeh"},
            { "mktime()  <time.h>", "0timeh"},
            { "strftime()  <time.h>", "0timeh"},
            { "time()  <time.h>", "0timeh"},
            { "<uchar.h>", "0ucharh"},
            { "c16rtomb()  <uchar.h>", "0ucharh"},
            { "c32rtomb()  <uchar.h>", "0ucharh"},
            { "mbrtoc16()  <uchar.h>", "0ucharh"},
            { "mbrtoc32()  <uchar.h>", "0ucharh"},
            { "<wchar.h>", "0wcharh"},
            { "btowc()  <wchar.h>", "0wcharh"},
            { "fgetwc()  <wchar.h>", "0wcharh"},
            { "fgetws()  <wchar.h>", "0wcharh"},
            { "fwide()  <wchar.h>", "0wcharh"},
            { "fwprintf()  <wchar.h>", "0wcharh"},
            { "fwscanf()  <wchar.h>", "0wcharh"},
            { "getwc()  <wchar.h>", "0wcharh"},
            { "getwchar()  <wchar.h>", "0wcharh"},
            { "mbrlen()  <wchar.h>", "0wcharh"},
            { "mbrtowc()  <wchar.h>", "0wcharh"},
            { "mbsinit()  <wchar.h>", "0wcharh"},
            { "mbstowcs()  <wchar.h>", "0wcharh"},
            { "putwchar()  <wchar.h>", "0wcharh"},
            { "swprintf()  <wchar.h>", "0wcharh"},
            { "swscanf()  <wchar.h>", "0wcharh"},
            { "ungetwc()  <wchar.h>", "0wcharh"},
            { "vfwprintf()  <wchar.h>", "0wcharh"},
            { "vfwscanf()  <wchar.h>", "0wcharh"},
            { "vswprintf()  <wchar.h>", "0wcharh"},
            { "vswscanf()  <wchar.h>", "0wcharh"},
            { "vwprintf()  <wchar.h>", "0wcharh"},
            { "vwscanf()  <wchar.h>", "0wcharh"},
            { "wcrtomb()  <wchar.h>", "0wcharh"},
            { "wcscat()  <wchar.h>", "0wcharh"},
            { "wcschr()  <wchar.h>", "0wcharh"},
            { "wcscmp()  <wchar.h>", "0wcharh"},
            { "wcscoll()  <wchar.h>", "0wcharh"},
            { "wcscpy()  <wchar.h>", "0wcharh"},
            { "wcsftime()  <wchar.h>", "0wcharh"},
            { "wcslen()  <wchar.h>", "0wcharh"},
            { "wcsncat()  <wchar.h>", "0wcharh"},
            { "wcsncmp()  <wchar.h>", "0wcharh"},
            { "wcsncpy()  <wchar.h>", "0wcharh"},
            { "wcsspn()  <wchar.h>", "0wcharh"},
            { "wcsstr()  <wchar.h>", "0wcharh"},
            { "wcstod()  <wchar.h>", "0wcharh"},
            { "wcstof()  <wchar.h>", "0wcharh"},
            { "wcstok()  <wchar.h>", "0wcharh"},
            { "wcstol()  <wchar.h>", "0wcharh"},
            { "wcstold()  <wchar.h>", "0wcharh"},
            { "wcstoll()  <wchar.h>", "0wcharh"},
            { "wcstoul()  <wchar.h>", "0wcharh"},
            { "wcstoull()  <wchar.h>", "0wcharh"},
            { "wcsxfrm()  <wchar.h>", "0wcharh"},
            { "wctob()  <wchar.h>", "0wcharh"},
            { "wmemchr()  <wchar.h>", "0wcharh"},
            { "wmemcmp()  <wchar.h>", "0wcharh"},
            { "wmemcpy()  <wchar.h>", "0wcharh"},
            { "wmemmove()  <wchar.h>", "0wcharh"},
            { "wmemset()  <wchar.h>", "0wcharh"},
            { "wprintf()  <wchar.h>", "0wcharh"},
            { "wscanf()  <wchar.h>", "0wcharh"},
            { "<wctype.h>", "0wctypeh"},
            { "iswalnum()  <wctype.h>", "0wctypeh"},
            { "iswalpha()  <wctype.h>", "0wctypeh"},
            { "iswblank()  <wctype.h>", "0wctypeh"},
            { "iswcntrl()  <wctype.h>", "0wctypeh"},
            { "iswdigit()  <wctype.h>", "0wctypeh"},
            { "iswgraph()  <wctype.h>", "0wctypeh"},
            { "iswlower()  <wctype.h>", "0wctypeh"},
            { "iswprint()  <wctype.h>", "0wctypeh"},
            { "iswunct()  <wctype.h>", "0wctypeh"},
            { "iswspace()  <wctype.h>", "0wctypeh"},
            { "iswupper()  <wctype.h>", "0wctypeh"},
            { "iswxdigit()  <wctype.h>", "0wctypeh"},
            { "iswctype()  <wctype.h>", "0wctypeh"},
            { "towcrans()  <wctype.h>", "0wctypeh"},
            { "towlower()  <wctype.h>", "0wctypeh"},
            { "towupper()  <wctype.h>", "0wctypeh"},
            { "wctrans()  <wctype.h>", "0wctypeh"},
            { "wctype()  <wctype.h>", "0wctypeh"},
            { "<string>", "1stringc"},
            { "operator+  <string>", "1stringc"},
            { "operator!=  <string>", "1stringc"},
            { "operator==  <string>", "1stringc"},
            { "operator<  <string>", "1stringc"},
            { "operator<=  <string>", "1stringc"},
            { "operator<<  <string>", "1stringc"},
            { "operator>  <string>", "1stringc"},
            { "operator>=  <string>", "1stringc"},
            { "operator>>  <string>", "1stringc"},
            { "operator\"\"s  <string>", "1stringc"},
            { "erase()  <string>", "1stringc"},
            { "erase_if()  <string>", "1stringc"},
            { "getline()  <string>", "1stringc"},
            { "stod()  <string>", "1stringc"},
            { "stof()  <string>", "1stringc"},
            { "stoi()  <string>", "1stringc"},
            { "stol()  <string>", "1stringc"},
            { "stold()  <string>", "1stringc"},
            { "stoll()  <string>", "1stringc"},
            { "stoul()  <string>", "1stringc"},
            { "swap()  <string>", "1stringc"},
            { "to_string()  <string>", "1stringc"},
            { "to_wstring()  <string>", "1stringc"},
            { "hash �\����  <string>", "1stringc"},
            { "basic_string �N���X", "1basic_string0"},
            { "basic_string()  (basic_string �N���X)", "1basic_string0"},
            { "operator+=  (basic_string �N���X)", "1basic_string0"},
            { "operator=  (basic_string �N���X)", "1basic_string0"},
            { "operator[]  (basic_string �N���X)", "1basic_string0"},
            { "append()  (basic_string �N���X)", "1basic_string0"},
            { "assign()  (basic_string �N���X)", "1basic_string0"},
            { "at()  (basic_string �N���X)", "1basic_string0"},
            { "back()  (basic_string �N���X)", "1basic_string0"},
            { "begin()  (basic_string �N���X)", "1basic_string0"},
            { "c_str()  (basic_string �N���X)", "1basic_string0"},
            { "capacity()  (basic_string �N���X)", "1basic_string0"},
            { "cbegin()  (basic_string �N���X)", "1basic_string0"},
            { "cend()  (basic_string �N���X)", "1basic_string0"},
            { "clear()  (basic_string �N���X)", "1basic_string0"},
            { "compare()  (basic_string �N���X)", "1basic_string0"},
            { "contains()  (basic_string �N���X)", "1basic_string0"},
            { "copy()  (basic_string �N���X)", "1basic_string0"},
            { "crbegin()  (basic_string �N���X)", "1basic_string0"},
            { "crend()  (basic_string �N���X)", "1basic_string0"},
            { "data()  (basic_string �N���X)", "1basic_string0"},
            { "empty()  (basic_string �N���X)", "1basic_string0"},
            { "end()  (basic_string �N���X)", "1basic_string0"},
            { "ends_with()  (basic_string �N���X)", "1basic_string0"},
            { "erase()  (basic_string �N���X)", "1basic_string0"},
            { "find()  (basic_string �N���X)", "1basic_string0"},
            { "find_first_not_of()  (basic_string �N���X)", "1basic_string0"},
            { "find_first_of()  (basic_string �N���X)", "1basic_string0"},
            { "find_last_not_of()  (basic_string �N���X)", "1basic_string0"},
            { "find_last_of()  (basic_string �N���X)", "1basic_string0"},
            { "front()  (basic_string �N���X)", "1basic_string0"},
            { "get_allocator()  (basic_string �N���X)", "1basic_string0"},
            { "insert()  (basic_string �N���X)", "1basic_string0"},
            { "length()  (basic_string �N���X)", "1basic_string0"},
            { "max_size()  (basic_string �N���X)", "1basic_string0"},
            { "pop_back()  (basic_string �N���X)", "1basic_string0"},
            { "push_back()  (basic_string �N���X)", "1basic_string0"},
            { "rbegin()  (basic_string �N���X)", "1basic_string0"},
            { "rend()  (basic_string �N���X)", "1basic_string0"},
            { "replace()  (basic_string �N���X)", "1basic_string0"},
            { "reserve()  (basic_string �N���X)", "1basic_string0"},
            { "rfind()  (basic_string �N���X)", "1basic_string0"},
            { "shrink_to_fit()  (basic_string �N���X)", "1basic_string0"},
            { "size()  (basic_string �N���X)", "1basic_string0"},
            { "starts_with()  (basic_string �N���X)", "1basic_string0"},
            { "substr()  (basic_string �N���X)", "1basic_string0"},
            { "swap()  (basic_string �N���X)", "1basic_string0"},
            { "char_traits �\����", "1char_traits0"},
            { "assign()  (char_traits �\����)", "1char_traits0"},
            { "compare()  (char_traits �\����)", "1char_traits0"},
            { "copy()  (char_traits �\����)", "1char_traits0"},
            { "eof()  (char_traits �\����)", "1char_traits0"},
            { "eq()  (char_traits �\����)", "1char_traits0"},
            { "eq_int_type()  (char_traits �\����)", "1char_traits0"},
            { "find()  (char_traits �\����)", "1char_traits0"},
            { "length()  (char_traits �\����)", "1char_traits0"},
            { "lt()  (char_traits �\����)", "1char_traits0"},
            { "move()  (char_traits �\����)", "1char_traits0"},
            { "not_eof()  (char_traits �\����)", "1char_traits0"},
            { "to_char_type()  (char_traits �\����)", "1char_traits0"},
            { "to_int_type()  (char_traits �\����)", "1char_traits0"},
            { "<bit>", "2bit" },
            { "bit_cast()  <bit>", "2bit" },
            { "bit_ceil()  <bit>", "2bit" },
            { "bit_floor()  <bit>", "2bit" },
            { "bit_width()  <bit>", "2bit" },
            { "countl_one()  <bit>", "2bit" },
            { "countl_zero()  <bit>", "2bit" },
            { "countr_one()  <bit>", "2bit" },
            { "countr_zero()  <bit>", "2bit" },
            { "has_single_bit()  <bit>", "2bit" },
            { "popcount()  <bit>", "2bit" },
            { "rotl()  <bit>", "2bit" },
            { "rotr()  <bit>", "2bit" },
            { "<limits>", "3limits" },
            { "numeric_limits �N���X", "3numeric_limits0" },
            { "digits  (numeric_limits �N���X)", "3numeric_limits0" },
            { "digits10  (numeric_limits �N���X)", "3numeric_limits0" },
            { "has_denorm  (numeric_limits �N���X)", "3numeric_limits0" },
            { "has_denorm_loss  (numeric_limits �N���X)", "3numeric_limits0" },
            { "has_infinity  (numeric_limits �N���X)", "3numeric_limits0" },
            { "has_quiet_NaN  (numeric_limits �N���X)", "3numeric_limits0" },
            { "has_signaling_NaN  (numeric_limits �N���X)", "3numeric_limits0" },
            { "is_bounded  (numeric_limits �N���X)", "3numeric_limits0" },
            { "is_exact  (numeric_limits �N���X)", "3numeric_limits0" },
            { "is_iec559  (numeric_limits �N���X)", "3numeric_limits0" },
            { "is_integer  (numeric_limits �N���X)", "3numeric_limits0" },
            { "is_modulo  (numeric_limits �N���X)", "3numeric_limits0" },
            { "is_signed  (numeric_limits �N���X)", "3numeric_limits0" },
            { "is_sepcialized  (numeric_limits �N���X)", "3numeric_limits0" },
            { "max_digits10  (numeric_limits �N���X)", "3numeric_limits0" },
            { "max_exponent  (numeric_limits �N���X)", "3numeric_limits0" },
            { "max_exponent10  (numeric_limits �N���X)", "3numeric_limits0" },
            { "min_exponent  (numeric_limits �N���X)", "3numeric_limits0" },
            { "min_exponent10  (numeric_limits �N���X)", "3numeric_limits0" },
            { "radix  (numeric_limits �N���X)", "3numeric_limits0" },
            { "round_style  (numeric_limits �N���X)", "3numeric_limits0" },
            { "tinyness_before  (numeric_limits �N���X)", "3numeric_limits0" },
            { "traps  (numeric_limits �N���X)", "3numeric_limits0" },
            { "denorm_min()  (numeric_limits �N���X)", "3numeric_limits0" },
            { "epsilon()  (numeric_limits �N���X)", "3numeric_limits0" },
            { "infinity()  (numeric_limits �N���X)", "3numeric_limits0" },
            { "lowest()  (numeric_limits �N���X)", "3numeric_limits0" },
            { "max()  (numeric_limits �N���X)", "3numeric_limits0" },
            { "min()  (numeric_limits �N���X)", "3numeric_limits0" },
            { "quiet_NaN()  (numeric_limits �N���X)", "3numeric_limits0" },
            { "round_error()  (numeric_limits �N���X)", "3numeric_limits0" },
            { "signaling_NaN()  (numeric_limits �N���X)", "3numeric_limits0" },
            { "<conio.h>", "4conioh"},
            { "cgets()  <conio.h>", "4conioh"},
            { "_cprintf()  <conio.h>", "4conioh"},
            { "_cputs()  <conio.h>", "4conioh"},
            { "_cscanf()  <conio.h>", "4conioh"},
            { "_getch()  <conio.h>", "4conioh"},
            { "_getche()  <conio.h>", "4conioh"},
            { "_kbhit()  <conio.h>", "4conioh"},
            { "_putch()  <conio.h>", "4conioh"},
            { "_ungetch()  <conio.h>", "4conioh"},
            { "<ConsoleApi.h>", "4ConsoleApi"},
            { "ReadConsole()  <ConsoleApi.h>", "4ConsoleApi"},
            { "WriteConsole()  <ConsoleApi.h>", "4ConsoleApi"},
            { "<ConsoleApi2.h>", "4ConsoleApi2h"},
            { "GetConsoleTitle()  <ConsoleApi2.h>", "4ConsoleApi2h"},
            { "GetConsoleOriginalTitle()  <ConsoleApi2.h>", "4ConsoleApi2h"},
            { "GetLargestConsoleWindowSize()  <ConsoleApi2.h>", "4ConsoleApi2h"},
            { "SetConsoleCursorPosition()  <ConsoleApi2.h>", "4ConsoleApi2h"},
            { "SetConsoleTextAttribute()  <ConsoleApi2.h>", "4ConsoleApi2h"},
            { "SetConsoleTitle()  <ConsoleApi2.h>", "4ConsoleApi2h"},
            { "<ConsoleApi3.h>", "4ConsoleApi3h"},
            { "GetConsoleProcessList()  <ConsoleApi3.h>", "4ConsoleApi3h"},
            { "GetConsoleWindow()  <ConsoleApi3.h>", "4ConsoleApi3h"},
            {"GetCurrentConsoleFont()  <ConsoleApi3.h>", "4ConsoleApi3h" },
            {"GetNumberOfConsoleMouseButtons()  <ConsoleApi3.h>", "4ConsoleApi3h" },
            {"<ProcessEnv.h>", "4ProcessEnv" },
            {"FreeEnvironmentStrings()  <ProcessEnv.h>", "4ProcessEnv" },
            {"GetCommandLine()  <ProcessEnv.h>", "4ProcessEnv" },
            {"GetEnvironmentStrings()  <ProcessEnv.h>", "4ProcessEnv" },
            {"GetEnvironmentVariable()  <ProcessEnv.h>", "4ProcessEnv" },
            {"GetStdHandle()  <ProcessEnv.h>", "4ProcessEnv" },
            {"SearchPath()  <ProcessEnv.h>", "4ProcessEnv" },
            {"SetEnvironmentVariable()  <ProcessEnv.h>", "4ProcessEnv" },
            {"<Windows.h>", "4Windows" },
            { "<WinUser.h>", "4WinUser" },
            { "CharLower()  <WinUser.h>", "4WinUser" },
            { "CharNext()  <WinUser.h>", "4WinUser" },
            { "CharPrev()  <WinUser.h>", "4WinUser" },
            { "CharUpper()  <WinUser.h>", "4WinUser" },
            { "CloseWindow()  <WinUser.h>", "4WinUser" },
            { "EnableWindow()  <WinUser.h>", "4WinUser" },
            { "GetCapture()  <WinUser.h>", "4WinUser" },
            { "GetClassName()  <WinUser.h>", "4WinUser" },
            { "GetDpiForSystem()  <WinUser.h>", "4WinUser" },
            { "GetDpiForWindow()  <WinUser.h>", "4WinUser" },
            { "GetWindowRect()  <WinUser.h>", "4WinUser" },
            { "GetWindowText()  <WinUser.h>", "4WinUser" },
            { "IsCharAlpha()  <WinUser.h>", "4WinUser" },
            { "IsCharNumeric()  <WinUser.h>", "4WinUser" },
            { "IsCharLower()  <WinUser.h>", "4WinUser" },
            { "IsCharUpper()  <WinUser.h>", "4WinUser" },
            { "IsZoomed()  <WinUser.h>", "4WinUser" },
            { "MessageBeep()  <WinUser.h>", "4WinUser" },
            { "MessageBox()  <WinUser.h>", "4WinUser" },
            { "MoveWindow()  <WinUser.h>", "4WinUser" },
            { "SetWindowText()  <WinUser.h>", "4WinUser" },
            { "<bitset>", "5bitset"},
            { "operator&amp;  <bitset>", "5bitset"},
            { "operator&lt;&lt;  <bitset>", "5bitset"},
            { "operator>>  <bitset>", "5bitset"},
            { "operator^  <bitset>", "5bitset"},
            { "operator|  <bitset>", "5bitset"},
            { "bitset �N���X", "5bitset0"},
            { "bitset()  (bitset �N���X)", "5bitset0"},
            { "operator!=  (bitset �N���X)", "5bitset0"},
            { "operator&=  (bitset �N���X)", "5bitset0"},
            { "operator<<  (bitset �N���X)", "5bitset0"},
            { "operator<<=  (bitset �N���X)", "5bitset0"},
            { "operator==  (bitset �N���X)", "5bitset0"},
            { "operator>>  (bitset �N���X)", "5bitset0"},
            { "operator>>=  (bitset �N���X)", "5bitset0"},
            { "operator[]  (bitset �N���X)", "5bitset0"},
            { "operator^=  (bitset �N���X)", "5bitset0"},
            { "operator|=  (bitset �N���X)", "5bitset0"},
            { "operator~  (bitset �N���X)", "5bitset0"},
            { "all()  (bitset �N���X)", "5bitset0"},
            { "any()  (bitset �N���X)", "5bitset0"},
            { "count()  (bitset �N���X)", "5bitset0"},
            { "flip()  (bitset �N���X)", "5bitset0"},
            { "none()  (bitset �N���X)", "5bitset0"},
            { "reset()  (bitset �N���X)", "5bitset0"},
            { "set()  (bitset �N���X)", "5bitset0"},
            { "size()  (bitset �N���X)", "5bitset0"},
            { "test()  (bitset �N���X)", "5bitset0"},
            { "to_string()  (bitset �N���X)", "5bitset0"},
            { "to_ullong()  (bitset �N���X)", "5bitset0"},
            { "to_ulong()  (bitset �N���X)", "5bitset0"},
            { "<strstream>", "6strstream"},
            { "swap()  <strstream>", "6strstream"},
            { "ostrstream �N���X", "6ostrstream0"},
            { "ostrstream()  (ostrstream �N���X)", "6ostrstream0"},
            { "freeze()  (ostrstream �N���X)", "6ostrstream0"},
            { "pcount()  (ostrstream �N���X)", "6ostrstream0"},
            { "rdbuf()  (ostrstream �N���X)", "6ostrstream0"},
            { "str()  (ostrstream �N���X)", "6ostrstream0"},
            { "swap()  (ostrstream �N���X)", "6ostrstream0"},
            { "<array>", "7array" },
            { "operator==  <array>", "7array" },
            { "operator!=  <array>", "7array" },
            { "operator<  <array>", "7array" },
            { "operator<=  <array>", "7array" },
            { "operator>  <array>", "7array" },
            { "operator>=  <array>", "7array" },
            { "get()  <array>", "7array" },
            { "swap()  <array>", "7array" },
            { "array �N���X", "7array0" },
            { "array()  (array �N���X)", "7array0" },
            { "operator=  (array �N���X)", "7array0" },
            { "operator[]  (array �N���X)", "7array0" },
            { "assign()  (array �N���X)", "7array0" },
            { "at()  (array �N���X)", "7array0" },
            { "back()  (array �N���X)", "7array0" },
            { "begin()  (array �N���X)", "7array0" },
            { "cbegin()  (array �N���X)", "7array0" },
            { "cend()  (array �N���X)", "7array0" },
            { "crbegin()  (array �N���X)", "7array0" },
            { "crend()  (array �N���X)", "7array0" },
            { "data()  (array �N���X)", "7array0" },
            { "empty()  (array �N���X)", "7array0" },
            { "end()  (array �N���X)", "7array0" },
            { "fill()  (array �N���X)", "7array0" },
            { "front()  (array �N���X)", "7array0" },
            { "max_size()  (array �N���X)", "7array0" },
            { "rbegin()  (array �N���X)", "7array0" },
            { "rend()  (array �N���X)", "7array0" },
            { "size()  (array �N���X)", "7array0" },
            { "swap()  (array �N���X)", "7array0" },
            { "<iosfwd>", "8iosfwd" },
            { "fpos �N���X", "8fpos0" },
            { "fpos()  (fpos �N���X)", "8fpos0" },
            { "operator!=  (fpos �N���X)", "8fpos0" },
            { "operator+  (fpos �N���X)", "8fpos0" },
            { "operator+=  (fpos �N���X)", "8fpos0" },
            { "operator-  (fpos �N���X)", "8fpos0" },
            { "operator-=  (fpos �N���X)", "8fpos0" },
            { "operator==  (fpos �N���X)", "8fpos0" },
            { "operator streamoff  (fpos �N���X)", "8fpos0" },
            { "seekpos()  (fpos �N���X)", "8fpos0" },
            { "state()  (fpos �N���X)", "8fpos0" },
            { "<iostream>", "9iostream" },
            { "cerr  <iostream>", "9iostream" },
            { "cin  <iostream>", "9iostream" },
            { "clog  <iostream>", "9iostream" },
            { "cout  <iostream>", "9iostream" },
            { "wcerr  <iostream>", "9iostream" },
            { "wcin  <iostream>", "9iostream" },
            { "wclog  <iostream>", "9iostream" },
            { "wcout  <iostream>", "9iostream" },
            { "<ios>", "aios" },
            { "boolalpha()  <ios>", "aios" },
            { "dec()  <ios>", "aios" },
            { "defaultfloat()  <ios>", "aios" },
            { "fixed()  <ios>", "aios" },
            { "hex()  <ios>", "aios" },
            { "hexfloat()  <ios>", "aios" },
            { "internal()  <ios>", "aios" },
            { "left()  <ios>", "aios" },
            { "noboolalpha()  <ios>", "aios" },
            { "noshowbase()  <ios>", "aios" },
            { "noshowpoint()  <ios>", "aios" },
            { "noshowpos()  <ios>", "aios" },
            { "noskipws()  <ios>", "aios" },
            { "nounitbuf()  <ios>", "aios" },
            { "nouppercase()  <ios>", "aios" },
            { "oct()  <ios>", "aios" },
            { "right()  <ios>", "aios" },
            { "scientific()  <ios>", "aios" },
            { "showbase()  <ios>", "aios" },
            { "showpoint()  <ios>", "aios" },
            { "showpos()  <ios>", "aios" },
            { "skipws()  <ios>", "aios" },
            { "unitbuf()  <ios>", "aios" },
            { "uppercase()  <ios>", "aios" },
            { "basic_ios �N���X", "abasic_ios0" },
            { "failure �N���X  (ios_base �N���X)", "aios_base0" },
            { "Init �N���X  (ios_base �N���X)", "aios_base0" },
            { "basic_ios()  (basic_ios �N���X)", "abasic_ios0" },
            { "operator bool  (basic_ios �N���X)", "abasic_ios0" },
            { "operator void *  (basic_ios �N���X)", "abasic_ios0" },
            { "operator!  (basic_ios �N���X)", "abasic_ios0" },
            { "operator=  (basic_ios �N���X)", "abasic_ios0" },
            { "bad()  (basic_ios �N���X)", "abasic_ios0" },
            { "clear()  (basic_ios �N���X)", "abasic_ios0" },
            { "copyfmt()  (basic_ios �N���X)", "abasic_ios0" },
            { "eof()  (basic_ios �N���X)", "abasic_ios0" },
            { "exceptions()  (basic_ios �N���X)", "abasic_ios0" },
            { "fail()  (basic_ios �N���X)", "abasic_ios0" },
            { "fill()  (basic_ios �N���X)", "abasic_ios0" },
            { "good()  (basic_ios �N���X)", "abasic_ios0" },
            { "imbue()  (basic_ios �N���X)", "abasic_ios0" },
            { "move()  (basic_ios �N���X)", "abasic_ios0" },
            { "narrow()  (basic_ios �N���X)", "abasic_ios0" },
            { "rdbuf()  (basic_ios �N���X)", "abasic_ios0" },
            { "rdstate()  (basic_ios �N���X)", "abasic_ios0" },
            { "set_rdbuf()  (basic_ios �N���X)", "abasic_ios0" },
            { "setstate()  (basic_ios �N���X)", "abasic_ios0" },
            { "swap()  (basic_ios �N���X)", "abasic_ios0" },
            { "tie()  (basic_ios �N���X)", "abasic_ios0" },
            { "widen()  (basic_ios �N���X)", "abasic_ios0" },
            { "ios_base �N���X", "aios_base0" },
            { "ios_base()  (ios_base �N���X)", "aios_base0" },
            { "operator=  (ios_base �N���X)", "aios_base0" },
            { "flags()  (ios_base �N���X)", "aios_base0" },
            { "getloc()  (ios_base �N���X)", "aios_base0" },
            { "imbue()  (ios_base �N���X)", "aios_base0" },
            { "iword()  (ios_base �N���X)", "aios_base0" },
            { "precision()  (ios_base �N���X)", "aios_base0" },
            { "pword()  (ios_base �N���X)", "aios_base0" },
            { "register_callback()  (ios_base �N���X)", "aios_base0" },
            { "self()  (ios_base �N���X)", "aios_base0" },
            { "sync_with_stdio()  (ios_base �N���X)", "aios_base0" },
            { "unself()  (ios_base �N���X)", "aios_base0" },
            { "width()  (ios_base �N���X)", "aios_base0" },
            { "xalloc()  (ios_base �N���X)", "aios_base0" },
            { "<iomanip>", "biomanip" },
            { "get_money()  <iomanip>", "biomanip" },
            { "get_time()  <iomanip>", "biomanip" },
            { "put_money()  <iomanip>", "biomanip" },
            { "put_time()  <iomanip>", "biomanip" },
            { "quoted()  <iomanip>", "biomanip" },
            { "resetiosflags()  <iomanip>", "biomanip" },
            { "setbase()  <iomanip>", "biomanip" },
            { "setfill()  <iomanip>", "biomanip" },
            { "setiosflags()  <iomanip>", "biomanip" },
            { "setprecision()  <iomanip>", "biomanip" },
            { "setw()  <iomanip>", "biomanip" },
            { "<list>", "clist" },
            { "operator!=  <list>", "clist" },
            { "operator<  <list>", "clist" },
            { "operator<=  <list>", "clist" },
            { "operator==  <list>", "clist" },
            { "operator>  <list>", "clist" },
            { "operator>=  <list>", "clist" },
            { "swap()  <list>", "clist" },
            { "list �N���X", "clist0" },
            { "list()  (list �N���X)", "clist0" },
            { "operator=  (list �N���X)", "clist0" },
            { "assign()  (list �N���X)", "clist0" },
            { "back()  (list �N���X)", "clist0" },
            { "begin()  (list �N���X)", "clist0" },
            { "cbegin()  (list �N���X)", "clist0" },
            { "cend()  (list �N���X)", "clist0" },
            { "clear()  (list �N���X)", "clist0" },
            { "crbegin()  (list �N���X)", "clist0" },
            { "crend()  (list �N���X)", "clist0" },
            { "emplace()  (list �N���X)", "clist0" },
            { "emplace_back()  (list �N���X)", "clist0" },
            { "emplace_front()  (list �N���X)", "clist0" },
            { "empty()  (list �N���X)", "clist0" },
            { "end()  (list �N���X)", "clist0" },
            { "erase()  (list �N���X)", "clist0" },
            { "front()  (list �N���X)", "clist0" },
            { "get_allocator()  (list �N���X)", "clist0" },
            { "insert()  (list �N���X)", "clist0" },
            { "max_size()  (list �N���X)", "clist0" },
            { "merge()  (list �N���X)", "clist0" },
            { "pop_back()  (list �N���X)", "clist0" },
            { "pop_front()  (list �N���X)", "clist0" },
            { "push_back()  (list �N���X)", "clist0" },
            { "push_front()  (list �N���X)", "clist0" },
            { "rbegin()  (list �N���X)", "clist0" },
            { "remove()  (list �N���X)", "clist0" },
            { "remove_if()  (list �N���X)", "clist0" },
            { "rend()  (list �N���X)", "clist0" },
            { "resize()  (list �N���X)", "clist0" },
            { "reverse()  (list �N���X)", "clist0" },
            { "size()  (list �N���X)", "clist0" },
            { "sort()  (list �N���X)", "clist0" },
            { "splice()  (list �N���X)", "clist0" },
            { "swap()  (list �N���X)", "clist0" },
            { "unique()  (list �N���X)", "clist0" },
            { "<istream>", "distream" },
            { "operator>>  <istream>", "distream" },
            { "ws()  <istream>", "distream" },
            { "swap()  <istream>", "distream" },
            { "basic_istream �N���X", "dbasic_istream0" },
            { "basic_istream()  (basic_istream �N���X)", "dbasic_istream0" },
            { "operator>>  (basic_istream �N���X)", "dbasic_istream0" },
            { "operator=  (basic_istream �N���X)", "dbasic_istream0" },
            { "gcount()  (basic_istream �N���X)", "dbasic_istream0" },
            { "get()  (basic_istream �N���X)", "dbasic_istream0" },
            { "getline()  (basic_istream �N���X)", "dbasic_istream0" },
            { "ignore()  (basic_istream �N���X)", "dbasic_istream0" },
            { "peek()  (basic_istream �N���X)", "dbasic_istream0" },
            { "putback()  (basic_istream �N���X)", "dbasic_istream0" },
            { "read()  (basic_istream �N���X)", "dbasic_istream0" },
            { "readsome()  (basic_istream �N���X)", "dbasic_istream0" },
            { "seekg()  (basic_istream �N���X)", "dbasic_istream0" },
            { "sentry()  (basic_istream �N���X)", "dbasic_istream0" },
            { "swap()  (basic_istream �N���X)", "dbasic_istream0" },
            { "sync()  (basic_istream �N���X)", "dbasic_istream0" },
            { "tellg()  (basic_istream �N���X)", "dbasic_istream0" },
            { "unget()  (basic_istream �N���X)", "dbasic_istream0" },
            { "basic_iostream �N���X", "dbasic_iostream0" },
            { "bacic_iostream()  (basic_iostream �N���X)", "dbasic_iostream0" },
            { "operator=  (basic_iostream �N���X)", "dbasic_iostream0" },
            { "swap()  (basic_iostream �N���X)", "dbasic_iostream0" },
            { "<map>", "emap" },
            { "operator!=  <map>", "emap" },
            { "operator<  <map>", "emap" },
            { "operator<=  <map>", "emap" },
            { "operator==  <map>", "emap" },
            { "operator>  <map>", "emap" },
            { "operator>=  <map>", "emap" },
            { "erase_if()  <map>", "emap" },
            { "swap()  <map>", "emap" },
            { "map �N���X", "emap0" },
            { "map()  (map �N���X)", "emap0" },
            { "operator[]  (map �N���X)", "emap0" },
            { "operator=  (map �N���X)", "emap0" },
            { "at()  (map �N���X)", "emap0" },
            { "begin()  (map �N���X)", "emap0" },
            { "cbegin()  (map �N���X)", "emap0" },
            { "cend()  (map �N���X)", "emap0" },
            { "clear()  (map �N���X)", "emap0" },
            { "contains()  (map �N���X)", "emap0" },
            { "count()  (map �N���X)", "emap0" },
            { "crbegin()  (map �N���X)", "emap0" },
            { "crend()  (map �N���X)", "emap0" },
            { "emplace()  (map �N���X)", "emap0" },
            { "emplace_hint()  (map �N���X)", "emap0" },
            { "empty()  (map �N���X)", "emap0" },
            { "end()  (map �N���X)", "emap0" },
            { "equal_range()  (map �N���X)", "emap0" },
            { "erase()  (map �N���X)", "emap0" },
            { "find()  (map �N���X)", "emap0" },
            { "get_allocator()  (map �N���X)", "emap0" },
            { "insert()  (map �N���X)", "emap0" },
            { "key_comp()  (map �N���X)", "emap0" },
            { "lower_bound()  (map �N���X)", "emap0" },
            { "max_size()  (map �N���X)", "emap0" },
            { "rbegin()  (map �N���X)", "emap0" },
            { "rend()  (map �N���X)", "emap0" },
            { "size()  (map �N���X)", "emap0" },
            { "swap()  (map �N���X)", "emap0" },
            { "upper_bound()  (map �N���X)", "emap0" },
            { "value_comp()  (map �N���X)", "emap0" },
            { "<ostream>", "fostream" },
            { "operator<<  <ostream>", "fostream" },
            { "endl()  <ostream>", "fostream" },
            { "ends()  <ostream>", "fostream" },
            { "flush()  <ostream>", "fostream" },
            { "print()  <ostream>", "fostream" },
            { "println()  <ostream>", "fostream" },
            { "swap()  <ostream>", "fostream" },
            { "vprint_nonunicode()  <ostream>", "fostream" },
            { "vprint_unicode()  <ostream>", "fostream" },
            { "basic_ostream �N���X", "fbasic_ostream0" },
            { "basic_ostream()  (basic_ostream �N���X)", "fbasic_ostream0" },
            { "operator=  (basic_ostream �N���X)", "fbasic_ostream0" },
            { "operator<<  (basic_ostream �N���X)", "fbasic_ostream0" },
            { "flush()  (basic_ostream �N���X)", "fbasic_ostream0" },
            { "put()  (basic_ostream �N���X)", "fbasic_ostream0" },
            { "seekp()  (basic_ostream �N���X)", "fbasic_ostream0" },
            { "sentry()  (basic_ostream �N���X)", "fbasic_ostream0" },
            { "swap()  (basic_ostream �N���X)", "fbasic_ostream0" },
            { "tellp()  (basic_ostream �N���X)", "fbasic_ostream0" },
            { "write()  (basic_ostream �N���X)", "fbasic_ostream0" },
            { "<streambuf>", "gstreambuf" },
            { "basic_streambuf �N���X", "gbasic_streambuf0" },
            { "basic_streambuf()  (basic_streambuf �N���X)", "gbasic_streambuf0" },
            { "operator=  (basic_streambuf �N���X)", "gbasic_streambuf0" },
            { "eback()  (basic_streambuf �N���X)", "gbasic_streambuf0" },
            { "egptr()  (basic_streambuf �N���X)", "gbasic_streambuf0" },
            { "epptr()  (basic_streambuf �N���X)", "gbasic_streambuf0" },
            { "gbump()  (basic_streambuf �N���X)", "gbasic_streambuf0" },
            { "getloc()  (basic_streambuf �N���X)", "gbasic_streambuf0" },
            { "gptr()  (basic_streambuf �N���X)", "gbasic_streambuf0" },
            { "imbue()  (basic_streambuf �N���X)", "gbasic_streambuf0" },
            { "in_avail()  (basic_streambuf �N���X)", "gbasic_streambuf0" },
            { "overflow()  (basic_streambuf �N���X)", "gbasic_streambuf0" },
            { "pbackfail()  (basic_streambuf �N���X)", "gbasic_streambuf0" },
            { "pbase()  (basic_streambuf �N���X)", "gbasic_streambuf0" },
            { "pbump()  (basic_streambuf �N���X)", "gbasic_streambuf0" },
            { "pptr()  (basic_streambuf �N���X)", "gbasic_streambuf0" },
            { "pubimbue()  (basic_streambuf �N���X)", "gbasic_streambuf0" },
            { "pubseekoff()  (basic_streambuf �N���X)", "gbasic_streambuf0" },
            { "pubseekpos()  (basic_streambuf �N���X)", "gbasic_streambuf0" },
            { "pubsetbuf()  (basic_streambuf �N���X)", "gbasic_streambuf0" },
            { "pubsync()  (basic_streambuf �N���X)", "gbasic_streambuf0" },
            { "sbumpc()  (basic_streambuf �N���X)", "gbasic_streambuf0" },
            { "seekoff()  (basic_streambuf �N���X)", "gbasic_streambuf0" },
            { "seekpos()  (basic_streambuf �N���X)", "gbasic_streambuf0" },
            { "setbuf()  (basic_streambuf �N���X)", "gbasic_streambuf0" },
            { "setg()  (basic_streambuf �N���X)", "gbasic_streambuf0" },
            { "setp()  (basic_streambuf �N���X)", "gbasic_streambuf0" },
            { "sgetc()  (basic_streambuf �N���X)", "gbasic_streambuf0" },
            { "sgetn()  (basic_streambuf �N���X)", "gbasic_streambuf0" },
            { "showmanyc()  (basic_streambuf �N���X)", "gbasic_streambuf0" },
            { "snextc()  (basic_streambuf �N���X)", "gbasic_streambuf0" },
            { "sputbackc()  (basic_streambuf �N���X)", "gbasic_streambuf0" },
            { "sputc()  (basic_streambuf �N���X)", "gbasic_streambuf0" },
            { "sputn()  (basic_streambuf �N���X)", "gbasic_streambuf0" },
            { "stossc()  (basic_streambuf �N���X)", "gbasic_streambuf0" },
            { "sungetc()  (basic_streambuf �N���X)", "gbasic_streambuf0" },
            { "swap()  (basic_streambuf �N���X)", "gbasic_streambuf0" },
            { "sync()  (basic_streambuf �N���X)", "gbasic_streambuf0" },
            { "uflow()  (basic_streambuf �N���X)", "gbasic_streambuf0" },
            { "underflow()  (basic_streambuf �N���X)", "gbasic_streambuf0" },
            { "xsgetn()  (basic_streambuf �N���X)", "gbasic_streambuf0" },
            { "xsputn()  (basic_streambuf �N���X)", "gbasic_streambuf0" },
            { "<vector>", "hvector" },
            { "operator!=  <vector>", "hvector" },
            { "operator<  <vector>", "hvector" },
            { "operator<=  <vector>", "hvector" },
            { "operator==  <vector>", "hvector" },
            { "operator>  <vector>", "hvector" },
            { "operator>=  <vector>", "hvector" },
            { "swap()  <vector>", "hvector" },
            { "vector �N���X", "hvector0" },
            { "operator[]  (vector �N���X)", "hvector0" },
            { "operator=  (vector �N���X)", "hvector0" },
            { "assign()  (vector �N���X)", "hvector0" },
            { "at()  (vector �N���X)", "hvector0" },
            { "back()  (vector �N���X)", "hvector0" },
            { "begin()  (vector �N���X)", "hvector0" },
            { "capacity()  (vector �N���X)", "hvector0" },
            { "cbegin()  (vector �N���X)", "hvector0" },
            { "cend()  (vector �N���X)", "hvector0" },
            { "clear()  (vector �N���X)", "hvector0" },
            { "crbegin()  (vector �N���X)", "hvector0" },
            { "crend()  (vector �N���X)", "hvector0" },
            { "data()  (vector �N���X)", "hvector0" },
            { "emplace()  (vector �N���X)", "hvector0" },
            { "emplace_back()  (vector �N���X)", "hvector0" },
            { "empty()  (vector �N���X)", "hvector0" },
            { "end()  (vector �N���X)", "hvector0" },
            { "erase()  (vector �N���X)", "hvector0" },
            { "front()  (vector �N���X)", "hvector0" },
            { "get_allocator()  (vector �N���X)", "hvector0" },
            { "insert()  (vector �N���X)", "hvector0" },
            { "max_size()  (vector �N���X)", "hvector0" },
            { "pop_back()  (vector �N���X)", "hvector0" },
            { "push_back()  (vector �N���X)", "hvector0" },
            { "rbegin()  (vector �N���X)", "hvector0" },
            { "rend()  (vector �N���X)", "hvector0" },
            { "reserve()  (vector �N���X)", "hvector0" },
            { "resize()  (vector �N���X)", "hvector0" },
            { "shrink_to_fit()  (vector �N���X)", "hvector0" },
            { "size()  (vector �N���X)", "hvector0" },
            { "swap()  (vector �N���X)", "hvector0" },
            { "<fstream>", "ifstream" },
            { "swap()  <fstream>", "ifstream" },
            { "basic_filebuf �N���X", "ibasic_filebuf0" },
            { "basic_filebuf()  (basic_filebuf �N���X)", "ibasic_filebuf0" },
            { "operator= (basic_filebuf �N���X)", "ibasic_filebuf0" },
            { "close()  (basic_filebuf �N���X)", "ibasic_filebuf0" },
            { "eback()  (basic_filebuf �N���X)", "ibasic_filebuf0" },
            { "egptr()  (basic_filebuf �N���X)", "ibasic_filebuf0" },
            { "epptr()  (basic_filebuf �N���X)", "ibasic_filebuf0" },
            { "gbump()  (basic_filebuf �N���X)", "ibasic_filebuf0" },
            { "getloc()  (basic_filebuf �N���X)", "ibasic_filebuf0" },
            { "gptr()  (basic_filebuf �N���X)", "ibasic_filebuf0" },
            { "imbue()  (basic_filebuf �N���X)", "ibasic_filebuf0" },
            { "in_avail()  (basic_filebuf �N���X)", "ibasic_filebuf0" },
            { "is_open()  (basic_filebuf �N���X)", "ibasic_filebuf0" },
            { "open()  (basic_filebuf �N���X)", "ibasic_filebuf0" },
            { "overflow()  (basic_filebuf �N���X)", "ibasic_filebuf0" },
            { "pbackfail()  (basic_filebuf �N���X)", "ibasic_filebuf0" },
            { "pbase()  (basic_filebuf �N���X)", "ibasic_filebuf0" },
            { "pbump()  (basic_filebuf �N���X)", "ibasic_filebuf0" },
            { "pptr()  (basic_filebuf �N���X)", "ibasic_filebuf0" },
            { "pubimbue()  (basic_filebuf �N���X)", "ibasic_filebuf0" },
            { "pubseekoff()  (basic_filebuf �N���X)", "ibasic_filebuf0" },
            { "pubseekpos()  (basic_filebuf �N���X)", "ibasic_filebuf0" },
            { "pubsetbuf()  (basic_filebuf �N���X)", "ibasic_filebuf0" },
            { "pubsync()  (basic_filebuf �N���X)", "ibasic_filebuf0" },
            { "sbumpc()  (basic_filebuf �N���X)", "ibasic_filebuf0" },
            { "seekoff()  (basic_filebuf �N���X)", "ibasic_filebuf0" },
            { "seekpos()  (basic_filebuf �N���X)", "ibasic_filebuf0" },
            { "setbuf()  (basic_filebuf �N���X)", "ibasic_filebuf0" },
            { "setg()  (basic_filebuf �N���X)", "ibasic_filebuf0" },
            { "setp()  (basic_filebuf �N���X)", "ibasic_filebuf0" },
            { "sgetc()  (basic_filebuf �N���X)", "ibasic_filebuf0" },
            { "sgetn()  (basic_filebuf �N���X)", "ibasic_filebuf0" },
            { "showmanyc()  (basic_filebuf �N���X)", "ibasic_filebuf0" },
            { "snextc()  (basic_filebuf �N���X)", "ibasic_filebuf0" },
            { "sputbackc()  (basic_filebuf �N���X)", "ibasic_filebuf0" },
            { "sputc()  (basic_filebuf �N���X)", "ibasic_filebuf0" },
            { "sputn()  (basic_filebuf �N���X)", "ibasic_filebuf0" },
            { "stossc()  (basic_filebuf �N���X)", "ibasic_filebuf0" },
            { "sungetc()  (basic_filebuf �N���X)", "ibasic_filebuf0" },
            { "swap()  (basic_filebuf �N���X)", "ibasic_filebuf0" },
            { "sync()  (basic_filebuf �N���X)", "ibasic_filebuf0" },
            { "uflow()  (basic_filebuf �N���X)", "ibasic_filebuf0" },
            { "underflow()  (basic_filebuf �N���X)", "ibasic_filebuf0" },
            { "xsgetn()  (basic_filebuf �N���X)", "ibasic_filebuf0" },
            { "xsputn()  (basic_filebuf �N���X)", "ibasic_filebuf0" },
            { "basic_fstream �N���X", "ibasic_fstream0" },
            { "basic_fstream()  (basic_fstream �N���X)", "ibasic_fstream0" },
            { "operator= (basic_fstream �N���X)", "ibasic_fstream0" },
            { "bad()  (basic_fstream �N���X)", "ibasic_fstream0" },
            { "close()  (basic_fstream �N���X)", "ibasic_fstream0" },
            { "is_open()  (basic_fstream �N���X)", "ibasic_fstream0" },
            { "open()  (basic_fstream �N���X)", "ibasic_fstream0" },
            { "rdbuf()  (basic_fstream �N���X)", "ibasic_fstream0" },
            { "swap()  (basic_fstream �N���X)", "ibasic_fstream0" },
            { "basic_ifstream �N���X", "ibasic_ifstream0" },
            { "basic_ifstream()  (basic_ifstream �N���X)", "ibasic_ifstream0" },
            { "operator= (basic_ifstream �N���X)", "ibasic_ifstream0" },
            { "operator void * (basic_filebuf �N���X)", "ibasic_filebuf0" },
            { "operator>> (basic_filebuf �N���X)", "ibasic_filebuf0" },
            { "operator! (basic_filebuf �N���X)", "ibasic_filebuf0" },
            { "bad()  (basic_ifstream �N���X)", "ibasic_ifstream0" },
            { "close()  (basic_ifstream �N���X)", "ibasic_ifstream0" },
            { "clear()  (basic_ifstream �N���X)", "ibasic_ifstream0" },
            { "copyfmt()  (basic_ifstream �N���X)", "ibasic_ifstream0" },
            { "eof()  (basic_ifstream �N���X)", "ibasic_ifstream0" },
            { "exceptions()  (basic_ifstream �N���X)", "ibasic_ifstream0" },
            { "fail()  (basic_ifstream �N���X)", "ibasic_ifstream0" },
            { "fill()  (basic_ifstream �N���X)", "ibasic_ifstream0" },
            { "flags()  (basic_ifstream �N���X)", "ibasic_ifstream0" },
            { "gcount()  (basic_ifstream �N���X)", "ibasic_ifstream0" },
            { "get()  (basic_ifstream �N���X)", "ibasic_ifstream0" },
            { "getline()  (basic_ifstream �N���X)", "ibasic_ifstream0" },
            { "getloc()  (basic_ifstream �N���X)", "ibasic_ifstream0" },
            { "good()  (basic_ifstream �N���X)", "ibasic_ifstream0" },
            { "ignore()  (basic_ifstream �N���X)", "ibasic_ifstream0" },
            { "imbue()  (basic_ifstream �N���X)", "ibasic_ifstream0" },
            { "iword()  (basic_ifstream �N���X)", "ibasic_ifstream0" },
            { "is_open()  (basic_ifstream �N���X)", "ibasic_ifstream0" },
            { "move()  (basic_ifstream �N���X)", "ibasic_ifstream0" },
            { "narrow()  (basic_ifstream �N���X)", "ibasic_ifstream0" },
            { "open()  (basic_ifstream �N���X)", "ibasic_ifstream0" },
            { "peek()  (basic_ifstream �N���X)", "ibasic_ifstream0" },
            { "precision()  (basic_ifstream �N���X)", "ibasic_ifstream0" },
            { "putback()  (basic_ifstream �N���X)", "ibasic_ifstream0" },
            { "pword()  (basic_ifstream �N���X)", "ibasic_ifstream0" },
            { "rdbuf()  (basic_ifstream �N���X)", "ibasic_ifstream0" },
            { "rdstate()  (basic_ifstream �N���X)", "ibasic_ifstream0" },
            { "read()  (basic_ifstream �N���X)", "ibasic_ifstream0" },
            { "readsome()  (basic_ifstream �N���X)", "ibasic_ifstream0" },
            { "register_call_back()  (basic_ifstream �N���X)", "ibasic_ifstream0" },
            { "seekg()  (basic_ifstream �N���X)", "ibasic_ifstream0" },
            { "self()  (basic_ifstream �N���X)", "ibasic_ifstream0" },
            { "sentry()  (basic_ifstream �N���X)", "ibasic_ifstream0" },
            { "set_rdbuf()  (basic_ifstream �N���X)", "ibasic_ifstream0" },
            { "setstate()  (basic_ifstream �N���X)", "ibasic_ifstream0" },
            { "swap()  (basic_ifstream �N���X)", "ibasic_ifstream0" },
            { "sync()  (basic_ifstream �N���X)", "ibasic_ifstream0" },
            { "tellg()  (basic_ifstream �N���X)", "ibasic_ifstream0" },
            { "tie()  (basic_ifstream �N���X)", "ibasic_ifstream0" },
            { "unget()  (basic_ifstream �N���X)", "ibasic_ifstream0" },
            { "unself()  (basic_ifstream �N���X)", "ibasic_ifstream0" },
            { "widen()  (basic_ifstream �N���X)", "ibasic_ifstream0" },
            { "width()  (basic_ifstream �N���X)", "ibasic_ifstream0" },
            { "basic_ofstream �N���X", "ibasic_ofstream0" },
            { "basic_ofstream()  (basic_ofstream �N���X)", "ibasic_ofstream0" },
            { "operator bool (basic_ofstream �N���X)", "ibasic_ofstream0" },
            { "operator void * (basic_ofstream �N���X)", "ibasic_ofstream0" },
            { "operator! (basic_ofstream �N���X)", "ibasic_ofstream0" },
            { "operator<< (basic_ofstream �N���X)", "ibasic_ofstream0" },
            { "operator= (basic_ofstream �N���X)", "ibasic_ofstream0" },
            { "bad()  (basic_ofstream �N���X)", "ibasic_ofstream0" },
            { "clear()  (basic_ofstream �N���X)", "ibasic_ofstream0" },
            { "close()  (basic_ofstream �N���X)", "ibasic_ofstream0" },
            { "copyfmt()  (basic_ofstream �N���X)", "ibasic_ofstream0" },
            { "eof()  (basic_ofstream �N���X)", "ibasic_ofstream0" },
            { "exceptions()  (basic_ofstream �N���X)", "ibasic_ofstream0" },
            { "fail()  (basic_ofstream �N���X)", "ibasic_ofstream0" },
            { "fill()  (basic_ofstream �N���X)", "ibasic_ofstream0" },
            { "flags()  (basic_ofstream �N���X)", "ibasic_ofstream0" },
            { "getloc()  (basic_ofstream �N���X)", "ibasic_ofstream0" },
            { "good()  (basic_ofstream �N���X)", "ibasic_ofstream0" },
            { "imbue()  (basic_ofstream �N���X)", "ibasic_ofstream0" },
            { "iword()  (basic_ofstream �N���X)", "ibasic_ofstream0" },
            { "is_open()  (basic_ofstream �N���X)", "ibasic_ofstream0" },
            { "move()  (basic_ofstream �N���X)", "ibasic_ofstream0" },
            { "narrow()  (basic_ofstream �N���X)", "ibasic_ofstream0" },
            { "open()  (basic_ofstream �N���X)", "ibasic_ofstream0" },
            { "precision()  (basic_ofstream �N���X)", "ibasic_ofstream0" },
            { "put()  (basic_ofstream �N���X)", "ibasic_ofstream0" },
            { "pword()  (basic_ofstream �N���X)", "ibasic_ofstream0" },
            { "rdbuf()  (basic_ofstream �N���X)", "ibasic_ofstream0" },
            { "rdstate()  (basic_ofstream �N���X)", "ibasic_ofstream0" },
            { "register_callback()  (basic_ofstream �N���X)", "ibasic_ofstream0" },
            { "seekp()  (basic_ofstream �N���X)", "ibasic_ofstream0" },
            { "self()  (basic_ofstream �N���X)", "ibasic_ofstream0" },
            { "sentry()  (basic_ofstream �N���X)", "ibasic_ofstream0" },
            { "set_rdbuf()  (basic_ofstream �N���X)", "ibasic_ofstream0" },
            { "setstate()  (basic_ofstream �N���X)", "ibasic_ofstream0" },
            { "swap()  (basic_ofstream �N���X)", "ibasic_ofstream0" },
            { "tellp()  (basic_ofstream �N���X)", "ibasic_ofstream0" },
            { "tie()  (basic_ofstream �N���X)", "ibasic_ofstream0" },
            { "unself()  (basic_ofstream �N���X)", "ibasic_ofstream0" },
            { "widen()  (basic_ofstream �N���X)", "ibasic_ofstream0" },
            { "width()  (basic_ofstream �N���X)", "ibasic_ofstream0" },
            { "write()  (basic_ofstream �N���X)", "ibasic_ofstream0" },
            { "<valarray>", "jvalarray" },
            { "operator!=  <valarray>", "jvalarray" },
            { "operator%  <valarray>", "jvalarray" },
            { "operator&  <valarray>", "jvalarray" },
            { "operator&&  <valarray>", "jvalarray" },
            { "operator>  <valarray>", "jvalarray" },
            { "operator>=  <valarray>", "jvalarray" },
            { "operator>>  <valarray>", "jvalarray" },
            { "operator<  <valarray>", "jvalarray" },
            { "operator<=  <valarray>", "jvalarray" },
            { "operator<<  <valarray>", "jvalarray" },
            { "operator*  <valarray>", "jvalarray" },
            { "operator+  <valarray>", "jvalarray" },
            { "operator-  <valarray>", "jvalarray" },
            { "operator/  <valarray>", "jvalarray" },
            { "operator==  <valarray>", "jvalarray" },
            { "operator^  <valarray>", "jvalarray" },
            { "operator|  <valarray>", "jvalarray" },
            { "operator||  <valarray>", "jvalarray" },
            { "abs()  <valarray>", "jvalarray" },
            { "acos()  <valarray>", "jvalarray" },
            { "asin()  <valarray>", "jvalarray" },
            { "atan()  <valarray>", "jvalarray" },
            { "atan2()  <valarray>", "jvalarray" },
            { "begin()  <valarray>", "jvalarray" },
            { "cos()  <valarray>", "jvalarray" },
            { "cosh()  <valarray>", "jvalarray" },
            { "end()  <valarray>", "jvalarray" },
            { "exp()  <valarray>", "jvalarray" },
            { "log()  <valarray>", "jvalarray" },
            { "log10()  <valarray>", "jvalarray" },
            { "pow()  <valarray>", "jvalarray" },
            { "sin()  <valarray>", "jvalarray" },
            { "sinh()  <valarray>", "jvalarray" },
            { "sqrt()  <valarray>", "jvalarray" },
            { "swap()  <valarray>", "jvalarray" },
            { "tan()  <valarray>", "jvalarray" },
            { "tanh()  <valarray>", "jvalarray" },
            { "slice �N���X", "jslice0"},
            { "slice()  (slice �N���X)", "jslice0"},
            { "size()  (slice �N���X)", "jslice0"},
            { "start()  (slice �N���X)", "jslice0"},
            { "stride()  (slice �N���X)", "jslice0"},
            { "valarray �N���X", "jvalarray0"},
            { "valarray()  (valarray �N���X)", "jvalarray0"},
            { "operator!  (valarray �N���X)", "jvalarray0"},
            { "operator%=  (valarray �N���X)", "jvalarray0"},
            { "operator&=  (valarray �N���X)", "jvalarray0"},
            { "operator>>=  (valarray �N���X)", "jvalarray0"},
            { "operator<<=  (valarray �N���X)", "jvalarray0"},
            { "operator*=  (valarray �N���X)", "jvalarray0"},
            { "operator+  (valarray �N���X)", "jvalarray0"},
            { "operator+=  (valarray �N���X)", "jvalarray0"},
            { "operator-  (valarray �N���X)", "jvalarray0"},
            { "operator-=  (valarray �N���X)", "jvalarray0"},
            { "operator/=  (valarray �N���X)", "jvalarray0"},
            { "operator=  (valarray �N���X)", "jvalarray0"},
            { "operator[]  (valarray �N���X)", "jvalarray0"},
            { "operator^=  (valarray �N���X)", "jvalarray0"},
            { "operator|=  (valarray �N���X)", "jvalarray0"},
            { "operator~  (valarray �N���X)", "jvalarray0"},
            { "apply()  (valarray �N���X)", "jvalarray0"},
            { "cshift()  (valarray �N���X)", "jvalarray0"},
            { "free()  (valarray �N���X)", "jvalarray0"},
            { "max()  (valarray �N���X)", "jvalarray0"},
            { "min()  (valarray �N���X)", "jvalarray0"},
            { "resize()  (valarray �N���X)", "jvalarray0"},
            { "shift()  (valarray �N���X)", "jvalarray0"},
            { "size()  (valarray �N���X)", "jvalarray0"},
            { "sum()  (valarray �N���X)", "jvalarray0"},
            { "swap()  (valarray �N���X)", "jvalarray0"},
            { "<initializer_list>", "kinitializer_list" },
            { "initializer_list �N���X", "kinitializer_list0" },
            { "initializer_list()  (initializer_list �N���X)", "kinitializer_list0" },
            { "begin()  (initializer_list �N���X)", "kinitializer_list0" },
            { "end()  (initializer_list �N���X)", "kinitializer_list0" },
            { "size()  (initializer_list �N���X)", "kinitializer_list0" },
            { "<sstream>", "lsstream" },
            { "swap()  <sstream>", "lsstream" },
            { "basic_stringbuf �N���X", "lbasic_stringbuf0" },
            { "basic_stringbuf()  (basic_stringbuf �N���X)", "lbasic_stringbuf0" },
            { "operator=  (basic_stringbuf �N���X)", "lbasic_stringbuf0" },
            { "overflow()  (basic_stringbuf �N���X)", "lbasic_stringbuf0" },
            { "pbackfail()  (basic_stringbuf �N���X)", "lbasic_stringbuf0" },
            { "seekoff()  (basic_stringbuf �N���X)", "lbasic_stringbuf0" },
            { "seekpos()  (basic_stringbuf �N���X)", "lbasic_stringbuf0" },
            { "str()  (basic_stringbuf �N���X)", "lbasic_stringbuf0" },
            { "swap()  (basic_stringbuf �N���X)", "lbasic_stringbuf0" },
            { "underflow()  (basic_stringbuf �N���X)", "lbasic_stringbuf0" },
            { "basic_istringstream �N���X", "lbasic_istringstream0" },
            { "basic_istringstream()  (basic_istringstream �N���X)", "lbasic_istringstream0" },
            { "operator=  (basic_istringstream �N���X)", "lbasic_istringstream0" },
            { "rdbuf()  (basic_istringstream �N���X)", "lbasic_istringstream0" },
            { "str()  (basic_istringstream �N���X)", "lbasic_istringstream0" },
            { "swap()  (basic_istringstream �N���X)", "lbasic_istringstream0" },
            { "basic_ostringstream �N���X", "lbasic_ostringstream0" },
            { "basic_ostringstream()  (basic_ostringstream �N���X)", "lbasic_ostringstream0" },
            { "operator bool  (basic_ostringstream �N���X)", "lbasic_ostringstream0" },
            { "operator void *  (basic_ostringstream �N���X)", "lbasic_ostringstream0" },
            { "operator!  (basic_ostringstream �N���X)", "lbasic_ostringstream0" },
            { "operator=  (basic_ostringstream �N���X)", "lbasic_ostringstream0" },
            { "operator<<  (basic_ostringstream �N���X)", "lbasic_ostringstream0" },
            { "bad()  (basic_ostringstream �N���X)", "lbasic_ostringstream0" },
            { "clear()  (basic_ostringstream �N���X)", "lbasic_ostringstream0" },
            { "copyfmt()  (basic_ostringstream �N���X)", "lbasic_ostringstream0" },
            { "eof()  (basic_ostringstream �N���X)", "lbasic_ostringstream0" },
            { "exceptions()  (basic_ostringstream �N���X)", "lbasic_ostringstream0" },
            { "fail()  (basic_ostringstream �N���X)", "lbasic_ostringstream0" },
            { "fill()  (basic_ostringstream �N���X)", "lbasic_ostringstream0" },
            { "flags()  (basic_ostringstream �N���X)", "lbasic_ostringstream0" },
            { "flush()  (basic_ostringstream �N���X)", "lbasic_ostringstream0" },
            { "getloc()  (basic_ostringstream �N���X)", "lbasic_ostringstream0" },
            { "good()  (basic_ostringstream �N���X)", "lbasic_ostringstream0" },
            { "imbue()  (basic_ostringstream �N���X)", "lbasic_ostringstream0" },
            { "iword()  (basic_ostringstream �N���X)", "lbasic_ostringstream0" },
            { "move()  (basic_ostringstream �N���X)", "lbasic_ostringstream0" },
            { "narrow()  (basic_ostringstream �N���X)", "lbasic_ostringstream0" },
            { "precision()  (basic_ostringstream �N���X)", "lbasic_ostringstream0" },
            { "put()  (basic_ostringstream �N���X)", "lbasic_ostringstream0" },
            { "pword()  (basic_ostringstream �N���X)", "lbasic_ostringstream0" },
            { "rdbuf()  (basic_ostringstream �N���X)", "lbasic_ostringstream0" },
            { "rdstate()  (basic_ostringstream �N���X)", "lbasic_ostringstream0" },
            { "register_callback()  (basic_ostringstream �N���X)", "lbasic_ostringstream0" },
            { "seekp()  (basic_ostringstream �N���X)", "lbasic_ostringstream0" },
            { "self()  (basic_ostringstream �N���X)", "lbasic_ostringstream0" },
            { "sentry()  (basic_ostringstream �N���X)", "lbasic_ostringstream0" },
            { "set_rdbuf()  (basic_ostringstream �N���X)", "lbasic_ostringstream0" },
            { "setstate()  (basic_ostringstream �N���X)", "lbasic_ostringstream0" },
            { "str()  (basic_ostringstream �N���X)", "lbasic_ostringstream0" },
            { "swap()  (basic_ostringstream �N���X)", "lbasic_ostringstream0" },
            { "tellp()  (basic_ostringstream �N���X)", "lbasic_ostringstream0" },
            { "tie()  (basic_ostringstream �N���X)", "lbasic_ostringstream0" },
            { "unself()  (basic_ostringstream �N���X)", "lbasic_ostringstream0" },
            { "widen()  (basic_ostringstream �N���X)", "lbasic_ostringstream0" },
            { "width()  (basic_ostringstream �N���X)", "lbasic_ostringstream0" },
            { "write()  (basic_ostringstream �N���X)", "lbasic_ostringstream0" },
            { "basic_stringstream �N���X", "lbasic_stringstream0" },
            { "basic_stringstream()  (basic_stringstream �N���X)", "lbasic_stringstream0" },
            { "operator=  (basic_stringstream �N���X)", "lbasic_stringstream0" },
            { "rdbuf()  (basic_stringstream �N���X)", "lbasic_stringstream0" },
            { "str()  (basic_stringstream �N���X)", "lbasic_stringstream0" },
            { "<new>", "mnew" },
            { "nothrow", "mnew" },
            { "operator delete  <new>", "mnew" },
            { "operator delete[]  <new>", "mnew" },
            { "operator new  <new>", "mnew" },
            { "operator new[]  <new>", "mnew" },
            { "get_new_handler()  <new>", "mnew" },
            { "launder()  <new>", "mnew" },
            { "set_new_handler()  <new>", "mnew" },
            { "bad_alloc �N���X", "mnew" },
            { "bad_array_new_length �N���X", "mnew" },
            { "nothrow_t �\����", "mnew" },
            { "<random>", "nrandom" },
            { "linear_congruential_engine �N���X", "nlinear_congruential_engine0" },
            { "<complex>", "ocomplex" },
            { "operator!=  <complex>", "ocomplex" },
            { "operator*  <complex>", "ocomplex" },
            { "operator+  <complex>", "ocomplex" },
            { "operator-  <complex>", "ocomplex" },
            { "operator/  <complex>", "ocomplex" },
            { "operator<<  <complex>", "ocomplex" },
            { "operator==  <complex>", "ocomplex" },
            { "operator>>  <complex>", "ocomplex" },
            { "operator\"\"i  <complex>", "ocomplex" },
            { "operator\"\"if  <complex>", "ocomplex" },
            { "operator\"\"il  <complex>", "ocomplex" },
            { "abs()  <complex>", "ocomplex" },
            { "acos()  <complex>", "ocomplex" },
            { "acosh()  <complex>", "ocomplex" },
            { "arg()  <complex>", "ocomplex" },
            { "asin()  <complex>", "ocomplex" },
            { "asinh()  <complex>", "ocomplex" },
            { "atan()  <complex>", "ocomplex" },
            { "atanh()  <complex>", "ocomplex" },
            { "conj()  <complex>", "ocomplex" },
            { "cos()  <complex>", "ocomplex" },
            { "cosh()  <complex>", "ocomplex" },
            { "exp()  <complex>", "ocomplex" },
            { "imag()  <complex>", "ocomplex" },
            { "log()  <complex>", "ocomplex" },
            { "log10()  <complex>", "ocomplex" },
            { "norm()  <complex>", "ocomplex" },
            { "polar()  <complex>", "ocomplex" },
            { "pow()  <complex>", "ocomplex" },
            { "proj()  <complex>", "ocomplex" },
            { "real()  <complex>", "ocomplex" },
            { "sin()  <complex>", "ocomplex" },
            { "sinh()  <complex>", "ocomplex" },
            { "sqrt()  <complex>", "ocomplex" },
            { "tan()  <complex>", "ocomplex" },
            { "tanh()  <complex>", "ocomplex" },
            { "complex �N���X", "ocomplex0" },
            { "complex()  (complex �N���X)", "ocomplex0" },
            { "operator*=  (complex �N���X)", "ocomplex0" },
            { "operator+=  (complex �N���X)", "ocomplex0" },
            { "operator-=  (complex �N���X)", "ocomplex0" },
            { "operator/=  (complex �N���X)", "ocomplex0" },
            { "operator=  (complex �N���X)", "ocomplex0" },
            { "imag()  (complex �N���X)", "ocomplex0" },
            { "real()  (complex �N���X)", "ocomplex0" },
            {"<stack>", "pstack" },
            {"operator!=  <stack>", "pstack" },
            {"operator<  <stack>", "pstack" },
            {"operator<=  <stack>", "pstack" },
            {"operator==  <stack>", "pstack" },
            {"operator>  <stack>", "pstack" },
            {"operator>=  <stack>", "pstack" },
            {"stack �N���X", "pstack0" },
            {"stack()  (stack �N���X)", "pstack0" },
            {"emplace()  (stack �N���X)", "pstack0" },
            {"empty()  (stack �N���X)", "pstack0" },
            {"pop()  (stack �N���X)", "pstack0" },
            {"push()  (stack �N���X)", "pstack0" },
            {"size()  (stack �N���X)", "pstack0" },
            {"top()  (stack �N���X)", "pstack0" },
            {"<queue>", "qqueue" },
            {"operator!=  <queue>", "qqueue" },
            {"operator<  <queue>", "qqueue" },
            {"operator<=  <queue>", "qqueue" },
            {"operator==  <queue>", "qqueue" },
            {"operator>  <queue>", "qqueue" },
            {"operator>=  <queue>", "qqueue" },
            {"queue �N���X", "qqueue0" },
            {"queue()  (queue �N���X)", "qqueue0" },
            {"back()  (queue �N���X)", "qqueue0" },
            {"emplace()  (queue �N���X)", "qqueue0" },
            {"empty()  (queue �N���X)", "qqueue0" },
            {"front()  (queue �N���X)", "qqueue0" },
            {"pop()  (queue �N���X)", "qqueue0" },
            {"push()  (queue �N���X)", "qqueue0" },
            {"size()  (queue �N���X)", "qqueue0" },
            {"<any>", "rany" },
            {"any_cast()  <any>", "rany" },
            {"make_any()  <any>", "rany" },
            {"swap()  <any>", "rany" },
            {"bad_any_cast �N���X", "rany" },
            {"any �N���X", "rany0" },
            {"any()  (any �N���X)", "rany0" },
            {"operator=  (any �N���X)", "rany0" },
            {"emplace()  (any �N���X)", "rany0" },
            {"has_value()  (any �N���X)", "rany0" },
            {"reset()  (any �N���X)", "rany0" },
            {"swap()  (any �N���X)", "rany0" },
            {"type()  (any �N���X)", "rany0" },
            {"<stdexcept>", "sstdexcept" },
            {"domain_error �N���X", "sstdexcept" },
            {"invalid_argument �N���X", "sstdexcept" },
            {"length_error �N���X", "sstdexcept" },
            {"logic_error �N���X", "sstdexcept" },
            {"out_of_range �N���X", "sstdexcept" },
            {"overflow_error �N���X", "sstdexcept" },
            {"range_error �N���X", "sstdexcept" },
            {"runtime_error �N���X", "sstdexcept" },
            {"underflow_error �N���X", "sstdexcept" },
            {"<string_view>", "string_view" },
            {"operator!=  <string_view>", "tstring_view" },
            {"operator==  <string_view>", "tstring_view" },
            {"operator<  <string_view>", "tstring_view" },
            {"operator<=  <string_view>", "tstring_view" },
            {"operator<<  <string_view>", "tstring_view" },
            {"operator>  <string_view>", "tstring_view" },
            {"operator>=  <string_view>", "tstring_view" },
            {"operator\"\"sv  <string_view>", "tstring_view" },
            {"hash �\����  <string_view>", "tstring_view" },
            {"basic_string_view �N���X", "tbasic_string_view0" },
            {"basic_string_view()  (basic_string_view �N���X)", "tbasic_string_view0" },
            {"operator=  (basic_string_view �N���X)", "tbasic_string_view0" },
            {"operator[]  (basic_string_view �N���X)", "tbasic_string_view0" },
            {"at()  (basic_string_view �N���X)", "tbasic_string_view0" },
            {"back()  (basic_string_view �N���X)", "tbasic_string_view0" },
            {"begin()  (basic_string_view �N���X)", "tbasic_string_view0" },
            {"cbegin()  (basic_string_view �N���X)", "tbasic_string_view0" },
            {"cend()  (basic_string_view �N���X)", "tbasic_string_view0" },
            {"copy()  (basic_string_view �N���X)", "tbasic_string_view0" },
            {"compare()  (basic_string_view �N���X)", "tbasic_string_view0" },
            {"crbegin()  (basic_string_view �N���X)", "tbasic_string_view0" },
            {"crend()  (basic_string_view �N���X)", "tbasic_string_view0" },
            {"data()  (basic_string_view �N���X)", "tbasic_string_view0" },
            {"empty()  (basic_string_view �N���X)", "tbasic_string_view0" },
            {"end()  (basic_string_view �N���X)", "tbasic_string_view0" },
            {"ends_with()  (basic_string_view �N���X)", "tbasic_string_view0" },
            {"find()  (basic_string_view �N���X)", "tbasic_string_view0" },
            {"find_first_not_of()  (basic_string_view �N���X)", "tbasic_string_view0" },
            {"find_first_of()  (basic_string_view �N���X)", "tbasic_string_view0" },
            {"find_last_not_of()  (basic_string_view �N���X)", "tbasic_string_view0" },
            {"find_last_of()  (basic_string_view �N���X)", "tbasic_string_view0" },
            {"front()  (basic_string_view �N���X)", "tbasic_string_view0" },
            {"length()  (basic_string_view �N���X)", "tbasic_string_view0" },
            {"max_size()  (basic_string_view �N���X)", "tbasic_string_view0" },
            {"rbegin()  (basic_string_view �N���X)", "tbasic_string_view0" },
            {"remove_prefix()  (basic_string_view �N���X)", "tbasic_string_view0" },
            {"remove_suffix()  (basic_string_view �N���X)", "tbasic_string_view0" },
            {"rend()  (basic_string_view �N���X)", "tbasic_string_view0" },
            {"rfind()  (basic_string_view �N���X)", "tbasic_string_view0" },
            {"size()  (basic_string_view �N���X)", "tbasic_string_view0" },
            {"starts_with()  (basic_string_view �N���X)", "tbasic_string_view0" },
            {"substr()  (basic_string_view �N���X)", "tbasic_string_view0" },
            {"swap()  (basic_string_view �N���X)", "tbasic_string_view0" },
            {"<type_traits>", "utype_traits" },
            {"integral_constant �\����", "utype_traits" },
            {"is_arithmetic �\����", "utype_traits" },
            {"is_array �\����", "utype_traits" },
            {"is_class �\����", "utype_traits" },
            {"is_compound �\����", "utype_traits" },
            {"is_const �\����", "utype_traits" },
            {"is_enum �\����", "utype_traits" },
            {"is_floating_point �\����", "utype_traits" },
            {"is_function �\����", "utype_traits" },
            {"is_integral �\����", "utype_traits" },
            {"is_lvalue_reference �\����", "utype_traits" },
            {"is_member_function_pointer �\����", "utype_traits" },
            {"is_member_object_pointer �\����", "utype_traits" },
            {"is_null_pointer �\����", "utype_traits" },
            {"is_object �\����", "utype_traits" },
            {"is_pointer �\����", "utype_traits" },
            {"is_reference �\����", "utype_traits" },
            {"is_rvalue_reference �\����", "utype_traits" },
            {"is_scalar �\����", "utype_traits" },
            {"is_standard_layout �\����", "utype_traits" },
            {"is_trivial �\����", "utype_traits" },
            {"is_trivially_copyable �\����", "utype_traits" },
            {"is_union �\����", "utype_traits" },
            {"is_void �\����", "utype_traits" },
            {"is_volatile �\����", "utype_traits" },
            {"<span>", "vspan" },
            {"as_bytes()  <span>", "vspan" },
            {"as_writable_bytes()  <span>", "vspan" },
            {"span �N���X", "vspan0" },
            {"span()  (span �N���X)", "vspan0" },
            {"operator=  (span �N���X)", "vspan0" },
            {"operator[]  (span �N���X)", "vspan0" },
            {"back()  (span �N���X)", "vspan0" },
            {"begin()  (span �N���X)", "vspan0" },
            {"data()  (span �N���X)", "vspan0" },
            {"empty()  (span �N���X)", "vspan0" },
            {"end()  (span �N���X)", "vspan0" },
            {"first()  (span �N���X)", "vspan0" },
            {"front()  (span �N���X)", "vspan0" },
            {"last()  (span �N���X)", "vspan0" },
            {"rbegin()  (span �N���X)", "vspan0" },
            {"rend()  (span �N���X)", "vspan0" },
            {"size()  (span �N���X)", "vspan0" },
            {"size_bytes()  (span �N���X)", "vspan0" },
            {"subspan()  (span �N���X)", "vspan0" },
            {"<filesystem>", "wfilesystem" },
            {"absolute()  <filesystem>", "wfilesystem" },
            {"begin()  <filesystem>", "wfilesystem" },
            {"canonical()  <filesystem>", "wfilesystem" },
            {"copy()  <filesystem>", "wfilesystem" },
            {"copy_file()  <filesystem>", "wfilesystem" },
            {"copy_symlink()  <filesystem>", "wfilesystem" },
            {"create_directories()  <filesystem>", "wfilesystem" },
            {"create_directory()  <filesystem>", "wfilesystem" },
            {"create_directory_symlink()  <filesystem>", "wfilesystem" },
            {"create_hard_link()  <filesystem>", "wfilesystem" },
            {"create_symlink()  <filesystem>", "wfilesystem" },
            {"current_path()  <filesystem>", "wfilesystem" },
            {"end()  <filesystem>", "wfilesystem" },
            {"equivalent()  <filesystem>", "wfilesystem" },
            {"exists()  <filesystem>", "wfilesystem" },
            {"file_size()  <filesystem>", "wfilesystem" },
            {"hard_link_count()  <filesystem>", "wfilesystem" },
            {"hash_value()  <filesystem>", "wfilesystem" },
            {"is_block_file()  <filesystem>", "wfilesystem" },
            {"is_character_file()  <filesystem>", "wfilesystem" },
            {"is_directory()  <filesystem>", "wfilesystem" },
            {"is_empty()  <filesystem>", "wfilesystem" },
            {"is_fifo()  <filesystem>", "wfilesystem" },
            {"is_other()  <filesystem>", "wfilesystem" },
            {"is_regular_file()  <filesystem>", "wfilesystem" },
            {"is_socket()  <filesystem>", "wfilesystem" },
            {"is_symlink()  <filesystem>", "wfilesystem" },
            {"last_write_time()  <filesystem>", "wfilesystem" },
            {"permissions()  <filesystem>", "wfilesystem" },
            {"proximate()  <filesystem>", "wfilesystem" },
            {"read_symlink()  <filesystem>", "wfilesystem" },
            {"relative()  <filesystem>", "wfilesystem" },
            {"remove()  <filesystem>", "wfilesystem" },
            {"remove_all()  <filesystem>", "wfilesystem" },
            {"rename()  <filesystem>", "wfilesystem" },
            {"resize_file()  <filesystem>", "wfilesystem" },
            {"space()  <filesystem>", "wfilesystem" },
            {"status()  <filesystem>", "wfilesystem" },
            {"status_known()  <filesystem>", "wfilesystem" },
            {"swap()  <filesystem>", "wfilesystem" },
            {"symlink_status()  <filesystem>", "wfilesystem" },
            {"system_complete()  <filesystem>", "wfilesystem" },
            {"temp_directory_path()  <filesystem>", "wfilesystem" },
            {"u8path()  <filesystem>", "wfilesystem" },
            {"weakly_canonical()  <filesystem>", "wfilesystem" },
            {"directory_error �N���X", "wfilesystem" },
            {"space_info �\����", "wfilesystem" },
            {"file_status �N���X", "wfile_status0" },
            {"file_status()  (file_status �N���X)", "wfile_status0" },
            {"operator=  (file_status �N���X)", "wfile_status0" },
            {"permissions()  (file_status �N���X)", "wfile_status0" },
            {"type()  (file_status �N���X)", "wfile_status0" },
            {"path �N���X", "wpath0" },
            {"path()  (path �N���X)", "wpath0" },
            {"operator<<  (path �N���X)", "wpath0" },
            {"operator>>  (path �N���X)", "wpath0" },
            {"operator=  (path �N���X)", "wpath0" },
            {"operator+=  (path �N���X)", "wpath0" },
            {"operator/=  (path �N���X)", "wpath0" },
            {"operator string_type  (path �N���X)", "wpath0" },
            {"append()  (path �N���X)", "wpath0" },
            {"assign()  (path �N���X)", "wpath0" },
            {"begin()  (path �N���X)", "wpath0" },
            {"c_str()  (path �N���X)", "wpath0" },
            {"clear()  (path �N���X)", "wpath0" },
            {"compare()  (path �N���X)", "wpath0" },
            {"concat()  (path �N���X)", "wpath0" },
            {"empty()  (path �N���X)", "wpath0" },
            {"end()  (path �N���X)", "wpath0" },
            {"extension()  (path �N���X)", "wpath0" },
            {"filename()  (path �N���X)", "wpath0" },
            {"generic_string()  (path �N���X)", "wpath0" },
            {"generic_u8string()  (path �N���X)", "wpath0" },
            {"generic_u16string()  (path �N���X)", "wpath0" },
            {"generic_u32string()  (path �N���X)", "wpath0" },
            {"generic_wstring()  (path �N���X)", "wpath0" },
            {"has_extension()  (path �N���X)", "wpath0" },
            {"has_filename()  (path �N���X)", "wpath0" },
            {"has_parent_path()  (path �N���X)", "wpath0" },
            {"has_relative_path()  (path �N���X)", "wpath0" },
            {"has_root_directory()  (path �N���X)", "wpath0" },
            {"has_root_path()  (path �N���X)", "wpath0" },
            {"has_stem()  (path �N���X)", "wpath0" },
            {"is_absolute()  (path �N���X)", "wpath0" },
            {"is_relative()  (path �N���X)", "wpath0" },
            {"make_preferred()  (path �N���X)", "wpath0" },
            {"native()  (path �N���X)", "wpath0" },
            {"parent_path()  (path �N���X)", "wpath0" },
            {"relative_path()  (path �N���X)", "wpath0" },
            {"remove_filename()  (path �N���X)", "wpath0" },
            {"replace_filename()  (path �N���X)", "wpath0" },
            {"root_directory()  (path �N���X)", "wpath0" },
            {"root_name()  (path �N���X)", "wpath0" },
            {"root_path()  (path �N���X)", "wpath0" },
            {"stem()  (path �N���X)", "wpath0" },
            {"string()  (path �N���X)", "wpath0" },
            {"swap()  (path �N���X)", "wpath0" },
            {"u8string()  (path �N���X)", "wpath0" },
            {"u16string()  (path �N���X)", "wpath0" },
            {"u32string()  (path �N���X)", "wpath0" },
            {"wstring()  (path �N���X)", "wpath0" },
            {"directory_iterator �N���X", "wdirectory_iterator0" },
            {"directory_iterator()  (directory_iterator �N���X)", "wdirectory_iterator0" },
            {"operator!=  (directory_iterator �N���X)", "wdirectory_iterator0" },
            {"operator=  (directory_iterator �N���X)", "wdirectory_iterator0" },
            {"operator==  (directory_iterator �N���X)", "wdirectory_iterator0" },
            {"operator*  (directory_iterator �N���X)", "wdirectory_iterator0" },
            {"operator->  (directory_iterator �N���X)", "wdirectory_iterator0" },
            {"operator++  (directory_iterator �N���X)", "wdirectory_iterator0" },
            {"increment()  (directory_iterator �N���X)", "wdirectory_iterator0" },
            {"<forward_list>", "xforward_list" },
            {"operator==  <forward_list>", "xforward_list" },
            {"operator!=  <forward_list>", "xforward_list" },
            {"operator<  <forward_list>", "xforward_list" },
            {"operator<=  <forward_list>", "xforward_list" },
            {"operator>  <forward_list>", "xforward_list" },
            {"operator>=  <forward_list>", "xforward_list" },
            {"swap()  <forward_list>", "xforward_list" },
            {"forward_list �N���X", "xforward_list0" },
            {"forward_list()  (forward_list �N���X)", "xforward_list0" },
            {"operator=  (forward_list �N���X)", "xforward_list0" },
            {"assign()  (forward_list �N���X)", "xforward_list0" },
            {"before_begin()  (forward_list �N���X)", "xforward_list0" },
            {"begin()  (forward_list �N���X)", "xforward_list0" },
            {"cbefore_begin()  (forward_list �N���X)", "xforward_list0" },
            {"cbegin()  (forward_list �N���X)", "xforward_list0" },
            {"cend()  (forward_list �N���X)", "xforward_list0" },
            {"clear()  (forward_list �N���X)", "xforward_list0" },
            {"emplace_after()  (forward_list �N���X)", "xforward_list0" },
            {"emplace_front()  (forward_list �N���X)", "xforward_list0" },
            {"empty()  (forward_list �N���X)", "xforward_list0" },
            {"end()  (forward_list �N���X)", "xforward_list0" },
            {"erase_after()  (forward_list �N���X)", "xforward_list0" },
            {"front()  (forward_list �N���X)", "xforward_list0" },
            {"get_allocator()  (forward_list �N���X)", "xforward_list0" },
            {"insert_after()  (forward_list �N���X)", "xforward_list0" },
            {"max_size()  (forward_list �N���X)", "xforward_list0" },
            {"merge()  (forward_list �N���X)", "xforward_list0" },
            {"pop_front()  (forward_list �N���X)", "xforward_list0" },
            {"push_front()  (forward_list �N���X)", "xforward_list0" },
            {"remove()  (forward_list �N���X)", "xforward_list0" },
            {"remove_if()  (forward_list �N���X)", "xforward_list0" },
            {"resize()  (forward_list �N���X)", "xforward_list0" },
            {"reverse()  (forward_list �N���X)", "xforward_list0" },
            {"sort()  (forward_list �N���X)", "xforward_list0" },
            {"splice_after()  (forward_list �N���X)", "xforward_list0" },
            {"swap()  (forward_list �N���X)", "xforward_list0" },
            {"unique()  (forward_list �N���X)", "xforward_list0" },
            {"<deque>", "ydeque" },
            {"operator!=  <deque>", "ydeque" },
            {"operator<  <deque>", "ydeque" },
            {"operator<=  <deque>", "ydeque" },
            {"operator==  <deque>", "ydeque" },
            {"operator>  <deque>", "ydeque" },
            {"operator>=  <deque>", "ydeque" },
            {"swap()  <deque>", "ydeque" },
            {"deque �N���X", "ydeque0" },
            {"<charconv>", "zcharconv" },
            {"from_chars()  <charconv>", "zcharconv" },
            {"to_chars()  <charconv>", "zcharconv" },
            {"<set>", "Aset" },
            {"operator!=  <set>", "Aset" },
            {"operator<  <set>", "Aset" },
            {"operator<=  <set>", "Aset" },
            {"operator==  <set>", "Aset" },
            {"operator>  <set>", "Aset" },
            {"operator>=  <set>", "Aset" },
            {"swap()  <set>", "Aset" },
            {"set �N���X", "Aset0" },
            {"set()  (set �N���X)", "Aset0" },
            {"operator=  (set �N���X)", "Aset0" },
            {"begin()  (set �N���X)", "Aset0" },
            {"cbegin()  (set �N���X)", "Aset0" },
            {"cend()  (set �N���X)", "Aset0" },
            {"clear()  (set �N���X)", "Aset0" },
            {"contains()  (set �N���X)", "Aset0" },
            {"count()  (set �N���X)", "Aset0" },
            {"crbegin()  (set �N���X)", "Aset0" },
            {"crend()  (set �N���X)", "Aset0" },
            {"emplace()  (set �N���X)", "Aset0" },
            {"emplace_hint()  (set �N���X)", "Aset0" },
            {"empty()  (set �N���X)", "Aset0" },
            {"end()  (set �N���X)", "Aset0" },
            {"equal_range()  (set �N���X)", "Aset0" },
            {"erase()  (set �N���X)", "Aset0" },
            {"find()  (set �N���X)", "Aset0" },
            {"get_allocator()  (set �N���X)", "Aset0" },
            {"insert()  (set �N���X)", "Aset0" },
            {"key_comp()  (set �N���X)", "Aset0" },
            {"lower_bound()  (set �N���X)", "Aset0" },
            {"max_size()  (set �N���X)", "Aset0" },
            {"rbegin()  (set �N���X)", "Aset0" },
            {"rend()  (set �N���X)", "Aset0" },
            {"size()  (set �N���X)", "Aset0" },
            {"swap()  (set �N���X)", "Aset0" },
            {"upper_bound()  (set �N���X)", "Aset0" },
            {"value_comp()  (set �N���X)", "Aset0" },
            {"multiset �N���X", "Amultiset0" },
            {"multiset()  (multiset �N���X)", "Amultiset0" },
            {"operator=  (multiset �N���X)", "Amultiset0" },
            {"begin()  (multiset �N���X)", "Amultiset0" },
            {"cbegin()  (multiset �N���X)", "Amultiset0" },
            {"cend()  (multiset �N���X)", "Amultiset0" },
            {"clear()  (multiset �N���X)", "Amultiset0" },
            {"contains()  (multiset �N���X)", "Amultiset0" },
            {"count()  (multiset �N���X)", "Amultiset0" },
            {"crbegin()  (multiset �N���X)", "Amultiset0" },
            {"crend()  (multiset �N���X)", "Amultiset0" },
            {"emplace()  (multiset �N���X)", "Amultiset0" },
            {"emplace_hint()  (multiset �N���X)", "Amultiset0" },
            {"empty()  (multiset �N���X)", "Amultiset0" },
            {"end()  (multiset �N���X)", "Amultiset0" },
            {"equal_range()  (multiset �N���X)", "Amultiset0" },
            {"erase()  (multiset �N���X)", "Amultiset0" },
            {"find()  (multiset �N���X)", "Amultiset0" },
            {"get_allocator()  (multiset �N���X)", "Amultiset0" },
            {"insert()  (multiset �N���X)", "Amultiset0" },
            {"key_comp()  (multiset �N���X)", "Amultiset0" },
            {"lower_bound()  (multiset �N���X)", "Amultiset0" },
            {"max_size()  (multiset �N���X)", "Amultiset0" },
            {"rbegin()  (multiset �N���X)", "Amultiset0" },
            {"rend()  (multiset �N���X)", "Amultiset0" },
            {"size()  (multiset �N���X)", "Amultiset0" },
            {"swap()  (multiset �N���X)", "Amultiset0" },
            {"upper_bound()  (multiset �N���X)", "Amultiset0" },
            {"value_comp()  (multiset �N���X)", "Amultiset0" },
            {"<numeric>", "Bnumeric" },
            {"accumulate()  <numeric>", "Bnumeric" },
            {"adjacent_difference()  <numeric>", "Bnumeric" },
            {"exclusive_scan()  <numeric>", "Bnumeric" },
            {"gcd()  <numeric>", "Bnumeric" },
            {"inclusive_scan()  <numeric>", "Bnumeric" },
            {"inner_product()  <numeric>", "Bnumeric" },
            {"iota()  <numeric>", "Bnumeric" },
            {"lcm()  <numeric>", "Bnumeric" },
            {"partial_sum()  <numeric>", "Bnumeric" },
            {"reduce()  <numeric>", "Bnumeric" },
            {"transform_exclusive_scan()  <numeric>", "Bnumeric" },
            {"transform_inclusive_scan()  <numeric>", "Bnumeric" },
            {"transform_reduce()  <numeric>", "Bnumeric" },
            {"<ranges>", "Cranges" },
            {"begin()  <ranges>", "Cranges" },
            {"cbegin()  <ranges>", "Cranges" },
            {"cdata()  <ranges>", "Cranges" },
            {"cend()  <ranges>", "Cranges" },
            {"crbegin()  <ranges>", "Cranges" },
            {"crend()  <ranges>", "Cranges" },
            {"data()  <ranges>", "Cranges" },
            {"empty()  <ranges>", "Cranges" },
            {"end()  <ranges>", "Cranges" },
            {"rbegin()  <ranges>", "Cranges" },
            {"rend()  <ranges>", "Cranges" },
            {"size()  <ranges>", "Cranges" },
            {"ssize()  <ranges>", "Cranges" },
            {"<numbers>", "Dnumbers" },
            {"e  <numbers>", "Dnumbers" },
            {"log2e  <numbers>", "Dnumbers" },
            {"log10e  <numbers>", "Dnumbers" },
            {"pi  <numbers>", "Dnumbers" },
            {"inv_pi  <numbers>", "Dnumbers" },
            {"inv_sqrtpi  <numbers>", "Dnumbers" },
            {"ln2  <numbers>", "Dnumbers" },
            {"ln10  <numbers>", "Dnumbers" },
            {"sqrt2  <numbers>", "Dnumbers" },
            {"sqrt3  <numbers>", "Dnumbers" },
            {"inv_sqrt3  <numbers>", "Dnumbers" },
            {"egamma  <numbers>", "Dnumbers" },
            {"phi  <numbers>", "Dnumbers" },
            {"<source_location>", "Esource_location" },
            {"source_location �N���X", "Esource_location0" },
            {"source_location()  (source_location �N���X)", "Esource_location0" },
            {"operator=  (source_location �N���X)", "Esource_location0" },
            {"column()  (source_location �N���X)", "Esource_location0" },
            {"current()  (source_location �N���X)", "Esource_location0" },
            {"file_name()  (source_location �N���X)", "Esource_location0" },
            {"function_name()  (source_location �N���X)", "Esource_location0" },
            {"line()  (source_location �N���X)", "Esource_location0" },
            {"<concepts>", "Fconcepts" },
            {"assignable_from  <concepts>", "Fconcepts" },
            {"common_reference_with  <concepts>", "Fconcepts" },
            {"common_with  <concepts>", "Fconcepts" },
            {"constructible_from  <concepts>", "Fconcepts" },
            {"convertible_to  <concepts>", "Fconcepts" },
            {"copyable  <concepts>", "Fconcepts" },
            {"copy_constructible  <concepts>", "Fconcepts" },
            {"default_initializable  <concepts>", "Fconcepts" },
            {"derived_from  <concepts>", "Fconcepts" },
            {"destructible  <concepts>", "Fconcepts" },
            {"equality_comparable  <concepts>", "Fconcepts" },
            {"equality_comparable_with  <concepts>", "Fconcepts" },
            {"equivalence_relation  <concepts>", "Fconcepts" },
            {"floating_point  <concepts>", "Fconcepts" },
            {"integral  <concepts>", "Fconcepts" },
            {"invocable  <concepts>", "Fconcepts" },
            {"movable  <concepts>", "Fconcepts" },
            {"move_constructible  <concepts>", "Fconcepts" },
            {"predicate  <concepts>", "Fconcepts" },
            {"regular  <concepts>", "Fconcepts" },
            {"regular_invocable  <concepts>", "Fconcepts" },
            {"relation  <concepts>", "Fconcepts" },
            {"same_as  <concepts>", "Fconcepts" },
            {"semiregular  <concepts>", "Fconcepts" },
            {"signed_integral  <concepts>", "Fconcepts" },
            {"strict_weak_order  <concepts>", "Fconcepts" },
            {"swappable  <concepts>", "Fconcepts" },
            {"swappable_with  <concepts>", "Fconcepts" },
            {"totally_ordered  <concepts>", "Fconcepts" },
            {"totally_ordered_with  <concepts>", "Fconcepts" },
            {"unsigned_integral  <concepts>", "Fconcepts" },
            {"<typeinfo>", "Gtypeinfo" },
            {"bad_cast �N���X  <typeinfo>", "Gtypeinfo" },
            {"bad_typeid �N���X  <typeinfo>", "Gtypeinfo" },
            {"type_info �N���X", "Gtype_info0" },
            {"operator=  (type_info �N���X)", "Gtype_info0" },
            {"operator==  (type_info �N���X)", "Gtype_info0" },
            {"operator!=  (type_info �N���X)", "Gtype_info0" },
            {"before()  (type_info �N���X)", "Gtype_info0" },
            {"hash_code()  (type_info �N���X)", "Gtype_info0" },
            {"name()  (type_info �N���X)", "Gtype_info0" },
            {"<dwmapi.h>", "Hdwmapi" },
            {"DwmSetWindowAttribute()  <dwmapi.h>", "HDwmSetWindowAttribute0" },
            {"<print>", "IPrint" },
            {"print()  <print>", "IPrint" },
            {"println()  <print>", "IPrint" },
            {"vprint_nonunicode()  <print>", "IPrint" },
            {"vprint_unicode()  <print>", "IPrint" },
            {"<format>", "Jformat" },
            {"format()  <format>", "Jformat" },
            {"format_to()  <format>", "Jformat" },
            {"format_to_n()  <format>", "Jformat" },
            {"formatted_size()  <format>", "Jformat" },
            {"vformat()  <format>", "Jformat" },
            {"vformat_to()  <format>", "Jformat" },
            {"format_to_n_result  <format>", "Jformat" },
            {"formattable  <format>", "Jformat" },
            {"format_error  <format>", "Jformat" },
            {"<algorithm>", "Kalgorithm" },
            {"adjacent_find()  <algorithm>", "Kalgorithm" },
            {"all_of()  <algorithm>", "Kalgorithm" },
            {"any_of()  <algorithm>", "Kalgorithm" },
            {"count()  <algorithm>", "Kalgorithm" },
            {"count_if()  <algorithm>", "Kalgorithm" },
            {"binary_search()  <algorithm>", "Kalgorithm" },
            {"equal()  <algorithm>", "Kalgorithm" },
            {"find()  <algorithm>", "Kalgorithm" },
            {"find_end()  <algorithm>", "Kalgorithm" },
            {"find_first_of()  <algorithm>", "Kalgorithm" },
            {"find_if()  <algorithm>", "Kalgorithm" },
            {"find_if_not()  <algorithm>", "Kalgorithm" },
            {"for_each()  <algorithm>", "Kalgorithm" },
            {"for_each_n()  <algorithm>", "Kalgorithm" },
            {"generate()  <algorithm>", "Kalgorithm" },
            {"mismatch()  <algorithm>", "Kalgorithm" },
            {"none_of()  <algorithm>", "Kalgorithm" },
            {"search()  <algorithm>", "Kalgorithm" },
            {"search_n()  <algorithm>", "Kalgorithm" },
            {"for_each_result  <algorithm>", "Kalgorithm" },
        };
        private static SortedDictionary<string, string> UseCppSuggestDictionary { get; } = new SortedDictionary<string, string>();

        public MainPage()
        {
            this.InitializeComponent();
            Handle = this;

            UserAPI.ToCompactForResources(this, NavView, ContentFrame);

            if (App.UseCppInCSample) ChangeCHeaderName();
            CurrentUseCppSetting = App.UseCppInCSample;
        }

        public static void Initialize()
        {
            if (UseCppSuggestDictionary.Count != 0) return;

            foreach (var pair in SuggestDictionary)
            {
                if (pair.Value[0] == '0') UseCppSuggestDictionary[pair.Key.Replace("<", "<c").Replace(".h>", ">")] = pair.Value;
                else UseCppSuggestDictionary[pair.Key] = pair.Value;
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
                        MainWindow.Handle.ShowCSampleTeaching();
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
                    suitableItems.Add("���ʂ�������܂���ł���");
                }
                sender.ItemsSource = suitableItems;
            }

        }

        private void Suggest_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            var dictionary = App.UseCppInCSample ? UseCppSuggestDictionary : SuggestDictionary;
            ChosedItemsTag = dictionary[(string)args.SelectedItem];
            
        }

        public static async void ActivateWindow(Window window)
        {
            await Task.Delay(100);
            window.Activate();
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

                }
                SetHeader(folder, tag);
            }
            else
            {
                NavView.SelectedItem = GetNavigationViewItem(tag);
                SetHeader(tag);

            }

            int count = (int)this.Tag;

            foreach (var element in MainWindow.Handle.TabChildren)
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
                    item.ContextFlyout = UserAPI.GetMenuFlyout(item.Tag as string);
                    item.RightTapped += (s, e) =>
                    {
                        (s as NavigationViewItem).ContextFlyout.ShowAt(s as FrameworkElement);
                    };
                    foreach (var menuItem in item.MenuItems)
                    {
                        (menuItem as NavigationViewItem).ContextFlyout = UserAPI.GetMenuFlyout((menuItem as NavigationViewItem).Tag as string);
                        (menuItem as NavigationViewItem).ContextRequested += (s, e) =>
                        {
                            (s as NavigationViewItem).ContextFlyout.ShowAt(s as FrameworkElement);
                        };
                    }
                }
            }
            NavView_Navigate(FirstShowPageTag);
        }

        private void NavView_Navigate(string tag, bool left = false)
        {
            if (tag != openedst)
            {
                var targetType = PageDictionary.First(c => c.Value.Equals(tag)).Key;
                if (openedst == "�z�[��")
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

        private void Suggest_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            if (args.ChosenSuggestion != null)
            {
                NavView_Navigate(ChosedItemsTag, false);
            }
                
        }

        public async void UpdateSample()
        {
            if(CurrentUseCppSetting != App.UseCppInCSample)
            {
                CurrentUseCppSetting = App.UseCppInCSample;
                ChangeCHeaderName();
            }
            ContentDialog dialog = new ContentDialog
            {
                RequestedTheme = App.ResultTheme,
                XamlRoot = this.XamlRoot,
                Title = "�ēǂݍ��݂��K�v�ł�",
                Content = "\n�ݒ�̕ύX�𔽉f����ɂ͍��ڂ��ēǂݍ��݂���K�v������܂�",
                PrimaryButtonText = "�ēǂݍ���",
                SecondaryButtonText = "����",
                DefaultButton = ContentDialogButton.Primary,
            };

            var resylt = await dialog.ShowAsync();

            if(resylt == ContentDialogResult.Primary)
            {
                ContentFrame.Navigate(ContentFrame.SourcePageType);
            }

            
            _updated = true;
        }

        public static void RequestUpdate()
        {
            foreach (var element in MainWindow.Handle.Children)
            {
                if(element is MainPage mainPage)
                {
                    mainPage._updated = false;
                }
            }
        }
    }
}
