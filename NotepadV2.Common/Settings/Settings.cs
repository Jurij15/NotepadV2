using LogSharper;
using Microsoft.Build.Tasks.Deployment.Bootstrapper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;
using Windows.UI.Xaml;
using Newtonsoft.Json;

namespace NotepadV2.Common.Settings
{
    public class SettingsBase
    {
        public string Theme;
        public bool ShortcutsBarVisibility;
        public bool ShowTimeInMenuBar;
    }
    /// <summary> TODO
    /// REWRITE THIS WITH SAVING SUPPORT!
    /// </summary>
    public class Settings
    { 
        public static bool RecreateConfigFile()
        {
            if (CheckConfingFileExisting())
            {
                try
                {
                    File.Delete(strings.AppDataSavesFile);
                    if (CheckConfingFileExisting())
                    {
                        using (StreamWriter sw = File.CreateText(strings.AppDataSavesFile))
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
                        return false; ;
                    }
                }
                catch (Exception ex)
                {
                    Global.Error = ex.Message;
                    throw ex;
                }
            }
            else if (!CheckConfingFileExisting())
            {
                try
                {
                    Directory.CreateDirectory(strings.RootAppDataDir);
                        using (StreamWriter sw = File.CreateText(strings.AppDataSavesFile))
                        {
                            sw.WriteLine("Theme==Dark");
                            sw.WriteLine("ShortcutsBarVisibility==true");
                            sw.WriteLine("ShowTimeInMenuBar==true");
                            sw.Close();
                            return true;
                        }
                }
                catch (Exception ex)
                {
                    Global.Error = ex.Message;
                    throw ex;
                }
            }
            return false;
        }

        public static bool ParseConfigFile()
        {
            Logger.Info("Trying to parse the config file...");
            if (CheckConfingFileExisting())
            {
                if (File.ReadAllText(strings.AppDataSavesFile) != null)
                {
                    Logger.Info("Settings file already exists!");
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
                    LogSucess();
                    return true;
                }
                else if (File.ReadAllText(strings.AppDataSavesFile) == String.Empty)
                {
                    Logger.Warning("Config file is empty, recreating one...");
                    Logger.Info("Settings will be reset to default!");
                    RecreateConfigFile();
                }
            }
            else if (!CheckConfingFileExisting())
            {
                Logger.Warning("Config file doesnt exist, creating one...");
                Logger.Info("Settings will be reset to default!");
                RecreateConfigFile();
            }
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
        }/// <summary>
        /// Checks if settings file already exists
        /// </summary>
        /// <returns>bExists</returns>
        /// 

        //not really needed but eh
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
                throw ex;
            }
        }

        public static void LogSucess()
        {
            Logger.Success("Applied all settings!");
        }
    }

    public class SettingsV2
    {
        public static void CreateDirectory()
        {
            if (Directory.Exists(strings.RootAppDataDir))
            {

            }
            else if (!Directory.Exists(strings.RootAppDataDir))
            {
                Directory.CreateDirectory(strings.RootAppDataDir);
            }
        }

        public static void InitSettings()
        {
            if (!File.Exists(strings.AppDataSavesFile))
            {
                CreateDirectory();
                //set it to default
                SettingsBase settings = new SettingsBase();
                settings.Theme = "Dark";
                settings.ShortcutsBarVisibility = true;
                settings.ShowTimeInMenuBar = true;
                string output = JsonConvert.SerializeObject(settings);

                File.WriteAllText(strings.AppDataSavesFile, output);
            }
            else if (File.Exists(strings.AppDataSavesFile))
            {

            }
        }
    }
}
