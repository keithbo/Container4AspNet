﻿namespace Container4AspNet.DependencyInjection
{
	using Owin;
	using System;
	using System.Collections.Generic;

	internal class DependencyContainerAppBuilderAdapter<TContainer> : IAppBuilder
	{
		private IAppBuilder _source;
		private IDependencyContainerWrapper<TContainer> _container;

		public DependencyContainerAppBuilderAdapter(IAppBuilder source, IDependencyContainerWrapper<TContainer> container)
		{
			this._source = source;
			this._container = container;
		}

		public object Build(Type returnType)
		{
			return this._source.Build(returnType);
		}

		public IAppBuilder New()
		{
			return new DependencyContainerAppBuilderAdapter<TContainer>(this._source.New(), this._container);
		}

		public IDictionary<string, object> Properties
		{
			get
			{
				return this._source.Properties;
			}
		}

		public IAppBuilder Use(object middleware, params object[] args)
		{
			object resolvedMiddleware = null;
			if (middleware is Type)
			{
				try
				{
					resolvedMiddleware = this._container.ResolveType(middleware as Type);
				}
				catch
				{
					// if no component was found we delegate to the sourcing IAppBuilder's Type based middleware handling
				}
			}
			// either Resolve didn't find anything, or the passed in object is not a Type
			// pass the original value along
			if (resolvedMiddleware == null)
			{
				resolvedMiddleware = middleware;
			}
			return this._source.Use(resolvedMiddleware, args);
		}
	}
}