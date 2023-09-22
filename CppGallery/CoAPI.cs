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

        [DllImport(DllName, CharSet = CharSet.Unicode)]
        public static extern uint Execute(string args, uint wait);

        [DllImport(DllName, CharSet = CharSet.Unicode)]
        private static extern IntPtr _getOutputFileName();

        [DllImport(DllName, CharSet = CharSet.Unicode)]
        private static extern IntPtr _getInputFileName();

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
                return Execute(args, App.WaitFor);
            });
        }
    }
}
