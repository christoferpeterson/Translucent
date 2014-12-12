using System.ServiceModel.Syndication;
using System.Web.Mvc;
using System.Xml;

namespace DenverChessClub.Controllers.CustomActionResults
{
	public class SyndicationFeedActionResult : ActionResult
	{
		public SyndicationFeed Feed { get; set; }

		/// <summary>Render as Atom rather than RSS
		/// </summary>
		public bool Atom { get; set; }

		public override void ExecuteResult(ControllerContext context)
		{
			if(Atom)
			{
				context.HttpContext.Response.ContentType = "application/atom+xml";
				Atom10FeedFormatter rssFormatter = new Atom10FeedFormatter(Feed);

				using (XmlWriter writer = XmlWriter.Create(context.HttpContext.Response.Output))
				{
					rssFormatter.WriteTo(writer);
				}
			}
			else
			{
				context.HttpContext.Response.ContentType = "application/rss+xml";
				Rss20FeedFormatter rssFormatter = new Rss20FeedFormatter(Feed, false);

				using (XmlWriter writer = XmlWriter.Create(context.HttpContext.Response.Output))
				{
					rssFormatter.WriteTo(writer);
				}
			}
		}
	}
}