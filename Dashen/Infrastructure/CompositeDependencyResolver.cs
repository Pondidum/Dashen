using System.Web.Http.Dependencies;

namespace Dashen.Infrastructure
{
	public class CompositeDependencyResolver : CompositeDependencyScope, IDependencyResolver
	{
		private readonly IDependencyResolver _original;
		private readonly StructureMapDependencyResolver _custom;

		public CompositeDependencyResolver(IDependencyResolver original, StructureMapDependencyResolver custom)
			: base(original, custom)
		{
			_original = original;
			_custom = custom;
		}

		public IDependencyScope BeginScope()
		{
			var s1 = _original.BeginScope();
			var s2 = (StructureMapDependencyScope) _custom.BeginScope();

			return new CompositeDependencyScope(s1, s2);
		}
	}
}