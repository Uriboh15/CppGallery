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
            "DWORD",
            "DwmSetWindowAttributePtr",
            "HBRUSH",
            "HDC",
            "HINSTANCE",
            "HMODULE",
            "HRESULT",
            "HWND",
            
            "LPARAM",
            "LPCVOID",
            "LPWSTR",
            "LRESULT",
            "MainWindow",
            "MARGINS",
            "MSG",
            "NTSTATUS",
            "PAINTSTRUCT",
            "UINT",
            "WNDCLASSEX",
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
            "CALLBACK",
            "COLOR_WINDOW",
            "CreateWindowEx",
            "CW_USEDEFAULT",
            "DefWindowProc",
            "DispatchMessage",
            "DWMWA_COLOR_DEFAULT",
            "DWMWA_COLOR_NONE",
            "FALSE",
            "GetMessage",
            "LoadLibrary",
                
            "RegisterClassEx",
            "TRUE",
            "UNICODE",
            "WINAPI",
            "WM_DESTROY",
            "WM_PAINT",
            "WS_OVERLAPPEDWINDOW",
                "_In_",
                "_In_opt_",
                "_In_reads_bytes_",
        };
    }
}
