using Microsoft.UI.Xaml.Controls;
using SimplePad.Pages;
using SimplePad.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplePad.Services
{
    public class TabService
    {
        private TabView _tabView;
        private int _tabCount;

        public TabService(TabView tabView)
        {
            _tabView = tabView;
        }

        public int GetTabCount()
        {
            return _tabCount;
        }

        public void AddNewEditorTab(TabService _tabService)
        {
            TabViewItem item = new TabViewItem();
            TabbedEditorPage page = new TabbedEditorPage(_tabService);
            Frame f = new Frame();
            TabTagStruct tag = new TabTagStruct();

            tag.GUID = Guid.NewGuid().ToString();
            tag.CanRename = true;
            tag.Saved = true;

            f.Content = page;
            item.Header = "New Tab";
            item.Content = f;
            item.Tag = tag;

            _tabView.TabItems.Add(item);
            _tabCount++;
            _tabView.SelectedItem = item;
        }

        public void AddNewSettingsTab(TabService _tabService)
        {
            TabViewItem item = new TabViewItem();
            SettingsPage page = new SettingsPage(_tabService);
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
            _tabView.TabItems.Remove(tab);
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
    }
}
