namespace V5.Library.Reflecter.Method
{
    using System.Reflection;

    public class MethodInvokerCache : ReflectionCache<MethodInfo, IMethodInvoker>
    {
        protected override IMethodInvoker Create(MethodInfo key)
        {
            return ReflectionFactories.MethodInvokerFactory.Create(key);
        }
    }
}