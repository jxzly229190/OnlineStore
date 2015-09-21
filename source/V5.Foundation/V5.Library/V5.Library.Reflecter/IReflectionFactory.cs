namespace V5.Library.Reflecter
{
    public interface IReflectionFactory<in TKey, out TValue>
    {
        TValue Create(TKey key);
    }
}