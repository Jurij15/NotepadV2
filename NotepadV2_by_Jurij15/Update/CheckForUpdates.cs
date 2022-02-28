using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Net.Http;
using System.Windows;
using NotepadV2_by_Jurij15.Crash;

namespace NotepadV2_by_Jurij15.Update
{
    public  class CheckForUpdates
    {
            public void UpdateCheck()
            {
            try
            {
                Version version = new Version();
                //soooo, after making a api on heroku, localhost is no longer needed
                //var url = "http://localhost:4000/api/versioncheck/latestversion";
                var url = "https://notepadV2api.herokuapp.com/api/versioncheck/latestversion";

                var request = WebRequest.Create(url);
                request.Method = "GET";

                WebResponse webResponse = request.GetResponse();
                Stream webStream = webResponse.GetResponseStream();

                StreamReader reader = new StreamReader(webStream);
                var data = reader.ReadToEnd();
                string datarecievedtest = reader.ReadToEnd();

                if (data != version.VersionString)
                {
                    UpdateAvailabe updateAvailabe = new UpdateAvailabe();
                    updateAvailabe.UpdateMessage();
                }
                else if (data == version.VersionString)
                {
                    MessageBox.Show("Up to date");
                }
            }
            catch (Exception ex)
            {
                string exceptiontext = ex.Message;
                string commonerror = "The remote server returned an error";
                CrashDetails crashDetails = new CrashDetails();
                crashDetails.CrashDetailsString = exceptiontext;
                ToCrashHandler toCrashHandler = new ToCrashHandler();
                MessageBox.Show(exceptiontext, "Couldd't check for updates");
                if (exceptiontext.Contains(commonerror))
                {
                    toCrashHandler.updateerror = "UPDATE:404";
                    toCrashHandler.SendToCrashHandler();
                }
                
            }
            }
        ToCrashHandler toCrashHandler = new ToCrashHandler();
        public string errorstring   // property
        {
            get { return toCrashHandler.updateerror; }
            set { toCrashHandler.updateerror = value; }
        }
    }
    }

