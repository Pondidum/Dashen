using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Dashen
{
	public class DefinitionCollection : IEnumerable<Definition>
	{
		private readonly List<Definition> _definitions;

		public DefinitionCollection()
		{
			_definitions = new List<Definition>();
		}

		public void Add(Definition definition)
		{
			_definitions.Add(definition);
		}

		public Definition GetByName(string name)
		{
			return _definitions.FirstOrDefault(d => d.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
		}

		public IEnumerator<Definition> GetEnumerator()
		{
			return _definitions.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

	}
}
