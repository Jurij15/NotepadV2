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

namespace NotepadV2_by_Jurij15
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public void onstartup()
        {
            Version version = new Version();
            VersionPopUp versionPopUp = new VersionPopUp();
            //no more version popup on startup (soon)
            versionPopUp.VerPopUp();
            CheckForUpdates checkForUpdates = new CheckForUpdates();
            //no more checking for updates on startup
            //checkForUpdates.UpdateCheck();

            versionstringbox.Text = version.VersionString;
        }

        public MainWindow()
        {
            InitializeComponent();
            onstartup();
        }

        private void NewBtn_Click(object sender, RoutedEventArgs e)
        {
            TextBox1.Document.Blocks.Clear();
        }

        private void OpenBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog.ShowDialog();
            TextBox1.Document.Blocks.Clear();
            string location = openFileDialog.FileName;
            TextBox1.Document.Blocks.Add(new Paragraph(new Run(File.ReadAllText(location))));
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
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

        }

        private void ColorBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void WebBrowserBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PDFReaderBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AboutBtn_Click(object sender, RoutedEventArgs e)
        {
            VersionPopUp versionPopUp = new VersionPopUp();
            versionPopUp.VerPopUp();
        }

        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            CheckForUpdates checkForUpdates = new CheckForUpdates();
            checkForUpdates.UpdateCheck();
        }

        private void PatchNotesBtn_Click(object sender, RoutedEventArgs e)
        {
            PatchNotesWindow patchNotes = new PatchNotesWindow();
            patchNotes.ShowDialog();
        }
    }
}
