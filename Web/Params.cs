using System.Web;

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
	}
}
