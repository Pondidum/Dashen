using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using Dashen.Controls;
using Dashen.Endpoints.Static.ContentProviders;
using Dashen.Infrastructure;

namespace Dashen
{
	public class DashenConfiguration
	{
		/// <summary>
		/// The Uri to host the webui on.
		/// </summary>
		public Uri ListenOn { get; set; }

		/// <summary>
		/// A bright colour for highlighting UI elements.
		/// </summary>
		public Color HighlightColor { get; set; }

		/// <summary>
		/// A dark colour for highlighting UI elements.<br/>
		/// Used by Graphs, ProgressBars for outline/progress colour.
		/// </summary>
		public Color LowlightColor { get; set; }

		/// <summary>
		/// The name to display on the dashboard.
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// The version to display on the dashboard.
		/// </summary>
		public string Version { get; set; }

		/// <summary>
		/// Custom MessageHandlers to be invoked on each request.<br/>
		/// Console logging can be added/removed by <see cref="EnableConsoleLog"/> and <see cref="DisableConsoleLog"/>.
		/// </summary>
		public List<DelegatingHandler> MessageHandlers { get; private set; }

		internal Dictionary<string, AdhocContentProvider.ResourceContent> Resources { get; private set; }
		internal Dictionary<Type, byte[]> CustomViews { get; private set; }
		
		public string Prefix { get; set; }

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
			Prefix = "Dashen";
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

		/// <summary>
		/// Adds a custom model and view pair for widgets
		/// </summary>
		/// <typeparam name="T">The model type for the widget's view</typeparam>
		/// <param name="view">Byte array representing a spark view</param>
		public void AddWidgetTypeAndView<T>(byte[] view) where T : WidgetModel
		{
			CustomViews[typeof (T)] = view;
		}

		internal string ApplyPrefix(string route)
		{
			if (string.IsNullOrWhiteSpace(Prefix))
			{
				return route;
			}

			var prefix = Prefix.Trim('/');

			if (string.IsNullOrWhiteSpace(route))
			{
				return prefix;
			}

			return prefix + '/' + route;
		}
	}
}
