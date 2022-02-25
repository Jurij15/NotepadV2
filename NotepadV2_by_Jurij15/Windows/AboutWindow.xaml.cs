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
using System.Diagnostics;
using System.Threading;
using NotepadV2_by_Jurij15.Crash;

namespace NotepadV2_by_Jurij15.Windows
{
    /// <summary>
    /// Interaction logic for AboutWindow.xaml
    /// </summary>
    public partial class AboutWindow : Window
    {
        [DllImport("Kernel32")]
        public static extern void AllocConsole();

        [DllImport("Kernel32")]
        public static extern void FreeConsole();

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
        
        public void ErrorOccured() 
        {

        }
        public AboutWindow()
        {
            InitializeComponent();
            Settings settings = new Settings();
            string showcrashbutton = settings.CanCrashAppTest;
            string showcrashbuttontrue = "true";
            string showcrashbuttonfalse = "false";
            if (showcrashbutton == showcrashbuttonfalse)
            {
                CrashButton.Visibility = Visibility.Hidden;
            }
            else if (showcrashbutton == showcrashbuttontrue)
            {
                //do nothing, just proceed
            }
            MainWindow mainWindow = new MainWindow();
            System.Windows.Forms.Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
        }

        public void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            // Log the exception, display it, etc
            //Debug.WriteLine(e.Exception.Message);
            //string crash = e.Exception.Message;
            MainWindow main = new MainWindow();
            //main.CrashDetails = e.Exception.Message;
            ToCrashHandler toCrashHandler = new ToCrashHandler();
            toCrashHandler.SendToCrashHandler();

        }
        public void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            // Log the exception, display it, etc
            //Debug.WriteLine((e.ExceptionObject as Exception).Message);
            MainWindow main = new MainWindow();
            //string crash = (e.ExceptionObject as Exception).Message;
            //main.CrashDetails = (e.ExceptionObject as Exception).Message;
            ToCrashHandler toCrashHandler = new ToCrashHandler(); 
            toCrashHandler.SendToCrashHandler();
        }

        private void DebugConsBtn_Click(object sender, RoutedEventArgs e)
        {
            AllocConsole();
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void CrashButton_Click(object sender, RoutedEventArgs e)
        {
            Exception();
        }

        private void Exception()
        {
            throw new StackOverflowException();
        }
        
    }
}
