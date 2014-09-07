using System.Collections.Generic;
using System.Linq;

namespace Dashen.Endpoints.Static.ContentProviders
{
	public class CompositeContentProvider : IStaticContentProvider
	{
		private readonly List<IStaticContentProvider> _others;

		public CompositeContentProvider(params IStaticContentProvider[] others)
		{
			_others = others.ToList();
		}

		public StaticContent GetContent(string urlFragment)
		{
			return _others
				.Select(o => o.GetContent(urlFragment))
				.FirstOrDefault(c => c != null);
		}
	}
}
