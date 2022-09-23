using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotepadV2.Common
{
    public class Global
    {
        public static string Theme = "dark"; //dark by default
        public static bool ShortcutsBarVisibility = true; //true by default
        public static bool ShowTimeInMenuBar = true; //true by default

        public static string Error { get; set; }
    }
}
