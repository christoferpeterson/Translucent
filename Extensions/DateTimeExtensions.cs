using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Translucent.Extensions
{
	public static class DateTimeExtensions
	{
		/// <summary>Determine which datetime is larger
		/// </summary>
		/// <param name="d">dates to check</param>
		/// <returns>the largest date</returns>
		public static DateTime? Max(params DateTime?[] d)
		{
			return d.Max();
		}

		/// <summary>Determine which datetime is smaller
		/// </summary>
		/// <param name="d">dates to check</param>
		/// <returns>the smallest date</returns>
		public static DateTime? Min(params DateTime?[] d)
		{
			return d.Min();
		}
	}
}
