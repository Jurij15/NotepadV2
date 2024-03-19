using System;
using System.Runtime.InteropServices;

namespace SimplePad.Interop;

public class MessageBox
{
    public static void Show(string Content, string Title = "")
    {
        [DllImport("user32.dll")]
        static extern int MessageBox(IntPtr hWind, String text, String caption, int options);
        MessageBox(IntPtr.Zero, Content, Title, 0);
    }
}