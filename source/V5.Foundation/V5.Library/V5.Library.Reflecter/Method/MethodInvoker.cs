namespace V5.Library.Reflecter.Method
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Reflection;
    
    public class MethodInvoker : IMethodInvoker
    {
        private readonly Func<object, object[], object> invoker;

        public MethodInfo MethodInfo { get; private set; }

        public MethodInvoker(MethodInfo methodInfo)
        {
            this.MethodInfo = methodInfo;
            this.invoker = CreateInvokeDelegate(methodInfo);
        }

        public object Invoke(object instance, params object[] parameters)
        {
            return this.invoker(instance, parameters);
        }

        private static Func<object, object[], object> CreateInvokeDelegate(MethodInfo methodInfo)
        {
            var instanceParameter = Expression.Parameter(typeof(object), "instance");
            var parametersParameter = Expression.Parameter(typeof(object[]), "parameters");

            var parameterExpressions = new List<Expression>();
            var parameterInfos = methodInfo.GetParameters();

            for (int i = 0; i < parameterInfos.Length; i++)
            {
                BinaryExpression binaryExpression = Expression.ArrayIndex(parametersParameter, Expression.Constant(i));
                UnaryExpression unaryExpression = Expression.Convert(binaryExpression, parameterInfos[i].ParameterType);

                parameterExpressions.Add(unaryExpression);
            }

            var instanceExpression = methodInfo.IsStatic ? null : Expression.Convert(instanceParameter, methodInfo.ReflectedType);
            var methodCallExpression = Expression.Call(instanceExpression, methodInfo, parameterExpressions);

            if (methodCallExpression.Type == typeof(void))
            {
                var lambda = Expression.Lambda<Action<object, object[]>>(methodCallExpression, instanceParameter, parametersParameter);
                Action<object, object[]> execute = lambda.Compile();
                return (instance, parameters) =>
                {
                    execute(instance, parameters);
                    return null;
                };
            }
            else
            {
                var castMethodCall = Expression.Convert(methodCallExpression, typeof(object));
                var lambda = Expression.Lambda<Func<object, object[], object>>(castMethodCall, instanceParameter, parametersParameter);
                return lambda.Compile();
            }
        }

        object IMethodInvoker.Invoke(object instance, params object[] parameters)
        {
            return this.Invoke(instance, parameters);
        }
    }
}