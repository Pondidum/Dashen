using System.Collections;
using Newtonsoft.Json;

namespace Dashen.Components
{
	[JsonArray]
	public class Pair : IEnumerable
	{
		private readonly int[] _array;

		public Pair(int x, int y)
		{
			_array = new[] { x, y };
		}

		public IEnumerator GetEnumerator()
		{
			return _array.GetEnumerator();
		}
	}
}
