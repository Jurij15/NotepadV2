using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplePad.Structs
{
    public class TabTagStruct
    {
        public string GUID {  get; set; }

        public bool CanRename { get; set; } = true;
        public bool Saved { get; set; } = true;
    }
}
