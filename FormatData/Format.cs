using System;
using System.Web;
using System.Globalization;
using System.Collections.Generic;

namespace Translucent.FormatData
{
	public partial class Format
	{

		/// <summary>Get and set the culture for the current request
		/// </summary>
		public static FormatOptions Options
		{
			get
			{
				FormatOptions output;
				output = (HttpContext.Current.Items["FormatOptions"] as FormatOptions) ?? new FormatOptions();
				return output;
			}
			set
			{
				HttpContext.Current.Items["FormatOptions"] = value;
			}
		}

		/// <summary>Gauge an object's validity by comparing it against the state cases
		/// </summary>
		/// <param name="dataPoint">the object to validate</param>
		/// <returns>the value's DataState</returns>
		private static DataState IsValid(object dataPoint)
		{
			if (dataPoint == null)
				return DataState.in_error;
			DataState output;
			output = Options.DataStateCases.TryGetValue(dataPoint, out output) ? output : DataState.valid;
			return output;
		}

		/// <summary>Return the error string corresponding to the error state
		/// </summary>
		/// <param name="valid"></param>
		/// <returns></returns>
		private static string ErrorString(DataState valid)
		{
			switch(valid)
			{
				case DataState.not_valid: return Options.NotValidString;
				case DataState.not_meaningful: return Options.NotMeaningfulString;
				case DataState.in_error:
				default: return Options.ErrorString;
			}
		}
	}
}
