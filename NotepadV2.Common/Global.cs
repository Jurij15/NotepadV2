﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotepadV2.Common
{
    public class Global
    {
        //settings
        public static string Theme = "dark"; //dark by default
        public static bool ShortcutsBarVisibility = true; //true by default
        public static bool ShowTimeInMenuBar = true; //true by default

        //not important DO NOT CHANGE
        public static string Error { get; set; }

        //global settings
        public static bool bGlobalDebug = true;
        public static bool bDebugSettingsMain = false;

        //some other things (will probably get changed on runtime)
        public static string DocumentFileTitle { get; set; }
        public static string AppTitle = "NotepadV2";
    }
}