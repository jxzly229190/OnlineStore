namespace V5.Library.Reflecter
{
    using System.Reflection;

    using V5.Library.Reflecter.Constructor;
    using V5.Library.Reflecter.Field;
    using V5.Library.Reflecter.Method;
    using V5.Library.Reflecter.Property;
    
    public class ReflectionCaches
    {
        static ReflectionCaches()
        {
            ConstructorInvokerCache = new ConstructorInvokerCache();
            MethodInvokerCache = new MethodInvokerCache();
            FieldAccessorCache = new FieldAccessorCache();
            PropertyAccessorCache = new PropertyAccessorCache();
        }

        public static IReflectionCache<ConstructorInfo, IConstructorInvoker> ConstructorInvokerCache { get; set; }

        public static IReflectionCache<MethodInfo, IMethodInvoker> MethodInvokerCache { get; set; }

        public static IReflectionCache<FieldInfo, IFieldAccessor> FieldAccessorCache { get; set; }

        public static IReflectionCache<PropertyInfo, IPropertyAccessor> PropertyAccessorCache { get; set; }
    }
}