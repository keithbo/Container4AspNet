namespace Container4AspNet.Windsor.Mvc
{
	using Castle.Windsor;
	using System;

	public class WindsorMvcViewPageActivator : System.Web.Mvc.IViewPageActivator
	{
		private IWindsorContainer _container;

		public WindsorMvcViewPageActivator(IWindsorContainer container)
		{
			this._container = container;
		}

		public object Create(System.Web.Mvc.ControllerContext controllerContext, Type type)
		{
			return this._container.Resolve(type);
		}
	}
}
