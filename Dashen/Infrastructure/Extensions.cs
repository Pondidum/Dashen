using System;
using System.Collections.Generic;

namespace Dashen.Infrastructure
{
	public static class Extensions
	{
		public static void ForEach<T>(this IEnumerable<T> self, Action<T> action)
		{
			foreach (var item in self)
			{
				action(item);
			}
		}
	}
}
