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
using System.Windows.Threading;
using LogSharper;
using NotepadV2.Common;
using NotepadV2.Common.Settings;
using NotepadV2.Windows;

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
                Logger.Info("App theme is DARK");
            }
            else if (Global.Theme.Contains("Light"))
            {
                ModernWpf.ThemeManager.Current.ApplicationTheme = ModernWpf.ApplicationTheme.Light;
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
    }
}
