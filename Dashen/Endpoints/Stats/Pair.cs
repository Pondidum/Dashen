using System.Collections;
using Newtonsoft.Json;

namespace Dashen.Endpoints.Stats
{
	[JsonArray]
	public class Pair : IEnumerable
	{
		private readonly int[] _array;

		public Pair(int x, int y)
		{
			_array = new[] { x, y };
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return _array.GetEnumerator();
		}
	}
}
