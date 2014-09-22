using System.Collections.Generic;
using Dashen.Infrastructure;

namespace Dashen.Endpoints.Static
{
	public class ReplacementSource
	{
		private readonly Dictionary<string, string> _replacements;

		public ReplacementSource()
		{
			_replacements = new Dictionary<string, string>();
		}

		public void Add(string key, string value)
		{
			_replacements.Add("!{" + key + "}", value);
		}

		public string Process(string input)
		{
			_replacements.Each(pair => input = input.Replace(pair.Key, pair.Value));

			return input;
		}
	}
}
