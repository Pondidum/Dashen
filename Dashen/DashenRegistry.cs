using Dashen.Configuration;
using Dashen.Infrastructure.Spark;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;

namespace Dashen
{
	public class DashenRegistry : Registry
	{
		public DashenRegistry()
		{
			For<SparkEngine>().Singleton();
			For<DefinitionCollection>().Singleton();

			Scan(a =>
			{
				a.TheCallingAssembly();
				a.AddAllTypesOf<IDashboardConfiguration>();
			});
		}
	}
}
