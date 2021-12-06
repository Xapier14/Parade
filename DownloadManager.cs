using System;
using System.Linq;

namespace Parade
{
    public class ParadeManager
    {
        private int _maxThreads;
        private Type[] _extraDownloaders;
        public ParadeManager(int maxThreads = 12)
        {
            if (maxThreads <= 0)
                throw new ArgumentOutOfRangeException(nameof(maxThreads) + " must be greater than 0.");
            _maxThreads = maxThreads;
            _extraDownloaders = Loader.LoadDownloaders();
        }

        private Type[] GetDownloaders()
        {
            var type = typeof(IDownloader);
            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p) && p.IsClass && !p.IsAbstract).ToArray();
        }

        public IDownloader GetSpecialDownloader(IDownloadable downloadable)
        {
            var downloaders = GetDownloaders();
            var available = downloaders.Where(downloader => 
                {
                    if (downloader.Name == "LinkDownloader")
                        return false;
                    var method = downloader.GetMethod("IsDownloadable");
                    if (method != null)
                    {
                        try
                        {
                            var result = method.Invoke(null, new object[] { downloadable });
                            if (result.Equals(true))
                                return true;
                        } catch (Exception ex)
                        {

                        }
                    }
                    return false;
                }
            ).ToArray();

            if (available.Count() > 0)
            {
                foreach (var type in available)
                {
                    var constructor = type.GetConstructor(new Type[] { typeof(ParadeManager) });
                    if (constructor != null)
                    {
                        return (IDownloader)constructor.Invoke(new object[]{this});
                    }
                }
                return null;
            }
            else
            {
                return null;
            } 
        }
    }
}
