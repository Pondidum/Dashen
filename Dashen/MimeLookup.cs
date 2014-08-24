using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using Dashen.Infrastructure;

namespace Dashen
{
	public class MimeLookup
	{
		private readonly Dictionary<string, MediaTypeHeaderValue> _mimeLookup;

		public MimeLookup()
		{
			_mimeLookup = new Dictionary<string, MediaTypeHeaderValue>(StringComparer.OrdinalIgnoreCase);
			_mimeLookup[".js"] = new MediaTypeHeaderValue("text/javascript");
			_mimeLookup[".css"] = new MediaTypeHeaderValue("text/css");
		}

		public MediaTypeHeaderValue Get(string filename)
		{
			return _mimeLookup.Get(Path.GetExtension(filename));
		}
	}
}
