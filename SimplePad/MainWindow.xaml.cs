using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using SimplePad.Services;
using SimplePad.Interop;
using SimplePad.Structs;
using WinUIEx;
using Microsoft.UI.Xaml.Controls.Primitives;
using UnhandledExceptionEventArgs = Microsoft.UI.Xaml.UnhandledExceptionEventArgs;
using Windows.Foundation;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace SimplePad
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : WindowEx
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
            DialogService.XamlRoot = RootGrid.XamlRoot;
        }

        public MainWindow()
        {
            this.InitializeComponent();
            InitDesign();
            
            Application.Current.UnhandledException += CurrentOnUnhandledException;
        }

        private void CurrentOnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            MessageBox.Show(e.Message,"Unhandled Exception");
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
            _tabService = new TabService(RootTabs);
            _configService = new();

            //apply theme
            ThemeService.ChangeTheme(_configService.GetTheme());
            BackdropService.SetBackdrop(_configService.GetBackdrop());

            //check for hometab
            _tabService.AddNewEditorTab(_tabService, _configService);
        }

        private void RootGrid_ContextRequested(UIElement sender, ContextRequestedEventArgs args)
        {
            FlyoutShowOptions options = new FlyoutShowOptions();
            Point p = new Point();
            args.TryGetPosition(sender, out p);
            options.Position = p;
            TabContextMenu.ShowAt(sender, options);
        }
    }
}
