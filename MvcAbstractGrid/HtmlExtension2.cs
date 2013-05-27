using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Xml;
using System.IO;
using System.Web.UI;

namespace MvcAbstractGrid
{
	public class Html
    {
        string sortAsc_imagePath = "iconASC.png";
        string sortDsc_imagePath = "iconDSC.png";
        string imageSortWidth = "10";
        string imageSortHeight = "10";
        string url;
        public Html(string url)
        {
            this.url = url;
        }

		public String GridFromViewModel<T>(IEnumerable<GridViewModel<T>> model)
		{
            SortedDictionary<DictionaryItem, List<String>> value_dict = new SortedDictionary<DictionaryItem, List<string>>(new DictionaryItemComparer());
            
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

            Console.WriteLine(createHTMLTable(value_dict));
            return createHTMLTable(value_dict);
		}


        
        private string createHTMLTable(SortedDictionary<DictionaryItem, List<String>> value_dict)
        {
	        StringWriter stringWriter = new StringWriter();

            using (HtmlTextWriter writer = new HtmlTextWriter(stringWriter))
            {
                writer.RenderBeginTag(HtmlTextWriterTag.Table);
                writer.RenderBeginTag(HtmlTextWriterTag.Tr);

                foreach (DictionaryItem key in value_dict.Keys) //Insert keys to table
                {
                    writer.RenderBeginTag(HtmlTextWriterTag.Th);
                    writer.Write(key.displayName);
                    if (key.sortable) 
                    {
                        string url_Asc = string.Format("{0}&sortColumn={1}&sortOrder=ASC", url, key.displayName);
                        writer.AddAttribute(HtmlTextWriterAttribute.Href, url_Asc);
                        writer.RenderBeginTag(HtmlTextWriterTag.A); 

                        writer.AddAttribute(HtmlTextWriterAttribute.Src, sortAsc_imagePath);
                        writer.AddAttribute(HtmlTextWriterAttribute.Width, imageSortWidth);
                        writer.AddAttribute(HtmlTextWriterAttribute.Height, imageSortHeight);
                        writer.AddAttribute(HtmlTextWriterAttribute.Alt, "Sort ASC");
                        writer.RenderBeginTag(HtmlTextWriterTag.Img);  
                        writer.RenderEndTag(); 

                        writer.RenderEndTag();

                        string url_Dsc = string.Format("{0}&sortColumn={1}&sortOrder=DSC", url, key.displayName);
                        writer.AddAttribute(HtmlTextWriterAttribute.Href, url_Dsc);
                        writer.RenderBeginTag(HtmlTextWriterTag.A);

                        writer.AddAttribute(HtmlTextWriterAttribute.Src, sortDsc_imagePath);
                        writer.AddAttribute(HtmlTextWriterAttribute.Width, imageSortWidth);
                        writer.AddAttribute(HtmlTextWriterAttribute.Height, imageSortHeight);
                        writer.AddAttribute(HtmlTextWriterAttribute.Alt, "Sort DSC");
                        writer.RenderBeginTag(HtmlTextWriterTag.Img);
                        writer.RenderEndTag();

                        writer.RenderEndTag(); 
                    }
                    writer.RenderEndTag();
                }

                for (int i = 0; i < value_dict.Values.Count; i++) //Insert values to table
                {
                    writer.RenderBeginTag(HtmlTextWriterTag.Tr);
                    foreach (List<string> list in value_dict.Values)
                    {
                        writer.RenderBeginTag(HtmlTextWriterTag.Td);
                        writer.Write(list[i]);
                        writer.RenderEndTag();
                    }
                    writer.RenderEndTag();
                }
                writer.RenderEndTag();
                writer.RenderEndTag();

                return stringWriter.ToString();
            }
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


        /* Old solution
        
        private Tuple<string, string> _tableTag = new Tuple<string, string>("<table>", "</table>");
        private Tuple<string, string> _rowTag = new Tuple<string, string>("<tr>", "</tr>");
        private Tuple<string, string> _columnTag = new Tuple<string, string>("<td>", "</td>");
        private Tuple<string, string> _tableHeadTag = new Tuple<string, string>("<th>", "</th>");
        private const string sortAscImg = "<img src=\"iconASC.png\" alt=\"sortASC\" height=\"10\" width=\"10\">";
        private const string sortDscImg = "<img src=\"iconDSC.png\" alt=\"sortASC\" height=\"10\" width=\"10\">";
        
        private string createTable(SortedDictionary<DictionaryItem, List<String>> value_dict)
        {
            //Use some XML Writter etc.
            StringBuilder fullTableString = new StringBuilder();
            fullTableString.Append(_tableTag.Item1 + _rowTag.Item1);

            var sorted = value_dict.Keys.OrderBy(s => s.displayOrder)
                              .ThenBy(s => s.displayName);

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

         
         */
    }
}
