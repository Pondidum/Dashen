﻿using System.Web.Http;
using System.Web.Http.SelfHost;

namespace Dashen.Initialisation
{
	public interface IDashboardInitialisation
	{
		void ApplyTo(HttpConfiguration config);
	}
}
