using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Dependencies;

namespace Dashen.Infrastructure
{
	internal class CompositeDependencyScope : IDependencyScope
	{
		private readonly IDependencyScope _original;
		private readonly StructureMapDependencyScope _custom;

		public CompositeDependencyScope(IDependencyScope original, StructureMapDependencyScope custom)
		{
			_original = original;
			_custom = custom;
		}

		public object GetService(Type serviceType)
		{
			var service = _custom.GetService(serviceType);

			if (service != null)
				return service;

			return _original.GetService(serviceType);
		}

		public IEnumerable<object> GetServices(Type serviceType)
		{
			var services = _custom.GetServices(serviceType).ToList();

			if (services.Any())
				return services;

			return _original.GetServices(serviceType);
		}

		public void Dispose()
		{
			_original.Dispose();
			_custom.Dispose();
		}
	}
}
