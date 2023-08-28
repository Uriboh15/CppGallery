using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CppGallery
{
    public static class ProcessHost
    {
        public static void Start(string path)
        {
            
            Process.Start(Data.DefaultSampleExePath + "SampleHost", Parse(path));
        }

        public static void Start(string[] args)
        {
            if(args.Length == 3)
            {
                Process.Start(Data.DefaultSampleExePath + "SampleHost", new string[] { Parse(args[0]), args[1], args[2] });
            }
        }

        public static void WinStart(string[] args)
        {
            if (args.Length == 3)
            {
                Process.Start(Data.DefaultSampleExePath + args[0], new string[] { args[1], args[2] });
            }
        }

        public static string Parse(string text)
        {
            string tmp = string.Empty;
            for (int i = 0; i < text.Length; ++i)
            {
                if (text[i] == '/')
                {
                    tmp += '\\';
                }
                else
                {
                    tmp += text[i];
                }
            }
            return tmp;
        }
    }
}
