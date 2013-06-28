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
	public class HtmlGrid
	{
		string sortAsc_imagePath = "/images/Green_Arrow_Up.png";
		string sortDsc_imagePath = "/images/Red_Arrow_Down.png";
		string imageSortWidth = "10";
		string imageSortHeight = "10";
		string _pageUrl;
		private HtmlTextWriter _writer;

		public HtmlGrid(string pageUrl)
		{
			this._pageUrl = pageUrl;
		}

		public HtmlGrid(string pageUrl, string ascSortingIcon, string dscSortingIcon)
			: this(pageUrl)
		{
			sortAsc_imagePath = ascSortingIcon;
			sortDsc_imagePath = dscSortingIcon;
		}


		public String GridFromViewModel<T>(IEnumerable<GridViewModel<T>> model)
		{
			var columns = new SortedDictionary<ColumnDescriptor, List<string>>(new ColumnDescriptorComparer());

			foreach (GridViewModel<T> entity in model)
			{
				var entityProperties = entity.GetType().GetProperties();

				foreach (var property in entityProperties)
				{
					try
					{
						var columnHeader = new ColumnDescriptor(getAttributes(property));

						if (!columns.ContainsKey(columnHeader))
							columns.Add(columnHeader, new List<string>());

						columns[columnHeader].Add(property.GetValue(entity, null).ToString());
					}
					catch (InvalidOperationException) { }
				}
			}

			return createHTMLTable(columns);
		}



		private string createHTMLTable(SortedDictionary<ColumnDescriptor, List<String>> value_dict)
		{
			StringWriter stringWriter = new StringWriter();

			_writer = new HtmlTextWriter(stringWriter);

			this._writer.AddAttribute(HtmlTextWriterAttribute.Class, "test");
			this._writer.RenderBeginTag(HtmlTextWriterTag.Table);
			this._writer.RenderBeginTag(HtmlTextWriterTag.Tr);

			foreach (ColumnDescriptor key in value_dict.Keys) //Insert keys to table
			{
				this._writer.RenderBeginTag(HtmlTextWriterTag.Th);
				this._writer.Write(key.displayName);
				if (key.sortable)
				{
					AddSortingLinks(key, this._writer);
				}
				this._writer.RenderEndTag();
			}

			for (int i = 0; i < value_dict.First().Value.Count; i++) //Insert values to table
			{
				this._writer.RenderBeginTag(HtmlTextWriterTag.Tr);
				foreach (List<string> list in value_dict.Values)
				{
					this._writer.RenderBeginTag(HtmlTextWriterTag.Td);
					this._writer.Write(list[i]);
					this._writer.RenderEndTag();
				}
				this._writer.RenderEndTag();
			}
			this._writer.RenderEndTag();
			this._writer.RenderEndTag();

			return stringWriter.ToString();

		}

		private void AddSortingLinks(ColumnDescriptor key, HtmlTextWriter writer)
		{
			SortingLink(key, "ASC");
			SortingLink(key, "DSC");
		}

		private void SortingLink(ColumnDescriptor key, string order)
		{
			string url_Asc = string.Format("{0}&sortColumn={1}&sortOrder={2}", _pageUrl, key.variableName, order);
			_writer.AddAttribute(HtmlTextWriterAttribute.Href, url_Asc);
			_writer.RenderBeginTag(HtmlTextWriterTag.A);

			_writer.AddAttribute(HtmlTextWriterAttribute.Src, sortAsc_imagePath);
			_writer.AddAttribute(HtmlTextWriterAttribute.Width, imageSortWidth);
			_writer.AddAttribute(HtmlTextWriterAttribute.Height, imageSortHeight);
			_writer.AddAttribute(HtmlTextWriterAttribute.Alt, String.Format("Sort {0}", order));
			_writer.RenderBeginTag(HtmlTextWriterTag.Img);
			_writer.RenderEndTag();

			_writer.RenderEndTag();
		}


		private ColumnDescriptor getAttributes(System.Reflection.PropertyInfo info)
		{

			object[] attributes = info.GetCustomAttributes(true);
			ColumnDescriptor DI = new ColumnDescriptor(info.Name);
			DI.sortable = false;
			DI.displayOrder = 0;
			DI.displayName = null;

			foreach (Object att in attributes)
			{
				if (att is DisplayNameAttribute)
					DI.displayName = ((DisplayNameAttribute)att).DisplayName;
				if (att is SortableAttribute)
					DI.sortable = ((SortableAttribute)att).value;
				if (att is DisplayOrderAttribute)
					DI.displayOrder = ((DisplayOrderAttribute)att).value;
			}
			if (DI.displayName == null)
			{
				throw new InvalidOperationException("Column does not have display parameters");
			}
			return DI;
		}
	}
}
