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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net;
using System.IO;
using System.Data;
using Microsoft.Win32;
using NotepadV2_by_Jurij15.Update;
using NotepadV2_by_Jurij15.Windows;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using Run = System.Windows.Documents.Run;
using System.Windows.Forms;
using System.Threading;

namespace NotepadV2_by_Jurij15
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
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
            Environment.Exit(0);
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

        private void Activate_Title_Icons(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Close_btn.Fill = (ImageBrush)Main.Resources["Close_act"];
            Min_btn.Fill = (ImageBrush)Main.Resources["Min_act"];
            Max_btn.Fill = (ImageBrush)Main.Resources["Max_act"];
        }

        private void Deactivate_Title_Icons(object sender, System.Windows.Input.MouseEventArgs e)
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
        public void checkforlimits()
        {
            Settings settings = new Settings();
            string indevsetting = settings.IsInDev;
            string indev = "true";
            string notindev = "false";
            if (indevsetting == notindev)
            {
                versionstringbox.Text = "";
            }
            else if (indevsetting == indev)
            {
                //do nothing, just countiniue
            }
        }

        public void onstartup()
        {
            Version version = new Version();
            VersionPopUp versionPopUp = new VersionPopUp();
            //no more version popup on startup (soon)
            //versionPopUp.VerPopUp();
            //CheckForUpdates checkForUpdates = new CheckForUpdates();
            //no more checking for updates on startup
            //checkForUpdates.UpdateCheck();
            versionstringbox.Text = version.VersionString;
            //textbox fix wired spacing
            System.Windows.Controls.RichTextBox rtb = new System.Windows.Controls.RichTextBox();
            //setLineFormat(1,1);
            /*
            Paragraph p = rtb.Document.Blocks.FirstBlock as Paragraph;
            p.LineHeight = 1;
            */
            checkforlimits();
        }

        public void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            // Log the exception, display it, etc
            //Debug.WriteLine(e.Exception.Message);
            //CrashDetails = e.Exception.Message;
            
        }
        public void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            // Log the exception, display it, etc
            //Debug.WriteLine((e.ExceptionObject as Exception).Message);
            //CrashDetails = (e.ExceptionObject as Exception).Message;
        }

        public MainWindow()
        {
            InitializeComponent();
            onstartup();
            System.Windows.Forms.Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
        }

        private void NewBtn_Click(object sender, RoutedEventArgs e)
        {
            TextBox1.Document.Blocks.Clear();
        }

        private void OpenBtn_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog.ShowDialog();
            TextBox1.Document.Blocks.Clear();
            string location = openFileDialog.FileName;
            TextBox1.Document.Blocks.Add(new Paragraph(new System.Windows.Documents.Run(File.ReadAllText(location))));
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog();
            saveFileDialog.Filter = "Text File (*.txt)|*.txt|Show All Files (*.*)|*.*";
            saveFileDialog.FileName = "Untitled";
            saveFileDialog.Title = "Save As";
            string range = new TextRange(TextBox1.Document.ContentStart, TextBox1.Document.ContentEnd).Text;
            if (saveFileDialog.ShowDialog() == true)
            {
                File.WriteAllText(saveFileDialog.FileName, range);
            }
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void UndoBtn_Click(object sender, RoutedEventArgs e)
        {
            TextBox1.Undo();
        }

        private void RedoBtn_Click(object sender, RoutedEventArgs e)
        {
            TextBox1.Redo();
        }

        private void CutBtn_Click(object sender, RoutedEventArgs e)
        {
            TextBox1.Cut();
        }

        private void CopyBtn_Click(object sender, RoutedEventArgs e)
        {
            TextBox1.Copy();
        }

        private void PasteBtn_Click(object sender, RoutedEventArgs e)
        {
            TextBox1.Paste();
        }

        private void SelectAllBtn_Click(object sender, RoutedEventArgs e)
        {
            TextBox1.SelectAll();
        }

        private void DateTimeBtn_Click(object sender, RoutedEventArgs e)
        {
            string datetimenow = System.DateTime.Now.ToString();
            TextBox1.Document.Blocks.Add((new Paragraph(new Run(datetimenow))));
        }

        private void FontBtn_Click(object sender, RoutedEventArgs e)
        {
            FontDialog dlg = new FontDialog();
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                TextBox1.FontFamily = new FontFamily(dlg.Font.Name);
                TextBox1.FontSize = dlg.Font.Size * 98.0 / 72.0;
                TextBox1.FontWeight = dlg.Font.Bold ? FontWeights.Bold : FontWeights.Regular;
                TextBox1.FontStyle = dlg.Font.Italic ? FontStyles.Italic : FontStyles.Normal;
            }
        }

        private void ColorBtn_Click(object sender, RoutedEventArgs e)
        {
            ColorDialog dlg = new ColorDialog();
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                TextBox1.Foreground = new SolidColorBrush(Color.FromArgb(dlg.Color.A, dlg.Color.R, dlg.Color.G, dlg.Color.B));            
            }
        }

        private void WebBrowserBtn_Click(object sender, RoutedEventArgs e)
        {
            WebBrowserWindow webBrowser = new WebBrowserWindow();
            webBrowser.ShowInTaskbar = true;
        webBrowser.Show();
        }

        private void PDFReaderBtn_Click(object sender, RoutedEventArgs e)
        {
            PDFReader pdfReader = new PDFReader();
            pdfReader.ShowInTaskbar = true;
            pdfReader.Show();
        }

        private void AboutBtn_Click(object sender, RoutedEventArgs e)
        {
            AboutWindow aboutWindow = new AboutWindow();
            aboutWindow.Show();
        }

        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            CheckForUpdates checkForUpdates = new CheckForUpdates();
            checkForUpdates.UpdateCheck();
        }

        private void PatchNotesBtn_Click(object sender, RoutedEventArgs e)
        {
            PatchNotesWindow patchNotes = new PatchNotesWindow();
            patchNotes.Show();
        }
    }
}
