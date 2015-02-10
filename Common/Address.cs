using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Translucent.Common
{
	public class Address
	{
		public string Name { get; set; }
		public string StreetOne { get; set; }
		public string StreetTwo { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public double Zip { get; set; }

		public string GetDirectionsURL
		{
			get
			{
				return String.Format("http://maps.google.com/maps?rls=ig&hl=en&q={0}&um=1&ie=UTF-8&hq=&hnear={0}+{1}", ToUrlEncodedString(), Zip);
			}
		}

		private string _rawAddress;

		public string CityStateZip { get { return string.Format("{0}, {1} {2:######}", City, State, Zip); } }

		public string RawAddress
		{
			get { return _rawAddress; }
			set
			{
				_rawAddress = value;
				ParseAddress();
			}
		}

		private void ParseAddress()
		{
			return;
		}

		public string ToUrlEncodedString()
		{
			var s = StreetOne + " " + StreetTwo + " " + City + ", " + State;

			return HttpUtility.UrlEncode(s);
		}
	}
}
