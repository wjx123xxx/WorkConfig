// *******************************************************************
// * 文件名称： MockingHelper.cs
// * 作　　者： 王璟星
// * 创建日期： 2018-01-13 17:29:44
// *******************************************************************

using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Wx.Test.WxFrameworkTest.TestHelper
{
    /// <summary>
    /// 单元测试辅助类
    /// </summary>
    internal static class MockingHelper
    {
        #region Static Method

        public static T CreateInstance<T>(params object[] args)
        {
            Type typeToCreate = typeof(T);

            Type[] parameterTypes = args.Select(arg => arg.GetType()).ToArray();

            ConstructorInfo constructorInfoObj = typeToCreate.GetConstructor(
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic,
                null, parameterTypes, null);

            return (T)constructorInfoObj.Invoke(args);
        }

        public static MethodInfo GetMethodReference(Type targetType, string methodName)
        {
            MethodInfo method = targetType.GetMethod(methodName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            if (method == null && targetType.BaseType != null)
            {
                return GetMethodReference(targetType.BaseType, methodName);
            }
            return method;
        }

        public static string GetPropertyName<T>(Expression<Func<T>> property)
        {
            LambdaExpression lambdaExpression = property;
            var memberExpression = lambdaExpression.Body as MemberExpression ??
                                   ((UnaryExpression)lambdaExpression.Body).Operand as MemberExpression;
            return memberExpression.Member.Name;
        }

        public static void SetFieldValue(object target, string fieldName, object newValue)
        {
            FieldInfo field = GetFieldReference(target.GetType(), fieldName);
            field.SetValue(target, newValue);
        }

        public static void SetPropertyValue(object target, string memberName, object newValue)
        {
            PropertyInfo prop = GetPropertyReference(target.GetType(), memberName);
            prop.SetValue(target, newValue, null);
        }

        private static FieldInfo GetFieldReference(Type targetType, string fieldName)
        {
            FieldInfo field = targetType.GetField(fieldName,
                BindingFlags.Public |
                BindingFlags.NonPublic |
                BindingFlags.Instance);

            if (field == null && targetType.BaseType != null)
            {
                return GetFieldReference(targetType.BaseType, fieldName);
            }
            return field;
        }

        private static PropertyInfo GetPropertyReference(Type targetType, string memberName)
        {
            PropertyInfo propInfo = targetType.GetProperty(memberName,
                BindingFlags.Public |
                BindingFlags.NonPublic |
                BindingFlags.Instance);

            if (propInfo == null && targetType.BaseType != null)
            {
                return GetPropertyReference(targetType.BaseType, memberName);
            }
            return propInfo;
        }

        #endregion
    }
}