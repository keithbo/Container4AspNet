namespace Container4AspNet.DependencyInjection
{
	using System;

	internal class DependencyContainerWrapper<TContainer> : IDependencyContainerWrapper<TContainer>
	{
		private Func<TContainer, Type, object> _resolver;

		public TContainer Container
		{
			get;
			private set;
		}

		public DependencyContainerWrapper(TContainer container, Func<TContainer, Type, object> resolver)
		{
			this.Container = container;
			this._resolver = resolver;
		}

		public object ResolveType(Type type)
		{
			return this._resolver(this.Container, type);
		}
	}
}
