namespace V5.Library.Reflecter
{
    public interface IReflectionCache<in TKey, out TValue>
    {
        TValue Get(TKey key);
    }
}