using System;
using System.Collections.Generic;
using System.Globalization;

namespace Translucent.FormatData
{
	/// <summary>Flags: indicates data points as valid or not
	/// </summary>
	[Flags]
	public enum DataState
	{
		valid = 0,
		not_valid = 1,
		not_meaningful = 2,
		in_error = not_valid | not_meaningful
	}

	public class FormatOptions
	{
		/// <summary>Culture information to adjust formatting
		/// </summary>
		public CultureInfo Culture
		{
			get { _culture = _culture ?? _defaultCulture; return _culture; }
			set { _culture = value; }
		}
		private CultureInfo _culture;
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

		/// <summary>Timezone details for adjusting datetimes
		/// </summary>
		public TimeZoneInfo TimeZone
		{
			get { _timezone = _timezone ?? _defaultTimeZone; return _timezone; }
			set { _timezone = value; }
		}
		private TimeZoneInfo _timezone;
		static TimeZoneInfo _defaultTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Mountain Standard Time");

		#region Error Handling

		const string NOT_VALID_STRING = "--";
		const string NOT_MEANINGFUL_STRING = "--";
		const string ERROR_STRING = "--";

		/// <summary>display string when values are in error
		/// default: <see cref="ERROR_STRING" />
		/// </summary>
		public string ErrorString
		{
			get { _errorString = _errorString ?? ERROR_STRING;  return _errorString; }
			set { _errorString = value; }
		}
		private string _errorString;

		/// <summary>display string when values are not valid
		/// default: <see cref="NOT_VALID_STRING" />
		/// </summary>
		public string NotValidString
		{
			get { _notValidString = _notValidString ?? NOT_VALID_STRING; return _notValidString; }
			set { _notValidString = value; }
		}
		private string _notValidString;

		/// <summary>display string when values are not meaningful
		/// default: <see cref="NOT_MEANINGFUL_STRING" />
		/// </summary>
		public string NotMeaningfulString
		{
			get { _notMeaningfulString = _notMeaningfulString ?? NOT_MEANINGFUL_STRING; return _notMeaningfulString; }
			set { _notMeaningfulString = value; }
		}
		private string _notMeaningfulString;

		Dictionary<object, DataState> _defaultStateCases = new Dictionary<object, DataState>
		{
			{ int.MaxValue, DataState.not_meaningful },
			{ int.MinValue, DataState.not_meaningful },
			{ DateTime.MinValue, DataState.not_meaningful },
			{ DateTime.MaxValue, DataState.not_meaningful },
			{ long.MinValue, DataState.not_meaningful },
			{ long.MaxValue, DataState.not_meaningful },
			{ double.MinValue, DataState.not_meaningful },
			{ double.MaxValue, DataState.not_meaningful },
			{ double.NaN, DataState.not_valid },
			{ double.PositiveInfinity, DataState.not_valid },
			{ double.NegativeInfinity, DataState.not_valid },
			{ float.Epsilon, DataState.not_meaningful },
			{ float.MinValue, DataState.not_meaningful },
			{ float.MaxValue, DataState.not_meaningful },
			{ float.NaN, DataState.not_valid },
			{ float.PositiveInfinity, DataState.not_meaningful },
			{ float.NegativeInfinity, DataState.not_meaningful },
			{ TimeSpan.MinValue, DataState.not_meaningful },
			{ TimeSpan.MaxValue, DataState.not_meaningful }
		};

		/// <summary>A collection of invalid, not meaningful, and/or error data points and corresponding states
		/// This dictionary is used to determine if a data point is valid or not and why
		/// *WARNING* If you are planning on overriding this, be thorough.
		/// you may also want to start with the default and add to it to avoid missing important cases.
		/// </summary>
		public Dictionary<object, DataState> DataStateCases
		{
			get { _dataStateCases = _dataStateCases ?? _defaultStateCases; return _dataStateCases; }
			set { _dataStateCases = value; }
		}
		private Dictionary<object, DataState> _dataStateCases;

		#endregion Error Handling

		#region Color Classes

		const string POS_COLOR_CLASS = "color-pos";
		const string NEG_COLOR_CLASS = "color-neg";
		const string NEU_COLOR_CLASS = "color-neu";

		/// <summary>Class name applied to spans containing positive values
		/// default: <see cref="POS_COLOR_CLASS" />
		/// </summary>
		public string PositiveColorClass
		{
			get { _posColorClass = _posColorClass ?? POS_COLOR_CLASS; return _posColorClass; }
			set { _posColorClass = value; }
		}
		private string _posColorClass;

		/// <summary>Class name applied to spans containing negative values
		/// default: <see cref="NEG_COLOR_CLASS" />
		/// </summary>
		public string NegativeColorClass
		{
			get { _negColorClass = _negColorClass ?? NEG_COLOR_CLASS; return _negColorClass; }
			set { _negColorClass = value; }
		}
		private string _negColorClass;

		/// <summary>Class name applied to spans containing neutral values
		/// default: <see cref="NEU_COLOR_CLASS" />
		/// </summary>
		public string NeutralColorClass
		{
			get { _neuColorClass = _neuColorClass ?? NEU_COLOR_CLASS; return _neuColorClass; }
			set { _neuColorClass = value; }
		}
		private string _neuColorClass;

		#endregion Color Classes
	}
}
