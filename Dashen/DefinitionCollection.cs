using System.Collections.Generic;

namespace Dashen
{
	public class DefinitionCollection
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
	}
}
