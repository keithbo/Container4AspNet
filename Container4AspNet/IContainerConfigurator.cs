namespace Container4AspNet
{
	using System;

	public interface IContainerConfigurator<TContainer>
	{
		TContainer Container { get; set; }
		Func<TContainer, Type, object> Resolve { get; set; }
		Func<TContainer, Type, object[]> ResolveAll { get; set; }
		Func<TContainer, IScopeResolver> ScopeFactory { get; set; }
	}
}
