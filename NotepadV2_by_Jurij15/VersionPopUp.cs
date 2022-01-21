using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NotepadV2_by_Jurij15
{
    internal class VersionPopUp
    {
        public void VerPopUp()
        {
            Version version = new Version();
            MessageBox.Show("Notepad_by_Jurij15, version {0}", version.VersionString);
        }
    }
}
