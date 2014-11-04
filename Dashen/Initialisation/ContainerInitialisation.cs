﻿using System.Web.Http;
using System.Web.Http.SelfHost;
using Dashen.Infrastructure;
using StructureMap;

namespace Dashen.Initialisation
{
	public class ContainerInitialisation : IDashboardInitialisation
	{
		private readonly IContainer _container;

		public ContainerInitialisation(IContainer container)
		{
			_container = container;
		}

		public void ApplyTo(HttpConfiguration config)
		{
			config.DependencyResolver = new StructureMapDependencyResolver(_container);
		}
	}
}
