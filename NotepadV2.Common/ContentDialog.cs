using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using ModernWpf.Controls;

namespace NotepadV2.Common
{
    public class ContentDialog
    {
        public static void ShowContentDialog(string Title, string Content, string CloseButtonText, bool bFullScreen)
        {
            ModernWpf.Controls.ContentDialog dialog = new ModernWpf.Controls.ContentDialog();
            dialog.Title = Title;
            dialog.Content = Content;
            dialog.CloseButtonText = CloseButtonText;
            dialog.FullSizeDesired = bFullScreen;
            dialog.ShowAsync();
        }

        public static void ShowPreferencesDialog()
        {
            var container = new StackPanel();

            CheckBox checkBox = new CheckBox()
            {
                //TODO:Finish this in a seperated class
            };

            //container.Children.Add(Testbox);

            ModernWpf.Controls.ContentDialog dialog = new ModernWpf.Controls.ContentDialog();
            dialog.Title = "Preferences";
            dialog.Content = container;
            dialog.FullSizeDesired = true;
            dialog.ShowAsync();
        }
    }
}
