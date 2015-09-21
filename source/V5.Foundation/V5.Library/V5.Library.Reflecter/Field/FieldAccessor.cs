namespace V5.Library.Reflecter.Field
{
    using System;
    using System.Linq.Expressions;
    using System.Reflection;

    public class FieldAccessor : IFieldAccessor
    {
        private readonly Func<object, object> getter;

        public FieldInfo FieldInfo { get; private set; }

        public FieldAccessor(FieldInfo fieldInfo)
        {
            this.FieldInfo = fieldInfo;
            this.getter = this.GetDelegate(fieldInfo);
        }

        public object GetValue(object instance)
        {
            return this.getter(instance);
        }

        private Func<object, object> GetDelegate(FieldInfo fieldInfo)
        {
            var instanceParameter = Expression.Parameter(typeof(object), "instance");

            var instanceExpression = fieldInfo.IsStatic ? null : Expression.Convert(instanceParameter, fieldInfo.ReflectedType);

            var fieldMember = Expression.Field(instanceExpression, fieldInfo);

            var fieldValue = Expression.Convert(fieldMember, typeof(object));

            var lambda = Expression.Lambda<Func<object, object>>(fieldValue, instanceParameter);

            return lambda.Compile();
        }

        object IFieldAccessor.GetValue(object instance)
        {
            return this.GetValue(instance);
        }
    }
}