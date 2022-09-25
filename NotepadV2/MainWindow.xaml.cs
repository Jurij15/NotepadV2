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
using NotepadV2.Common;
using NotepadV2.Common.Settings;

namespace NotepadV2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public void Init(bool bDebug, bool bDebugSettings)
        {
            Arguments.ProcessCommandLineArgs(true);
            if (bDebug)
            {
                Util.SetupConsole();
                Util.SetupLogger();
            }

            Settings.ReadConfigFile();

            if (bDebugSettings)
            {
                MessageBox.Show(Settings.CheckConfingFileExisting().ToString());
                MessageBox.Show(Global.Theme.ToString());
                MessageBox.Show(Global.ShowTimeInMenuBar.ToString());
                MessageBox.Show(Global.ShowTimeInMenuBar.ToString());
            }

            this.Title = Global.AppTitle; 
        }

        public MainWindow()
        {
            InitializeComponent();
            Init(Global.bGlobalDebug, Global.bDebugSettingsMain);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
