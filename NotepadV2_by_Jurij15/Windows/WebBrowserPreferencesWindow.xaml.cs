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

namespace NotepadV2_by_Jurij15.Windows
{
    /// <summary>
    /// Interaction logic for WebBrowserPreferencesWindow.xaml
    /// </summary>
    public partial class WebBrowserPreferencesWindow : Window
    {
        WebBrowserSettings webBrowserSettings = new WebBrowserSettings();
        public string homestring { get; set; } 
        public WebBrowserPreferencesWindow()
        {
            InitializeComponent();
            homestring = webBrowserSettings.HomePageAdress;
            HomeBox.Text = homestring;
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            WebBrowserSettings webBrowserSettings = new WebBrowserSettings();
            NotepadV2_by_Jurij15.Windows.WebBrowserSettings.Default.HomePageAdress = HomeBox.Text;
            NotepadV2_by_Jurij15.Windows.WebBrowserSettings.Default.Save();
        }
    }
}
