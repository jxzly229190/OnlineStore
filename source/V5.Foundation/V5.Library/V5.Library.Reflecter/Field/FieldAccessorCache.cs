namespace V5.Library.Reflecter.Field
{
    using System.Reflection;
    
    public class FieldAccessorCache : ReflectionCache<FieldInfo, IFieldAccessor>
    {
        protected override IFieldAccessor Create(FieldInfo key)
        {
            return ReflectionFactories.FieldAccessorFactory.Create(key);
        }
    }
}