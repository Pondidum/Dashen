using System.Collections;
using Newtonsoft.Json;

namespace Dashen.Components
{
	[JsonArray]
	public class Label : IEnumerable
	{
		private readonly object[] _array;

		public Label(int value, string text)
		{
			_array = new object[] { value, text };
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return _array.GetEnumerator();
		}
	}
}
