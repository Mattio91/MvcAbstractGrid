using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcAbstractGrid
{
    class ColumnDescriptor
    {
        public string variableName { set; get; }
        public string displayName { set; get;}
        public bool sortable { set; get; }
        public int displayOrder { set; get; }

        public ColumnDescriptor(ColumnDescriptor di)
        {
            this.variableName = di.variableName;
            this.displayName = di.displayName;
            this.sortable = di.sortable;
            this.displayOrder = di.displayOrder;
        }

        public ColumnDescriptor(string variableName, string displayName, bool sortable, int displayOrder)
        {
            this.variableName = variableName;
            this.displayName = displayName;
            this.sortable = sortable;
            this.displayOrder = displayOrder;
        }

        public ColumnDescriptor(string variableName)
        {
            this.variableName = variableName;
        }

    }
}
