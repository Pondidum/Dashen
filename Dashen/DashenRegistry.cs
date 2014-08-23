using Dashen.Infrastructure.Spark;
using StructureMap.Configuration.DSL;

namespace Dashen
{
	public class DashenRegistry : Registry
	{
		public DashenRegistry()
		{
			For<SparkEngine>().Use<SparkEngine>().Singleton();
		}
	}
}
