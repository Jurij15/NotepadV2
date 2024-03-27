using ABI.Windows.AI.MachineLearning;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using SimplePad.Pages;
using SimplePad.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using WinUIEx;

namespace SimplePad.Services
{
    public class TabService
    {
        private TabView _tabView;
        private int _tabCount;

        MainWindow mainWindow;

        TypedEventHandler<UIElement, ContextRequestedEventArgs> TabViewItemContextRequested;

        public TabService(TabView tabView, MainWindow wnd, TypedEventHandler<UIElement, ContextRequestedEventArgs> TViewContextReq)
        {
            _tabView = tabView;
            mainWindow = wnd;   
            TabViewItemContextRequested = TViewContextReq;
        }

        public int GetTabCount()
        {
            return _tabCount;
        }

        public TabViewItem AddNewEditorTab(TabService _tabService, ConfigService _configService, FileService _fileService)
        {
            TabViewItem item = new TabViewItem();
            TabbedEditorPage page = new TabbedEditorPage(_tabService, _configService, _fileService);
            Frame f = new Frame();
            TabTagStruct tag = new TabTagStruct();

            tag.GUID = Guid.NewGuid().ToString();
            tag.CanRename = true;
            tag.Saved = true;

            f.Content = page;
            item.Header = "New Tab";
            item.Content = f;
            item.Tag = tag;

            item.ContextRequested += TabViewItemContextRequested;

            _tabView.TabItems.Add(item);
            _tabCount++;
            _tabView.SelectedItem = item;

            return item;
        }

        public void AddNewSettingsTab(TabService _tabService, ConfigService _configService)
        {
            TabViewItem item = new TabViewItem();
            SettingsPage page = new SettingsPage(_tabService, _configService);
            Frame f = new Frame();
            TabTagStruct tag = new TabTagStruct();

            tag.GUID = Guid.NewGuid().ToString();
            tag.CanRename = false;
            tag.Saved = true;

            f.Content = page;
            item.Header = "Settings";
            item.Content = f;
            item.Tag = tag;

            bool canAdd = true;
            foreach (TabViewItem tab in _tabView.TabItems)
            {
                if (tab.Header.ToString() == "Settings")
                {
                    _tabView.SelectedItem = tab;
                    canAdd = false;
                    break;
                }
            }

            if (!canAdd)
            {
                return;
            }

            _tabView.TabItems.Add(item);
            _tabCount++;
            _tabView.SelectedItem = item;
        }

        public void CloseTab(TabViewItem tab)
        {
            foreach (TabViewItem item in _tabView.TabItems)
            {
                if ((item.Tag as TabTagStruct).GUID == (tab.Tag as TabTagStruct).GUID)
                {
                    _tabView.TabItems.Remove(item);
                    break;
                }
            }
        }

        public void RenameTag(string GUID, string NewName)
        {
            foreach (TabViewItem item in _tabView.TabItems)
            {
                item.Header = NewName;
            }
        }

        public TabViewItem GetCurrentTab()
        {
            return _tabView.SelectedItem as TabViewItem;
        }

        public TabTagStruct GetCurrentTabTag()
        {
            return (_tabView.SelectedItem as TabViewItem).Tag as TabTagStruct;
        }

        public void SetCurrentTabTitle(string NewTitle)
        {
            GetCurrentTab().Header = NewTitle;
            mainWindow.SetTitle(NewTitle);
        }
    }
}
