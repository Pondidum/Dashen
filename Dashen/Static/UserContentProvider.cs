using System.Linq;
using Dashen.Infrastructure;

namespace Dashen.Static
{
	public class UserContentProvider : IStaticContentProvider
	{
		private readonly DashboardConfiguration _configuration;

		public UserContentProvider(DashboardConfiguration configuration)
		{
			_configuration = configuration;
		}

		public bool Handles(string directory)
		{
			return directory.EqualsIgnore("user");
		}

		public Resource GetResource(string directory, string filename)
		{
			var resource = _configuration
				.Resources
				.FirstOrDefault(r => r.Name.EqualsIgnore(filename));

			return resource ?? Resource.Empty;
		}
	}
}
