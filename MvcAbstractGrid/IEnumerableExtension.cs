using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcAbstractGrid
{
	public static class IEnumerableExtension
	{
		public static IEnumerable SortBy(this IEnumerable collection,String propertyName,String sortOrder)
		{
            switch (sortOrder)
            {
                case "ASC":
                    {
                        
                        break;
                    }
                case "DSC":
                    {
                        break;
                    }
            }

            return collection;
		}
	}
}
