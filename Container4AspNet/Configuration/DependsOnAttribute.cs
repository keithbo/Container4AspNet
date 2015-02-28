namespace Container4AspNet.Configuration
{
	using System;

	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	public sealed class DependsOnAttribute : Attribute
	{
		public Type TargetType { get; private set; }

		public DependsOnAttribute(Type targetType)
		{
			TargetType = targetType;
		}
	}
}
