using System;
using System.Collections.Generic;
using System.IO;
using Dashen.Infrastructure;

namespace Dashen.Endpoints.Static
{
	public class MimeLookup
	{
		private readonly Dictionary<string, string> _mimeLookup;

		public MimeLookup()
		{
			_mimeLookup = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
			_mimeLookup[".js"] = "text/javascript";
			_mimeLookup[".css"] = "text/css";
		}

		public string Get(string filename)
		{
			return _mimeLookup.Get(Path.GetExtension(filename));
		}
	}
}
