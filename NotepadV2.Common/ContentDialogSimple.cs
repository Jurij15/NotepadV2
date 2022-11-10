using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using ModernWpf.Controls;

namespace NotepadV2.Common
{
    public class ContentDialogSimple
    {
        public static void ShowSimpleContentDialog(string Title, string Content, string CloseButtonText, bool bFullScreen)
        {
            ModernWpf.Controls.ContentDialog dialog = new ModernWpf.Controls.ContentDialog();
            dialog.Title = Title;
            dialog.Content = Content;
            dialog.CloseButtonText = CloseButtonText;
            dialog.FullSizeDesired = bFullScreen;
            dialog.ShowAsync();
        }
    }
}
