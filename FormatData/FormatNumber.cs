namespace Translucent.FormatData
{
	public partial class Format
	{
		public static string Number(int? n) { return Number(n, null); }
		public static string Number(int? n, int? precision) { return Number(n, precision, false); }

		/// <summary>Format a number
		/// </summary>
		/// <param name="n">The number to format</param>
		/// <param name="precision">the number of decimal places to show</param>
		/// <param name="color">wrap the result in an html span and give it a color class</param>
		/// <returns></returns>
		public static string Number(double? n, int? precision, bool color)
		{
			var valid = IsValid(n);
			if(valid == DataState.valid)
			{
				var culture = Options.Culture;
				if(precision.HasValue)
					culture.NumberFormat.NumberDecimalDigits = precision.Value;
				
				var output = n.Value.ToString("n", culture);

				if(color)
				{
					var colorClass = n.Value < 0 ? Options.NegativeColorClass : n.Value > 0 ? Options.PositiveColorClass : Options.NeutralColorClass;
					output = string.Format("<span class=\"{0}\">{1}</span>", colorClass, output);
				}

				return output;
			}

			return ErrorString(valid);
		}

		public static string Percent(double? n, int? precision = 1, bool color = false)
		{
			var valid = IsValid(n);
			if (valid == DataState.valid)
			{
				var culture = Options.Culture;
				if (precision.HasValue)
					culture.NumberFormat.PercentDecimalDigits = precision.Value;

				var output = n.Value.ToString("p", culture);

				if (color)
				{
					var colorClass = n.Value < 0 ? Options.NegativeColorClass : n.Value > 0 ? Options.PositiveColorClass : Options.NeutralColorClass;
					output = string.Format("<span class=\"{0}\">{1}</span>", colorClass, output);
				}

				return output;
			}

			return ErrorString(valid);
		}

		public static string Currency(double? n, int? precision = 2, bool color = false)
		{
			var valid = IsValid(n);
			if (valid == DataState.valid)
			{
				var culture = Options.Culture;
				if (precision.HasValue)
					culture.NumberFormat.NumberDecimalDigits = precision.Value;

				var output = n.Value.ToString("c", culture);

				if (color)
				{
					var colorClass = n.Value < 0 ? Options.NegativeColorClass : n.Value > 0 ? Options.PositiveColorClass : Options.NeutralColorClass;
					output = string.Format("<span class=\"{0}\">{1}</span>", colorClass, output);
				}

				return output;
			}

			return ErrorString(valid);
		}
	}
}
