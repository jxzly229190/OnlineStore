namespace V5.Library.Reflecter
{
    using System.Collections.Generic;
    using System.Threading;
    
    public abstract class ReflectionCache<TKey, TValue> : IReflectionCache<TKey, TValue>
    {
        private readonly Dictionary<TKey, TValue> cacheDictionary = new Dictionary<TKey, TValue>();

        private readonly ReaderWriterLockSlim readerWriterLockSlim = new ReaderWriterLockSlim();

        public TValue Get(TKey key)
        {
            TValue value;

            this.readerWriterLockSlim.EnterReadLock();
            bool hit = this.cacheDictionary.TryGetValue(key, out value);
            this.readerWriterLockSlim.ExitReadLock();

            if (hit)
            {
                return value;
            }

            this.readerWriterLockSlim.EnterWriteLock();
            if (!this.cacheDictionary.TryGetValue(key, out value))
            {
                try
                {
                    value = this.Create(key);
                    this.cacheDictionary[key] = value;
                }
                finally
                {
                    this.readerWriterLockSlim.ExitWriteLock();
                }
            }

            return value;
        }

        protected abstract TValue Create(TKey key);
    }
}