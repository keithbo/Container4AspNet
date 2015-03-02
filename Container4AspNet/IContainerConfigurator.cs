namespace Container4AspNet
{
	using System;

	/// <summary>
	/// This interface defines the contract required by container implementations for container
	/// registration process and resolution.
	/// </summary>
	/// <typeparam name="TContainer">container implementation type</typeparam>
	public interface IContainerConfigurator<TContainer>
	{
		/// <summary>
		/// Gets and sets the container implementation
		/// </summary>
		TContainer Container { get; set; }
		/// <summary>
		/// Gets and sets the single-instance resolution method
		/// </summary>
		Func<TContainer, Type, object> Resolve { get; set; }
		/// <summary>
		/// Gets and sets the multi-instance resolution method
		/// </summary>
		Func<TContainer, Type, object[]> ResolveAll { get; set; }
		/// <summary>
		/// Gets and sets the IScopeResolver factory method.
		/// If scope is not allowed for the implementing container this should be null
		/// </summary>
		Func<TContainer, IScopeResolver> ScopeFactory { get; set; }
	}
}
