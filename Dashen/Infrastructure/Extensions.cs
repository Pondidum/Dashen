using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Newtonsoft.Json;

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

		[DebuggerStepThrough]
		public static void Each<T>(this IEnumerable<T> values, Action<T> eachAction)
		{
			foreach (var item in values)
			{
				eachAction(item);
			}
		}

		public static string ToJson<T>(this T target)
		{
			return JsonConvert.SerializeObject(target);
		}

		public static string ToFlatJson<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source)
		{
			return source.Select(p => new object[] { p.Key, p.Value }).ToJson();
		}
	}


}
