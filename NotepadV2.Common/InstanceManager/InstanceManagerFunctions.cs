using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Windows.UI.Xaml;

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
                if (process.ProcessName.Contains(Global.AppTitleDefault))
                {
                    AllInstancesNames.Add(process.ProcessName);
                }
                else if (!process.ProcessName.Contains(Global.AppTitleDefault))
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
    }
}
