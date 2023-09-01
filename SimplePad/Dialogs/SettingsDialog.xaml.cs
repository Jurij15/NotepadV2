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

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace SimplePad.Dialogs
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SettingsDialog : Page
    {
        public enum Theme
        {
            //
            // Summary:
            //     Use the Application.RequestedTheme value for the element. This is the default.
            Default,
            //
            // Summary:
            //     Use the **Light** default theme.
            Light,
            //
            // Summary:
            //     Use the **Dark** default theme.
            Dark
        }

        bool InitFinished = false;

        public SettingsDialog()
        {
            this.InitializeComponent();

            var enumVal = Enum.GetValues(typeof(Theme)).Cast<Theme>();
            ThemeCombo.ItemsSource = enumVal;
        }

        private void ThemeCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ElementTheme theme = (ElementTheme)((ComboBox)sender).SelectedItem;
            ThemeService.ChangeTheme(theme);
        }
    }
}
