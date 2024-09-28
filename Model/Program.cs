using Model.Sensors;
using Model.Time;

namespace Model
{
    public class Program
    {
        public static void Main(string[] args)
        {
            SensorsHost sensorHost = new SensorsHost();
            

            Thread TimeThread = new Thread(new ThreadStart(SimulationTime.Start));
            TimeThread.Start();

        }
    }
}