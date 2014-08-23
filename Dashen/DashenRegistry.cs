using Dashen.Infrastructure.Spark;
using StructureMap.Configuration.DSL;

namespace Dashen
{
	public class DashenRegistry : Registry
	{
		public DashenRegistry()
		{
			For<SparkEngine>().Singleton();
			For<DefinitionCollection>().Singleton();
		}
	}
}
