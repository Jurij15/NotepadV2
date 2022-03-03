using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;

namespace CrashHandler
{
    public class ReadCrashDetails
    {
        public string line { get; set; }
        public void ReadDetails()
        {
            
            string filenamedatetime = DateTime.Now.ToString();
            string filename = "CrashDetails/crash.txt";
            try
            {
                StreamReader sr = new StreamReader(filename);
                line = sr.ReadLine();
                sr.Close();
            }
            catch (Exception e)
            {
                
            }
            if (line.Contains("404"))
            {
                Console.WriteLine("Error found!");
                Console.ReadLine();
            }
            else 
            {
                Console.WriteLine(line);
            }
        }
    }
}
