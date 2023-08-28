﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CppGallery.Pages.UserControls
{
    public class CCodeButton : CLanCodeButton
    {
        private static List<string> KeywordsBlue { get; } = new List<string>
        {
            "char",
            "const",
            "dllexport",
            "double",
            "extern",
            "float",
            "int",
            "long",
            "short",
            "sizeof",
            "static",
            "struct",
            "typedef",
            "typeid",
            "union",
            "unsigned",
            "void",
            "__declspec",
        };

        private static List<string> KeywordsPurple { get; } = new List<string>
        {
            "break",
            "case",
            "continue",
            "default",
            "do",
            "else",
            "for",
            "if",
            "return",
            "switch",
            "try",
            "while",
        };

        private static List<string> KeywordsGreen { get; } = new List<string>
        {
            "char16_t",
            "char32_t",
            "clock_t",
            "div_t",
            "errno_t",
            "FILE",
            "fpos_t",
            "imaxdiv_t",
            "int32_t",
            "int64_t",
            "intmax_t",
            "ldiv_t",
            "lldiv_t",
            "size_t",
            "time_t",
            "tm",
            "uintmax_t",
            "va_list",
            "valarray",
            "wchar_t",
            "wint_t",
        };

        private static List<string> KeywordsDefine { get; } = new List<string>
        {
            "assert",
            "bool",
            "EOF",
            "errno",
            "EXIT_FAILURE",
            "false",
            "FE_TONEAREST",
            "FE_UPWARD",
            "FE_DOWNWARD",
            "FE_TOWARDZERO",
            "FILENAME_MAX",
            "LC_ALL",
            "MB_CUR_MAX",
            "MB_LEN_MAX",
            "M_PI",
            "NULL",
            "SEEK_CUR",
            "SEEK_END",
            "SEEK_SET",
            "stderr",
            "stdin",
            "stdout",
            "true",
            "va_end",
            "va_start",
            "WEOF",
            "ULLONG_MAX",
            "_IOFBF",
            "_IOLBF",
            "_IONBF",
            
        };

        private static List<string> KeywordsFunc { get; } = new List<string>
        {
            "abs",
            "acos",
            "acosf",
            "acosh",
            "acoshf",
            "acoshl",
            "acosl",
            "arg",
            "asctime",
            "asin",
            "asinf",
            "asinh",
            "asinhf",
            "asinhl",
            "asinl",
            "atan",
            "atanf",
            "atanh",
            "atanhf",
            "atanhl",
            "atanl",
            "atan2",
            "atan2f",
            "atan2l",
            "atexit",
            "atof",
            "atoi",
            "atol",
            "atoll",
            "begin",
            "bit_ceil",
            "bit_floor",
            "bit_width",
            "btowc",
            "c16rtomb",
            "c32rtomb",
            "calloc",
            "cbrt",
            "cbrtf",
            "cbrtl",
            "ceil",
            "ceilf",
            "ceill",
            "clearerr",
            "clock",
            "conj",
            "copysign",
            "copysignf",
            "copysignl",
            "cos",
            "cosf",
            "cosh",
            "coshf",
            "coshl",
            "cosl",
            "count",
            "countl_one",
            "countl_zero",
            "countr_one",
            "countr_zero",
            "cprintf",
            "cputs",
            "ctime",
            "difftime",
            "div",
            "end",
            "erf",
            "erfc",
            "erfcf",
            "erfcl",
            "erff",
            "erfl",
            "exit",
            "exp",
            "exp2",
            "exp2f",
            "exp2l",
            "expf",
            "expl",
            "expm1",
            "expm1f",
            "expm1l",
            "fabs",
            "fabsf",
            "fabsl",
            "fclose",
            "fdim",
            "fdimf",
            "fdiml",
            "feof",
            "ferror",
            "fflush",
            "fgetc",
            "fgetpos",
            "fgets",
            "fgetwc",
            "fgetws",
            "fill",
            "floor",
            "floorf",
            "floorl",
            "flush",
            "fma",
            "fmaf",
            "fmal",
            "fmax",
            "fmaxf",
            "fmaxl",
            "fmin",
            "fminf",
            "fminl",
            "fmod",
            "fmodf",
            "fmodl",
            "fopen",
            "fopen_s",
            "fpclassify",
            "fprintf",
            "fputc",
            "fputs",
            "fread",
            "free",
            "freopen",
            "frexp",
            "frexpf",
            "frexpl",
            "front",
            "fscanf",
            "fseek",
            "fsetpos",
            "ftell",
            "fwide",
            "fwprintf",
            "fwscanf",
            "fwrite",
            "has_single_bit",
            "hypot",
            "hypotf",
            "hypotl",
            "getline",
            "get_money",
            "get_time",
            "getc",
            "getch",
            "getchar",
            "getche",
            "getenv",
            "getloc",
            "getwc",
            "getwchar",
            "gmtime",
            "good",
            "ilogb",
            "ilogbf",
            "ilogbl",
            "imag",
            "imbue",
            "init",
            "isalnum",
            "isalpha",
            "isblank",
            "iscntrl",
            "isdigit",
            "isfinite",
            "isgraph",
            "isgreater",
            "isgreaterequal",
            "isinf",
            "isless",
            "islessequal",
            "islessgreater",
            "islower",
            "isnan",
            "isnormal",
            "isprint",
            "ispunct",
            "isspace",
            "isundered",
            "isupper",
            "iswalnum",
            "iswalpha",
            "iswblank",
            "iswcntrl",
            "iswdigit",
            "iswgraph",
            "iswlower",
            "iswprint",
            "iswpunct",
            "iswspace",
            "iswupper",
            "iswxdigit",
            "iswctype",
            "isxdigit",
            "iword",
            "kbhit",
            "labs",
            "ldexp",
            "ldexpf",
            "ldexpl",
            "ldiv",
            "lgamma",
            "lgammaf",
            "lgammal",
            "llabs",
            "lldiv",
            "llrint",
            "llrintf",
            "llrintl",
            "llround",
            "llroundf",
            "llroundl",
            "lnit",
            "localtime",
            "localtime_s",
            "log",
            "log10",
            "log10f",
            "log10l",
            "log1p",
            "log1pf",
            "log1pl",
            "log2",
            "log2f",
            "log2l",
            "logb",
            "logbf",
            "logbl",
            "logf",
            "logl",
            "lrint",
            "lrintf",
            "lrintl",
            "lround",
            "lroundf",
            "lroundl",
            "main",
            "malloc",
            "mblen",
            "mbrlen",
            "mbrtoc16",
            "mbrtoc32",
            "mbrtowc",
            "mbsinit",
            "mbstowcs",
            "mbtowc",
            "memchr",
            "memcmp",
            "memcpy",
            "memmove",
            "memset",
            "mktime",
            "modf",
            "modff",
            "modfl",
            "move",
            "nan",
            "nanf",
            "nanl",
            "nearbyint",
            "nearbyintf",
            "nearbyintl",
            "nextafter",
            "nextafterf",
            "nextafterl",
            "nexttoward",
            "nexttowardf",
            "nexttowardl",
            "norm",
            "overflow",
            "parsesize",
            "pbackfail",
            "perror",
            "polar",
            "popcount",
            "pow",
            "powf",
            "powl",
            "printf",
            "proj",
            "push_back",
            "push_front",
            "put",
            "put_money",
            "put_time",
            "putc",
            "putch",
            "putchar",
            "puts",
            "putwchar",
            "quoted",
            "rand",
            "real",
            "realloc",
            "remainder",
            "remainderf",
            "remainderl",
            "remove",
            "remquo",
            "remquof",
            "remquol",
            "rename",
            "resetiosflags",
            "rewind",
            "rint",
            "rintf",
            "rintl",
            "round",
            "roundf",
            "roundl",
            "scalbln",
            "scalblnf",
            "scalblnl",
            "scalbn",
            "scalbnf",
            "scalbnl",
            "scanf",
            "seekoff",
            "set_rdbuf",
            "setbase",
            "setbuf",
            "setfill",
            "setiosflags",
            "setlocale",
            "setprecision",
            "setstate",
            "setw",
            "setvbuf",
            "signbit",
            "sinf",
            "sinh",
            "sinhf",
            "sinhl",
            "sinl",
            "Sleep",
            "snprintf",
            "sprintf",
            "sqrt",
            "sqrtf",
            "sqrtl",
            "srand",
            "sscanf",
            "stod",
            "stof",
            "stoi",
            "stold",
            "stoll",
            "stoul",
            "stoull",
            "strcat",
            "strchr",
            "strcmp",
            "strcoll",
            "strcpy",
            "strcspn",
            "strerror",
            "strftime",
            "strlen",
            "strerror",
            "strncat",
            "strncmp",
            "strncpy",
            "strpbrk",
            "strrchr",
            "strspn",
            "strstr",
            "strtod",
            "strtof",
            "strtok",
            "strtol",
            "strtold",
            "strtoll",
            "strtoul",
            "strtoull",
            "strxfrm",
            "swap",
            "swprintf",
            "swscanf",
            "system",
            "tan",
            "tanf",
            "tanh",
            "tanhf",
            "tanhl",
            "tanl",
            "tgamma",
            "tgammaf",
            "tgammal",
            "tie",
            "time",
            "tmpfile",
            "tmpnam",
            "to_string",
            "to_wstring",
            "tolower",
            "toupper",
            "towcrans",
            "towlower",
            "towupper",
            "trunc",
            "truncf",
            "truncl",
            "uflow",
            "underflow",
            "ungetc",
            "ungetwc",
            "vfprintf",
            "vfscanf",
            "vfwprintf",
            "vfwscanf",
            "vprintf",
            "vscanf",
            "vsnprintf",
            "vsprintf",
            "vsscanf",
            "vswprintf",
            "vswscanf",
            "vwprintf",
            "vwscanf",
            "wcrtomb",
            "wcscat",
            "wcschr",
            "wcscmp",
            "wcscoll",
            "wcscpy",
            "wcsftime",
            "wcslen",
            "wcsncat",
            "wcsncmp",
            "wcsncpy",
            "wcsspn",
            "wcsstr",
            "wcstod",
            "wcstof",
            "wcstok",
            "wcstol",
            "wcstold",
            "wcstoll",
            "wcstombs",
            "wcstoul",
            "wcstoull",
            "wcsxfrm",
            "wctomb",
            "wctrans",
            "wctype",
            "widen",
            "width",
            "wmemchr",
            "wmemcmp",
            "wmemcpy",
            "wmemmove",
            "wmemset",
            "wprintf",
            "ws",
            "wscanf",
            "_cprintf",
        };

        private static List<string> KeywordsUserFunction { get; } = new List<string>
        {

            "create_array2",
            "free_array2",
            "func",
            "get_int",
            "get_s",
            "on_closed",
            "print_value",
            "replace",
            "resize_array2",
        };

        public CCodeButton() : base()
        {
            Lan = "C";
            AddList();
        }

        private void AddList()
        {
            KeyBlue.Add(KeywordsBlue);
            KeyPurple.Add(KeywordsPurple);
            KeyGreen.Add(KeywordsGreen);
            KeyDefine.Add(KeywordsDefine);
            KeyFunc.Add(KeywordsFunc);
            KeyUserFunction.Add(KeywordsUserFunction);
        }
    }
}
