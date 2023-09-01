using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using SimplePad.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using SimplePad.Dialogs;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace SimplePad.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EditorPage : Page
    {
        bool IsFileSaved = false;
        public EditorPage()
        {
            this.InitializeComponent();
        }

        private void NewWindowMenuBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Activate();
        }

        private void NewFileMenuBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void OpenFileMenuBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SaveFileMenuBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PrintFileMenuBtn_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void ExitFileMenuBtn_Click(object sender, RoutedEventArgs e)
        {
            /*
            if (!IsFileSaved)
            {
                //save file
            }
            */

            Application.Current.Exit();
        }

        private void SettingsMenuBtn_Click(object sender, RoutedEventArgs e)
        {
            DialogService.ShowDialog(new SettingsDialog(), "Settings");
        }
    }
}
