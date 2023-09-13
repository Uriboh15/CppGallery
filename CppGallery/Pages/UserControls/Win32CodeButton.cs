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
        }

        private static List<string> KeywordsGreen { get; } = new List<string>()
        {
            "BOOL",
            "COLORREF",
            "CONSOLE_FONT_INFO",
            "COORD",
            "DWORD",
            "HANDLE",
            "HBRUSH",
            "HDC",
            "HINSTANCE",
            "HMODULE",
            "HRESULT",
            "HWND",
            
            "LPARAM",
            "LPCVOID",
            "LPWCH",
            "LPWSTR",
            "LRESULT",
            "MainWindow",
            "MARGINS",
            "MSG",
            "NTSTATUS",
            "PAINTSTRUCT",
            "RECT",
            "UINT",
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
            "BACKGROUND_BLUE",
            "BACKGROUND_GREEN",
            "BACKGROUND_RED",
            "BACKGROUND_INTENSITY",
            "CALLBACK",
            "CharLower",
            "CharNext",
            "CharPrev",
            "CharUpper",
            "COLOR_WINDOW",
            "CreateWindowEx",
            "CW_USEDEFAULT",
            "DefWindowProc",
            "DispatchMessage",
            "DWMWA_COLOR_DEFAULT",
            "DWMWA_COLOR_NONE",
            "FALSE",
            "FOREGROUND_BLUE",
            "FOREGROUND_GREEN",
            "FOREGROUND_RED",
            "FOREGROUND_INTENSITY",
            "FreeEnvironmentStrings",
            "GetClassName",
            "GetCommandLine",
            "GetConsoleOriginalTitle",
            "GetConsoleTitle",
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
            "ReadConsole",
            "RegisterClassEx",
            "SearchPath",
            "SetConsoleTitle",
            "SetEnvironmentVariable",
            "SetWindowText",
            "STD_ERROR_HANDLE",
            "STD_INPUT_HANDLE",
            "STD_OUTPUT_HANDLE",
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
    }
}
