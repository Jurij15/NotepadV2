using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using SimplePad.Helpers;
using SimplePad.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Gaming.Input.ForceFeedback;
using Windows.Security.Isolation;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI;
using WinUIEditor;
using WinUIEx;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace SimplePad.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TabbedEditorPage : Page
    {
        /// <summary>
        /// We can change between a more translucent background and this more opaque background
        /// ApplicationPageBackgroundThemeBrush
        /// CardBackgroundFillColorDefaultBrush
        /// </summary>

        private TabService _tabService;
        private ConfigService _configService;
        public TabbedEditorPage(TabService tabService, ConfigService config)
        {
            this.InitializeComponent();
            _tabService = tabService;
            _configService = config;
        }

        void ApplyTheme()
        {
            string Theme = "ControlFillColorTertiaryBrush"; //semi translucent
            //string Theme = "SolidBackgroundFillColorTertiaryBrush"; //solid (does not raect to theme changes)
            //string Theme = "SubtleFillColorTertiaryBrush"; //more solid

            CodeEditor.RequestedTheme = _configService.GetTheme();
            CodeEditor.Background = Application.Current.Resources[Theme] as SolidColorBrush;
        }

        private void CodeEditorControl_Loaded(object sender, RoutedEventArgs e)
        {
            //CodeEditor.HighlightingLanguage = "csharp";
            CodeEditor.Editor.WrapMode = WinUIEditor.Wrap.Word;
            WordWrapToggle.IsChecked = true;
            ApplyTheme();
            //CodeEditor.Editor.SetText(_configService.GetTheme().ToString());
        }

        private void SettingsBtn_Click(object sender, RoutedEventArgs e)
        {
            _tabService.AddNewSettingsTab(_tabService, _configService);
        }

        #region Flyout Event Handlers
        private void LangFlyoutItem_Click(object sender, RoutedEventArgs e)
        {
            string language = LanguageHelper.RemoveSpecialLanguageCharacters((sender as MenuFlyoutItem).Text).ToLower();

            CodeEditor.HighlightingLanguage = language;

            foreach (var item in LanguageItem.Items)
            {
                if (item.GetType() == typeof(ToggleMenuFlyoutItem))
                {
                    (item as ToggleMenuFlyoutItem).IsChecked = false;
                }
                else
                {
                    continue;
                }
            }

            (sender as ToggleMenuFlyoutItem).IsChecked = true;
        }

        private void WordWrapToggle_Click(object sender, RoutedEventArgs e)
        {
            if (WordWrapToggle.IsChecked)
            {
                CodeEditor.Editor.WrapMode = WinUIEditor.Wrap.None;
                WordWrapToggle.IsChecked = false;
            }
            else
            {
                CodeEditor.Editor.WrapMode = WinUIEditor.Wrap.Word;
                WordWrapToggle.IsChecked = true;
            }
        }

        private void NewMenuButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void OpenNewInstanceMenuButton_Click(object sender, RoutedEventArgs e)
        {
            Process p = Process.Start(Regex.Replace(System.Reflection.Assembly.GetExecutingAssembly().Location, "\\.dll", ".exe"));
        }

        private async void OpenMenuButton_Click(object sender, RoutedEventArgs e)
        {
            FileOpenPicker openPicker = new Windows.Storage.Pickers.FileOpenPicker();

            var window = ThemeService.m_window; //just pull it from there, not really ideal but it works
            var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(window);

            WinRT.Interop.InitializeWithWindow.Initialize(openPicker, hWnd);
            openPicker.FileTypeFilter.Add("*");

            // Open the picker for the user to pick a file
            var file = await openPicker.PickSingleFileAsync();
            var text = await FileIO.ReadTextAsync(file);

            CodeEditor.Editor.SetText(text);
        }

        private void SaveMenuButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SaveAsMenuButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CloseTabMenuButton_Click(object sender, RoutedEventArgs e)
        {
            //TODO: check if file is saved
            _tabService.CloseTab(_tabService.GetCurrentTab());
        }

        private void ExitMenuButton_Click(object sender, RoutedEventArgs e)
        {
            //TODO: check if file saved
            Application.Current.Exit();
        }

        private void UndoMenuButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RedoMenuButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CutMenuButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CopyMenuButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PasteMenuButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SelectAllMenuButton_Click(object sender, RoutedEventArgs e)
        {

        }
        #endregion
    }
}
