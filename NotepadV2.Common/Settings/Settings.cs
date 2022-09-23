using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotepadV2.Common.Settings
{
    public class Settings
    {
        public static bool CreateConfigFile()
        {
            return false;
        }/// <summary>
         /// Creates the config file
         /// </summary>
         /// <returns>OperationSuccessfull</returns>

        public static bool ReadConfigFile()
        {
            return false;
        }

        public static bool CheckConfingFileExisting()
        {
            if (File.Exists(strings.AppDataSavesFile))
            {
                return true;
            }
            else if (!File.Exists(strings.AppDataSavesFile))
            {
                return false;
            }
            return false;
        }

        public static bool AssingDefaultValuesToSetting()
        {
            try
            {
                if (File.Exists(strings.AppDataSavesFile))
                {
                    using (StreamWriter sw = new StreamWriter(strings.AppDataSavesFile))
                    {
                        sw.WriteLine("Theme==Dark");
                        sw.WriteLine("ShortcutsBarVisibility==true");
                        sw.WriteLine("ShowTimeInMenuBar==true");
                        sw.Close();
                        return true;
                    }
                }
                else
                {
                    return false;;
                }
            }
            catch (Exception ex)
            {
                Global.Error = ex.Message;
                throw ex.InnerException;
            }
        }
    }
}
