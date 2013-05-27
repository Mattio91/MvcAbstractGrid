using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcAbstractGrid
{
	public interface GridViewModel<T>
	{
		IEnumerable<GridViewModel<T>> FromCollection(IEnumerable<T> items);
	}
}
