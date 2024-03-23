using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using SimplePad.Enums;
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
        
        public EditorTheme GetEditorBackground()
        {
            EditorTheme theme = EditorTheme.Solid; //default

            try
            {
                theme = (EditorTheme)Convert.ToInt32(ApplicationData.Current.LocalSettings.Values["EditorTheme"]);
            }
            catch (Exception) // it probably does not exist, create one
            {
                SetEditorTheme(theme);
            }

            return theme;
        }

        public void SetEditorTheme(EditorTheme theme)
        {
            try
            {
                ApplicationData.Current.LocalSettings.Values["EditorTheme"] = Convert.ToInt32(theme).ToString();
            }
            catch (Exception)
            {
            }
        }

        public bool GetAutoSetLanguage()
        {
            bool res = true; //default

            try
            {
                res = (bool)Convert.ToBoolean(ApplicationData.Current.LocalSettings.Values["AutoSetLanguage"]);
            }
            catch (Exception) // it probably does not exist, create one
            {
                SetAutoSetLanguage(res);
            }

            return res;
        }

        public void SetAutoSetLanguage(bool value)
        {
            try
            {
                ApplicationData.Current.LocalSettings.Values["AutoSetLanguage"] = Convert.ToBoolean(value).ToString();
            }
            catch (Exception)
            {
            }
        }

        public bool GetShowAutoSetLanguageWarning()
        {
            bool res = false; //default

            try
            {
                res = (bool)Convert.ToBoolean(ApplicationData.Current.LocalSettings.Values["ShowAutoSetLanguageWarning"]);
            }
            catch (Exception) // it probably does not exist, create one
            {
                SetShowAutoSetLanguageWarning(res);
            }

            return res;
        }

        public void SetShowAutoSetLanguageWarning(bool value)
        {
            try
            {
                ApplicationData.Current.LocalSettings.Values["ShowAutoSetLanguageWarning"] = Convert.ToBoolean(value).ToString();
            }
            catch (Exception)
            {
            }
        }
    }
}
