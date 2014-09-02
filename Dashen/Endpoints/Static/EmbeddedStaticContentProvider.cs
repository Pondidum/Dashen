using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Dashen.Endpoints.Static
{
	public class EmbeddedStaticContentProvider : IStaticContentProvider
	{
		private const string Prefix = "Dashen.Static.";

		private readonly Dictionary<string, Func<Stream>> _resources;
		private readonly MimeLookup _mimeLookup;

		public EmbeddedStaticContentProvider(MimeLookup mimeLookup)
		{
			_mimeLookup = mimeLookup;
			var assembly = GetType().Assembly;

			_resources = assembly
				.GetManifestResourceNames()
				.Where(name => name.StartsWith(Prefix))
				.ToDictionary(
					name => name.Substring(Prefix.Length),
					name => new Func<Stream>(() => assembly.GetManifestResourceStream(name)),
					StringComparer.OrdinalIgnoreCase);

		}
		public StaticContent GetContent(string path)
		{
			if (_resources.ContainsKey(path) == false)
			{
				return null;
			}

			var stream = _resources[path].Invoke();
			var mime = _mimeLookup.Get(path);

			return new StaticContent { Stream = stream, MimeType = mime };
		}
	}
}
