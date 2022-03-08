using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace NotepadV2_by_Jurij15.Windows.NewWebBrowser
{
    /// <summary>
    /// Interaction logic for webToolbar.xaml
    /// </summary>
    public partial class webToolbar : Window
    {
        public webToolbar()
        {
            InitializeComponent();
        }
        void WebWindow_Closing(object sender, CancelEventArgs e)
        {
            //MessageBox.Show("Closing called");
            //webToolbar webToolbar = new webToolbar();
            //webToolbar.Close();
            //webToolbar.Close();
            //this.Close();
            this.Visibility = System.Windows.Visibility.Visible;
            e.Cancel = true;
        }
    }
}
