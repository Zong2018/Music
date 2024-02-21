using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Music.Infrastructure.Mvvm
{
	/// <summary>
	///  Provides support for extracting property information based on a property expression.
	/// </summary>
	public static class PropertySupport
	{
		/// <summary>
		/// Extracts the property name from a property expression.
		/// </summary>
		/// <typeparam name="T">The object type containing the property specified in the expression.</typeparam>
		/// <param name="propertyExpression">The property expression (e.g. p =&gt; p.PropertyName)</param>
		/// <returns>The name of the property.</returns>
		/// <exception cref="T:System.ArgumentNullException">Thrown if the <paramref name="propertyExpression" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">Thrown when the expression is:<br />
		///     Not a <see cref="T:System.Linq.Expressions.MemberExpression" /><br />
		///     The <see cref="T:System.Linq.Expressions.MemberExpression" /> does not represent a property.<br />
		///     Or, the property is static.
		/// </exception>
		public static string ExtractPropertyName<T>(Expression<Func<T>> propertyExpression)
		{
			if (propertyExpression == null)
			{
				throw new ArgumentNullException("propertyExpression");
			}
			return ExtractPropertyNameFromLambda(propertyExpression);
		}

		/// <summary>
		/// Extracts the property name from a LambdaExpression.
		/// </summary>
		/// <param name="expression">The LambdaExpression</param>
		/// <returns>The name of the property.</returns>
		/// <exception cref="T:System.ArgumentNullException">Thrown if the <paramref name="expression" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">Thrown when the expression is:<br />
		///     The <see cref="T:System.Linq.Expressions.MemberExpression" /> does not represent a property.<br />
		///     Or, the property is static.
		/// </exception>
		internal static string ExtractPropertyNameFromLambda(LambdaExpression expression)
		{
			if (expression == null)
			{
				throw new ArgumentNullException("expression");
			}
			MemberExpression obj = (expression.Body as MemberExpression) ?? throw new ArgumentException("PropertySupport_StaticExpression_Exception", "expression");
			PropertyInfo obj2 = obj.Member as PropertyInfo;
			if (obj2 == null)
			{
				throw new ArgumentException("PropertySupport_StaticExpression_Exception", "expression");
			}
			if (obj2.GetMethod.IsStatic)
			{
				throw new ArgumentException("PropertySupport_StaticExpression_Exception", "expression");
			}
			return obj.Member.Name;
		}
	}

}
