using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Reflection;
using System.Text;

/// <summary>Export a specific data type to a CSV file
/// </summary>
/// <typeparam name="dataType">The data type being exported</typeparam>
public class CsvExport<dataType> where dataType : class
{
	public List<dataType> Objects;

	/// <summary>Provide a data set to be exported
	/// </summary>
	/// <param name="objects"></param>
	public CsvExport(List<dataType> objects)
	{
		Objects = objects;
	}

	/// <summary>Export to a CSV type string including headers
	/// </summary>
	/// <returns></returns>
	public string Export()
	{
		return Export(true);
	}

	/// <summary>Export to a CSV string
	/// </summary>
	/// <param name="includeHeaderLine">Indicates whether column headers should be included</param>
	/// <returns>string in CSV format</returns>
	public string Export(bool includeHeaderLine)
	{

		StringBuilder sb = new StringBuilder();
		//Get properties using reflection.
		IList<PropertyInfo> propertyInfos = typeof(dataType).GetProperties();

		if (includeHeaderLine)
		{
			//add header line.
			foreach (PropertyInfo propertyInfo in propertyInfos)
			{
				if (Attribute.IsDefined(propertyInfo, typeof(JsonIgnoreAttribute)))
				{
					continue;
				}

				sb.Append(propertyInfo.Name).Append(",");
			}
			sb.Remove(sb.Length - 1, 1).AppendLine();
		}

		//add value for each property.
		foreach (dataType obj in Objects)
		{
			foreach (PropertyInfo propertyInfo in propertyInfos)
			{
				if (Attribute.IsDefined(propertyInfo, typeof(JsonIgnoreAttribute)))
				{
					continue;
				}

				sb.Append(MakeValueCsvFriendly(propertyInfo.GetValue(obj, null))).Append(",");
			}
			sb.Remove(sb.Length - 1, 1).AppendLine();
		}

		return sb.ToString();
	}

	/// <summary>Export to a specific file
	/// </summary>
	/// <param name="path">save path</param>
	public void ExportToFile(string path)
	{
		File.WriteAllText(path, Export());
	}

	/// <summary>Export the data to bytes (UTF8)
	/// </summary>
	/// <returns>byte array</returns>
	public byte[] ExportToBytes()
	{
		return Encoding.UTF8.GetBytes(Export());
	}

	/// <summary>Format the data to be CSV friendly
	/// </summary>
	/// <param name="value">data to be formated</param>
	/// <returns>string for use in the CSV data</returns>
	private string MakeValueCsvFriendly(object value)
	{
		if (value == null) return "";
		if (value is Nullable && ((INullable)value).IsNull) return "";

		if (value is DateTime)
		{
			if (((DateTime)value).TimeOfDay.TotalSeconds == 0)
				return ((DateTime)value).ToString("yyyy-MM-dd");
			return ((DateTime)value).ToString("yyyy-MM-dd HH:mm:ss");
		}
		string output = value.ToString();

		if (output.Contains(",") || output.Contains("\""))
			output = '"' + output.Replace("\"", "\"\"") + '"';

		return output;

	}
}