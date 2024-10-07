using Model.Manchester;
using Model.Sensors;
using Model.Time;
using Model.Variant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.AU
{
    internal class AutomationUnit
    {
        private SensorHost SensorHost;

        public AutomationUnit(SensorHost sensorHost)
        {
            SensorHost = sensorHost;
            SensorHost.isEnabled = true;
            SensorHost.UseReserve = false;


            SimulationTime.AddAction(Update);
        }

        private void Update(double DeltaTime)
        {
            SimulateFailure();
        }

        private bool isFailed = false;
        private void SimulateFailure()
        {
            const double FailTime = VariantData.t_fail;

            if (!isFailed && SimulationTime.CompareTimeTo(FailTime))
            {
                isFailed = true;
                SensorHost.UseReserve = true;
            }
        }


        public ResponceWord HandleMKIO(Word commandWord)
        {
            ResponceWord rw = new();
            rw.Data = commandWord.Data;
            return rw;
        }
    }
}
