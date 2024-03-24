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
using SimplePad.Enums;
using static SimplePad.Services.BackdropService;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace SimplePad.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SettingsPage : Page
    {
        private TabService _tabService;
        private ConfigService _configService;

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
        public SettingsPage(TabService service, ConfigService config)
        {
            this.InitializeComponent();
            _tabService = service;
            _configService = config;
        }


        bool _themeLoaded = false;
        private void ThemeCombo_Loaded(object sender, RoutedEventArgs e)
        {
            if (!_themeLoaded)
            {
                var enumVal = Enum.GetValues(typeof(Theme)).Cast<Theme>();
                ThemeCombo.ItemsSource = enumVal;

                ThemeCombo.SelectedIndex = (int)_configService.GetTheme();
                _themeLoaded = true;
            }
        }

        private void ThemeCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_themeLoaded) //prevent accidental clicks
            {
                ElementTheme theme = (ElementTheme)((ComboBox)sender).SelectedItem;
                ThemeService.ChangeTheme(theme);

                _configService.SetTheme(theme);
            }
        }

        private void ThemeCombo_OnUnloaded(object sender, RoutedEventArgs e)
        {
            //_themeLoaded = false;
        }

        private bool _backdropLoaded = false;
        private void BackdropCombo_OnLoaded(object sender, RoutedEventArgs e)
        {
            if (!_backdropLoaded)
            {
                var enumVal = Enum.GetValues(typeof(Backdrop)).Cast<Backdrop>();
                BackdropCombo.ItemsSource = enumVal;

                BackdropCombo.SelectedItem = _configService.GetBackdrop();
                _backdropLoaded = true;
            }
        }

        private void BackdropCombo_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_backdropLoaded) //prevent accidental clicks
            {
                Backdrop theme = (Backdrop)((ComboBox)sender).SelectedItem;
                SetBackdrop(theme);

                _configService.SetBackdrop(theme);
            }
        }

        private void BackdropCombo_Unloaded(object sender, RoutedEventArgs e)
        {

        }

        private void ResetSettingsBtn_OnClick(object sender, RoutedEventArgs e)
        {
            
        }

        private void VersionBlock_OnLoaded(object sender, RoutedEventArgs e)
        {
            VersionBlock.Text = NotepadVersion.Version;
        }

        private bool _editorBackgroundLoaded = false;
        private void EditorBackgroundCombo_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_editorBackgroundLoaded) //prevent accidental clicks
            {
                EditorTheme theme = (EditorTheme)((ComboBox)sender).SelectedItem;

                _configService.SetEditorTheme(theme);
            }
        }

        private void EditorBackgroundCombo_OnLoaded(object sender, RoutedEventArgs e)
        {
            if (!_editorBackgroundLoaded)
            {
                var enumVal = Enum.GetValues(typeof(EditorTheme)).Cast<EditorTheme>();
                EditorBackgroundCombo.ItemsSource = enumVal;

                EditorBackgroundCombo.SelectedItem = _configService.GetEditorBackground();
                _editorBackgroundLoaded = true;
            }
        }
        private void EditorBackgroundCombo_OnUnloaded(object sender, RoutedEventArgs e)
        {
            
        }

        private bool _autoSetLangLoaded = false;
        private void AutoSetLangSwitch_Loaded(object sender, RoutedEventArgs e)
        {
            if (!_autoSetLangLoaded)
            {
                AutoSetLangSwitch.IsOn = _configService.GetAutoSetLanguage();
                _autoSetLangLoaded = true;
            }
        }

        private void AutoSetLangSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            if (_autoSetLangLoaded) //prevent accidental clicks
            {
                bool newstate = AutoSetLangSwitch.IsOn;

                _configService.SetAutoSetLanguage(newstate);
            }
        }

        private bool _showAutoSetLangLoaded = false;
        private void ShowAutoSetNotificationSwitch_Loaded(object sender, RoutedEventArgs e)
        {
            if (!_showAutoSetLangLoaded)
            {
                ShowAutoSetNotificationSwitch.IsOn = _configService.GetShowAutoSetLanguageWarning();
                _showAutoSetLangLoaded = true;
            }
        }

        private void ShowAutoSetNotificationSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            if (_showAutoSetLangLoaded) //prevent accidental clicks
            {
                bool newstate = ShowAutoSetNotificationSwitch.IsOn;

                _configService.SetShowAutoSetLanguageWarning(newstate);
            }
        }
    }
}
