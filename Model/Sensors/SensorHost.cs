using Model.Common;
using Model.Time;
using Model.Variant;

namespace Model.Sensors
{
    internal class SensorHost
    {
        public bool isEnabled = false;
        public bool UseReserve = false;
        public bool isReady { get; private set; } = false;
        public double Output { get; private set; } = 0.0d;

        private enum HostState { Off, OnMain, OnReserve, }
        private HostState State = HostState.Off;

        private Sensor SensorMain;
        private Sensor SensorRes;

        private const double PollTime = VariantData.t_poll;
        private double PollingTimer = 0.0d;

        private const double eps = 0.001;

        public SensorHost()
        {
            SensorMain = new Sensor();
            SensorRes = new Sensor();

            SensorMain.isEnabled = true;
            SensorRes.isEnabled = false;

            SimulationTime.AddAction(Update);
        }

        public void Update(double DeltaTime)
        {
            SetState();

            switch (State)
            {
                case HostState.Off:
                    isReady = false;
                    Output = 0.0d;
                    break;

                case HostState.OnMain:
                    UpdateForMain(DeltaTime);
                    break;

                case HostState.OnReserve:
                    UpdateForReserve(DeltaTime);
                    break;
            }
            const string spaces = "      ";
            string logMsg = 
                $"T = {SimulationTime.CurrentTime.ToString("00.00")} | State = {State} \tisReady = {isReady}\t| " +
                $"Output = {(Output == 0.0d ? spaces : Output.ToString("00.000"))} | " +
                $"Sensor 1 = {(SensorMain.Output == 0.0d ? spaces : SensorMain.Output.ToString("00.000"))} | " +
                $"Sensor 2 = {(SensorRes.Output == 0.0d ? spaces : SensorRes.Output.ToString("00.000"))}";

            Logger.Log(logMsg);
        }

        private void SetState()
        {
            if (!isEnabled)
            {
                State = HostState.Off;
                return;
            }

            if (UseReserve)
                State = HostState.OnReserve;
            else
                State = HostState.OnMain;
        }

        private void UpdateForMain(double dT)
        {
            TurnOffSensor(ref SensorRes);
            TurnOnSensor(ref SensorMain);

            PollSensor(SensorMain, dT);
        }

        private void UpdateForReserve(double dT)
        {
            TurnOffSensor(ref SensorMain);
            TurnOnSensor(ref SensorRes);

            PollSensor(SensorRes, dT);
        }

        private void TurnOffSensor(ref Sensor sensor)
        {
            if (sensor.state != Sensor.SensoreState.Off)
                sensor.isEnabled = false;
        }

        private void TurnOnSensor(ref Sensor sensor)
        {
            switch (sensor.state)
            {
                case Sensor.SensoreState.Off:
                    sensor.isEnabled = true;
                    isReady = false;
                    Output = 0.0d;
                    break;

                case Sensor.SensoreState.Warmup:
                    isReady = false;
                    Output = 0.0d;
                    break;

                case Sensor.SensoreState.On:
                    if (isReady != true)
                        isReady = true;
                    break;

                default:
                    throw new NotImplementedException();
            }
        }

        private void PollSensor(Sensor sensor, double dT)
        {
            if (!isReady)
            {
                PollingTimer = 0.0d;
                return;
            }

            if (PollingTimer >= eps)
            {
                PollingTimer -= dT;
                return;
            }
            else
            {
                PollingTimer = PollTime;
                PollingTimer -= dT;
            }


            if (sensor.isEnabled && sensor.state == Sensor.SensoreState.On)
            {
                Output = sensor.Output;
            }
            else
            {
                Output = 0.0d;
            }
        }
    }
}
