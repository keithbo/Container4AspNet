namespace Container4AspNet.Configuration
{
	using Castle.Windsor.Installer;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Reflection;

	public class DependantInstallerFactory : InstallerFactory
	{
		public override IEnumerable<Type> Select(IEnumerable<Type> installerTypes)
		{
			return installerTypes.SortByDependsOn();
		}
	}
}
