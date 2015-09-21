namespace V5.Library.Reflecter
{
    using System.Reflection;

    using V5.Library.Reflecter.Constructor;
    using V5.Library.Reflecter.Field;
    using V5.Library.Reflecter.Method;
    using V5.Library.Reflecter.Property;

    public static class ReflectionFactories
    {
        static ReflectionFactories()
        {
            FieldAccessorFactory = new FieldAccessorFactory();
            PropertyAccessorFactory = new PropertyAccessorFactory();
            ConstructorInvokerFactory = new ConstructorInvokerFactory();
            MethodInvokerFactory = new MethodInvokerFactory();
        }

        public static IReflectionFactory<FieldInfo, IFieldAccessor> FieldAccessorFactory { get; set; }

        public static IReflectionFactory<PropertyInfo, IPropertyAccessor> PropertyAccessorFactory { get; set; }

        public static IReflectionFactory<ConstructorInfo, IConstructorInvoker> ConstructorInvokerFactory { get; set; }

        public static IReflectionFactory<MethodInfo, IMethodInvoker> MethodInvokerFactory { get; set; }
    }
}