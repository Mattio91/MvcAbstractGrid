using System;

namespace MvcAbstractGrid
{
	public class SortableAttribute : Attribute
	{
        public bool value { get; set; }
		public SortableAttribute(bool b)
		{
            value = b;
		}
	}
}