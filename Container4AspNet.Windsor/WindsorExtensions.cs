namespace Container4AspNet.Windsor
{
	using Castle.Windsor;

	public static class WindsorExtensions
	{
		public static IScopeResolver AsScopeResolver(this IWindsorContainer container)
		{
			return new WindsorKernelScopeResolver(container.Kernel);
		}
	}
}
