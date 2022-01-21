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
            versionPopUp.VerPopUp();
            CheckForUpdates checkForUpdates = new CheckForUpdates();
            checkForUpdates.UpdateCheck();
            versionstringbox.Text = version.VersionString;
        }

        public MainWindow()
        {
            InitializeComponent();
            onstartup();
        }

        private void NewBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void OpenBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog.ShowDialog();
            RichTextBox textbox = new RichTextBox();
            textbox.Document.Blocks.Clear();
            textbox.Document.Blocks.Add(new Paragraph(new Run(openFileDialog.FileName)));
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UndoBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RedoBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CutBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CopyBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PasteBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SelectAllBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DateTimeBtn_Click(object sender, RoutedEventArgs e)
        {

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
            MessageBox.Show("Under works!!");
        }
    }
}
