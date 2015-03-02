namespace Container4AspNet.Windsor.Mvc
{
	using Castle.Windsor;
	using System.Collections.Generic;
	using System.Web.Mvc;

	/// <summary>
	/// IFilterProvider implementation that adapts all IActionFilter instances in the container
	/// out as Filter instances.
	/// </summary>
	public class WindsorMvcFilterProvider : System.Web.Mvc.IFilterProvider
	{
		private IWindsorContainer _container;

		/// <summary>
		/// Constructs a new WindsorMvcFilterProvider
		/// </summary>
		/// <param name="container">IWindsorContainer</param>
		public WindsorMvcFilterProvider(IWindsorContainer container)
		{
			this._container = container;
		}

		/// <summary>
		/// Gets all Filter instances adapted from IActionFilter instances in IWindsorContainer
		/// </summary>
		/// <param name="controllerContext">ControllerContext</param>
		/// <param name="actionDescriptor">ActionDescriptor</param>
		/// <returns>IEnumerable of Filter instances</returns>
		public IEnumerable<Filter> GetFilters(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
		{
			foreach (IActionFilter actionFilter in this._container.ResolveAll<IActionFilter>())
			{
				yield return new Filter(actionFilter, FilterScope.First, null);
			}
		}
	}
}
