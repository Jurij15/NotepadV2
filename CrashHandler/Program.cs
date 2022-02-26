using System;
using System.Text;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace CrashHandler
{
    public class Program
    {
        static void Main (string[] args)
        {
            ReadCrashDetails readCrashDetails = new ReadCrashDetails();
            readCrashDetails.ReadDetails();
        }
    }
}
