using Model.Common;
using Model.Sensors;
using Model.Global;
using Model.Time;
using Model.Variant;

namespace Model
{
    public class Program
    {
        private static Global.System System = new();
        
        public static void Main(string[] args)
        {
            Thread TimeThread = new Thread(new ThreadStart(Program.TimeThread));
            TimeThread.Start();

            TimeThread.Join();
            
        }


        public static void TimeThread()
        {
            System.AdvanceStep();
        }
    }
}