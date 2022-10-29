using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotepadV2
{
    public class Version
    {
        public static double VersionNumber = 0.2;
        public static string BaseVersionString = VersionNumber.ToString();
        public static string VerString = "0.2";

        public static string GetVersionString()
        {
            if (NotepadV2.Common.Global.bGlobalDebug)
            {
                return BaseVersionString + "-Debug Release!";
            }
            else if (!NotepadV2.Common.Global.bGlobalDebug)
            {
                return BaseVersionString;
            }
            else
            {
                return null;    
            }
        }
    }
}
