using System.ComponentModel;
using System.Reflection;

namespace OleSync.Application.Utilities
{
	public static class EnumExtensions
	{
		public static string GetDescription(this Enum value)
		{
			if (value == null)
				return string.Empty;

			var fieldInfo = value.GetType().GetField(value.ToString());
			if (fieldInfo == null)
				return value.ToString();

			var attribute = fieldInfo.GetCustomAttribute<DescriptionAttribute>();
			return attribute?.Description ?? value.ToString();
		}
	}
}