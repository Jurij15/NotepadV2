using NotepadV2.Common;
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
using System.Windows.Threading;
using static System.Windows.Forms.AxHost;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ScrollBar;

namespace NotepadV2.Windows
{
    /// <summary>
    /// Interaction logic for StatisticsWindow.xaml
    /// </summary>
    public partial class StatisticsWindow : Window
    {
        public enum DetailedStat
        {
            AppVersion,
            AppUptime
        }
        public static string GetSelectedStatDetail(string Stat)
        {
            if (Stat.Contains("App Uptime"))
            {
                string Text = Global.RunningTimeInSeconds.ToString()+" seconds"+Environment.NewLine+"This Statistic contains the amount of time the app has been running for.";
                return Text;
            }
            else if (Stat == null)
            {
                return "Statistic detail not found";
            }

            return "Statistic detail not found";
        }
        /// <summary>
        /// Structure of stats:
        /// 
        /// StatName + Value
        /// 
        /// </summary>
        public List<string> Stats = new List<string>();
        public void Init()
        {
            //here, we will prepare all stats and add them to the array
            //again, STRUCTURE OF STATS

            Stats.Clear();
            AllStatsBox.Items.Clear();

            Stats.Add("App version: " + Version.GetVersionString());
            Stats.Add("Settings Location: " + strings.RootAppDataDir);
            Stats.Add("Number of opened tabs: " + Global.TabsCount.ToString());
            Stats.Add("App Uptime: "+ Global.RunningTimeInSeconds.ToString() + " seconds");

            foreach (var stat in Stats)
            {
                AllStatsBox.Items.Add(stat);
            }
        }
        public StatisticsWindow()
        {
            InitializeComponent();
            Init();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_tick;
            timer.Start();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Stats.Clear();
        }

        private void DetailsBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(AllStatsBox.SelectedValue.ToString());
            if (AllStatsBox.SelectedItems.ToString() == "")
            {

            }
            else if (AllStatsBox.SelectedItems.ToString() != null)
            {
                ContentDialogSimple.ShowSimpleContentDialog("Statistic Details", GetSelectedStatDetail(AllStatsBox.SelectedValue.ToString()), "Close", false);
            }
        }

        public void RefreshStat()
        {
            Stats.Clear();
            Init();
        }

        void timer_tick(object sender, EventArgs e)
        {
            //TimeBox.Text = DateTime.Now.ToString("HH:mm:ss");
            RefreshStat();
        }
    }
}
