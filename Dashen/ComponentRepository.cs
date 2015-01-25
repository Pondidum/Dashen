using System;
using System.Collections.Generic;

namespace Dashen
{
	public class ComponentRepository
	{
		private readonly Dictionary<string, string> _components;

		public ComponentRepository()
		{
			_components = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
		}

		public void Register(IJsxProvider component)
		{
			var jsx = component.GetJsx();
			var name = component.GetType().Name;

			var fileContents = string.Format(
				"var {0} = React.createClass({{{1}}});",
				name, 
				jsx);

			_components[name] = fileContents;
		}

		public string GetComponent(string name)
		{
			return _components[name];
		}
	}
}
