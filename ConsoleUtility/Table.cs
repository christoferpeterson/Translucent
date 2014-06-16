using System;
using System.Linq;
using System.Collections.Generic;

namespace Translucent.ConsoleUtility
{
	public enum TableCellAlignment
	{
		left,
		right,
		center
	}

	/// <summary>Console applications need to draw tables from time to time. This set of methods will allow tables to be drawn.
	/// </summary>
	public class Table
	{
		public int TableWidth { get; set; }
		public int CellHorizontalPadding { get; set; }

		/// <summary>print the line of a table
		/// </summary>
		public void PrintLine()
		{
			Console.WriteLine(new string('-', TableWidth));
		}

		/// <summary>Print a table row
		/// </summary>
		/// <param name="columns">what to display in the cells</param>
		public void PrintRow(params string[] columns)
		{
			PrintRow(columns.Select(c => new TableCell { Content = c, Alignment = TableCellAlignment.center }));
		}

		/// <summary>Draw a table row
		/// </summary>
		/// <param name="columns">A table row</param>
		public void PrintRow(IEnumerable<TableCell> columns)
		{
			int width = (TableWidth - columns.Count()) / columns.Count() - 2;
			string row = "| ";

			foreach(var c in columns)
			{
				if(c.Alignment == TableCellAlignment.center)
				{
					row += AlignCentre(c.Content, width);
				}
				else if(c.Alignment == TableCellAlignment.left)
				{
					row += AlignLeft(c.Content, width);
				}
				else
				{
					row += AlignRight(c.Content, width);
				}

				row += " | ";
			}

			Console.WriteLine(row);
		}

		/// <summary>Align the text in the center of the cell
		/// </summary>
		/// <param name="text">the text to display</param>
		/// <param name="width">the width of the column</param>
		/// <returns></returns>
		string AlignCentre(string text, int width)
		{
			text = Truncate(text, width);

			if (string.IsNullOrEmpty(text))
			{
				return new string(' ', width);
			}
			else
			{
				return text.PadRight(width - (width - text.Length) / 2).PadLeft(width);
			}
		}

		/// <summary>Align the text to the right
		/// </summary>
		/// <param name="text">the text input</param>
		/// <param name="width">the width of the column</param>
		/// <returns></returns>
		string AlignRight(string text, int width)
		{
			text = Truncate(text, width);

			if(string.IsNullOrEmpty(text))
			{
				return new string(' ', width);
			}
			else
			{
				return text.PadLeft(width);
			}
		}

		/// <summary>align the text to the left
		/// </summary>
		/// <param name="text">the text input</param>
		/// <param name="width">the width of the column</param>
		/// <returns></returns>
		string AlignLeft(string text, int width)
		{
			text = Truncate(text, width);

			if (string.IsNullOrEmpty(text))
			{
				return new string(' ', width);
			}
			else
			{
				return text.PadRight(width);
			}
		}

		string Truncate(string text, int width)
		{
			return text.Length > width ? text.Substring(0, width - 3) + "..." : text;
		}
	}

	public class TableCell
	{
		public string Content { get; set; }
		public TableCellAlignment Alignment { get; set; }

	}
}
