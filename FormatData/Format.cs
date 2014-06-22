using System;
using System.Web;
using System.Globalization;
using Translucent.Extensions;

namespace Translucent.FormatData
{
	public static partial class Format
	{
		static CultureInfo _defaultCulture
		{
			get
			{
				var culture = new CultureInfo("en-US");

				culture.DateTimeFormat.PMDesignator = "pm";
				culture.DateTimeFormat.AMDesignator = "am";

				culture.NumberFormat.PercentPositivePattern = 1;
				culture.NumberFormat.PercentNegativePattern = 1;

				return culture;
			}
		}

		/// <summary>Get and set the culture for the current request
		/// </summary>
		public static CultureInfo Culture
		{
			get
			{
				CultureInfo output;

				output = (HttpContext.Current.Items["CultureInfo"] as CultureInfo) ?? _defaultCulture;

				return output;
			}
			set
			{
				HttpContext.Current.Items["CultureInfo"] = value;
			}
		}

		#region DateTime

		public static string TimeStamp(DateTime? date)
		{
			string value = "";

			if (date.HasValue)
			{
				if (date.Value.Date == DateTime.Today)
					value = "Today at ";
				else if (date.Value.Date == DateTime.Today.AddDays(-1))
					value = "Yesterday at ";
				else if (date.Value.Date == DateTime.Today.AddDays(1))
					value = "Tomorrow at ";
				else
					value += "";

				value += date.Value.ToString(@"h:mmtt \MT", Culture);
			}
			else
			{
				value = "never";
			}

			return value;
		}

		public static string ShortDateTime(DateTime? date)
		{
			string value = "";

			if(date.HasValue)
			{
				if(date.Value.Date == DateTime.Today)
				{
					value = date.Value.ToString(@"h:mmtt \MT", Culture);
				}
				else
				{
					value = date.Value.ToString(@"M/d/yyyy h:mmtt \MT", Culture);
				}
			}
			else
			{
				value = "No date";
			}

			return value;
		}

		public static string FullDate(DateTime? date)
		{
			string value = "";

			if(date.HasValue)
			{
				if(date.Value.Date == DateTime.Today)
				{
					value = "Today, ";
				}
				else if(date.Value.Date == DateTime.Today.AddDays(-1))
				{
					value = "Yesterday, ";
				}
				else if(date.Value.Date == DateTime.Today.AddDays(1))
				{
					value = "Tomorrow, ";
				}
				else
				{
					value = date.Value.ToString("dddd") + ", ";
				}
				value += String.Format("{0} {1}, {2}", date.Value.ToString("MMMM", Culture), date.Value.Day.Ordinal(), date.Value.ToString("yyyy", Culture));
			}
			else
			{
				value = "No date";
			}

			return value;
		}

		#endregion
	}
}
