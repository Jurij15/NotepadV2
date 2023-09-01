using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplePad.Services
{
    public class DialogService
    {
        public static XamlRoot XamlRoot;
        public static async void ShowDialog(object Message, string Title, string CloseButtonText = "Close")
        {
            ContentDialog dialog = new ContentDialog();
            dialog.XamlRoot = XamlRoot;
            dialog.Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style;
            dialog.Title = Title;
            dialog.Content = Message;

            dialog.CloseButtonText = CloseButtonText;

            await dialog.ShowAsync();
        }
    }
}
