namespace V5.Library.Reflecter.Property
{
    using System;
    using System.Linq.Expressions;
    using System.Reflection;

    using V5.Library.Reflecter.Method;

    public class PropertyAccessor : IPropertyAccessor
    {
        private Func<object, object> getter;

        private MethodInvoker setMethodInvoker;

        public PropertyInfo PropertyInfo { get; private set; }

        public PropertyAccessor(PropertyInfo propertyInfo)
        {
            this.PropertyInfo = propertyInfo;
            this.InitializeGet(propertyInfo);
            this.InitializeSet(propertyInfo);
        }

        public object GetValue(object instance)
        {
            if (this.getter == null)
            {
                throw new NotSupportedException("Get method is not defined for this property.");
            }

            return this.getter(instance);
        }

        public void SetValue(object instance, object value)
        {
            if (this.setMethodInvoker == null)
            {
                throw new NotSupportedException("Set method is not defined for this property.");
            }

            this.setMethodInvoker.Invoke(instance, new[] { value });
        }

        private void InitializeGet(PropertyInfo propertyInfo)
        {
            if (!propertyInfo.CanRead)
            {
                return;
            }

            var instanceParameter = Expression.Parameter(typeof(object), "instance");

            var instanceExpression = propertyInfo.GetGetMethod(true).IsStatic ? null : Expression.Convert(instanceParameter, propertyInfo.ReflectedType);

            var propertyMember = Expression.Property(instanceExpression, propertyInfo);

            var propertyValue = Expression.Convert(propertyMember, typeof(object));

            var lambda = Expression.Lambda<Func<object, object>>(propertyValue, instanceParameter);

            this.getter = lambda.Compile();
        }

        private void InitializeSet(PropertyInfo propertyInfo)
        {
            if (!propertyInfo.CanWrite)
            {
                return;
            }

            this.setMethodInvoker = new MethodInvoker(propertyInfo.GetSetMethod(true));
        }

        object IPropertyAccessor.GetValue(object instance)
        {
            return this.GetValue(instance);
        }

        void IPropertyAccessor.SetValue(object instance, object value)
        {
            this.SetValue(instance, value);
        }
    }
}