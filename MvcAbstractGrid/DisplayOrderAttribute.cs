using System;

namespace MvcAbstractGrid
{
	public class DisplayOrderAttribute : Attribute
	{
        public int value {set; get;}
        
		public DisplayOrderAttribute(int i)
		{
            value = i;
		}
	}
}