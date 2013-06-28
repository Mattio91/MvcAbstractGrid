using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using MvcAbstractGrid;

namespace MvcAbstractGridTests
{
    [Serializable]
	public class MachineViewModel : GridViewModel<Machine>
	{
		public IEnumerable<GridViewModel<Machine>> FromCollection(IEnumerable<Machine> items)
		{
			return items.Select(
				x => new MachineViewModel
				{
					Id = x.Id.ToString(),
					OperatingSystemIcon = String.Format("<img src='{0}' alt='' />",x.OperatingSystem)
				}).
				ToList();
		}
        
		[DisplayName("Identyfikator"),DisplayOrder(2),Sortable(true)]
		public String Id { get; set; }
		[DisplayName("System Operacyjny"), DisplayOrder(1), Sortable(false)]
		public String OperatingSystemIcon { get; set; }
		
	} 
}
