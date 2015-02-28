using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Container4AspNet.DependencyInjection
{
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
