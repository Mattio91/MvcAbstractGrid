using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace MvcAbstractGrid
{
	public class Html
    {       // xml writer etc. http://stackoverflow.com/questions/1004776/write-html-in-c-sharp 
        private Tuple<string, string> _tableTag = new Tuple<string, string>("<table>", "</table>");
        private Tuple<string, string> _rowTag = new Tuple<string, string>("<tr>", "</tr>");
        private Tuple<string, string> _columnTag = new Tuple<string, string>("<td>", "</td>");
        private Tuple<string, string> _tableHeadTag = new Tuple<string, string>("<th>", "</th>");
        private const string sortAscImg = "<img src=\"iconASC.png\" alt=\"sortASC\" height=\"10\" width=\"10\">";
        private const string sortDscImg = "<img src=\"iconDSC.png\" alt=\"sortASC\" height=\"10\" width=\"10\">";
        string url;
        public Html(string url)
        {
            this.url = url;
        }

		public String GridFromViewModel<T>(IEnumerable<GridViewModel<T>> model)
		{
            DictionaryItemComparer ItemComparer = new DictionaryItemComparer();
            Dictionary<DictionaryItem, List<String>> value_dict = new Dictionary<DictionaryItem, List<string>>(ItemComparer);
            
            foreach (GridViewModel<T> item in model)
            {
                Type t = item.GetType();
                System.Reflection.PropertyInfo[] infos = t.GetProperties();

                foreach (System.Reflection.PropertyInfo info in infos)
                {
                    DictionaryItem dItem = new DictionaryItem(getAttributes(info));

                    if (!value_dict.ContainsKey(dItem))
                        value_dict.Add(dItem, new List<string>(infos.Length));
                    
                    value_dict[dItem].Add((string)info.GetValue(item, null));                
                }
            }

            Console.WriteLine(createTable(value_dict));
            return createTable(value_dict);
		}


        private string createTable(Dictionary<DictionaryItem, List<String>> value_dict)
        {
            //Use some XML Writter etc.
            StringBuilder fullTableString = new StringBuilder();
            fullTableString.Append(_tableTag.Item1 + _rowTag.Item1);

            

            //Sort key before making table http://www.dotnetperls.com/sort-dictionary

            foreach (DictionaryItem key in value_dict.Keys)
            {
                if (key.sortable)
                    fullTableString.Append(tagging(_tableHeadTag, key.displayName + string.Format("<a href={0}&sortColumn={1}&sortOrder=ASC>{2}</a><a href={0}&sortColumn={1}&sortOrder=DSC>{3}</a>", url, key.displayName, sortAscImg, sortDscImg)));
                else
                    fullTableString.Append(tagging(_tableHeadTag, key.displayName));
            }

            fullTableString.Append(_rowTag.Item2);

            for (int i = 0; i < value_dict.Values.Count; i++)
            {
                fullTableString.Append(_rowTag.Item1);
                foreach (List<string> list in value_dict.Values)
                {
                    fullTableString.Append(tagging(_columnTag, list[i]));
                }
                fullTableString.Append(_rowTag.Item2);
            }

            fullTableString.Append(_tableTag.Item2);

            return fullTableString.ToString();
        }

        private string tagging(Tuple<string, string> tag, string tagged)
        {
            return tag.Item1 + tagged + tag.Item2;
        }

        private DictionaryItem getAttributes(System.Reflection.PropertyInfo info)
        {
            object[] attributes = info.GetCustomAttributes(true);
            DictionaryItem DI = new DictionaryItem(info.Name);
            DI.sortable = false;
            DI.displayOrder = 0;
            DI.displayName = DI.variableName;

            foreach (Object att in attributes)
            {
                if (att is DisplayNameAttribute)
                    DI.displayName = ((DisplayNameAttribute)att).DisplayName;
                if (att is SortableAttribute)
                    DI.sortable = ((SortableAttribute)att).value;
                if (att is DisplayOrderAttribute)
                    DI.displayOrder = ((DisplayOrderAttribute)att).value;
            }
            return DI;
        }

    }
}
