using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using LogSharper;

namespace NotepadV2.Common
{
    public static class Util
    {
        [DllImport("kernel32.dll", EntryPoint = "AllocConsole", SetLastError = true, CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern int AllocConsole();

        public static void SetupConsole()
        {
            AllocConsole();
        }

        public static void SetupLogger()
        {
            LoggerSettings.UseColorLoggingSettingChange(true, false, 0);
            LoggerSettings.UseTimeDateOnLoggingSettingChange(false, false, 0);
            LogSharper.LogSharper.Setup(true);
        }
    }
}
