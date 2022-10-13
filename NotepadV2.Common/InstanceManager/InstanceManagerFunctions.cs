using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Windows.UI.Xaml;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace NotepadV2.Common.InstanceManager
{
    public class IMFuncs
    {
        public static List<string> AllInstancesNames = new List<string>();
        public static void GetAllInstances()
        {
            Process[] AllProcesses = Process.GetProcesses();

            foreach (var process in AllProcesses)
            {
                if (process.MainWindowTitle.Contains(Global.AppTitleDefault))
                {
                    AllInstancesNames.Add(process.MainWindowTitle);
                    MessageBox.Show(process.MainWindowTitle);
                }
                else if (!process.MainWindowTitle.Contains(Global.AppTitleDefault))
                {
                    continue;
                }
            }
        }

        public static void KillInstance(int InstancePID)
        {
            Process targetProcess = Process.GetProcessById(InstancePID);
            targetProcess.Kill();
        }

        public static int GetInstancePID(string instanceName)
        {
            Process[] targetProcess = Process.GetProcessesByName(instanceName);
            foreach (var process in targetProcess)
            {
                return process.Id;
            }
            return 0;
        }

        public static void CreateNewInstance()
        {
            string executable = System.Reflection.Assembly.GetEntryAssembly().Location;

            MessageBox.Show(executable);
        }
    }
}
