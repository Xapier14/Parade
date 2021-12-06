using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parade
{
    public interface IDownloader
    {
        public string Handler { get; }
#nullable enable
        public void Download(IDownloadable downloadable, string? destination);
        public static bool IsDownloadable(IDownloadable downloadable) => false;
        public void Start(IDownloadable downloadable);
        public void Stop(IDownloadable downloadable);
        public void Abort(IDownloadable downloadable);
    }
}
