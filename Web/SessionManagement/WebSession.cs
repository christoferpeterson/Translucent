using System.Globalization;
using System.Web;

namespace Translucent.Web.SessionManagement
{
	public class WebSession<Subclass>
			where Subclass : WebSession<Subclass>, new()
	{
		#region Fields

		protected CultureInfo _format;

		#endregion

		#region Properties

		private static string Key
		{
			get { return typeof(WebSession<Subclass>).FullName; }
		}

		private static Subclass Value
		{
			get { return (Subclass)HttpContext.Current.Session[Key]; }
			set { HttpContext.Current.Session[Key] = value; }
		}

		public static Subclass Current
		{
			get
			{
				var instance = Value;
				if (instance == null)
				{
					lock (typeof(Subclass))
					{
						instance = Value;
						if (instance == null)
						{
							Value = instance = new Subclass();
						}
					}
				}
				return instance;
			}
		}

		public CultureInfo Format
		{
			get
			{
				if (_format == null)
				{
					_format = new CultureInfo("en-US");
				}
				return _format;
			}
			set { _format = value; }
		}

		#endregion

		#region Virtual Methods

		public virtual void End() { }
		public virtual void Refresh() { }

		#endregion

	}
}
