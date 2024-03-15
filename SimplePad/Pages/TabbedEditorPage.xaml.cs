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
        public TabbedEditorPage(TabService tabService)
        {
            this.InitializeComponent();
            _tabService = tabService;
        }

        void ApplyTheme()
        {
            //string Theme = "CardBackgroundFillColorDefaultBrush"; //translucent
            string Theme = "ApplicationPageBackgroundThemeBrush"; //solid

            CodeEditor.Background = Application.Current.Resources[Theme] as SolidColorBrush;
        }

        private void CodeEditorControl_Loaded(object sender, RoutedEventArgs e)
        {
            //CodeEditor.HighlightingLanguage = "csharp";
            CodeEditor.Editor.WrapMode = WinUIEditor.Wrap.Char;
            ApplyTheme();

        }

        private void SettingsBtn_Click(object sender, RoutedEventArgs e)
        {
            _tabService.AddNewSettingsTab(_tabService);
        }
    }
}
