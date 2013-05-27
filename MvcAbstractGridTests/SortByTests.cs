using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcAbstractGrid;

namespace MvcAbstractGridTests
{
	[TestClass]
	public class SortByTests
	{
		[TestMethod]
		public void Result_Sorting_Works()
		{
			var testMachineCollection = MachineCollection.Get();

            
			var sortedCollection = testMachineCollection.SortBy("Id", "desc") as IEnumerable<Machine>;

			Assert.AreEqual(2, sortedCollection.First().Id);
		}
	}
}
