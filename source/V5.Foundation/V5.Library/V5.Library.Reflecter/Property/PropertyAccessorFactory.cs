namespace V5.Library.Reflecter.Property
{
    using System.Reflection;
    
    public class PropertyAccessorFactory : IReflectionFactory<PropertyInfo, IPropertyAccessor>
    {
        public IPropertyAccessor Create(PropertyInfo key)
        {
            return new PropertyAccessor(key);
        }

        IPropertyAccessor IReflectionFactory<PropertyInfo, IPropertyAccessor>.Create(PropertyInfo key)
        {
            return this.Create(key);
        }
    }
}