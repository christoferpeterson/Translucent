namespace Translucent.Web
{
	/// <summary>A simple interface for defining page details
	/// </summary>
	public interface IPageModel
	{
		/// <summary>Page title, gets placed in "title" tag in document "head"
		/// </summary>
		string Title { get; }
		/// <summary>Page description, gets placed in a "meta" tag in document "head"
		/// </summary>
		string Description { get; }
		/// <summary>SEO keywords, gets placed in a "meta" tag in document "head"
		/// </summary>
		string[] Keywords { get; }

		/// <summary>Page header, gets rendered in bootstrap head component or equivalent
		/// see http://getbootstrap.com/components/#page-header
		/// </summary>
		string Header { get; }
		/// <summary>Page subheader, gets rendered in bootstrap head component or equivalent
		/// see http://getbootstrap.com/components/#page-header
		/// </summary>
		string SubHeader { get; }

		/// <summary>The path to the javascript bundle for this page
		/// </summary>
		string JavascriptBundle { get; }
	}
}