namespace Container4AspNet
{
    using System;
    using System.Collections.Generic;

    internal class ContainerWrapper<TContainer> : IContainerWrapper<TContainer>
    {
        public IContainerConfigurator<TContainer> Context { get; }

        public TContainer Container
        {
            get { return Context.Container; }
        }

        public ContainerWrapper(IContainerConfigurator<TContainer> context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            Context = context;
        }

        public bool CanResolve(Type type)
        {
            return Context.CanResolve == null || Context.CanResolve(Container, type);
        }

        public object Resolve(Type type)
        {
            return Context.Resolve(Container, type);
        }

        public IEnumerable<object> ResolveAll(Type type)
        {
            return Context.ResolveAll(Container, type);
        }

        public void Release(object instance)
        {
            if (Context.Release != null)
            {
                Context.Release(Container, instance);
            }
        }

        public IScopeResolver GetScopeResolver()
        {
            return Context.ScopeFactory != null ? Context.ScopeFactory(Container) : null;
        }
    }
}
