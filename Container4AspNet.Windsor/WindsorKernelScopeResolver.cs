namespace Container4AspNet.Windsor
{
	using Castle.MicroKernel;
	using Castle.MicroKernel.Lifestyle;
	using System;

	/// <summary>
	/// Basic scope implementation of IScopeResolver to make use of
	/// IKernel.BeginScope
	/// </summary>
	public class WindsorKernelScopeResolver : IScopeResolver
	{
		private IKernel _kernel;

		/// <summary>
		/// Constructs a new WindsorKernelScopeResolver using IKernel
		/// </summary>
		/// <param name="kernel">IKernel to source scope creation</param>
		public WindsorKernelScopeResolver(IKernel kernel)
		{
			this._kernel = kernel;
		}

		/// <summary>
		/// Generates a new scope
		/// </summary>
		/// <returns>IDisposable scope instance</returns>
		public IDisposable NewScope()
		{
			return this._kernel.BeginScope();
		}

		/// <summary>
		/// Disposes this WindsorKernelScopeResolver
		/// </summary>
		public void Dispose()
		{
		}
	}
}
