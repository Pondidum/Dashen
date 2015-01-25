using System.Collections.Generic;
using System.Linq;

namespace Dashen
{
	public class Component<TModel> : IJsxProvider
		where TModel : Model
	{
		public ComponentName Name { get; private set; }

		public Component()
		{
			Name = new ComponentName(this);
		}

		public void AddAsset(string key, string contents)
		{
		}

		public IEnumerable<AssetInfo> GetAssets()
		{
			return Enumerable.Empty<AssetInfo>();
		}

		public virtual string GetJsx()
		{
			return string.Empty;
		}
	}
}
