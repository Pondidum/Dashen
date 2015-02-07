using System;
using System.Collections.Generic;
using System.IO;

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

		public static MemoryStream Reset(this MemoryStream self)
		{
			self.Position = 0;
			return self;
		}

		public static Boolean EqualsIgnore(this string self, string other)
		{
			return string.Equals(self, other, StringComparison.OrdinalIgnoreCase);
		}
	}
}
