namespace V5.Library.Reflecter.Constructor
{
    public interface IConstructorInvoker
    {
        object Invoke(params object[] parameters);
    }
}