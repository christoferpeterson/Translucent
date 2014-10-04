using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Translucent.Helpers
{
	public static class ExpressionHelpers
	{
		public static T GetAttribute<T>(this ICustomAttributeProvider provider)
		where T : Attribute
		{
			var attributes = provider.GetCustomAttributes(typeof(T), true);
			return attributes.Length > 0 ? attributes[0] as T : null;
		}

		public static MemberExpression GetMemberExpression<TModel, TProperty>(Expression<Func<TModel, TProperty>> expression)
		{
			MemberExpression memberExpression;
			if (expression.Body is UnaryExpression)
			{
				var unaryExpression = (UnaryExpression)expression.Body;
				memberExpression = (MemberExpression)unaryExpression.Operand;
			}
			else
			{
				memberExpression = (MemberExpression)expression.Body;
			}
			return memberExpression;
		}
	}
}