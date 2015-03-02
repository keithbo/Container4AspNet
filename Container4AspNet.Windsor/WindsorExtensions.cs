namespace Container4AspNet.Windsor
{
	using Castle.Windsor;

	/// <summary>
	/// Extension methods for Castle Windsor integration
	/// </summary>
	public static class WindsorExtensions
	{
		/// <summary>
		/// Constructs a new IScopeResolver for a given IWindsorContainer.
		/// </summary>
		/// <see cref="WindsorKernelScopeResolver"/>
		/// <param name="container">IWindsorContainer</param>
		/// <returns>WindsorKernelScopeResolver</returns>
		public static WindsorKernelScopeResolver AsScopeResolver(this IWindsorContainer container)
		{
			return new WindsorKernelScopeResolver(container.Kernel);
		}
	}
}
