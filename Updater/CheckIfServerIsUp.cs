using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using MessageBox = System.Windows.MessageBox;

namespace Updater
{
    public class CheckIfServerIsUp
    {
        public string IsServerUp { get; set; }
        public void CheckServer()
        {
            try
            {
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

                if (datarecievedtest == "ONLINE")
                {
                    IsServerUp = data;
                }
                else if (datarecievedtest == "OFFLINE")
                {
                    IsServerUp = "OFFLINE";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
