using CppGallery.Pages.UserControls;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CppGallery
{
    internal static class CoAPI
    {
        private const string DllName = @"../dll/CoAPI.dll";
        private static Dictionary<string, string> SpacialChars { get; } = new Dictionary<string, string>
        {
            { "^", "^^^^" },
            { "\"", "^\"" },
            { "&", "^^^&" },
            { "<", "^^^<" },
            { ">", "^^^>" },
            { "|", "^^^|" },
        };

        [DllImport(DllName, CharSet = CharSet.Unicode)]
        public static extern uint Execute(string args);

        [DllImport(DllName, CharSet = CharSet.Unicode)]
        private static extern IntPtr _getOutputFileName();

        [DllImport(DllName, CharSet = CharSet.Unicode)]
        private static extern IntPtr _getInputFileName();

        [DllImport(DllName, CharSet = CharSet.Unicode)]
        public static extern int SetFrameArea(IntPtr hWnd);

        public static string ParceSpecialChar(string str)
        {
            foreach (var pair in SpacialChars)
            {
                str.Replace(pair.Key, pair.Value);
            }

            return str;
        }

        public static string GetOutputFileName()
        {
            return Marshal.PtrToStringUni(_getOutputFileName());
        }

        public static string GetInputFileName()
        {
            return Marshal.PtrToStringUni(_getInputFileName());
        }

        public static async Task<uint> ExecuteAsync(string args)
        {
            return await Task.Run(() =>
            {
                return Execute(args);
            });
        }
    }
}
