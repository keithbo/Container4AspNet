namespace Container4AspNet.Mvc
{
    using System;
    using System.Collections.Generic;

    internal class NullMvcDependencyResolver : System.Web.Mvc.IDependencyResolver
    {
        public object GetService(Type serviceType)
        {
            return null;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return new object[0];
        }
    }
}
