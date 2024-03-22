using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using SimplePad.Helpers;
using SimplePad.Services;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;
using ABI.Microsoft.UI.Xaml.Controls.Primitives;
using SimplePad.Enums;
using SimplePad.Interop;
using WinUIEditor;


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

        private readonly TabService _tabService;
        private readonly ConfigService _configService;

        private StorageFile _currentFile;
        public TabbedEditorPage(TabService tabService, ConfigService config)
        {
            this.InitializeComponent();
            _tabService = tabService;
            _configService = config;
        }

        void ApplyTheme()
        {
            string Theme = "SubtleFillColorTertiaryBrush"; //semi translucent
            //string Theme = "SolidBackgroundFillColorTertiaryBrush"; //solid (does not react to theme changes)
            string SolidTheme = "SystemControlBackgroundChromeMediumLowBrush"; //solid
            
            if (_configService.GetEditorBackground() == EditorTheme.Translucent)
            {
                EditorBackgroundGrid.Background = Application.Current.Resources[Theme] as SolidColorBrush;
            }
            else
            {
                EditorBackgroundGrid.Background = Application.Current.Resources[SolidTheme] as SolidColorBrush;
            }
        }

        private void CodeEditorControl_Loaded(object sender, RoutedEventArgs e)
        {
            CodeEditor.Editor.WrapMode = Wrap.Word;
            ApplyTheme();
        }

        private async Task GetAndLoadFile()
        {
            FileOpenPicker openPicker = new FileOpenPicker();

            var window = ThemeService.m_window; //just pull it from there, not really ideal but it works
            var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(window);

            WinRT.Interop.InitializeWithWindow.Initialize(openPicker, hWnd);
            openPicker.FileTypeFilter.Add("*");

            // Open the picker for the user to pick a file
            var file = await openPicker.PickSingleFileAsync();
            _currentFile = file;

            var text = await FileIO.ReadTextAsync(file);

            CodeEditor.Editor.SetText(text);
            CodeEditor.Focus(FocusState.Keyboard);

            if (_configService.GetAutoSetLanguage())
            {
                string language = LanguageDefinitions.GetLanguageNameFromFileEnding(file.FileType.Replace(".", ""));
                CodeEditor.HighlightingLanguage = language;

                foreach (var item in LanguageItem.Items)
                {
                    if (item.GetType() == typeof(ToggleMenuFlyoutItem) && (item as ToggleMenuFlyoutItem).Text == LanguageDefinitions.GetLanguageDisplayNameFromName(language))
                    {
                        (item as ToggleMenuFlyoutItem).IsChecked = true;
                    }
                    else
                    {
                        (item as ToggleMenuFlyoutItem).IsChecked = false;
                    }
                }
            }
        }

        #region Flyout Event Handlers
        private void SettingsBtn_Click(object sender, RoutedEventArgs e)
        {
            _tabService.AddNewSettingsTab(_tabService, _configService);
        }

        private void LangFlyoutItem_Click(object sender, RoutedEventArgs e)
        {
            string language = LanguageDefinitions.GetLanguageNameFromDisplayName((sender as ToggleMenuFlyoutItem).Text);

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

        private void LanguageItem_Loaded(object sender, RoutedEventArgs e)
        {
            LanguageItem.Items.Clear(); //prepare to get all definitions

            foreach (var item in LanguageDefinitions.GetAllDisplayNames())
            {
                ToggleMenuFlyoutItem control = new ToggleMenuFlyoutItem();
                control.Text = item;
                control.Click += LangFlyoutItem_Click;

                LanguageItem.Items.Add(control);
            }
        }

        private void LanguageItem_Unloaded(object sender, RoutedEventArgs e)
        {
            //BUG: ITEMS GET DUPLICATED
            foreach (ToggleMenuFlyoutItem item in LanguageItem.Items)
            {
                LanguageItem.Items.Remove(item);
            }
        }

        private async void WordWrapToggle_Click(object sender, RoutedEventArgs e)
        {
            ToggleMenuFlyoutItem menuitem = (sender as ToggleMenuFlyoutItem);
            MessageBox.Show("loaded menuitem");
            MessageBox.Show(menuitem.IsChecked.ToString());
            if (menuitem.IsChecked)
            {
                MessageBox.Show("checked");
                menuitem.IsChecked = false;
                CodeEditor.Editor.WrapMode = Wrap.None;
            }
            else if (!menuitem.IsChecked)
            {
                menuitem.IsChecked = true;
                CodeEditor.Editor.WrapMode = Wrap.Word;
            }
            
            CodeEditor.Focus(FocusState.Keyboard);
        }

        private void NewMenuButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void OpenNewInstanceMenuButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start(Regex.Replace(Assembly.GetExecutingAssembly().Location, "\\.dll",
                    ".exe"));
            }
            catch (Exception exception)
            {
                DialogService.ShowDialog(exception.Message, "Failed to start");
            }
            finally
            {
                CodeEditor.Focus(FocusState.Keyboard);
            }
        }

        private async void OpenMenuButton_Click(object sender, RoutedEventArgs e)
        {
            await GetAndLoadFile();
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
            CodeEditor.Editor.Undo();
            CodeEditor.Focus(FocusState.Keyboard);
        }

        private void RedoMenuButton_Click(object sender, RoutedEventArgs e)
        {
            CodeEditor.Editor.Redo();
            CodeEditor.Focus(FocusState.Keyboard);
        }

        private void CutMenuButton_Click(object sender, RoutedEventArgs e)
        {
            CodeEditor.Editor.Cut();
            CodeEditor.Focus(FocusState.Keyboard);
        }

        private void CopyMenuButton_Click(object sender, RoutedEventArgs e)
        {
            CodeEditor.Editor.Copy();
            CodeEditor.Focus(FocusState.Keyboard);
        }

        private void PasteMenuButton_Click(object sender, RoutedEventArgs e)
        {
            CodeEditor.Editor.Paste();
            CodeEditor.Focus(FocusState.Keyboard);
        }

        private void SelectAllMenuButton_Click(object sender, RoutedEventArgs e)
        {
            CodeEditor.Editor.SelectAll();
            CodeEditor.Focus(FocusState.Keyboard);
        }
        #endregion
    }
}
