namespace V5.Library.Reflecter.Field
{
    using System.Reflection;

    public class FieldAccessorFactory : IReflectionFactory<FieldInfo, IFieldAccessor>
    {
        public IFieldAccessor Create(FieldInfo key)
        {
            return new FieldAccessor(key);
        }

        IFieldAccessor IReflectionFactory<FieldInfo, IFieldAccessor>.Create(FieldInfo key)
        {
            return this.Create(key);
        }
    }
}