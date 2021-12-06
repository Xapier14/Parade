using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parade
{
    public interface IDownloadable
    {
        public Status Status { get; internal set; }
        public double Progress { get; }
        public string Location { get; }
        public Metadata Metadata { get; }
    }
}
