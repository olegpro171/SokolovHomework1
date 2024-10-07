using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Manchester
{
    internal class SUResponceWord : ResponceWord
    {
        public double Time;
        new public double Data;

        public SUResponceWord(double time, double data)
        {
            Time = time;
            Data = data;
        }
    }
}
