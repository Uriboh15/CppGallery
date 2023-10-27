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
            "bad_array_new_length",
            "bad_alloc",
            "bad_any_cast",
            "bad_cast",
            "bad_typeid",
            "complex",
            "const_iterator",
            "const_reference",
            "difference_type",
            "directory_entry",
            "directory_iterator",
            "errc",
            "error_code",
            "exception",
            "false_type",
            "file_status",
            "filesystem_error",
            
            "format_error",
            "from_chars_result",
            "fstream",
            "greater",
            "ifstream",
            "input_iterator",
            "integral_constant",
            "invalid_argument",
            "ios",
            "ios_base",
            "istringstream",
            "istream",
            "iterator",
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
            "partial_ordering",
            "path",
            "reference",
            "rep",
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
            "strong_ordering",
            "system_clock",
            "to_chars_result",
            "Traits",
            "true_type",
            "Type",
            "type_index",
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
            "back_insert_iterator",
            "basic_istream",
            "basic_ostream",
            "basic_string",
            "bitset",
            "char_traits",
            "const_iterator_t",
            "const_reverse_iterator",
            "const_sentinel_t",
            "deque",
            "format_string",
            "format_to_n_result",
            "forward_list",
            "front_insert_iterator",
            "hash",
            "initializer_list",
            "insert_iterator",
            "is_arithmetic",
            "is_array",
            "is_class",
            "is_compound",
            "is_const",
            "is_enum",
            "is_floating_point",
            "is_fundamental",
            "is_integral",
            "is_literal_type",
            "is_lvalue_reference",
            "is_null_pointer",
            "is_object",
            "is_pointer",
            "is_reference",
            "is_rvalue_reference",
            "is_scalar",
            "is_trivial",
            "is_trivially_copyable",
            "is_union",
            "is_void",
            "iter_common_reference_t",
            "iter_const_reference_t",
            "iter_difference_t",
            "iter_reference_t",
            "iter_rvalue_reference_t",
            "iter_value_t",
            "iterator_t",
            "iterator_traits",
            "list",
            "map",
            "multimap",
            "multiset",
            "numeric_limits",
            "pair",
            "projected",
            "queue",
            "range_const_reference_t",
            "range_difference_t",
            "range_reference_t",
            "range_rvalue_reference_t",
            "range_size_t",
            "range_value_t",
            "sentinel_t",
            "stack",
            "valarray",
            "vector",
            "wformat_string",
        };

        private static List<string> KeywordsConcept { get; } = new List<string>()
        {
            "assignable_from",
            "bidirectional_iterator",
            "bidirectional_range",
            "borrowed_range",
            "common_reference_with",
            "common_with",
            "constructible_from",
            "contiguous_iterator",
            "contiguous_range",
            "convertible_to",
            "copyable",
            "copy_constructible",
            "default_initializable",
            "derived_from",
            "destructible",
            "equality_comparable",
            "equality_comparable_with",
            "floating_point",
            "formattable",
            "forward_iterator",
            "forward_range",
            "indirect_binary_predicate",
            "indirect_unary_predicate",
            "indirectly_comparable",
            "indirectly_copyable",
            "indirectly_copyable_storable",
            "indirectly_movable",
            "indirectly_movable_storable",
            "indirectly_readable",
            "indirectly_swappable",
            "indirectly_writable",
            "input_iterator",
            "input_or_output_iterator",
            "input_range",
            "integral",
            "movable",
            "move_constructible",
            "output_iterator",
            "output_range",
            "permutable",
            "random_access_iterator",
            "random_access_range",
            "range",
            "regular",
            "same_as",
            "semiregular",
            "sentinel_for",
            "signed_integral",
            "sized_range",
            "swappable",
            "swappable_with",
            "three_way_comparable",
            "three_way_comparable_with",
            "totally_ordered",
            "totally_ordered_with",
            "unsigned_integral",
            "view",
            "weakly_incrementable",
        };

        private static List<string> KeywordsGlobal { get; } = new List<string>
        {
            "chrono",
            "filesystem",
            "std",
            "numbers",
            "ranges",
            "this_thread",
            "nodiscard",
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


        public CodeButton()
        {
            Lan = "C++";
            AddList();
        }

        private void AddList()
        {
            KeyBlue.Add(KeywordsBlue);
            KeyPurple.Add(KeywordsPurple);
            KeyGreen.Add(KeywordsGreen);
            KeyYellow.Add(KeywordsYellow);
            KeyGlobal.Add(KeywordsGlobal);
            KeyDefine.Add(KeywordsDefine);
            KeyClassTemplate.Add(KeywordClassTemplate);
            KeyEnum.Add(KeywordsEnum);
            KeyConcept.Add(KeywordsConcept);
            KeyFunc.Add(KeywordsFunction);

            //<cstdbool>のマクロを削除
            DeletedMacro.Add("bool");
            DeletedMacro.Add("true");
            DeletedMacro.Add("false");
        }
    }
}
