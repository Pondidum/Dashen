using System;
using System.Collections.Generic;

namespace Dashen.Infrastructure
{
	public class ContentTypeMap
	{
		private static readonly Dictionary<string, string> _map;

		static ContentTypeMap()
		{
			_map = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

			_map[".jsx"] = "text/javascript";
			_map[".js"] = "text/javascript";
			_map[".css"] = "text/css";
		}

		public static string  GetMimeType(string extension)
		{
			string result;

			return _map.TryGetValue(extension, out result) ? result : "text/plain";
		}
	}
}
