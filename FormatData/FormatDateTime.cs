using System;

namespace Translucent.FormatData
{
	public partial class Format
	{
		/// <summary>Convert a DateTime from utc to the current timezone
		/// </summary>
		/// <param name="dt">the datetime to convert (must be utc)</param>
		/// <returns>adjusted datetime according to current value of Format.TimeZone</returns>
		public static DateTime? FromUTC(DateTime? dt)
		{
			DateTime? output;
			// verify date has a value before converting
			var valid = IsValid(dt);
			if (valid == DataState.valid)
			{
				output = TimeZoneInfo.ConvertTimeFromUtc(dt.Value, Format.Options.TimeZone);
				if (Format.Options.TimeZone.IsDaylightSavingTime(DateTime.Now))
				{
					if (!Format.Options.TimeZone.IsDaylightSavingTime(output.Value))
					{
						output = output.Value.AddHours(1);
					}
				}
				else
				{
					if (Format.Options.TimeZone.IsDaylightSavingTime(output.Value))
					{
						output = output.Value.AddHours(-1);
					}
				}

				return output;
			}

			return null;
		}

		/// <summary>Convert a nullable date time object to utc
		/// </summary>
		/// <param name="dt">extension method (must not be utc)</param>
		/// <returns>adjusted datetime according to current value of Format.TimeZone</returns>
		public static DateTime? ToUTC(DateTime? dt)
		{
			var output = dt.HasValue ? TimeZoneInfo.ConvertTimeToUtc(dt.Value, Format.Options.TimeZone) : dt;

			var valid = IsValid(dt);
			if (valid == DataState.valid)
			{
				if (Format.Options.TimeZone.IsDaylightSavingTime(DateTime.Now))
				{
					if (!Format.Options.TimeZone.IsDaylightSavingTime(output.Value))
					{
						output = output.Value.AddHours(-1);
					}
				}
				else
				{
					if (Format.Options.TimeZone.IsDaylightSavingTime(output.Value))
					{
						output = output.Value.AddHours(1);
					}
				}
			}

			return output;
		}

		/// <summary>Format a datetime into an Rfc822 compliant string
		/// </summary>
		/// <param name="dt">the datetime to format</param>
		/// <returns>"r"</returns>
		public static string Rfc822(DateTime? dt)
		{
			var valid = IsValid(dt);
			if (valid == DataState.valid)
			{
				return dt.Value.ToString("r", Format.Options.Culture);
			}

			return ErrorString(valid);
		}

		/// <summary>Creates a timestamp from a date time
		/// </summary>
		/// <param name="dt">the date time to convert</param>
		/// <returns>Relative day</returns>
		public static string TimeStamp(DateTime? dt)
		{
			return TimeStamp(dt, false);
		}

		/// <summary>Creates a timestamp from a date time
		/// </summary>
		/// <param name="dt">the date time to convert</param>
		/// <param name="includeDate">include the month, day, year in the timestamp</param>
		/// <returns>Relative day</returns>
		public static string TimeStamp(DateTime? dt, bool includeDate)
		{
			string value = "";
			var valid = IsValid(dt);
			var today = FromUTC(DateTime.Now.ToUniversalTime()).Value.Date;

			if (valid == DataState.valid)
			{
				if (dt.Value.Date == today)
					value = "Today at ";
				else if (dt.Value.Date == today.AddDays(-1))
					value = "Yesterday at ";
				else if (dt.Value.Date == today.AddDays(1))
					value = "Tomorrow at ";
				else if (includeDate)
					value += ShortDate(dt) + " at ";

				value += ShortTime(dt);
			}
			else
			{
				value = "never";
			}

			return value;
		}

		/// <summary>Convert a nullable datetime into a short formatted string
		/// </summary>
		/// <param name="dt">the date to convert</param>
		/// <returns>If today: "h:mmtt", else @"M/d/yyyy h:mmtt"</returns>
		public static string ShortDateTime(DateTime? dt)
		{
			string value = "";

			var valid = IsValid(dt);
			if (valid == DataState.valid)
			{
				if (dt.Value.Date == DateTime.Today)
				{
					value = ShortTime(dt);
				}
				else
				{
					value = String.Format("{0} {1}", ShortDate(dt), ShortTime(dt));
				}
			}
			else
			{
				value = "No date";
			}

			return value;
		}

		/// <summary>Convert a datetime into a short time
		/// </summary>
		/// <param name="dt">the time to convert</param>
		/// <returns>@"h:mmtt"</returns>
		public static string ShortTime(DateTime? dt)
		{
			var valid = IsValid(dt);
			if (valid == DataState.valid)
			{
				return dt.Value.ToString(@"h:mmtt", Format.Options.Culture);
			}
			return "";
		}

		/// <summary>Format a short date
		/// </summary>
		/// <param name="dt">the date to convert</param>
		/// <returns>@"M/d/yyyy"</returns>
		public static string ShortDate(DateTime? dt)
		{
			var valid = IsValid(dt);
			if (valid == DataState.valid)
			{
				return dt.Value.ToString(@"M/d/yyyy", Format.Options.Culture);
			}

			return "";
		}

		/// <summary>Convert a nullable datetime into a full formatted string
		/// </summary>
		/// <param name="dt">the date to convert</param>
		/// <returns>Includes relative day string or "dddd", "MMMM" "yyyy"</returns>
		public static string FullDate(DateTime? dt)
		{
			string value = "";
			var valid = IsValid(dt);

			if (valid == DataState.valid)
			{
				var today = FromUTC(DateTime.Now.ToUniversalTime()).Value.Date;
				var date = dt.Value.Date;

				if (date == today)
				{
					value = "Today, ";
				}
				else if (date == today.AddDays(-1))
				{
					value = "Yesterday, ";
				}
				else if (date == today.AddDays(1))
				{
					value = "Tomorrow, ";
				}
				else
				{
					value = date.ToString("dddd") + ", ";
				}
				value += String.Format("{0} {1}, {2}", dt.Value.ToString("MMMM", Format.Options.Culture), dt.Value.Day.Ordinal(), dt.Value.ToString("yyyy", Format.Options.Culture));
			}
			else
			{
				value = "No date";
			}

			return value;
		}

			/// <summary>Convert a nullable datetime into a full formatted string
		/// </summary>
		/// <param name="dt">the date to convert</param>
		/// <returns>Includes relative day string or "dddd", "MMMM" "yyyy"</returns>
		public static string FullDateTime(DateTime? dt)
		{
			string value = "";
			var valid = IsValid(dt);

			if (valid == DataState.valid)
			{
				value += ShortTime(dt);
				value += " ";
				value += FullDate(dt);
			}
			else
			{
				value = "No timestamp";
			}

			return value;
		}
	}
}
