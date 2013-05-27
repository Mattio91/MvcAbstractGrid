using System.Collections.Generic;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcAbstractGrid;

namespace MvcAbstractGridTests
{
	[TestClass]
	public class GridFromViewModelTests
	{
		private string _result;
		private List<Machine> _testMachineCollection;

		[TestInitialize]
		public void SetUp()
		{
			_testMachineCollection = MachineCollection.Get();
			_result = new Html("test").GridFromViewModel(new MachineViewModel().FromCollection(_testMachineCollection));
		}

		[TestMethod]
		public void Result_Contains_Identifier_For_First_Column()
		{
			Assert.IsTrue(_result.Contains("Identyfikator"));
		}

		[TestMethod]
		public void Result_Contains_Identifier_For_Second_Column()
		{
			Assert.IsTrue(_result.Contains("System Operacyjny"));
		}

		[TestMethod]
		public void Result_Contains_All_Data()
		{
			Assert.IsTrue(_result.Contains("1"));
			Assert.IsTrue(_result.Contains("2"));
			Assert.IsTrue(_result.Contains("Linux"));
			Assert.IsTrue(_result.Contains("Windows"));
		}

		[TestMethod]
		public void Result_Correct_Column_Order()
		{
			int firstIdentifierPosition = _result.IndexOf("Identyfikator");

			int secondIdentifierPosition = _result.IndexOf("System Operacyjny");

			Assert.IsTrue(firstIdentifierPosition < secondIdentifierPosition);
		}

		[TestMethod]
		public void Result_Is_Well_Formed_Html()
		{
			new XmlDocument().LoadXml(_result);
		}

		[TestMethod]
		public void Result_Contains_Sorting_Links()
		{
			_result.Contains("sortOrder");
		}
	}
}