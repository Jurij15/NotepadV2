using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using System.Net;
using System.IO;
using System.Net.Http;

namespace NotepadV2_by_Jurij15.Windows
{
    /// <summary>
    /// Interaction logic for PatchNotesWindow.xaml
    /// </summary>
    public partial class PatchNotesWindow : Window
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg,
                int wParam, int lParam);

        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        public void move_window(object sender, MouseButtonEventArgs e)
        {
            ReleaseCapture();
            SendMessage(new WindowInteropHelper(this).Handle,
                WM_NCLBUTTONDOWN, HT_CAPTION, 0);
        }

        private void EXIT(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        private void MINIMIZE(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void MAX_RESTORE(object sender, MouseButtonEventArgs e)
        {
            if (this.WindowState == WindowState.Normal) this.WindowState = WindowState.Maximized;
            else this.WindowState = WindowState.Normal;
        }

        private void Activate_Title_Icons(object sender, MouseEventArgs e)
        {
            Close_btn.Fill = (ImageBrush)Main.Resources["Close_act"];
            Min_btn.Fill = (ImageBrush)Main.Resources["Min_act"];
            Max_btn.Fill = (ImageBrush)Main.Resources["Max_act"];
        }

        private void Deactivate_Title_Icons(object sender, MouseEventArgs e)
        {
            Close_btn.Fill = (ImageBrush)Main.Resources["Close_inact"];
            Min_btn.Fill = (ImageBrush)Main.Resources["Min_inact"];
            Max_btn.Fill = (ImageBrush)Main.Resources["Max_inact"];
        }

        private void Close_pressing(object sender, MouseButtonEventArgs e)
        {
            Close_btn.Fill = (ImageBrush)Main.Resources["Close_pr"];
        }

        private void Min_pressing(object sender, MouseButtonEventArgs e)
        {
            Min_btn.Fill = (ImageBrush)Main.Resources["Min_pr"];
        }

        private void Max_pressing(object sender, MouseButtonEventArgs e)
        {
            Max_btn.Fill = (ImageBrush)Main.Resources["Max_pr"];
        }
        public void whenopened()
        {
            //i give up maybe fix this later
            /*
            //in here, it checks for the latest version that is available
            //if the version is older than 0.3, the patch notes will probably crash the program

            //make a get request to servers
            try
            {
                Version version = new Version();
                var url = "http://localhost:4000/api/versioncheck/latestversion";

                var request = WebRequest.Create(url);
                request.Method = "GET";

                WebResponse webResponse = request.GetResponse();
                Stream webStream = webResponse.GetResponseStream();

                StreamReader reader = new StreamReader(webStream);
                var data = reader.ReadToEnd();
                string datarecievedtest = reader.ReadToEnd();

                if (data != version.PatchNotesVer)
                {
                    pnotesDisplay.Text = "latestonappver";
                }
                else if (data == version.PatchNotesVer)
                {
                    string line;
                    //Pass the file path and file name to the StreamReader constructor
                    StreamReader sr = new StreamReader("../PatchNotesText/Patch0.3.txt");
                    //Read the first line of text
                    line = sr.ReadLine();
                    //Continue to read until you reach end of file
                    while (line != null)
                    {
                        //write the line to console window
                        Console.WriteLine(line);
                        //Read the next line
                        line = sr.ReadLine();
                        pnotesDisplay.Text = line;
                    }
                    //close the file
                    sr.Close();
                    Console.ReadLine();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Couldd't check for updates");
            }
            */
        }
        public PatchNotesWindow()
        {
            InitializeComponent();
            //string location = "../PatchNotesText/Patch0.3.txt";
            //string 0.3 = ""
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void tbMultiLine_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
