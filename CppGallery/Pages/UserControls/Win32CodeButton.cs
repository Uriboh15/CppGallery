using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CppGallery.Pages.UserControls
{
    public class Win32CodeButton : CodeButton
    {
        public Win32CodeButton() : base()
        {
            KeyGreen.Add(KeywordsGreen);
            KeyYellow.Add(KeywordsYellow);
            KeyDefine.Add(KeywordsDefine);
            KeyEnum.Add(KeywordsEnum);
            KeyFunctionMacro.Add(KeywordsFunctionMacro);
        }

        private static List<string> KeywordsGreen { get; } = new List<string>()
        {
            "BOOL",
            "COLORREF",
            "CONSOLE_FONT_INFO",
            "CONSOLE_FONT_INFOEX",
            "COORD",
            "DWORD",
            "HANDLE",
            "HBRUSH",
            "HDC",
            "HINSTANCE",
            "HMENU",
            "HMODULE",
            "HRESULT",
            "HWND",
            
            "LPARAM",
            "LPCVOID",
            "LPCWSTR",
            "LPDWORD",
            "LRESULT",
            "LPRECT",
            "LPTSTR",
            "LPVOID",
            "LPWCH",
            "LPWSTR",
            
            "MainWindow",
            "MARGINS",
            "MSG",
            "NTSTATUS",
            "PAINTSTRUCT",
            "PCONSOLE_FONT_INFO",
            "PCONSOLE_FONT_INFOEX",
            "RECT",
            "TCHAR",
            "UINT",
            "UINT_PTR",
            "WCHAR",
            "WNDCLASSEX",
            "WORD",
            "WPARAM",
        };

        private static List<string> KeywordsEnum { get; } = new List<string>()
        {
            "DWMWINDOWATTRIBUTE",
            "DWM_WINDOW_CORNER_PREFERENCE",
            "DWM_SYSTEMBACKDROP_TYPE",
        };

        private static List<string> KeywordsYellow { get; } = new List<string>()
        {
            "WindowProc",
        };

        private static List<string> KeywordsDefine { get; } = new List<string>()
        {
            "AppendMenu",
            "BACKGROUND_BLUE",
            "BACKGROUND_GREEN",
            "BACKGROUND_RED",
            "BACKGROUND_INTENSITY",
            "CALLBACK",
            "CharLower",
            "CharLowerBuff",
            "CharNext",
            "CharPrev",
            "CharUpper",
            "COLOR_WINDOW",
            "CONSOLE_FULLSCREEN",
            "CONSOLE_FULLSCREEN_HARDWARE",
            "CreateWindowEx",
            "CTRL_C_EVENT",
            "CW_USEDEFAULT",
            "DefWindowProc",
            "DeleteFile",
            "DispatchMessage",
            "DWMWA_COLOR_DEFAULT",
            "DWMWA_COLOR_NONE",
            "FALSE",
            "FillConsoleOutputCharacter",
            "FOREGROUND_BLUE",
            "FOREGROUND_GREEN",
            "FOREGROUND_RED",
            "FOREGROUND_INTENSITY",
            "FreeEnvironmentStrings",
            "GetClassName",
            "GetCommandLine",
            "GetConsoleAliases",
            "GetConsoleAliasesLength",
            "GetConsoleOriginalTitle",
            "GetConsoleTitle",
            "GetCurrentDirectory",
            "GetEnvironmentStrings",
            "GetEnvironmentVariable",
            "GetMessage",
            "GetWindowText",
            "IsCharAlpha",
            "IsCharAlphaNumeric",
            "IsCharLower",
            "IsCharUpper",
            "LoadLibrary",
            "MB_ICONERROR",
            "MB_ICONINFORMATION",
            "MB_ICONQUESTION",
            "MB_ICONWARNING",
            "MB_OK",
            "MessageBox",
            "MF_STRING",
            "ReadConsole",
            "RegisterClassEx",
            "SearchPath",
            "SetConsoleTitle",
            "SetCurrentDirectory",
            "SetEnvironmentVariable",
            "SetWindowText",
            "STD_ERROR_HANDLE",
            "STD_INPUT_HANDLE",
            "STD_OUTPUT_HANDLE",
            "TMPF_DEVICE",
            "TMPF_FIXED_PITCH",
            "TMPF_TRUETYPE",
            "TMPF_VECTOR",
            "TRUE",
            "UNICODE",
            "WINAPI",
            "WM_DESTROY",
            "WM_PAINT",
            "WriteConsole",
            "WS_OVERLAPPEDWINDOW",
            "_In_",
            "_In_opt_",
            "_In_reads_bytes_",
        };

        private static List<string> KeywordsFunctionMacro { get; } = new List<string>
        {
            "max",
            "min",
        };
    }
}
