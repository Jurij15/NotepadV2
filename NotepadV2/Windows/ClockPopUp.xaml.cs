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

namespace NotepadV2.Windows
{
    /// <summary>
    /// Interaction logic for ClockPopUp.xaml
    /// </summary>
    public partial class ClockPopUp : Window
    {
        public ClockPopUp()
        {
            InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_tick;
            timer.Start();
        }

        void timer_tick(object sender, EventArgs e)
        {
            TimeBox.Text = DateTime.Now.ToString("HH:mm:ss");
        }
    }
}
