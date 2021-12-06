using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parade
{
    public enum Status
    {
        Unprocessed,
        Processed,
        Downloading,
        Aborted,
        Failed,
        Paused,
        Finished
    }
}
