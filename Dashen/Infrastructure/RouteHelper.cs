using System;
using System.Linq.Expressions;

namespace Dashen.Infrastructure
{
	public class RouteHelper
	{
		public static string For<TController>(Expression<Func<TController, object>> action)
		{
			var actionName = ReflectionExtensions.GetMemberName(action.Body);

			var controllerName = typeof(TController).Name.Replace("Controller", "");

			return string.Format("{0}/{1}/", controllerName, actionName);
		}
	}
}
