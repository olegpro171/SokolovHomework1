using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Manchester;

namespace Model.MCU
{
    internal class MainComputingUnit
    {
        private Interface Interface;

        public MainComputingUnit(Interface @interface)
        {
            Interface = @interface;
        }

        public void Update()
        {

        }
    }
}
