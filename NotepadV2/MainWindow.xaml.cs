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
using NotepadV2.Dialogs;
using System.DirectoryServices.ActiveDirectory;
using ModernWpf.Controls.Primitives;
using System.Windows.Forms;
//using System.Windows.Forms;

namespace NotepadV2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region MainWindowFunctions
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

            public static class Tabs
            {
            }
        }
        #endregion
        #region Init
        public void Init(bool bDebug, bool bDebugSettings)
        {
            Arguments.ProcessCommandLineArgs(true);
            if (bDebug)
            {
                //Util.SetupConsole();
                Util.SetupLogger();
                this.Title = Global.AppTitle + "Debug Release";
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

            if (!Global.ShowThemeBtnInMenubar)
            {
                ThemeBtn.Visibility = Visibility.Collapsed;
                ThemeBtn.IsEnabled = false;
            }
            if (!Global.ShowPathBox)
            {
                PathBox.Visibility = Visibility.Collapsed;
            }
            TextBox.Name = "TextBoxDefault";
            RegisterName("TextBoxDefault", TextBox);
        }
        #endregion
        public MainWindow()
        {
            InitializeComponent();
            Init(Global.bGlobalDebug, Global.bDebugSettingsMain);
        }

        void timer_tick(object sender, EventArgs e)
        {
            TimeBtn.Content = DateTime.Now.ToString("HH:mm:ss");
        }
        #region Old Code
        public void AdjustAppTitleByDocumentName(string DocumentName)
        {
            Global.AppTitle = DocumentName + "-" + Global.AppTitleDefault;
            this.Title = Global.AppTitle;
        }
        #endregion

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            /*
            if (!Global.IsCurrentlDocumentSaved)
            {
                ContentDialog.ShowSimpleContentDialog("Do You want to save the document?", "..", "OK", false) ;  //TODO Finish This
            }
            else if (Global.IsCurrentlDocumentSaved)
            {
                Environment.Exit(0);
            }
            */ //might try to make this work again, but since tabs it no longer works properly
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
        #region MainWindowMenu
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
            if (docToOpen == "")
            {
                //no doc to open, do nothing
            }
            else if (docToOpen != "")
            {
                GetCurrentlySelectedTabTextBox().Document.Blocks.Clear();
                GetCurrentlySelectedTabTextBox().Document.Blocks.Add(new Paragraph(new System.Windows.Documents.Run(File.ReadAllText(docToOpen))));
                PathBox.Text = docToOpen;
                //AdjustAppTitleByDocumentName(docToOpen);
                GetCurrentlySelectedTab().Header = tabcontent;
            }
            Global.IsCurrentlDocumentSaved = false;
        }

        private void MenuSaveBtn_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog();
            saveFileDialog.Filter = "Text File (*.txt)|*.txt|Show All Files (*.*)|*.*";
            saveFileDialog.FileName = "Untitled";
            saveFileDialog.Title = "Save As";
            string range = new TextRange(GetCurrentlySelectedTabTextBox().Document.ContentStart, GetCurrentlySelectedTabTextBox().Document.ContentEnd).Text;
            if (saveFileDialog.ShowDialog() == true)
            {
                File.WriteAllText(saveFileDialog.FileName, range);
                GetCurrentlySelectedTab().Header = saveFileDialog.SafeFileName;
            }
        }

        private void MenuPreferencesBtn_Click(object sender, RoutedEventArgs e)
        {
            NotepadV2.Dialogs.PreferencesDialog dialog = new NotepadV2.Dialogs.PreferencesDialog();
            dialog.ShowAsync();
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
            GetCurrentlySelectedTabTextBox().Undo();
        }

        private void MenuRedoBtn_Click(object sender, RoutedEventArgs e)
        {
            GetCurrentlySelectedTabTextBox().Redo();
        }

        private void MenuCutBtn_Click(object sender, RoutedEventArgs e)
        {
            GetCurrentlySelectedTabTextBox().Cut();
        }

        private void MenuCopyBtn_Click(object sender, RoutedEventArgs e)
        {
            GetCurrentlySelectedTabTextBox().Copy();
        }

        private void MenuPasteBtn_Click(object sender, RoutedEventArgs e)
        {
            GetCurrentlySelectedTabTextBox().Paste();
        }

        private void MenuSelectAllBtn_Click(object sender, RoutedEventArgs e)
        {
            GetCurrentlySelectedTabTextBox().SelectAll();
        }

        private void MenuTimeDateBtn_Click(object sender, RoutedEventArgs e)
        {
            string CurrentText = new TextRange(GetCurrentlySelectedTabTextBox().Document.ContentStart, GetCurrentlySelectedTabTextBox().Document.ContentEnd).Text;
            string NewText = CurrentText + Util.GetCurrentDateTimeString();

            GetCurrentlySelectedTabTextBox().Document.Blocks.Clear();
            GetCurrentlySelectedTabTextBox().Document.Blocks.Add(new Paragraph(new Run(NewText)));
        }

        private void MenuFontBtn_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FontDialog dlg = new System.Windows.Forms.FontDialog();
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                GetCurrentlySelectedTabTextBox().FontFamily = new FontFamily(dlg.Font.Name);
                GetCurrentlySelectedTabTextBox().FontSize = dlg.Font.Size * 98.0 / 72.0;
                GetCurrentlySelectedTabTextBox().FontWeight = dlg.Font.Bold ? FontWeights.Bold : FontWeights.Regular;
                GetCurrentlySelectedTabTextBox().FontStyle = dlg.Font.Italic ? FontStyles.Italic : FontStyles.Normal;
            }
        }

        private void MenuColorBtn_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.ColorDialog dlg = new System.Windows.Forms.ColorDialog();
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                GetCurrentlySelectedTabTextBox().Foreground = new SolidColorBrush(Color.FromArgb(dlg.Color.A, dlg.Color.R, dlg.Color.G, dlg.Color.B));
            }
        }

        private void MenuAboutBtn_Click(object sender, RoutedEventArgs e)
        {
            //ContentDialog.ShowSimpleContentDialog("NotepadV2", strings.AboutString, "OK", false);
            AboutDialog about = new AboutDialog();
            about.ShowAsync();
        }
        #endregion

        #region Tabs

        public TabItem GetCurrentlySelectedTab()
        {
            return (TabItem)ControlTabs.SelectedItem;
        }
        public int GetSelectedTabIndex()
        {
            return ControlTabs.SelectedIndex;
        }
        public RichTextBox GetCurrentlySelectedTabTextBox()
        {
            //MessageBox.Show(GetSelectedTabIndex().ToString());
            if (GetSelectedTabIndex() == 0)
            {
                return ((RichTextBox)DPanel.FindName("TextBoxDefault")); //this is the default notepad that is created in normal xaml
            }
            return ((RichTextBox)DPanel.FindName("TextBox" + GetSelectedTabIndex().ToString()));
        }
        private void CloseTabBtn_Click(object sender, RoutedEventArgs e)
        {
            //unregister the name before it the tab gets removed and count is decremented (hope this works)
            if (GetSelectedTabIndex() == 0)
            {
                //we must not close the only open tab, we can just reset the text in it
                MenuNewBtn_Click(sender, e);
            }
            else
            {
                UnregisterName("TextBox" + GetSelectedTabIndex().ToString());
                ControlTabs.Items.Remove(GetCurrentlySelectedTab());
                Global.TabsCount--;
            }
        }

        private void AddTabBtn_Click(object sender, RoutedEventArgs e)
        {
            Global.TabsCount++;
            TabItem tab = new TabItem();
            tab.Header = "New Tab";

            RichTextBox rtextbox = new RichTextBox();
            int math = GetSelectedTabIndex() + 1;
            rtextbox.Name = "TextBox"+math.ToString();
            tab.Content = rtextbox;
            //MessageBox.Show(rtextbox.Name);
            //try to register the name so it can be found using FindName
            RegisterName(rtextbox.Name, rtextbox);

            ControlTabs.Items.Insert(1, tab);
        }
        #endregion
    }
}
