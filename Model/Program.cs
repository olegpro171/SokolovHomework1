using Model.Sensors;
using Model.Time;

namespace Model
{
    public class Program
    {
        public static void Main(string[] args)
        {
            SensorsHost sensorHost = new SensorsHost();
            sensorHost.isEnabled = true;
            sensorHost.UseReserve = true;

            Thread TimeThread = new Thread(new ThreadStart(Program.TimeThread));
            TimeThread.Start();

        }


        public static void TimeThread()
        {
            while (SimulationTime.CurrentTime < SimulationTime.Tmax)
            {
                SimulationTime.AdvanceStep();
                //Thread.Sleep((int)SimulationTime.dT);
            }
        }
    }
}