using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parade
{
    public class Metadata
    {
        public string Source { get; set; }
        public string Handler { get; set; }
        public ushort MaxThreads { get; set; }
        public long Size { get; set; }
    }
}
