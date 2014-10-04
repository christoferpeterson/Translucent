using System;

public static class TimeZoneInfoExtensions
{
	/// <summary>A daylights savings time adjusted time zone name
	/// </summary>
	/// <param name="tz">the timezone to view</param>
	/// <returns>the current time zone's display name adjusted for daylights savings</returns>
	public static string FullDisplayName(this TimeZoneInfo tz)
	{
		return tz.IsDaylightSavingTime(DateTime.Now) ? tz.DaylightName : tz.StandardName;
	}
}
