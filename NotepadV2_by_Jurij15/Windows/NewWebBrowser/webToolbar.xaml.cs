﻿using System;
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
using System.Runtime.InteropServices;
using System.Windows.Interop;
using System.ComponentModel;
using System.Reflection;

namespace NotepadV2_by_Jurij15.Windows.NewWebBrowser
{
    /// <summary>
    /// Interaction logic for webToolbar.xaml
    /// </summary>
    public partial class webToolbar : Window
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
            //Min_btn.Fill = (ImageBrush)Main.Resources["Min_act"];
            //Max_btn.Fill = (ImageBrush)Main.Resources["Max_act"];
        }

        private void Deactivate_Title_Icons(object sender, MouseEventArgs e)
        {
            Close_btn.Fill = (ImageBrush)Main.Resources["Close_inact"];
            //Min_btn.Fill = (ImageBrush)Main.Resources["Min_inact"];
            //Max_btn.Fill = (ImageBrush)Main.Resources["Max_inact"];
        }

        private void Close_pressing(object sender, MouseButtonEventArgs e)
        {
            Close_btn.Fill = (ImageBrush)Main.Resources["Close_pr"];
        }

        private void Min_pressing(object sender, MouseButtonEventArgs e)
        {
            //Min_btn.Fill = (ImageBrush)Main.Resources["Min_pr"];
        }

        private void Max_pressing(object sender, MouseButtonEventArgs e)
        {
            //Max_btn.Fill = (ImageBrush)Main.Resources["Max_pr"];
        }
        public webToolbar()
        {
            InitializeComponent();
        }
        public static void StartClose( Window x)
        {
            //this.Close();
            //Close();
            //this.Visibility = System.Windows.Visibility.Visible;
            Assembly currentAssembly = Assembly.GetExecutingAssembly();
            //  int count = Application.Current.Windows;
            foreach (Window w in Application.Current.Windows)
            {
                //Form f = Application.OpenForms[i];
                if (w.GetType().Assembly == currentAssembly && w == x)
                {
                    w.Close();
                }
            }
        }
        void WebWindow_Closing(object sender, CancelEventArgs e)
        {
            //MessageBox.Show("Closing called");
            //webToolbar webToolbar = new webToolbar();
            //webToolbar.Close();
            //webToolbar.Close();
            //this.Close();
            //this.Visibility = System.Windows.Visibility.Visible;
            //e.Cancel = true;
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            NewWebBrowser newWebBrowser = new NewWebBrowser();
            newWebBrowser.Close();
            this.Close();
        }
    }
}
