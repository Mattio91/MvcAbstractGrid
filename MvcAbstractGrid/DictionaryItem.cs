using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcAbstractGrid
{
    class DictionaryItem
    {
        public string variableName { set; get; }
        public string displayName { set; get;}
        public bool sortable { set; get; }
        public int displayOrder { set; get; }

        public DictionaryItem(DictionaryItem di)
        {
            this.variableName = di.variableName;
            this.displayName = di.displayName;
            this.sortable = di.sortable;
            this.displayOrder = di.displayOrder;
        }

        public DictionaryItem(string variableName, string displayName, bool sortable, int displayOrder)
        {
            this.variableName = variableName;
            this.displayName = displayName;
            this.sortable = sortable;
            this.displayOrder = displayOrder;
        }

        public DictionaryItem(string variableName)
        {
            this.variableName = variableName;
        }

    }
}
