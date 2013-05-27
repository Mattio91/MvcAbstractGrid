using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvcAbstractGrid;
using MvcAbstractGridTests;
using System.ComponentModel;

namespace Grid
{
    class main
    {
        public string _result;
        public List<MvcAbstractGridTests.Machine> _testMachineCollection;


        public void ini()
        {
            _testMachineCollection = MvcAbstractGridTests.MachineCollection.Get();
            IEnumerable<GridViewModel<Machine>> test = new MvcAbstractGridTests.MachineViewModel().FromCollection(_testMachineCollection);
			_result = new Html("test").GridFromViewModel(test);

            Console.ReadKey();
        }



    }


    class Machine1
    {
        public int Id { get; set; }
        public String Platform { get; set; }
        public String OperatingSystem { get; set; }
    }

}
