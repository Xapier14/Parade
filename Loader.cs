using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Parade
{
    internal static class Loader
    {
        public static Type[] LoadDownloaders()
        {
            string dir = Environment.CurrentDirectory + @"\downloaders";
            if (!Directory.Exists(dir))
            { 
                Directory.CreateDirectory(dir);
            }
            var dlls = (new DirectoryInfo(dir)).GetFiles();
            List<Type> downloaders = new();
            foreach (var dll in dlls)
            {
                if (dll.Extension == ".dll")
                {
                    var interf = typeof(IDownloader);
                    var loadedFile = Assembly.LoadFile(dll.FullName);
                    downloaders.AddRange(loadedFile.GetTypes().Where(obj =>
                        {
                            bool fromInterface = obj.IsAssignableFrom(interf);
                            return interf.IsAssignableFrom(obj) && obj.IsClass && !obj.IsAbstract;
                        }
                    ));
                }
            }
            return downloaders.ToArray();
        }
    }
}
