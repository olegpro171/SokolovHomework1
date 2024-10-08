using Model.Time;
using Model.Variant;
using System.Data;

namespace Model.Sensors
{
    public class Sensor
    {
        public bool isEnabled = false;
        private double _output = 0.0d;
        public double Output { get { return _output; } }

        private const double eps = 0.001;

        public enum SensoreState { Off, Warmup, On }
        public SensoreState state { get; private set; } = SensoreState.Off;

        private const double t_rdy = VariantData.t_rdy;
        private double ReadyTimer = t_rdy;
        private bool isFailed = false;

        public Sensor()
        {
            SimulationTime.AddAction(this.Update);
        }

        private static double Xs(double t)
        {
            return Environent.X(t) + Environent.F(t);
        }

        private double GetSensorOutputValue()
        {
            if (state == SensoreState.On)
            {
                return Xs(SimulationTime.CurrentTime);
            }
            return 0.0d;
        }

        public void Update(double DeltaTime)
        {
            if (isFailed)
                return;

            switch (isEnabled)
            {
                case false:
                    ReadyTimer = t_rdy;
                    ReadyTimer -= DeltaTime;
                    state = SensoreState.Off;
                    break;

                case true:
                    TurnOnProcedure(DeltaTime);
                    break;
            }

            _output = GetSensorOutputValue();
        }

        public void ImitateFailure()
        {
            isFailed = true;
        }

        private void TurnOnProcedure(double DeltaTime)
        {
            if (!isEnabled)
                return;

            switch (state)
            {
                case SensoreState.Off:
                    state = SensoreState.Warmup;
                    ReadyTimer -= DeltaTime;
                    break;

                case SensoreState.Warmup:
                    if (ReadyTimer > eps)
                    {
                        ReadyTimer -= DeltaTime;
                    }
                    else
                    {
                        state = SensoreState.On;
                    }
                    break;

                case SensoreState.On:
                    break;

                default:
                    throw new Exception("Invalid sensor state");
            }
        }
    }
}