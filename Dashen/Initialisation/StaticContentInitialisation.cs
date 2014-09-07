using System.Drawing;
using System.Web.Http.SelfHost;
using Dashen.Endpoints.Static;
using Dashen.Endpoints.Static.ContentProviders;
using Dashen.Infrastructure;

namespace Dashen.Initialisation
{
	public class StaticContentInitialisation : IDashboardInitialisation
	{
		private readonly ReplacementSource _replacements;
		private readonly AdhocContentProvider _adhockContent;

		public StaticContentInitialisation(ReplacementSource replacements, AdhocContentProvider adhockContent)
		{
			_replacements = replacements;
			_adhockContent = adhockContent;
		}

		public void ApplyTo(DashenConfiguration userConfig, HttpSelfHostConfiguration config)
		{
			_replacements.Add(
				userConfig.GetMemberName(x => x.HighlightColor), 
				ColorTranslator.ToHtml(userConfig.HighlightColor));

			_replacements.Add(
				userConfig.GetMemberName(x => x.LowlightColor),
				ColorTranslator.ToHtml(userConfig.LowlightColor));

			userConfig
				.Resources
				.Each(res => _adhockContent.Add(res.Key, res.Value));
		}
	}
}
