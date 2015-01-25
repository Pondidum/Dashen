using System;
using System.Collections.Generic;
using System.Linq;

namespace Dashen
{
	public class AssetInfo
	{
		private const string SelfClosingFormat = "<{0} {1} />";
		private const string NonClosingFormat = "<{0} {1}></{0}>";

		public string Tag { get; protected set; }
		public bool SelfClosing { get; protected set; }
		public AssetLocations Location { get; protected set;  }

		private readonly Dictionary<string, string> _attributes;
		private readonly Lazy<string> _tag;

		public AssetInfo()
		{
			_attributes = new Dictionary<string, string>();
			_tag = new Lazy<string>(BuildTag);
		}

		public void AddAttribute(string name, string value)
		{
			_attributes[name] = value;
		}

		private string BuildTag()
		{
			var format = SelfClosing ? SelfClosingFormat : NonClosingFormat;

			return string.Format(
				format,
				Tag,
				BuildAttributes());
		}

		private string BuildAttributes()
		{
			var format = "{0}=\"{1}\"";

			return _attributes
				.Select(pair => string.Format(format, pair.Key, pair.Value))
				.Aggregate((a, c) => a + " " + c);
		}

		public override string ToString()
		{
			return _tag.Value;
		}
	}
}
