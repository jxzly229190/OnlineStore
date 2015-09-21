namespace V5.Library.Reflecter
{
    using System;
    using System.Reflection;

    public static class ReflectionExtensions
    {
        public static object Invoke(this ConstructorInfo constructorInfo, params object[] parameters)
        {
            if (constructorInfo == null)
            {
                throw new ArgumentNullException("constructorInfo");
            }

            return ReflectionCaches.ConstructorInvokerCache.Get(constructorInfo).Invoke(parameters);
        }

        public static object Invoke(this MethodInfo methodInfo, object instance, params object[] parameters)
        {
            if (methodInfo == null)
            {
                throw new ArgumentNullException("methodInfo");
            }

            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }

            return ReflectionCaches.MethodInvokerCache.Get(methodInfo).Invoke(instance, parameters);
        }

        public static object GetValue(this FieldInfo fieldInfo, object instance)
        {
            if (fieldInfo == null)
            {
                throw new ArgumentNullException("fieldInfo");
            }

            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }

            return ReflectionCaches.FieldAccessorCache.Get(fieldInfo).GetValue(instance);
        }

        public static object GetValue(this PropertyInfo propertyInfo, object instance)
        {
            if (propertyInfo == null)
            {
                throw new ArgumentNullException("propertyInfo");
            }

            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }

            return ReflectionCaches.PropertyAccessorCache.Get(propertyInfo).GetValue(instance);
        }

        public static void SetValue(this PropertyInfo propertyInfo, object instance, object value)
        {
            if (propertyInfo == null)
            {
                throw new ArgumentNullException("propertyInfo");
            }

            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }

            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            if (value == DBNull.Value)
            {
                return;
            }

            ReflectionCaches.PropertyAccessorCache.Get(propertyInfo).SetValue(instance, value);
        }
    }
}