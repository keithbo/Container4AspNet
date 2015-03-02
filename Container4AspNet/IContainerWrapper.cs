namespace Container4AspNet
{
	/// <summary>
	/// Strong typed container wrapper with ITypeResolver extensions.
	/// </summary>
	/// <typeparam name="TContainer">Container type</typeparam>
	public interface IContainerWrapper<TContainer> : ITypeResolver
	{
		/// <summary>
		/// Container accessor
		/// </summary>
		TContainer Container { get; }
	}
}
