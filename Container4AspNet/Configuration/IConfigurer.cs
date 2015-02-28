namespace Container4AspNet.Configuration
{
	public interface IConfigurer
	{
		void Configure();
	}

	public interface IConfigurer<TContext>
	{
		void Configure(TContext context);
	}
}
