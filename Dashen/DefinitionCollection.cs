using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Dashen
{
	public class DefinitionCollection : IEnumerable<Widget>
	{
		private readonly List<Widget> _definitions;

		public DefinitionCollection()
		{
			_definitions = new List<Widget>();
		}

		public void Add(Widget definition)
		{
			_definitions.Add(definition);
		}

		public Widget GetByID(string id)
		{
			return _definitions.FirstOrDefault(d => d.ID.Equals(id, StringComparison.OrdinalIgnoreCase));
		}

		public IEnumerator<Widget> GetEnumerator()
		{
			return _definitions.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

	}
}
