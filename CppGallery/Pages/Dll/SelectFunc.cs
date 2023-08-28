using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using WinRT.Interop;

namespace CppGallery
{
    public static class SelectFunc
    {
        public static string PtrToStringAnsi(Func<nint> func1, Func<nint> func2)
        {
            switch (App.Compiler)
            {
                case Compiler.GCC:
                    return Marshal.PtrToStringAnsi(func1());

                case Compiler.VC:
                    return Marshal.PtrToStringAnsi(func2());

                default:
                    return "Error";
            }
        }

        public static string PtrToStringAnsi(Func<string, nint> func1, Func<string, nint> func2, string str)
        {
            switch (App.Compiler)
            {
                case Compiler.GCC:
                    return Marshal.PtrToStringAnsi(func1(str));

                case Compiler.VC:
                    return Marshal.PtrToStringAnsi(func2(str));

                default:
                    return "Error";
            }
        }
    }
}
