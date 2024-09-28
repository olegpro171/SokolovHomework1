using System.Xml;

namespace Model.Time
{
    public static class SimulationTime
    {
        public const double dT = 0.1d;
        public const double Tmax = 10.0d;
        private static double _CurrentTime;

        public static double CurrentTime { get { return _CurrentTime; } }

        private static List<Action<double>> Actions; 

        static SimulationTime()
        {
            _CurrentTime = 0;
            Actions = new List<Action<double>>();
        }

        public static void Start()
        {
            while (_CurrentTime < Tmax)
            {
                //Console.WriteLine($"[TIME] Time advance T = {Math.Round(_CurrentTime, 3)}");
                foreach (Action<double> action in Actions)
                {
                    action(dT);
                }
                Thread.Sleep((int)Math.Round(dT * 1000));
                _CurrentTime += dT;
            }
        }

        public static void AdvanceStep()
        {
            foreach (Action<double> action in Actions)
            {
                action(dT);
            }
            _CurrentTime += dT;
        }

        public static void AddAction(Action<double> action)
        {
            Actions.Add(action);
        }
    }
}