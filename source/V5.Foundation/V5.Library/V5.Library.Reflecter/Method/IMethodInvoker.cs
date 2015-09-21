namespace V5.Library.Reflecter.Method
{
    public interface IMethodInvoker
    {
        object Invoke(object instance, params object[] parameters);
    }
}