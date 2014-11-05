using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Dependencies;
using StructureMap;

namespace Dashen.Infrastructure
{
	public class StructureMapDependencyScope : IDependencyScope
	{
		public IContainer Container { get; private set; }

		public StructureMapDependencyScope(IContainer container)
		{
			Container = container;
		}

		public object GetService(Type serviceType)
		{
			if (serviceType == null)
			{
				return null;
			}

			if (serviceType.IsAbstract || serviceType.IsInterface)
			{
				return Container.TryGetInstance(serviceType);
			}

			//this is horrid, but TryGetInstance doesnt do as we need. See:
			//http://stackoverflow.com/questions/4805259/
			//http://codebetter.com/jeremymiller/2011/01/23/if-you-are-using-structuremap-with-mvc3-please-read-this/

			try
			{
				return Container.GetInstance(serviceType);
			}
			catch (Exception)
			{
				return null;
			}
		}

		public IEnumerable<object> GetServices(Type serviceType)
		{
			return Container.GetAllInstances(serviceType).Cast<Object>();
		}

		public void Dispose()
		{
			Container.Dispose();
			Container = null;
		}
	}
}
