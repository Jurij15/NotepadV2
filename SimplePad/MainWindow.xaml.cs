using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using SimplePad.Pages;
using SimplePad.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using SimplePad.Structs;
using WinUIEx;
using TabViewItem = ABI.Microsoft.UI.Xaml.Controls.TabViewItem;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace SimplePad
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : WinUIEx.WindowEx
    {
        private TabService _tabService;
        private ConfigService _configService;
        void InitDesign()
        {
            ExtendsContentIntoTitleBar = true;
            SetTitleBar(CustomDragRegion);

            this.Title = "SimplePad";

            this.SetWindowSize(1085, 700);
            //this.MinWidth = 1285;
            //this.MinHeight = 685;
            this.CenterOnScreen();
        }

        public MainWindow()
        {
            this.InitializeComponent();
            InitDesign();
        }

        private void RootGrid_AddTabButtonClick(TabView sender, object args)
        {
            _tabService.AddNewEditorTab(_tabService, _configService);
        }

        private void RootGrid_TabCloseRequested(TabView sender, TabViewTabCloseRequestedEventArgs args)
        {
            if (!(args.Tab.Tag as TabTagStruct).Saved)
            {
                //show a warning for closing
            }

            _tabService.CloseTab(args.Tab);
        }

        private void RootGrid_Loaded(object sender, RoutedEventArgs e)
        {
            _tabService = new TabService(RootGrid);
            _configService = new();

            //apply theme
            ThemeService.ChangeTheme(_configService.GetTheme());
            BackdropService.SetBackdrop(_configService.GetBackdrop());

            //check for hometab
            _tabService.AddNewEditorTab(_tabService, _configService);
        }

        private void RootGrid_ContextRequested(UIElement sender, ContextRequestedEventArgs args)
        {

        }
    }
}
