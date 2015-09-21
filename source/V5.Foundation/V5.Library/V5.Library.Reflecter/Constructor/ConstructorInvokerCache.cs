namespace V5.Library.Reflecter.Constructor
{
    using System.Reflection;
    
    public class ConstructorInvokerCache : ReflectionCache<ConstructorInfo, IConstructorInvoker>
    {
        protected override IConstructorInvoker Create(ConstructorInfo key)
        {
            return ReflectionFactories.ConstructorInvokerFactory.Create(key);
        }
    }
}