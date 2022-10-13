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
using NotepadV2.Common.InstanceManager;

namespace NotepadV2.Windows
{
    /// <summary>
    /// Interaction logic for InstanceManager.xaml
    /// </summary>
    public partial class InstanceManager : Window
    {
        public InstanceManager()
        {
            InitializeComponent();

            IMFuncs.GetAllInstances();

            foreach (var instance in IMFuncs.AllInstancesNames)
            {
                InstancesListBox.Items.Add(instance);
            }
        }

        private void KilInstanceBtn_Click(object sender, RoutedEventArgs e)
        {
            string InstanceName = InstancesListBox.Items[InstancesListBox.SelectedIndex].ToString();
            IMFuncs.KillInstance(IMFuncs.GetInstancePID(InstanceName));
        }

        private void NewInstanceBtn_Click(object sender, RoutedEventArgs e)
        {
            IMFuncs.CreateNewInstance();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            IMFuncs.AllInstancesNames.Clear();
        }

        private void InstancesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            InstanceNameBox.Clear();
            PIDBox.Clear();
            string instancename = InstancesListBox.Items[InstancesListBox.SelectedIndex].ToString();
            InstanceNameBox.Text = instancename;
            PIDBox.Text = IMFuncs.GetInstancePID(instancename).ToString();
        }
    }
}
