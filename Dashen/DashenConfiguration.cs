using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using Dashen.Endpoints.Static.ContentProviders;
using Dashen.Endpoints.Stats;
using Dashen.Infrastructure;

namespace Dashen
{
	public class DashenConfiguration
	{
		public Uri ListenOn { get; set; }

		public Color HighlightColor { get; set; }
		public Color LowlightColor { get; set; }
		public string Title { get; set; }
		public string Version { get; set; }

		public List<DelegatingHandler> MessageHandlers { get; private set; }
		internal Dictionary<string, AdhocContentProvider.ResourceContent> Resources { get; private set; }

		internal Dictionary<Type, byte[]> CustomViews { get; private set; }

		public DashenConfiguration()
		{
			CustomViews = new Dictionary<Type, byte[]>();
			Resources = new Dictionary<string, AdhocContentProvider.ResourceContent>();
			MessageHandlers = new List<DelegatingHandler>();

			ListenOn = new Uri("http://localhost:8080");

			HighlightColor = ColorTranslator.FromHtml("#33CC33");
			LowlightColor = ColorTranslator.FromHtml("#248F24");

			Title = "Dashen Stats Panel";
			Version = GetType().Assembly.GetName().Version.ToString();
		}

		/// <summary>
		/// Adds a resource to be used in the webui.
		/// </summary>
		/// <param name="relativeUrlFragment">The relative path, e.g. "/img/good.png"</param>
		/// <param name="content">The content to be used.  The stream can be safely closed after this call.</param>
		/// <param name="mimeType">The mimetype for the resource, e.g. "image/png"</param>
		public DashenConfiguration AddResource(string relativeUrlFragment, Stream content, string mimeType)
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

			return this;
		}

		/// <summary>
		/// Logs all requests to the Console.<br/>
		/// To add custom handlers, use the <see cref="MessageHandlers"/> property.
		/// </summary>
		public DashenConfiguration EnableConsoleLog()
		{
			if (MessageHandlers.OfType<ConsoleLoggingHandler>().Any() == false)
			{
				MessageHandlers.Add(new ConsoleLoggingHandler());
			}

			return this;
		}

		/// <summary>
		/// Disables logging of requests to the Console.
		/// </summary>
		public DashenConfiguration DisableConsoleLog()
		{
			MessageHandlers
				.OfType<ConsoleLoggingHandler>()
				.ToList()
				.Each(mh => MessageHandlers.Remove(mh));

			return this;
		}

		public void AddControlView<T>(byte[] view) where T : ControlViewModel
		{
			CustomViews[typeof (T)] = view;
		}
	}
}
