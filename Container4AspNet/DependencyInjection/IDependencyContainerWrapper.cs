namespace Container4AspNet.DependencyInjection
{
	public interface IDependencyContainerWrapper<TContainer> : ITypeResolver
	{
		TContainer Container { get; }
	}
}
