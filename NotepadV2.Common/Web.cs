using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Net;
using System.Diagnostics;
using LogSharper;

namespace NotepadV2.Common
{
    public class Web
    {
        public static void OpenGitRepoInDefaultBrowser()
        {
            Process git = new Process();
            git.StartInfo.UseShellExecute = true;
            git.StartInfo.FileName = "https://github.com/Jurij15/NotepadV2";
            git.Start();
            Logger.Success("Opened the GitHub Repo!");
        }
    }
}
