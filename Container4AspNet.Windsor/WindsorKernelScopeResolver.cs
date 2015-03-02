namespace Container4AspNet.Windsor
{
	using Castle.MicroKernel;
	using Castle.MicroKernel.Lifestyle;
	using System;

	public class WindsorKernelScopeResolver : IScopeResolver
	{
		private IKernel _kernel;

		public WindsorKernelScopeResolver(IKernel kernel)
		{
			this._kernel = kernel;
		}

		public IDisposable NewScope()
		{
			return this._kernel.BeginScope();
		}

		public void Dispose()
		{
		}
	}
}
