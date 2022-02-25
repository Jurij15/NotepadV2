using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CrashHandler
{
    public class ReadCrashDetails
    {
        public void ReadDetails()
        {
            string line;
            string filenamedatetime = DateTime.Now.ToString();
            string filename = "CrashDetails/crash" + filenamedatetime+".txt";
            try
            {
                StreamReader sr = new StreamReader(filename);
                line = sr.ReadLine();
                sr.Close();
            }
            catch (Exception e)
            {

                throw;
            }
        }
    }
}
