using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using NotepadV2_by_Jurij15.Windows;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Interop;

namespace NotepadV2_by_Jurij15.Crash
{
    public class ToCrashHandler
    {
        [DllImport("Kernel32")]
        public static extern void AllocConsole();

        [DllImport("Kernel32")]
        public static extern void FreeConsole();
        public void SendToCrashHandler()
        {
            //just call all the things needed
            CreateDirectory();
            //CreateCrashFile();
        }
        public void CreateDirectory()
        {
            //create a folder with crash details
            string dir = "CrashDetails";
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
                //CreateCrashFile();
            }
        }
        public void CreateCrashFile()
        {
            string filename = "CrashDetails/crash.txt";
            //if file exists from previous crashes, delete it
            if (File.Exists(filename))
            {
                File.Delete(filename);
            }
            //create a new file
            using (FileStream fs = File.Create(filename))
            {
                //string text = main.CrashDetails.ToString();
                CrashDetails crashDetails = new CrashDetails();
                string text = crashDetails.CrashDetailsString;
                Byte[] title = new UTF8Encoding(true).GetBytes(text);
                fs.Write(title, 0, title.Length);
                Console.WriteLine(text);
            }
        }
        //this didn't work out as planned, i will only catch errors that I make myself because this is too hard for me to understand why it doesn't work
        /*
        public void CreateCrashFile() 
        {
            string filenamedatetime = DateTime.Now.ToString();
            //string filename = "CrashDetails/crash" + filenamedatetime + ".txt";
            string filename = "CrashDetails/crash.txt";

            MainWindow main = new MainWindow();

            //if file exists from previous crashes, delete it
            if (File.Exists(filename))
            {
                File.Delete(filename);
            }
            //create a new file
            using (FileStream fs = File.Create(filename))
            {
                string text = main.CrashDetails.ToString();
                Byte[] title = new UTF8Encoding(true).GetBytes(text);
                fs.Write(title, 0, title.Length);
                AllocConsole();
                Console.WriteLine(text);
            }           
        }
        */
    }
}
