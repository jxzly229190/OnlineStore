namespace V5.Library.Reflecter.Property
{
    using System.Reflection;
    
    public class PropertyAccessorCache : ReflectionCache<PropertyInfo, IPropertyAccessor>
    {
        protected override IPropertyAccessor Create(PropertyInfo key)
        {
            return new PropertyAccessor(key);
        }
    }
}