using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace MvcAbstractGridTests
{
	public class MachineCollection
	{
		public static List<Machine> Get()
		{
			return new List<Machine>
			{
				new Machine {Id = 1, OperatingSystem = "Linux",Platform = "Haswell"},
				new Machine {Id = 2, OperatingSystem = "Windows",Platform = "Ivy Bridge"}
			};
		}
	}
}
