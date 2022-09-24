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
            if (CheckConfingFileExisting())
            {
                //it already exists, no need to do anything
                return true;
            }
            else if (!CheckConfingFileExisting())
            {
                Directory.CreateDirectory(strings.RootAppDataDir);
                File.Create(strings.AppDataSavesFile);
                AssingDefaultValuesToSetting();
                return true;
            }
            return false;
        }/// <summary>
         /// Creates the config file
         /// </summary>
         /// <returns>OperationSuccessfull</returns>

        public static bool ReadConfigFile()
        {
            if (CheckConfingFileExisting())
            {
                foreach (var line in File.ReadAllLines(strings.AppDataSavesFile))
                {
                    if (line.Contains("Theme"))
                    {
                        if (line.Contains("Dark"))
                        {
                            Global.Theme = "Dark";
                        }
                        else if (line.Contains("Light"))
                        {
                            Global.Theme = "Light";
                        }
                    }
                    else if (line.Contains("ShortcutsBarVisibility"))
                    {
                        if (line.Contains("true"))
                        {
                            Global.ShortcutsBarVisibility = true;
                        }
                        else if (line.Contains("false"))
                        {
                            Global.ShortcutsBarVisibility = false;
                        }
                    }
                    else if (line.Contains("ShowTimeInMenuBar"))
                    {
                        if (line.Contains("true"))
                        {
                            Global.ShowTimeInMenuBar = true;
                        }
                        else if (line.Contains("false"))
                        {
                            Global.ShowTimeInMenuBar = true;
                        }
                    }
                }
                return true;
            }
            else if (!CheckConfingFileExisting())
            {
                CreateConfigFile();
                return true;
            }
            else
            {
                return false;
            }
            return false;
        }/// <summary>
        /// Reads(parses) the confid file and applies the settings
        /// </summary>
        /// <returns>OperationSuccessfull</returns>

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
        }/// <summary>
        /// Checks if settings file already exists
        /// </summary>
        /// <returns>bExists</returns>
        /// 

        public static bool AssingDefaultValuesToSetting()
        {
            try
            {
                if (CheckConfingFileExisting())
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
