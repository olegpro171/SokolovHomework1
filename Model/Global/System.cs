using Model.AU;
using Model.Common;
using Model.Sensors;
using Model.Time;
using Model.Variant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Global
{
    internal class System
    {
        private SensorHost SensorHost;
        private AutomationUnit AutomationUnit;


        public System()
        {
            SensorHost = new SensorHost();
            SensorHost.isEnabled = true;
            SensorHost.UseReserve = false;

            AutomationUnit = new AutomationUnit(SensorHost);
        }

        public void AdvanceStep()
        {
            while (SimulationTime.CurrentTime < SimulationTime.Tmax)
            {
                bool failFlag = false;
                if (!failFlag && Math.Abs(SimulationTime.CurrentTime - VariantData.t_fail) < 0.001)
                {
                    Logger.Log("switch to reserve");
                    failFlag = true;
                    SensorHost.UseReserve = true;
                }

                SimulationTime.AdvanceStep();
            }
        }
    }
}
