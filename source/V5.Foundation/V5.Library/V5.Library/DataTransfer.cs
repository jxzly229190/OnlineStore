// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DataTransfer.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   The data transfer.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Library
{
    using System;
    using System.Reflection;

    using V5.Library.Reflecter;

    /// <summary>
    /// The data transfer.
    /// </summary>
    public class DataTransfer
    {
        #region Constants and Fields

        /// <summary>
        /// The binding flag.
        /// </summary>
        private const BindingFlags BindingFlag = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// 数据转换方法
        /// </summary>
        /// <param name="source">
        /// 源对象
        /// </param>
        /// <param name="sourceType">
        /// 源类型
        /// </param>
        /// <typeparam name="T">
        /// 指定返回类型
        /// </typeparam>
        /// <returns>
        /// 指定返回类型的实例
        /// </returns>
        public static T Transfer<T>(object source, Type sourceType) where T : new()
        {
            var target = new T();
            var targetType = typeof(T);

            var targetProperties = targetType.GetProperties(BindingFlag);
            var sourceProperties = sourceType.GetProperties(BindingFlag);

            foreach (var sourceProperty in sourceProperties)
            {
                foreach (var targetProperty in targetProperties)
                {
                    if (!string.IsNullOrEmpty(targetProperty.PropertyType.FullName))
                    {
                        if (targetProperty.PropertyType.FullName.StartsWith("System.Collections"))
                        {
                            break;
                        }
                    }

                    try
                    {
                        if (sourceProperty.Name == targetProperty.Name)
                        {
                            targetProperty.SetValue(target, sourceProperty.GetValue(source));
                        }
                    }
                    catch (Exception)
                    {
                        break;
                    }
                }
            }

            return target;
        }

        #endregion
    }
}