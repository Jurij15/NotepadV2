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
using LogSharper;
using ModernWpf;
using ModernWpf.Controls;
using NotepadV2.Common;
using ContentDialog = ModernWpf.Controls.ContentDialog;

namespace NotepadV2.Dialogs
{
    /// <summary>
    /// Interaction logic for PreferencesDialog.xaml
    /// </summary>
    public partial class PreferencesDialog : ContentDialog
    {
        void Init()
        {
            if (Global.Theme.Contains("Light"))
            {
                LightModeRadio.IsChecked = true;
                DarkModeRadio.IsChecked = false;
            }
            else if (Global.Theme.Contains("Dark"))
            {
                LightModeRadio.IsChecked = false;
                DarkModeRadio.IsChecked = true;
            }
        }
        public PreferencesDialog()
        {
            InitializeComponent();
            Init();
        }

        private void DarkModeRadio_Checked(object sender, RoutedEventArgs e)
        {
            if (Global.Theme.Contains("Dark"))
            {
                ModernWpf.ThemeManager.Current.ApplicationTheme = ModernWpf.ApplicationTheme.Dark;
                Global.Theme = "Dark";
                Logger.Success("Theme changed to" + Global.Theme);
            }
            else if (Global.Theme.Contains("Light"))
            {
                ModernWpf.ThemeManager.Current.ApplicationTheme = ModernWpf.ApplicationTheme.Dark;
                Global.Theme = "Dark";
                Logger.Success("Theme changed to" + Global.Theme);
            }
        }

        private void LightModeRadio_Checked(object sender, RoutedEventArgs e)
        {
            if (Global.Theme.Contains("Dark"))
            {
                ModernWpf.ThemeManager.Current.ApplicationTheme = ModernWpf.ApplicationTheme.Light;
                Global.Theme = "Light";
                Logger.Success("Theme changed to" + Global.Theme);
            }
            else if (Global.Theme.Contains("Light"))
            {
                ModernWpf.ThemeManager.Current.ApplicationTheme = ModernWpf.ApplicationTheme.Light;
                Global.Theme = "Light";
                Logger.Success("Theme changed to" + Global.Theme);
            }
        }
    }
}
