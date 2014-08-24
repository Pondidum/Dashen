using System.Collections.Generic;

namespace Dashen.Infrastructure
{
	public static class Extensions
	{
		public static TValue Get<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
		{
			return dictionary.Get(key, default(TValue));
		}

		public static TValue Get<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue defaultValue)
		{
			return dictionary.ContainsKey(key) 
				? dictionary[key] 
				: defaultValue;
		}
	}
}