using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Parade.Downloadables;

namespace Parade.Downloaders
{
    public class LinkDownloader : IDownloader
    {
        private static readonly string[] ALLOWED_PROTOCOLS =
        {
            "http",
            "https"
        };
        private Thread _controller;
        private List<DownloadThread> _managed;
        private Link _currentLink;
        private string _destination;
        private bool _working;
        private ParadeManager _parade;

        public string Handler => "Generic Link";

        public LinkDownloader(ParadeManager parade)
        {
            _working = false;
            _managed = new List<DownloadThread>();
            _parade = parade;
        }
#nullable enable
        public void Download(IDownloadable downloadable, string? destination)
        {
            if (!IsDownloadable(downloadable))
                throw new Exception("LinkDownloader cannot handle this protocol.");

            // set destination
            if (destination != null)
                _destination = Environment.CurrentDirectory;

            // get Filesize
            long filesize = -1;
            using (var http = new HttpClient())
            {
                http.DefaultRequestHeaders.Add("User-Agent", 
                    @"Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/96.0.4664.45 Safari/537.36"
                    );
                try
                {
                    var requestTask = http.GetAsync(downloadable.Metadata.Source, HttpCompletionOption.ResponseHeadersRead);
                    requestTask.Wait();
                    var request = requestTask.Result;
                    request.EnsureSuccessStatusCode();
                    if (request.Content.Headers.ContentLength == null)
                        throw new("Null Content Length");
                    filesize = request.Content.Headers.ContentLength.Value;
                } catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    downloadable.Status = Status.Failed;
                    return;
                    //throw new NotImplementedException(null, ex);
                }
            }

            // parsed info
            downloadable.Metadata.Handler = Handler;
            downloadable.Metadata.Size = filesize;
            downloadable.Status = Status.Processed;
        }

        public void Start(IDownloadable downloadable)
        {

        }
        public void Stop(IDownloadable downloadable)
        {

        }
        public void Abort(IDownloadable downloadable)
        {

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
