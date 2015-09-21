namespace V5.Library.Reflecter.Constructor
{
    using System.Reflection;

    public class ConstructorInvokerFactory : IReflectionFactory<ConstructorInfo, IConstructorInvoker>
    {
        public IConstructorInvoker Create(ConstructorInfo key)
        {
            return new ConstructorInvoker(key);
        }

        IConstructorInvoker IReflectionFactory<ConstructorInfo, IConstructorInvoker>.Create(ConstructorInfo key)
        {
            return this.Create(key);
        }
    }
}