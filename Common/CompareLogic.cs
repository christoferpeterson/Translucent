using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Translucent.Common
{
	public enum CompareLogic
	{
		/// <summary>Equal To
		/// </summary>
		EQ,

		/// <summary>Not Equal To
		/// </summary>
		NEQ,

		/// <summary>Greater than
		/// </summary>
		GT,

		/// <summary>Greater than or equal to
		/// </summary>
		GEQ,

		/// <summary>Not greater than
		/// </summary>
		NGT,

		/// <summary>Less Than
		/// </summary>
		LT,

		/// <summary>Less than or equal to
		/// </summary>
		LEQ,

		/// <summary>Not less than
		/// </summary>
		NLT,

		/// <summary>Like
		/// </summary>
		LIKE,

		/// <summary>Not Like
		/// </summary>
		NOTLIKE
	}

	public static class CompareLogicExtensions
	{
		/// <summary>Convert a compare logic enum value into a usable T-SQL operator
		/// </summary>
		/// <param name="logic">extension method</param>
		/// <returns>Returns the operator used in Transact-SQL queries</returns>
		public static string TSQLOperator(this CompareLogic logic)
		{
			switch (logic)
			{
				case CompareLogic.EQ:
					return "=";
				case CompareLogic.NEQ:
					return "!=";
				case CompareLogic.GT:
					return ">";
				case CompareLogic.GEQ:
					return ">=";
				case CompareLogic.LT:
					return "<";
				case CompareLogic.LEQ:
					return "<=";
				case CompareLogic.NGT:
					return "!>";
				case CompareLogic.NLT:
					return "!<";
				case CompareLogic.LIKE:
					return "LIKE";
				case CompareLogic.NOTLIKE:
					return "NOT LIKE";
				default:
					throw new Exception("Unsupported logic operator.");
			}
		}
	}
}
