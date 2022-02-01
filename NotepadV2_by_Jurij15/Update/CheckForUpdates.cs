using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Net.Http;
using System.Windows;

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
                var url = "https://notepadv2-by-jurij15-api.herokuapp.com/api/versioncheck/latestversion";

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

                MessageBox.Show(ex.Message, "Couldd't check for updates");
            }
            }
        }
    }

