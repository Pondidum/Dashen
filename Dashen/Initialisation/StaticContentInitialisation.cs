using System.Drawing;
using System.Web.Http.SelfHost;
using Dashen.Endpoints.Static;
using Dashen.Endpoints.Static.ContentProviders;
using Dashen.Infrastructure;

namespace Dashen.Initialisation
{
	public class StaticContentInitialisation : IDashboardInitialisation
	{
		private readonly DashenConfiguration _userConfiguration;
		private readonly ReplacementSource _replacements;
		private readonly AdhocContentProvider _adhockContent;

		public StaticContentInitialisation(DashenConfiguration userConfiguration, ReplacementSource replacements, AdhocContentProvider adhockContent)
		{
			_userConfiguration = userConfiguration;
			_replacements = replacements;
			_adhockContent = adhockContent;
		}

		public void ApplyTo(HttpSelfHostConfiguration config)
		{
			_replacements.Add(
				_userConfiguration.GetMemberName(x => x.HighlightColor),
				ColorTranslator.ToHtml(_userConfiguration.HighlightColor));

			_replacements.Add(
				_userConfiguration.GetMemberName(x => x.LowlightColor),
				ColorTranslator.ToHtml(_userConfiguration.LowlightColor));

			_userConfiguration
				.Resources
				.Each(res => _adhockContent.Add(res.Key, res.Value));
		}
	}
}
