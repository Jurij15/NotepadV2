using LogSharper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NotepadV2.Common
{
    public class Arguments
    {
        public static string[] GetCommandLineArgs()
        {
            return Environment.GetCommandLineArgs();
        }

        public static void ProcessCommandLineArgs(bool bLog)
        {
            foreach (var arg in GetCommandLineArgs())
            {
                if (arg.Contains("-debug"))
                {
                    Logger.Info("Detected -debug command line argument!");
                    //MessageBox.Show("Detected -debug command line argument!");
                    Global.bGlobalDebug = true;
                }
            }
        }
    }
}
