using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Translucent.Web.Attributes
{
	public class ParamsAttribute : ActionFilterAttribute
	{
		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			base.OnActionExecuting(filterContext);

			if (HttpContext.Current.Request.QueryString.AllKeys.Contains("..debug.."))
			{
				Params.Debug = HttpContext.Current.Request.QueryString["..debug.."] == "1";
			}
		}
	}
}
