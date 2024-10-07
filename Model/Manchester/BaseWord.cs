using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Manchester
{
    internal class BaseWord
    {
        public int Data;

        protected static BaseWord GetWordWithData(int Data)
        {
            var rw = new BaseWord();
            rw.Data = Data;
            return rw;
        }

        public BaseWord(int Data) { this.Data = Data; }
        public BaseWord() { }
    }
}
