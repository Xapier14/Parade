using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parade.Downloadables
{
    public class Link : IDownloadable
    {
        private readonly Metadata _metadata;
        private readonly string _location;
        public string Location
        {
            get => _location;
        }

        public Link(string url, string location)
        {
            _metadata = new Metadata();
            _metadata.Source = url;
            _metadata.Handler = DownloadHandler.URL;
            _location = location;
        }

        public double Progress {
            get
            {
                // do stuff here
                return 0.0;
            }
        }

        public Metadata Metadata { get
            {
                return _metadata;
            }
        }
    }
}
