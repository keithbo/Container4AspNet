namespace Container4AspNet.WebApi
{
    using System;
    using System.Collections.Generic;

    internal class NullWebApiDependencyResolver : System.Web.Http.Dependencies.IDependencyResolver
    {
        public object GetService(Type serviceType)
        {
            return null;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return new object[0];
        }

        public System.Web.Http.Dependencies.IDependencyScope BeginScope()
        {
            return null;
        }

        public void Dispose()
        {
        }
    }
}
