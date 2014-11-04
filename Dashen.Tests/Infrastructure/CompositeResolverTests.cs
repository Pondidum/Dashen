using Dashen.Infrastructure;
using Shouldly;
using StructureMap;
using Xunit;

namespace Dashen.Tests.Infrastructure
{
	public class CompositeResolverTests
	{
		private readonly IContainer _primary;
		private readonly IContainer _custom;
		private readonly CompositeDependencyResolver _composite;

		public CompositeResolverTests()
		{
			_primary = new Container(c =>
			{
				c.For<IPrimary>().Use<InPrimary>();
				c.For<IBoth>().Use<InBoth>();
				c.For<InPrimaryWithDependency>();
			});
			
			_custom = new Container(c =>
			{
				c.For<ICustom>().Use<InCustom>();
				c.For<IBoth>().Use<InBoth>();
			});

			_composite = new CompositeDependencyResolver(new StructureMapDependencyResolver(_primary), new StructureMapDependencyResolver(_custom));
		}

		[Fact]
		public void Test_setup_is_correct()
		{
			_primary.TryGetInstance<IPrimary>().ShouldNotBe(null);
			_primary.TryGetInstance<IBoth>().ShouldNotBe(null);
			_primary.TryGetInstance<ICustom>().ShouldBe(null);
			_primary.TryGetInstance<INeither>().ShouldBe(null);

			_custom.TryGetInstance<IPrimary>().ShouldBe(null);
			_custom.TryGetInstance<IBoth>().ShouldNotBe(null);
			_custom.TryGetInstance<ICustom>().ShouldNotBe(null);
			_custom.TryGetInstance<INeither>().ShouldBe(null);
		}

		[Fact]
		public void When_the_custom_resolver_can_resolve_an_instance()
		{
			_composite.GetService(typeof(ICustom)).ShouldBeOfType<InCustom>();
		}

		[Fact]
		public void When_the_custom_resolver_cannot_resolve_and_primary_can()
		{
			_composite.GetService(typeof(IPrimary)).ShouldBeOfType<InPrimary>();
		}

		[Fact]
		public void When_the_custom_resolver_cannot_resolve_and_primary_cannot()
		{
			_composite.GetService(typeof(INeither)).ShouldBe(null);
		}

		[Fact]
		public void When_the_custom_cannot_resolve_a_dependency_and_primary_can()
		{
			_composite.GetService(typeof (InPrimaryWithDependency)).ShouldBeOfType<InPrimaryWithDependency>();
		}

	}

	public interface IPrimary { }
	public interface ICustom { }
	public interface IBoth { }
	public interface INeither { }

	public class InPrimary : IPrimary { }
	public class InCustom : ICustom { }
	public class InBoth : IBoth { }

	public class InNeither : INeither { }

	public class InPrimaryWithDependency
	{
		public InPrimaryWithDependency(IPrimary primary) { }
	}
}
