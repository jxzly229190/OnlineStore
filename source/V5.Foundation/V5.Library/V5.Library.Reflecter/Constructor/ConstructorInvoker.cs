namespace V5.Library.Reflecter.Constructor
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Reflection;

    public class ConstructorInvoker : IConstructorInvoker
    {
        private readonly Func<object[], object> invoker;

        public ConstructorInfo ConstructorInfo { get; private set; }

        public ConstructorInvoker(ConstructorInfo constructorInfo)
        {
            this.ConstructorInfo = constructorInfo;
            this.invoker = this.InitializeInvoker(constructorInfo);
        }

        public object Invoke(params object[] parameters)
        {
            return this.invoker(parameters);
        }

        private Func<object[], object> InitializeInvoker(ConstructorInfo constructorInfo)
        {
            var parametersParameter = Expression.Parameter(typeof(object[]), "parameters");

            var parameterExpressions = new List<Expression>();
            var parameterInfos = constructorInfo.GetParameters();

            for (int i = 0; i < parameterInfos.Length; i++)
            {
                BinaryExpression binaryExpression = Expression.ArrayIndex(parametersParameter, Expression.Constant(i));
                UnaryExpression unaryExpression = Expression.Convert(binaryExpression, parameterInfos[i].ParameterType);

                parameterExpressions.Add(unaryExpression);
            }

            var instanceExpression = Expression.New(constructorInfo, parameterExpressions);

            var instanceCreateExpression = Expression.Convert(instanceExpression, typeof(object));

            var lambda = Expression.Lambda<Func<object[], object>>(instanceCreateExpression, parametersParameter);

            return lambda.Compile();
        }

        object IConstructorInvoker.Invoke(params object[] parameters)
        {
            return this.Invoke(parameters);
        }
    }
}