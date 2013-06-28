using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace MvcAbstractGrid
{
	public static class IEnumerableExtension
	{
		public static IEnumerable<T> SortBy<T>(this IEnumerable collection, String propertyName, String order)
		{
			var orderedCollection=collection.OfType<T>()
				.OrderBy(x => x.GetType().GetProperty(propertyName).GetValue(x, null));
			if (order == "DSC")
			{
				return orderedCollection.Reverse();
			}
			return orderedCollection;
		}
	}
}
