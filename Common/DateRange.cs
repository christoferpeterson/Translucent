using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Translucent.Common
{
	public class DateRange
	{
		public DateTime Start { get; private set; }
		public DateTime End { get; private set; }

		public DateRange(DateTime start, DateTime end)
		{
			Start = start;
			End = end;
		}

		private TimeSpan? _timespan;

		public TimeSpan TimeSpan { get { _timespan = _timespan ?? End - Start; return _timespan.Value; } }
	}
}
