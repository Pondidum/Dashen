using System.Drawing;
using System.Web.Http.SelfHost;
using Dashen.Endpoints.Static;
using Dashen.Infrastructure;

namespace Dashen.Initialisation
{
	public class ReplacementSourceInitialisation : IDashboardInitialisation
	{
		private readonly ReplacementSource _replacements;

		public ReplacementSourceInitialisation(ReplacementSource replacements)
		{
			_replacements = replacements;
		}

		public void ApplyTo(DashenConfiguration userConfig, HttpSelfHostConfiguration config)
		{
			_replacements.Add(
				userConfig.GetMemberName(x => x.HighlightColor), 
				ColorTranslator.ToHtml(userConfig.HighlightColor));

			_replacements.Add(
				userConfig.GetMemberName(x => x.LowlightColor),
				ColorTranslator.ToHtml(userConfig.LowlightColor));
		}
	}
}
