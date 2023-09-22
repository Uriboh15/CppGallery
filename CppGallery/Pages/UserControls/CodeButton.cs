using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CppGallery.Pages.UserControls
{
    public class CodeButton : CCodeButton
    {
        private static List<string> KeywordsBlue { get; } = new List<string>
        {
            "auto",
            "namespace",
            "bool",
            "char8_t",
            "char16_t",
            "char32_t",
            "class",
            "concept",
            "constexpr",
            "decltype",
            "delete",
            "dynamic_cast",
            "enum",
            "explicit",
            "false",
            "friend",
            "new",
            "noexcept",
            "nullptr",
            "operator",
            "override",
            "private",
            "protected",
            "public",
            "requires",
            "static_cast",
            "template",
            "this",
            "true",
            "typeid",
            "typename",
            "using",
            "virtual",
            "wchar_t",
        };

        private static List<string> KeywordsPurple { get; } = new List<string>
        {
            "catch",
            "throw",
            "try",
        };

        private static List<string> KeywordsGreen { get; } = new List<string>
        {
            "Allocator",
            "any",
            "Args",
            "assignable_from",
            "bad_array_new_length",
            "bad_alloc",
            "bad_any_cast",
            "bad_cast",
            "bad_typeid",
            "Base",
            "common_reference_with",
            "common_with",
            "complex",
            "constructible_from",
            "const_iterator",
            "const_reference",
            "const_reverse_iterator",
            "convertible_to",
            "copyable",
            "copy_constructible",
            "default_initializable",
            "difference_type",
            "Derived",
            "destructible",
            "directory_entry",
            "equality_comparable",
            "equality_comparable_with",
            "filesystem_error",
            "deque",
            "derived_from",
            "directory_iterator",
            "E",
            "errc",
            "error_code",
            "exception",
            "false_type",
            "file_status",
            
            "floating_point",
            "format_error",
            "from_chars_result",
            "fstream",
            "greater",
            "ifstream",
            "input_iterator",
            "integral",
            "integral_constant",
            "invalid_argument",
            "ios",
            "ios_base",
            "istringstream",
            "istream",
            "iterator",
            "Key",
            "locale",
            "minstd_rand",
            
            "mt19937",
            "mt19937_64",
            "multiplies",
            "nullptr_t",
            "ofstream",
            "ostream",
            "ostringstream",
            "ostrstream",
            "out_of_range",
            "path",
            "reference",
            "result_type",
            "reverse_iterator",
            "set",
            "size_type",
            "source_location",
            "span",
            "space_info",
            "streamsize",
            "string",
            "stringbuf",
            "stringstream",
            "string_type",
            "string_view",
            "totally_ordered",
            "totally_ordered_with",
            "to_chars_result",
            "Traits",
            "true_type",
            "Type",
            "type_info",
            "wifstream",
            "wofstream",
            "wostringstream",
            "wstring",
            "wstring_view",
        };

        private static List<string> KeywordsEnum { get; } = new List<string>()
        {
            "byte",
            "chars_format",
            "file_type",
        };

        private static List<string> KeywordClassTemplate { get; } = new List<string>()
        {
            "array",
            "basic_string",
            "bitset",
            "char_traits",
            "formattable",
            "format_string",
            "format_to_n_result",
            "forward_list",
            "hash",
            "initializer_list",
            "is_arithmetic",
            "is_array",
            "is_class",
            "is_compound",
            "is_const",
            "is_enum",
            "is_floating_point",
            "is_fundamental",
            "is_integral",
            "is_null_pointer",
            "is_object",
            "is_pointer",
            "is_reference",
            "is_scalar",
            "is_trivial",
            "is_trivially_copyable",
            "is_union",
            "is_void",
            "list",
            "map",
            "movable",
            "move_constructible",
            "multimap",
            "multiset",
            "numeric_limits",
            "pair",
            "queue",
            "regular",
            "same_as",
            "semiregular",
            "signed_integral",
            "stack",
            "swappable",
            "swappable_with",
            "unsigned_integral",
            "valarray",
            "vector",
            "wformat_string",
        };

        private static List<string> KeywordsUserFunction { get; } = new List<string>
        {
            "GetCppString",
            "GetRand",
            "Print",
            "print",
            "split",
        };

        private static List<string> KeywordsGlobal { get; } = new List<string>
        {
            "filesystem",
            "std",
            "numbers",
            "ranges",
            "this_thread",
        };

        private static List<string> KeywordsStatic { get; } = new List<string>
        {
#if false
            "binary",
            "end",
            "eofbit",
            "first",
            "in",
            "out",
            "second",
            "trunc",
#endif
        };

        private static List<string> KeywordsYellow { get; } = new List<string>
        {
            "any_cast",
            "bit_cast",
            "boolalpha",
            "dec",
            "defaultfloat",
            "endl",
            "ends",
            "fixed",
            "flush",
            "get",
            "hex",
            "hexfloat",
            "internal",
            "noboolalpha",
            "noshowbase",
            "noshowpoint",
            "noshowpos",
            "noskipws",
            "nounitbuf",
            "nouppercase",
            "oct",
            "scientific",
            "showbase",
            "showpoint",
            "showpos",
            "skipws",
            "unitbuf",
            "uppercase",
        };

        private static List<string> KeywordsFunction { get; } = new List<string>()
        {
            "get_new_handler",
            "set_new_handler",
            "left",
            "right",
        };

        private static List<string> KeywordsDefine { get; } = new List<string>()
        {
            "__cplusplus",
        };


        public CodeButton() : base()
        {
            Lan = "C++";
            AddList();
        }

        private void AddList()
        {
            KeyBlue.Add(KeywordsBlue);
            KeyPurple.Add(KeywordsPurple);
            KeyGreen.Add(KeywordsGreen);
            KeyUserFunction.Add(KeywordsUserFunction);
            KeyYellow.Add(KeywordsYellow);
            KeyGlobal.Add(KeywordsGlobal);
            KeyStatic.Add(KeywordsStatic);
            KeyDefine.Add(KeywordsDefine);
            KeyClassTemplate.Add(KeywordClassTemplate);
            KeyEnum.Add(KeywordsEnum);
            KeyFunc.Add(KeywordsFunction);
        }
    }
}
