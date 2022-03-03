using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Text;
using System.Windows;
using System.Windows.Forms;

namespace Updater
{
    public class Programrw
    {
        [STAThread]
        static void Main(string[] args)
        {
            CheckIfServerIsUp checkIfServerIsUp = new CheckIfServerIsUp(); 
            UpdateWindow updateWindow = new UpdateWindow();
            updateWindow.Activate();
            updateWindow.ShowInTaskbar = true;
            updateWindow.ShowDialog();
            //updateWindow.Show();
            Console.ReadLine();
        }
    }
}