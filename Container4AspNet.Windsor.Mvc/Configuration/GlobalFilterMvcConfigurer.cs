namespace Container4AspNet.Configuration
{
	using System.Web.Mvc;

	public class GlobalFilterMvcConfigurer : IMvcConfigurer
	{
		public void Configure()
		{
			GlobalFilterCollection filters = GlobalFilters.Filters;

			filters.Add(new HandleErrorAttribute());
		}
	}
}
