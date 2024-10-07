using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Model.AU;
using Model.Sensors;


namespace Model.Manchester
{
    internal class Interface
    {
        List<Func<Word, ResponceWord>> Recievers = new();
    }
}
