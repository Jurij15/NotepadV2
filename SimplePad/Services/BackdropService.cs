using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;

namespace SimplePad.Services;

public static class BackdropService
{
    public static Window MainWindow;
    public enum Backdrop
    {
        Mica,
        MicaAlt,
        Acrylic,
        None,
    }
    public static void SetBackdrop(Backdrop Backdrop)
    {
        switch (Backdrop)
        {
            case Backdrop.Mica:
                MainWindow.SystemBackdrop = null;
                (MainWindow.Content as Grid).Background = new SolidColorBrush(Microsoft.UI.Colors.Transparent);
                MicaBackdrop micaBackdrop = new MicaBackdrop();
                micaBackdrop.Kind = Microsoft.UI.Composition.SystemBackdrops.MicaKind.Base;

                MainWindow.SystemBackdrop = micaBackdrop;
                break;
            case Backdrop.MicaAlt:
                MainWindow.SystemBackdrop = null;
                (MainWindow.Content as Grid).Background = new SolidColorBrush(Microsoft.UI.Colors.Transparent);
                MicaBackdrop micaaltBackdrop = new MicaBackdrop();
                micaaltBackdrop.Kind = Microsoft.UI.Composition.SystemBackdrops.MicaKind.BaseAlt;

                MainWindow.SystemBackdrop = micaaltBackdrop;
                break;

            case Backdrop.Acrylic:
                MainWindow.SystemBackdrop = null;
                (MainWindow.Content as Grid).Background = new SolidColorBrush(Microsoft.UI.Colors.Transparent);
                DesktopAcrylicBackdrop backdrop = new DesktopAcrylicBackdrop();

                MainWindow.SystemBackdrop = backdrop;
                break;

            case Backdrop.None:
                MainWindow.SystemBackdrop = null;
                (MainWindow.Content as Grid).Background = App.Current.Resources["ApplicationPageBackgroundThemeBrush"] as SolidColorBrush;
                break;

            default:
                MainWindow.SystemBackdrop = null;
                (MainWindow.Content as Grid).Background = new SolidColorBrush(Microsoft.UI.Colors.Transparent);
                MicaBackdrop alt = new MicaBackdrop();
                alt.Kind = Microsoft.UI.Composition.SystemBackdrops.MicaKind.BaseAlt;

                MainWindow.SystemBackdrop = alt;
                break;
        }
    }
}