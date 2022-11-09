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

namespace NotepadV2.Windows
{
    /// <summary>
    /// Interaction logic for StatisticsWindow.xaml
    /// </summary>
    public partial class StatisticsWindow : Window
    {
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

            Stats.Add("App version: " + Version.GetVersionString());
            Stats.Add("Settings Location: " + strings.RootAppDataDir);
            Stats.Add("Number of opened tabs: " + Global.TabsCount.ToString());
            Stats.Add("App Uptime: "+ Global.RunningTimeInSeconds.ToString());
        }
        public StatisticsWindow()
        {
            InitializeComponent();
            Init();
            foreach (var stat in Stats)
            {
               AllStatsBox.Items.Add(stat);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Stats.Clear();
        }
    }
}
