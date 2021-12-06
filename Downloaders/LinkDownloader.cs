using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Parade.Downloaders
{
    public class LinkDownloader : IDownloader
    {
        private static readonly string[] ALLOWED_PROTOCOLS =
        {
            "http",
            "https"
        };
        private List<DownloadThread> _workers;
        private string _currentFile;
        private uint _blockSize;

        public DownloadHandler Handler => throw new NotImplementedException();

        public LinkDownloader(ParadeManager parade)
        {
            /*
             * def. blockSize = 5 * 1024^2 = 5 MiB
             *      maxThreads = 16
             */
            //_blockSize = blockSize;
        }

        public void Download(IDownloadable downloadable)
        {
            if (!IsDownloadable(downloadable))
                throw new Exception("LinkDownloader cannot handle this protocol.");
            using (HttpClient client = new())
            {

            }
            throw new NotImplementedException();
        }

        public static bool IsDownloadable(IDownloadable downloadable)
        {
            if (downloadable.Metadata.Source == "")
                return false;
            Metadata metadata = downloadable.Metadata;
            Match regex = Regex.Match(downloadable.Metadata.Source, RegexPatterns.URL);

            // default protocol for protocol-less links is https:
            string protocol = "https";
            if (regex.Groups[1].Success)
            {
                // protocol found
                protocol = regex.Groups[1].Value;
            }

            // if protocol is within allowed protocols
            return ALLOWED_PROTOCOLS.Contains(protocol.ToLower());
        }
    }
}
