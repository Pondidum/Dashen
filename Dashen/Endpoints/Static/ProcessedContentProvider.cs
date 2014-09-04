using System;
using System.IO;

namespace Dashen.Endpoints.Static
{
	public class ProcessedContentProvider : IStaticContentProvider
	{
		private readonly IStaticContentProvider _provider;
		private readonly ReplacementSource _replacements;

		public ProcessedContentProvider(IStaticContentProvider provider, ReplacementSource replacements)
		{
			_provider = provider;
			_replacements = replacements;
		}

		public StaticContent GetContent(string path)
		{
			var content = _provider.GetContent(path);

			if (content == null)
			{
				return null;
			}

			var ms = new MemoryStream();
			var writer = new StreamWriter(ms);

			using (var reader = new StreamReader(content.Stream))
			{
				string line;
				while ((line = reader.ReadLine()) != null)
				{
					writer.WriteLine(_replacements.Process(line));
				}
			}

			writer.Flush();
			ms.Position = 0;

			return new StaticContent { Stream = ms, MimeType = content.MimeType };
		}
	}
}
