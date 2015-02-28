namespace Container4AspNet.Configuration
{
	using System.Web.Mvc;

	public class AreaMvcConfigurer : IMvcConfigurer
	{
		public void Configure()
		{
			AreaRegistration.RegisterAllAreas();
		}
	}
}
