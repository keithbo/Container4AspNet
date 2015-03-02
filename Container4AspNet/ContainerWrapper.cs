namespace Container4AspNet
{
	using System;

	internal class ContainerWrapper<TContainer> : IContainerWrapper<TContainer>
	{
		private IContainerConfigurator<TContainer> _context;

		public TContainer Container
		{
			get { return _context.Container; }
		}

		public ContainerWrapper(IContainerConfigurator<TContainer> context)
		{
			_context = context;
		}

		public object ResolveType(Type type)
		{
			return this._context.Resolve(this.Container, type);
		}
	}
}
