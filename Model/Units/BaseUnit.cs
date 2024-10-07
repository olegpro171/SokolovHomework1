using Model.Manchester;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Units
{
    internal abstract class BaseUnit 
    {
        public abstract ResponceWord HandleMKIOWord(CommandWord word);


    }
}
