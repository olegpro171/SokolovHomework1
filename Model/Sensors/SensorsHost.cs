﻿using Model.Time;

namespace Model.Sensors
{
    internal class SensorsHost
    {
        public bool isEnabled = false;
        public bool UseReserve = false;
        public bool isReady { get; private set; } = false;
        public double Output { get; private set; } = 0.0d;

        private enum HostState { Off, OnMain, OnReserve, }
        private HostState State = HostState.Off;

        private Sensor SensorMain = new();
        private Sensor SensorRes = new();

        public SensorsHost()
        {
            SensorMain.isEnabled = true;
            SensorRes.isEnabled = false;

            SimulationTime.AddAction(SensorMain.Update);
            SimulationTime.AddAction(SensorRes.Update);
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
                    UpdateForMain();
                    break;

                case HostState.OnReserve:
                    UpdateForReserve();
                    break;
            }

            //Console.WriteLine();
            Console.WriteLine($"[SNS HOST] Update T = {SimulationTime.CurrentTime.ToString("0.0")} | State = {State}\tisReady = {isReady}\t | Output = {Output.ToString("00.000")} | Sensor 1 = {SensorMain.Output.ToString("00.000")} | Sensor 2 = {SensorRes.Output.ToString("00.000")}");
            //Console.WriteLine($"[SNS HOST] State = {State}");
            //Console.WriteLine($"[SNS HOST] isReady = {isReady} | Output = {Output}");
            //Console.WriteLine($"[SNS HOST] Sensor 1 = {SensorMain.Output.ToString("0.000")}");
            //Console.WriteLine($"[SNS HOST] Sensor 2 = {SensorRes.Output}");
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

        private void UpdateForMain()
        {
            TurnOffSensor(ref SensorRes);
            TurnOnSensor(ref SensorMain);
        }

        private void UpdateForReserve()
        {
            TurnOffSensor(ref SensorMain);
            TurnOnSensor(ref SensorRes);
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
                    isReady = true;
                    Output = sensor.Output;
                    break;

                default:
                    throw new NotImplementedException();
            }
        }
    }
}
