using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Dashen.Endpoints.Static.ContentProviders;

namespace Dashen
{
	public class DashenConfiguration
	{
		public Uri ListenOn { get; set; }

		public Color HighlightColor { get; set; }
		public Color LowlightColor { get; set; }

		internal Dictionary<string, AdhocContentProvider.ResourceContent> Resources { get; private set; }

		public DashenConfiguration()
		{
			Resources = new Dictionary<string, AdhocContentProvider.ResourceContent>();

			ListenOn = new Uri("http://localhost:8080");
			HighlightColor = ColorTranslator.FromHtml("#33CC33");
			LowlightColor = ColorTranslator.FromHtml("#248F24");
		}

		public void AddResource(string relativeUrlFragment, Stream content, string mimeType)
		{
			using (var ms = new MemoryStream())
			{
				content.CopyTo(ms);
				
				Resources[relativeUrlFragment] = new AdhocContentProvider.ResourceContent
				{
					StreamBytes = ms.ToArray(),
					MimeType = mimeType
				};
			}
		}
	}
}
