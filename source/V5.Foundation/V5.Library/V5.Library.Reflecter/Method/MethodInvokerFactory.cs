namespace V5.Library.Reflecter.Method
{
    using System.Reflection;
    
    public class MethodInvokerFactory : IReflectionFactory<MethodInfo, IMethodInvoker>
    {
        public IMethodInvoker Create(MethodInfo key)
        {
            return new MethodInvoker(key);
        }

        IMethodInvoker IReflectionFactory<MethodInfo, IMethodInvoker>.Create(MethodInfo key)
        {
            return this.Create(key);
        }
    }
}