using NotepadV2.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotepadV2
{
    public class Version
    {
        public static double VersionNumber = 0.1;
        public static string BaseVersionString = VersionNumber.ToString();
        public static string GetVersionString()
        {
            if (Global.bGlobalDebug)
            {
                return BaseVersionString + "DebugRelease";
            }
            else if (!Global.bGlobalDebug)
            {
                return BaseVersionString;
            }
            return null;
        }
    }
}
