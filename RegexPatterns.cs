using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parade
{
    internal static class RegexPatterns
    {
        public static readonly string Youtube = @"h?t?t?p?s?:?\/?\/?w?w?w\.?youtube.com\/(watch|playlist)\?(v|list)=(.+)";
        public static readonly string URL = @"(?:(.+):\/\/)?(.*)";
    }
}
