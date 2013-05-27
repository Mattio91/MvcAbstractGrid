using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcAbstractGrid
{
    class DictionaryItemComparer : IComparer<DictionaryItem>
    {
        public int Compare(DictionaryItem x, DictionaryItem y)
        {
            if (x.displayOrder.CompareTo(y.displayOrder) != 0)
                return x.displayOrder.CompareTo(y.displayOrder);
            else
                return x.displayName.CompareTo(y.displayName);

        }
    }
}