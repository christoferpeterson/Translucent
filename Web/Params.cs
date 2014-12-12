using System.Web;
using System.Web.Mvc;

namespace Translucent.Web
{
	public class Params
	{
		public static bool Debug
		{
			get
			{
				HttpContext.Current.Session["translucent.debug"] = HttpContext.Current.Items["translucent.debug"] ?? false;
				return (HttpContext.Current.Session["translucent.debug"] as bool?) ?? false;
			}
			set
			{
				HttpContext.Current.Session["translucent.debug"] = value;
			}
		}


		/// <summary>The current Http Request
		/// </summary>
		public static HttpRequest Request { get { return HttpContext.Current.Request; } }

		/// <summary>A URL helper which solves the problems of virtual pathed websites
		/// </summary>
		public static UrlHelper UrlHelper { get { return new UrlHelper(Request.RequestContext); } }

		/// <summary>The actual domain of this instance of the website
		/// </summary>
		public static string Domain { get { return string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, UrlHelper.Content("~")); } }
	}
}
