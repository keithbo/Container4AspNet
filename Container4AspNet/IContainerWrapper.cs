namespace Container4AspNet
{
	public interface IContainerWrapper<TContainer> : ITypeResolver
	{
		TContainer Container { get; }
	}
}
