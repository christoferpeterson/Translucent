using System;
using System.Text.RegularExpressions;
using System.Linq;

public static class StringExtensions
{
	public static string StripHtml(this string source)
	{
		return Regex.Replace(source, "<.*?>", string.Empty);
	}

	public static string StripHtmlScriptTags(this string source)
	{
		return Regex.Replace(source, @"<script [^>]*>[\s\S]*?</script>", string.Empty);
	}

	/// <summary>Truncate a string to a word specified length
	/// </summary>
	/// <param name="s">extension method</param>
	/// <param name="maxLength">maximum number of characters</param>
	/// <returns>a truncated string of length maxLength - 3, ellipsis (...) appended</returns>
	public static string Truncate(this string s, uint maxLength)
	{
		return s.Truncate(maxLength, "...");
	}

	/// <summary>Truncate a string to a word specified length
	/// </summary>
	/// <param name="s">extension method</param>
	/// <param name="maxLength">maximum number of characters</param>
	/// <param name="append">the string to attached to the end of the truncation</param>
	/// <returns>a truncated string of length maxLength - 3, ellipsis (...) appended</returns>
	public static string Truncate(this string s, uint maxLength, string append)
	{
		return s.Truncate(maxLength, append, new char[] { ' ', ',', '/', '\\', '\'', '\"', '.', '?', '(', '[', '{', '<' });
	}

	/// <summary>Truncate a string to specified length
	/// </summary>
	/// <param name="s">extension method</param>
	/// <param name="maxLength">maximum number of characters</param>
	/// <param name="append">the string to attached to the end of the truncation</param>
	/// <param name="truncateChars">an array of characters to be removed form the end of the string</param>
	/// <returns>a truncated string of length maxLength - append length, with a specified append</returns>
	public static string Truncate(this string s, uint maxLength, string append, char[] truncateChars)
	{
		if (s == null || s.Trim().Length <= maxLength || truncateChars == null)
			return s;

		int index = s.Trim().LastIndexOfAny(truncateChars);

		// find the index of where to truncate using the provided list of characters
		while ((index + append.Length) > maxLength)
			index = s.Substring(0, index).Trim().LastIndexOfAny(truncateChars);

		// if the index is > 0, truncate the string and append
		if (index > 0)
			return s.Substring(0, index) + append;

		// if the index is not 0, truncate regardless of provided characters
		return s.Substring(0, (int)maxLength - append.Length).Trim() + append;
	}

	/// <summary>Trim the trailing characters of a string based on input
	/// </summary>
	/// <param name="source">the string to trim</param>
	/// <param name="truncateChars">the characters to remove, must be all inclusive</param>
	/// <returns>The string with offending, trailing characters removed</returns>
	private static string RemoveTrailingCharacters(string source, char[] truncateChars)
	{
		if(!String.IsNullOrEmpty(source) && truncateChars != null && truncateChars.Contains(source[source.Length]))
		{
			return RemoveTrailingCharacters(source.Substring(0, source.Length - 1), truncateChars);
		}

		return source;
	}
}
