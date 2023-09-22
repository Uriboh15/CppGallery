using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CppGallery
{
    public static class Func
    {

        [DllImport("../dll/Func.dll")]
        public static extern int RandTest();

        [DllImport("../dll/Func.dll")]
        public static extern void SrandTest(int radix);

        [DllImport("../dll/Func.dll")]
        public static extern void VectorClassCapacityTest(bool add);

        [DllImport("../dll/Func.dll")]
        public static extern void VectorClassClear();

        [DllImport("../dll/Func.dll")]
        public static extern uint VectorClassCapacityTestGetCapacity();

        [DllImport("../dll/Func.dll")]
        public static extern uint VectorClassCapacityTestGetSize();

        [DllImport("../dll/Func.dll")]
        public static extern IntPtr ReallocTest(int add);

        [DllImport("../dll/Func.dll")]
        public static extern uint ReallocTestGetSize();

        [DllImport("../dll/Func.dll")]
        public static extern void ReallocTestInitialize();

        [DllImport("../dll/Func.dll")]
        public static extern IntPtr InputDouble(string str);

        [DllImport("../dll/Func.dll")]
        public static extern IntPtr TmpnamTest();

        [DllImport("../dll/Func.dll")]
        public static extern int ClockTest();

        [DllImport("../dll/Func.dll")]
        public static extern IntPtr LocaltimeTest();

        [DllImport("../dll/Func.dll")]
        public static extern IntPtr TimeTest();

        [DllImport("../dll/Func.dll")]
        public static extern IntPtr CtimeTest();

        [DllImport("../dll/Func.dll")]
        public static extern IntPtr GmtimeTest();

        [DllImport("../dll/Func.dll")]
        public static extern IntPtr StrftimeTest();

        
    }

    public static class Wcharh
    {
        private const string DllName = @"../dll/Wcharh.dll";

        [DllImport(DllName)]
        private static extern IntPtr FgetwcTest_JP(string path);

        [DllImport(DllName)]
        private static extern IntPtr FgetwsTest_JP(string path);
        

        [DllImport(DllName, CharSet = CharSet.Unicode)]
        private static extern IntPtr WcscmpTest_JP(string str1, string str2);

        [DllImport(DllName)]
        private static extern IntPtr WcsftimeTest_JP();

        [DllImport(DllName, CharSet = CharSet.Unicode)]
        private static extern IntPtr WcslenTest_JP(string str);

        [DllImport(DllName, CharSet = CharSet.Unicode)]
        private static extern IntPtr WcsstrTest_JP(string str);

        [DllImport(DllName, CharSet = CharSet.Unicode)]
        private static extern IntPtr WcstodTest_JP(string str);

        [DllImport(DllName, CharSet = CharSet.Unicode)]
        private static extern IntPtr WcstofTest_JP(string str);

        [DllImport(DllName)]
        private static extern IntPtr WcstokTest_JP(string path);

        [DllImport(DllName, CharSet = CharSet.Unicode)]
        private static extern IntPtr WcstoldTest_JP(string str);

        [DllImport(DllName)]
        private static extern IntPtr WmemmoveTest_JP();

        [DllImport(DllName)]
        private static extern IntPtr WprintfTest_JP();


        public static string FgetwcTest(string path)
        {
            return Marshal.PtrToStringUni(FgetwcTest_JP(path));
        }

        public static string FgetwsTest(string path)
        {
            return Marshal.PtrToStringUni(FgetwsTest_JP(path));
        }

        public static string WcscmpTest(string str1, string str2)
        {
            return Marshal.PtrToStringUni(WcscmpTest_JP(str1, str2));
        }

        public static string WcsftimeTest()
        {
            return Marshal.PtrToStringUni(WcsftimeTest_JP());
        }

        public static string WcslenTest(string str)
        {
            return Marshal.PtrToStringUni(WcslenTest_JP(str));
        }

        public static string WcsstrTest(string str)
        {
            return Marshal.PtrToStringUni(WcsstrTest_JP(str));
        }

        public static string WcstodTest(string str)
        {
            return Marshal.PtrToStringUni(WcstodTest_JP(str));
        }

        public static string WcstofTest(string str)
        {
            return Marshal.PtrToStringUni(WcstofTest_JP(str));
        }

        public static string WcstokTest(string path)
        {
            return Marshal.PtrToStringUni(WcstokTest_JP(path));
        }

        public static string WcstoldTest(string str)
        {
            return Marshal.PtrToStringUni(WcstoldTest_JP(str));
        }

        public static string WmemmoveTest()
        {
            return Marshal.PtrToStringUni(WmemmoveTest_JP());
        }

        public static string WprintfTest()
        {
            return Marshal.PtrToStringUni(WprintfTest_JP());
        }
    }
}
