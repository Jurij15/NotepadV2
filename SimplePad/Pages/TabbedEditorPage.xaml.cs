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
using System.Linq;
using System.IO;
using Windows.Storage.Streams;
using Windows.ApplicationModel.UserDataTasks;


// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace SimplePad.Pages
{
    public sealed partial class TabbedEditorPage : Page
    {
        /// <summary>
        /// We can change between a more translucent background and this more opaque background
        /// ApplicationPageBackgroundThemeBrush
        /// CardBackgroundFillColorDefaultBrush
        /// </summary>

        private readonly TabService _tabService;
        private readonly ConfigService _configService;
        private readonly FileService _fileService;

        private StorageFile _currentFile;
        private string _currentLanguage;
        public TabbedEditorPage(TabService tabService, ConfigService config, FileService fileService)
        {
            this.InitializeComponent();
            _tabService = tabService;
            _configService = config;
            _fileService = fileService;
        }

        void ApplyTheme()
        {
            string Theme = "SubtleFillColorTertiaryBrush"; //semi translucent
            string SolidTheme = "SystemControlBackgroundChromeMediumLowBrush"; //solid (does not react to theme changes)

            //NOTE: UNCOMMENT THIS TO GET THEME CHANGES WORKING AGAIN
            if (_configService.GetEditorBackground() == EditorTheme.Translucent)
            {
                //EditorBackgroundGrid.Background = Application.Current.Resources[Theme] as SolidColorBrush;
            }
            else
            {
                //EditorBackgroundGrid.Background = Application.Current.Resources[SolidTheme] as SolidColorBrush;
            }
        }

        private void SetZoomSlider()
        {
            ZoomSlider.Maximum = 282;

            CodeEditor.Editor.ZoomChanged += Editor_ZoomChanged;

            int size = CodeEditor.Editor.StyleGetSizeFractional((int)StylesCommon.Default);
            double num = ((size + CodeEditor.Editor.Zoom * 100) * 100.0 / size);
            int amount = (int)Math.Round(num);
            ZoomAmountBox.Text = amount.ToString();

            ZoomSlider.Value = amount;
        }

        private async Task PickSetSaveFile()
        {
            _currentFile = await _fileService.GetSaveFile();
        }

        private async Task SaveToCurrentFile()
        {
            long lenght = CodeEditor.Editor.Length;
            IBuffer buffer = _fileService.GetBufferFromLong(lenght);
            long text = CodeEditor.Editor.GetTextWriteBuffer(lenght, buffer);

            await _fileService.SaveToStorageFile(_currentFile, text, buffer);
            CodeEditor.Editor.SetSavePoint();
        }

        private void Editor_ZoomChanged(Editor sender, ZoomChangedEventArgs args)
        {
            int size = CodeEditor.Editor.StyleGetSizeFractional((int)StylesCommon.Default);
            double num = ((size + CodeEditor.Editor.Zoom * 100) * 100.0 / size);
            int amount = (int)Math.Round(num);
            ZoomAmountBox.Text = amount.ToString();

            ZoomSlider.Value = amount;
        }

        private void CodeEditorControl_Loaded(object sender, RoutedEventArgs e)
        {
            CodeEditor.Editor.WrapMode = Wrap.Word;
            ApplyTheme();
            SetZoomSlider();

            CodeEditor.Editor.UpdateUI += Editor_UpdateUI;
            CodeEditor.FontFamily = new FontFamily("Arial");

            long pos = CodeEditor.Editor.CurrentPos;
            CaretPosBox.Text = $"Ln {CodeEditor.Editor.LineFromPosition(pos)}, Col {CodeEditor.Editor.GetColumn(pos)}";
        }

        private void Editor_UpdateUI(Editor sender, UpdateUIEventArgs args)
        {
            //update caret position
            long pos = CodeEditor.Editor.CurrentPos;
            CaretPosBox.Text = $"Ln {CodeEditor.Editor.LineFromPosition(pos)}, Col {CodeEditor.Editor.GetColumn(pos)}";
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

            if (file == null)
            {
                return; // no file opened
            }

            _currentFile = file;

            var text = await FileIO.ReadTextAsync(file);

            CodeEditor.Editor.SetText(text);
            CodeEditor.Focus(FocusState.Keyboard);

            _tabService.SetCurrentTabTitle(file.Name);

            //auto set language
            if (_configService.GetAutoSetLanguage())
            {
                string language = LanguageDefinitions.GetLanguageNameFromFileEnding(file.FileType.Replace(".", ""));
                bool shouldShowInfo = false;

                if (language is not null && language != string.Empty)
                {
                    shouldShowInfo = true; //only show if lang detected
                }

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
                _currentLanguage = LanguageDefinitions.GetLanguageDisplayNameFromName(language);

                if (_configService.GetShowAutoSetLanguageWarning() && shouldShowInfo)
                {
                    AutoDetectedFileType.IsOpen = false;
                    AutoDetectedFileType.Title = $"Language recognized as {_currentLanguage}";
                    AutoDetectedFileType.IsOpen = true;
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
            LanguageItem.Items.Clear(); //for some reason, this does actually not clear the list

            foreach (var item in LanguageDefinitions.GetAllDisplayNames())
            {
                ToggleMenuFlyoutItem control = new ToggleMenuFlyoutItem();
                control.Text = item;
                control.Click += LangFlyoutItem_Click;

                if (item == "Plain Text") //select plain text by default
                {
                    control.IsChecked = true;
                }

                LanguageItem.Items.Add(control);
            }
        }

        private void LanguageItem_Unloaded(object sender, RoutedEventArgs e)
        {
            //the stupidest code i probably ever had to write, but it works
            /*
             * LanguageItem.Items.Clear(); this does absolutely not work, so i have to do it manually
             * but for some reason, clearing the array once does not work, neither does two times, only 3 times works
             */
            foreach (var item in LanguageItem.Items)
            {
                LanguageItem.Items.Remove(item);
            }
            foreach (var item in LanguageItem.Items)
            {
                LanguageItem.Items.Remove(item);
            }
            foreach (var item in LanguageItem.Items)
            {
                LanguageItem.Items.Remove(item);
            }

            //await Task.Delay(100);
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

        private async void SaveMenuButton_Click(object sender, RoutedEventArgs e)
        {
            if (_currentFile != null)
            {
                //file already exists, delete it and save it

                await SaveToCurrentFile();
            }
            else
            {
                await PickSetSaveFile();
            }
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

        private void ZoomSlider_ValueChanged(object sender, Microsoft.UI.Xaml.Controls.Primitives.RangeBaseValueChangedEventArgs e)
        {

        }

        private void RestoreDefaultZoom_Click(object sender, RoutedEventArgs e)
        {
            CodeEditor.Editor.Zoom = 0;
        }
    }
}
