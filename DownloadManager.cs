using System;

namespace Parade
{
    public class ParadeManager
    {
        private int _maxThreads;
        public ParadeManager(int maxThreads = 12)
        {
            if (maxThreads <= 0)
                throw new ArgumentOutOfRangeException(nameof(maxThreads) + " must be greater than 0.");
            _maxThreads = maxThreads;
        }
    }
}
