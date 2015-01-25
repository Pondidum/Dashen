using System;
using System.Collections.Generic;

namespace Dashen
{
	public class ComponentRepository
	{
		private readonly Dictionary<ComponentName, string> _components;

		public ComponentRepository()
		{
			_components = new Dictionary<ComponentName, string>();
		}

		public void Register(IJsxProvider component)
		{
			var jsx = component.GetJsx();
			var name = component.Name;

			var fileContents = string.Format(
				"var {0} = React.createClass({{{1}}});",
				name, 
				jsx);

			_components[name] = fileContents;
		}

		public string GetComponent(string name)
		{
			return _components[ComponentName.From(name)];
		}
	}
}
