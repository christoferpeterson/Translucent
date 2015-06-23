using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

public static class ObjectExtensions
{
	public static string Serialize(this object o)
	{
		var serializer = new JavaScriptSerializer();
		return serializer.Serialize(o);
	}
}
