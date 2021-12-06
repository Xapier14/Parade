using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parade
{
    public interface IDownloader
    {
        public DownloadHandler Handler { get; }
        public void Download(IDownloadable downloadable);
        public static bool IsDownloadable(IDownloadable downloadable) => false;
    }
}
