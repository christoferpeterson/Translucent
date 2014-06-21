using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Translucent.Web
{
	public class Params
	{
		public static bool Debug
		{
			get
			{
				HttpContext.Current.Items["translucent.debug"] = HttpContext.Current.Items["translucent.debug"] ?? false;
				return (HttpContext.Current.Items["translucent.debug"] as bool?) ?? false;
			}
			set
			{
				HttpContext.Current.Items["translucent.debug"] = value;
			}
		}
	}
}
