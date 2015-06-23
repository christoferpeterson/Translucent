using System;
using System.Web.Mvc;

public class PlaceholderAttribute : Attribute, IMetadataAware
{
	private readonly string _placeholder;
	public PlaceholderAttribute(string placeholder)
	{
		_placeholder = placeholder;
	}

	public void OnMetadataCreated(ModelMetadata metadata)
	{
		metadata.AdditionalValues["placeholder"] = _placeholder;
	}
}