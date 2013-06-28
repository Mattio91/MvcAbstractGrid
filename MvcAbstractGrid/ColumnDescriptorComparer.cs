using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcAbstractGrid
{
    class ColumnDescriptorComparer : IComparer<ColumnDescriptor>
    {
        public int Compare(ColumnDescriptor x, ColumnDescriptor y)
        {
            if (x.displayOrder.CompareTo(y.displayOrder) != 0)
                return x.displayOrder.CompareTo(y.displayOrder);
            else
                return x.displayName.CompareTo(y.displayName);

        }
    }
}