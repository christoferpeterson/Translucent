using System;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Translucent.Helpers;
using Translucent.Web.Attributes;

public static class HtmlExtensions
{
	public static MvcHtmlString AliasFor<TModel, TProperty>(this MvcHtmlString html,
		Expression<Func<TModel, TProperty>> expression)
	{
		var memberExpression = ExpressionHelpers.GetMemberExpression(expression);
		if (memberExpression == null)
			throw new InvalidOperationException("Expression must be a member expression");
		var aliasAttr = memberExpression.Member.GetAttribute<BindAliasAttribute>();
		if (aliasAttr != null)
		{
			html = html.SetHtmlAttribute("name", aliasAttr.Alias);
			html = html.SetHtmlAttribute("id", TagBuilder.CreateSanitizedId(aliasAttr.Alias));
			return html;
		}
		return html;
	}

	/// <summary>
	/// If BindAliasAttribute setted for property, it replaces the name and id attributes of given html element.
	/// Otherwise gives same html element.
	/// </summary>
	/// <typeparam name="TModel"></typeparam>
	/// <param name="html">html element to change name and id parameters</param>
	/// <param name="expression">the property which has the BindAliasAttribute property</param>
	/// <returns></returns>
	public static MvcHtmlString AliasFor<TModel>(this MvcHtmlString html,
		Expression<Func<TModel, object>> expression)
	{
		return html.AliasFor<TModel, object>(expression);
	}

	/// <summary>
	/// generates id based on BindAliasAttribute for given expression. 
	/// If no attribute setted for given property, uses default IdFor
	/// </summary>
	/// <typeparam name="TModel">View Model</typeparam>
	/// <typeparam name="TProperty">Property</typeparam>
	/// <param name="htmlHelper">model htmlHelper</param>
	/// <param name="expression">ViewModel property expression</param>
	/// <returns></returns>
	public static string AliasNameFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
		Expression<Func<TModel, TProperty>> expression)
	{
		var memberExpression = ExpressionHelpers.GetMemberExpression(expression);
		if (memberExpression == null)
			throw new InvalidOperationException("Expression must be a member expression");
		var aliasAttr = memberExpression.Member.GetAttribute<BindAliasAttribute>();
		if (aliasAttr != null)
		{
			return MvcHtmlString.Create(aliasAttr.Alias).ToHtmlString();
		}
		return htmlHelper.NameFor(expression).ToHtmlString();
	}

	/// <summary>
	/// generates id based on BindAliasAttribute for given expression. 
	/// If no attribute setted for given property, uses default IdFor
	/// </summary>
	/// <typeparam name="TModel">View Model</typeparam>
	/// <typeparam name="TProperty">Property</typeparam>
	/// <param name="htmlHelper">model htmlHelper</param>
	/// <param name="expression">ViewModel property expression</param>
	/// <returns></returns>
	public static string AliasIdFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
		Expression<Func<TModel, TProperty>> expression)
	{
		var memberExpression = ExpressionHelpers.GetMemberExpression(expression);
		if (memberExpression == null)
			throw new InvalidOperationException("Expression must be a member expression");
		var aliasAttr = memberExpression.Member.GetAttribute<BindAliasAttribute>();
		if (aliasAttr != null)
		{
			return MvcHtmlString.Create(TagBuilder.CreateSanitizedId(aliasAttr.Alias)).ToHtmlString();
		}
		return htmlHelper.IdFor(expression).ToHtmlString();
	}

	/// <summary>
	/// sets the attribute value of given html string
	/// </summary>
	/// <param name="html">given html string</param>
	/// <param name="attributeName">attribute to change</param>
	/// <param name="newValue">new value for given attribute</param>
	/// <returns></returns>
	public static MvcHtmlString SetHtmlAttribute(this MvcHtmlString html, string attributeName, string newValue)
	{
		var newAttr = System.Text.RegularExpressions.Regex
			.Replace(html.ToHtmlString(),
				string.Format("{0}=(\"|')([^\"]*)(\"|')", attributeName),
				string.Format("{0}=\"{1}\"", attributeName, newValue));
		return MvcHtmlString.Create(newAttr);
	}
}