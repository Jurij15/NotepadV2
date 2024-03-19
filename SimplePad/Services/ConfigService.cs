using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using static SimplePad.Services.BackdropService;

namespace SimplePad.Services
{
    public class ConfigService
    {
        public ElementTheme GetTheme()
        {
            ElementTheme theme = ElementTheme.Dark; //default

            try
            {
                theme = (ElementTheme)Convert.ToInt32(ApplicationData.Current.LocalSettings.Values["Theme"]);
            }
            catch (Exception) // it probably does not exist, create one
            {
                SetTheme(theme);
            }

            return theme;
        }

        public void SetTheme(ElementTheme theme)
        {
            try
            {
                ApplicationData.Current.LocalSettings.Values["Theme"] = Convert.ToInt32(theme).ToString();
            }
            catch (Exception)
            {
            }
        }

        public Backdrop GetBackdrop()
        {
            Backdrop theme = Backdrop.MicaAlt; //default

            try
            {
                theme = (Backdrop)Convert.ToInt32(ApplicationData.Current.LocalSettings.Values["Backdrop"]);
            }
            catch (Exception) // it probably does not exist, create one
            {
                SetBackdrop(theme);
            }

            return theme;
        }

        public void SetBackdrop(Backdrop theme)
        {
            try
            {
                ApplicationData.Current.LocalSettings.Values["Backdrop"] = Convert.ToInt32(theme).ToString();
            }
            catch (Exception)
            {
            }
        }
    }
}
