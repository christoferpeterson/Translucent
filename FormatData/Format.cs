using System;

namespace Translucent.FormatData
{
	public static partial class Format
	{
		public static string TimeStamp(DateTime? date)
		{
			string value;

			if (date.HasValue)
			{
				if (date.Value.Date == DateTime.Today)
					value = "Today at " + date.Value.ToString("h:mm:ss tt");
				else if (date.Value.Date == DateTime.Today.AddDays(-1))
					value = "Yesterday at " + date.Value.ToString("h:mm:ss tt");
				else if (date.Value.Date == DateTime.Today.AddDays(1))
					value = "Tomorrow at " + date.Value.ToString("h:mm:ss tt");
				else
					value = date.Value.ToString();
			}
			else
			{
				value = "never";
			}

			return value;
		}
	}
}
