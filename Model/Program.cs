using Model.Common;
using Model.Sensors;
using Model.Time;
using Model.Variant;

namespace Model
{
    public class Program
    {
        static SensorHost sensorHost = new SensorHost();

        public static void Main(string[] args)
        {

            Thread TimeThread = new Thread(new ThreadStart(Program.TimeThread));
            TimeThread.Start();

            while (TimeThread.ThreadState == ThreadState.Running);
        }


        public static void TimeThread()
        {
            sensorHost.isEnabled = true;
            sensorHost.UseReserve = false;

            while (SimulationTime.CurrentTime < SimulationTime.Tmax)
            {
                bool failFlag = false;
                if (!failFlag && Math.Abs(SimulationTime.CurrentTime - VariantData.t_fail) < 0.001)
                {
                    //Logger.Log("switch to reserve");
                    failFlag = true;
                    sensorHost.UseReserve = true;
                }

                SimulationTime.AdvanceStep();
            }
        }
    }
}