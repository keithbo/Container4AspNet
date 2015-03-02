namespace Container4AspNet.Windsor.Mvc
{
	using Castle.Windsor;
	using System.Collections.Generic;
	using System.Web.Mvc;

	public class WindsorMvcFilterProvider : System.Web.Mvc.IFilterProvider
	{
		private IWindsorContainer _container;

		public WindsorMvcFilterProvider(IWindsorContainer container)
		{
			this._container = container;
		}

		public IEnumerable<Filter> GetFilters(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
		{
			foreach (IActionFilter actionFilter in this._container.ResolveAll<IActionFilter>()){
				yield return new Filter(actionFilter, FilterScope.First, null);
			}
		}
	}
}
