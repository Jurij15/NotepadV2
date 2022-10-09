using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
using System.Windows.Threading;
using LogSharper;
using NotepadV2.Common;
using NotepadV2.Common.Settings;
using NotepadV2.Windows;
using System.IO;
using System.IO.Compression;
using System.Web;
using Microsoft.Win32;

namespace NotepadV2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public class MainWindowFunctions
        {
            public class TextBoxSpecific
            {
                public static void PasteFromClipboard()
                {
                    MainWindow main = new MainWindow();
                    main.TextBox.Paste();
                }

                public static void CopyToClipboard()
                {
                    MainWindow main = new MainWindow();
                    main.TextBox.Copy();
                }

                public static void CutToClipboard()
                {
                    MainWindow main = new MainWindow();
                    main.TextBox.Cut();
                }

                public static void TextBoxUndo()
                {
                    MainWindow main = new MainWindow();
                    main.TextBox.Undo();
                }

                public static void TextBoxRedo()
                {
                    MainWindow main = new MainWindow();
                    main.TextBox.Redo();
                }

                public static void TextBoxSelectAll()
                {
                    MainWindow main = new MainWindow();
                    main.TextBox.SelectAll();
                }

                public static void TextBoxInsertDateTime()
                {
                    MainWindow main = new MainWindow();
                    string CurrentText = new TextRange(main.TextBox.Document.ContentStart, main.TextBox.Document.ContentEnd).Text;
                    string NewText = CurrentText + Util.GetCurrentDateTimeString();

                    main.TextBox.Document.Blocks.Clear();
                    main.TextBox.Document.Blocks.Add(new Paragraph(new Run(NewText)));
                }
            }

            public static string GetCurrentAppTitle()
            {
                return Util.GetCurrentAppTitle();
            }

            public static string GetCurrentDocumentLocation()
            {
                if (Global.DocumentLocation != null)
                {
                    return Global.DocumentLocation;
                }
                else
                {
                    return null;
                }
            }

            public static string GetCurrentDocumentTitle()
            {
                if (Global.DocumentFileTitle != null)
                {
                    return Global.DocumentFileTitle;
                }
                else
                {
                    return null;
                }
            }

            public class General
            {
                public static void OpenFileWithDialog()
                {
                    MainWindow main = new MainWindow();
                    Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
                    openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                    openFileDialog.ShowDialog();
                    string docToOpen = openFileDialog.FileName;
                    string tabcontent = openFileDialog.SafeFileName;
                    if (docToOpen == null)
                    {
                        //no doc to open, do nothing
                    }
                    else if (docToOpen != null)
                    {
                        main.TextBox.Document.Blocks.Clear();
                        main.TextBox.Document.Blocks.Add(new Paragraph(new System.Windows.Documents.Run(File.ReadAllText(docToOpen))));
                        main.PathBox.Text = docToOpen;
                    }
                }

                public static void OpenFile(string filePath, string safeFilePath)
                {
                    MainWindow main = new MainWindow();
                    string docToOpen = filePath;
                    string tabcontent = safeFilePath;
                    if (docToOpen == null)
                    {
                        //no doc to open, do nothing
                    }
                    else if (docToOpen != null)
                    {
                        main.TextBox.Document.Blocks.Clear();
                        main.TextBox.Document.Blocks.Add(new Paragraph(new System.Windows.Documents.Run(File.ReadAllText(docToOpen))));
                        main.PathBox.Text = docToOpen;
                    }
                }

                public static void SaveFile(string filePath, string safeFilePath)
                {

                }

                public static void SaveFileWithDialog()
                {

                }

                public static void New()
                {
                    MainWindow main = new MainWindow();
                    main.TextBox.Document.Blocks.Clear();
                }

                public static void Close()
                {
                    Environment.Exit(0);
                }
            }
        }
        public void Init(bool bDebug, bool bDebugSettings)
        {
            Arguments.ProcessCommandLineArgs(true);
            if (bDebug)
            {
                Util.SetupConsole();
                Util.SetupLogger();
            }

            Settings.ParseConfigFile();

            if (bDebugSettings)
            {
                MessageBox.Show(Settings.CheckConfingFileExisting().ToString());
                MessageBox.Show(Global.Theme.ToString());
                MessageBox.Show(Global.ShowTimeInMenuBar.ToString());
                MessageBox.Show(Global.ShowTimeInMenuBar.ToString());
            }

            this.Title = Global.AppTitle;

            //start the timer
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_tick;
            timer.Start();

            //set the current theme
            if (Global.Theme.Contains("Dark"))
            {
                ModernWpf.ThemeManager.Current.ApplicationTheme = ModernWpf.ApplicationTheme.Dark;
                ThemeBtn.Content = "Light";
                Logger.Info("App theme is DARK");
            }
            else if (Global.Theme.Contains("Light"))
            {
                ModernWpf.ThemeManager.Current.ApplicationTheme = ModernWpf.ApplicationTheme.Light;
                ThemeBtn.Content = "Dark";
                Logger.Info("App theme is LIGHT");
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            Init(Global.bGlobalDebug, Global.bDebugSettingsMain);
        }

        void timer_tick(object sender, EventArgs e)
        {
            TimeBtn.Content = DateTime.Now.ToString("HH:mm:ss");
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Environment.Exit(0);
        }

        private void TimeBtn_Click(object sender, RoutedEventArgs e)
        {
            ClockPopUp window = new ClockPopUp();
            window.Show();
        }

        private void ThemeBtn_Click(object sender, RoutedEventArgs e)
        {
            //TODO: Add config saving
            if (ThemeBtn.Content == "Dark")
            {
                ThemeBtn.Content = "Light";
                ModernWpf.ThemeManager.Current.ApplicationTheme = ModernWpf.ApplicationTheme.Light;
                Global.Theme = "Light";
                Logger.Success("Theme changed to" + Global.Theme);
            }
            else if (ThemeBtn.Content == "Light")
            {
                ThemeBtn.Content = "Dark";
                ModernWpf.ThemeManager.Current.ApplicationTheme = ModernWpf.ApplicationTheme.Dark;
                Global.Theme = "Dark";
                Logger.Success("Theme changed to" + Global.Theme);
            }
        }

        private void MenuNewBtn_Click(object sender, RoutedEventArgs e)
        {
            TextBox.Document.Blocks.Clear();
        }

        private void MenuOpenBtn_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog.ShowDialog();
            string docToOpen = openFileDialog.FileName;
            string tabcontent = openFileDialog.SafeFileName;
            if (docToOpen == null)
            {
                //no doc to open, do nothing
            }
            else if (docToOpen != null)
            {
                TextBox.Document.Blocks.Clear();
                TextBox.Document.Blocks.Add(new Paragraph(new System.Windows.Documents.Run(File.ReadAllText(docToOpen))));
                PathBox.Text = docToOpen;
            }
        }

        private void MenuSaveBtn_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog();
            saveFileDialog.Filter = "Text File (*.txt)|*.txt|Show All Files (*.*)|*.*";
            saveFileDialog.FileName = "Untitled";
            saveFileDialog.Title = "Save As";
            string range = new TextRange(TextBox.Document.ContentStart, TextBox.Document.ContentEnd).Text;
            if (saveFileDialog.ShowDialog() == true)
            {
                File.WriteAllText(saveFileDialog.FileName, range);
            }
        }

        private void MenuPreferencesBtn_Click(object sender, RoutedEventArgs e)
        {
            PreferencesWindow prefs = new PreferencesWindow();
            prefs.ShowDialog();
        }

        private void InstanceManagerBtn_Click(object sender, RoutedEventArgs e)
        {
            InstanceManager instancemng = new InstanceManager();
            instancemng.ShowDialog();
        }

        private void MenuExitBtn_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void MenuUndoBtn_Click(object sender, RoutedEventArgs e)
        {
            TextBox.Undo();
        }

        private void MenuRedoBtn_Click(object sender, RoutedEventArgs e)
        {
            TextBox.Redo();
        }

        private void MenuCutBtn_Click(object sender, RoutedEventArgs e)
        {
            TextBox.Cut();
        }

        private void MenuCopyBtn_Click(object sender, RoutedEventArgs e)
        {
            TextBox.Copy();
        }

        private void MenuPasteBtn_Click(object sender, RoutedEventArgs e)
        {
            TextBox.Paste();
        }

        private void MenuSelectAllBtn_Click(object sender, RoutedEventArgs e)
        {
            TextBox.SelectAll();
        }

        private void MenuTimeDateBtn_Click(object sender, RoutedEventArgs e)
        {
            string CurrentText = new TextRange(TextBox.Document.ContentStart, TextBox.Document.ContentEnd).Text;
            string NewText = CurrentText + Util.GetCurrentDateTimeString();

            TextBox.Document.Blocks.Clear();
            TextBox.Document.Blocks.Add(new Paragraph(new Run(NewText)));
        }

        private void MenuFontBtn_Click(object sender, RoutedEventArgs e)
        {
            //todo
        }

        private void MenuColorBtn_Click(object sender, RoutedEventArgs e)
        {
            //todo
        }

        private void MenuAboutBtn_Click(object sender, RoutedEventArgs e)
        {
            //todo
        }
    }
}
