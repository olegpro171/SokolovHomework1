using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Model.Sensors;


namespace Model.Manchester
{
    internal class Interface
    {
        List<Func<CommandWord, ResponceWord>> RecieverHandlers = new();

        public ResponceWord SendWord(CommandWord word, int address)
        {
            Func<CommandWord, ResponceWord> Handler;
            try
            {
                Handler = RecieverHandlers[address];
            }
            catch (IndexOutOfRangeException ex)
            {
                throw new ArgumentException("Не существует блока с указанным адресом", ex);
            }

            return Handler(word);
        }

        public void AddNew(Func<CommandWord, ResponceWord> HandlerFunction)
        {
            RecieverHandlers.Add(HandlerFunction);
        }
    }
}
