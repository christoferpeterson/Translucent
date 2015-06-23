using System;
using System.IO;
using System.Web.Mvc;
using Translucent.Web.Attributes;

namespace Translucent.Web.Controllers
{
	/// <summary>A base controller with some useful methods for the inheriting controller
	/// </summary>
	[Params]
	public abstract class BaseController : Controller
	{
		public static string RenderPartialToString(Controller controller, string viewName, object model)
		{
			controller.ViewData.Model = model;

			using (StringWriter sw = new StringWriter())
			{
				ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(controller.ControllerContext, viewName);

				if(viewResult.View == null)
				{
					throw new NullReferenceException("Unable to locate view. View name: \"" + viewName + "\"");
				}

				ViewContext viewContext = new ViewContext(controller.ControllerContext, viewResult.View, controller.ViewData, controller.TempData, sw);
				viewResult.View.Render(viewContext, sw);

				return sw.GetStringBuilder().ToString();
			}
		}
	}
}
