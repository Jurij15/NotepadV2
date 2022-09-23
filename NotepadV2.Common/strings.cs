﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotepadV2.Common
{
    public static class strings
    {
        public static string LocalAppData = Environment.GetEnvironmentVariable("LocalAppData");

        public static string RootAppDataDir = LocalAppData + "/NotepadV2";

        public static string AppDataSavesFile = RootAppDataDir + "/Settings.config";
    }
}
