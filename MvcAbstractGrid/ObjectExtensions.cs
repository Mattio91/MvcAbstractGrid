using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace MvcAbstractGrid
{
	public static class ObjectExtensions
	{
		public static Dictionary<String, Object> GetPropertiesDictionary(this Object objectInstance)
		{
			List<String> propertyNames = TypeDescriptor.GetProperties(objectInstance)
				.Cast<PropertyDescriptor>()
				.Where(propertyInfo => propertyInfo.PropertyType.Namespace.Equals("System"))
				.Select(x => x.Name).ToList();

			var properties = new Dictionary<string, object>();

			foreach (string name in propertyNames)
			{
				properties[name] = objectInstance.GetType().GetProperty(name).GetValue(objectInstance, null);
			}

			return properties;
		}
	}
}
