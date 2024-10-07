using Model.Common;
using Model.Time;
using Model.Variant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Manchester;
using Model.Units;

namespace Model.Global
{
    internal class System
    {
        private SensorUnit SensorHost;
        private AutomationUnit AutomationUnit;
        private Interface MKIO;
        private MainComputingUnit MainComputingUnit;

        public System()
        {
            SensorHost = new SensorUnit();
            SensorHost.isEnabled = true;
            SensorHost.UseReserve = false;

            AutomationUnit = new AutomationUnit(SensorHost);

            MKIO = new Interface();
            MKIO.AddNew(SensorHost.HandleMKIOWord);
            MKIO.AddNew(AutomationUnit.HandleMKIOWord);

            MainComputingUnit = new MainComputingUnit(MKIO);
        }

        public void AdvanceStep()
        {
            while (SimulationTime.CurrentTime < SimulationTime.Tmax)
            {
                bool failFlag = false;
                if (!failFlag && Math.Abs(SimulationTime.CurrentTime - VariantData.t_fail) < 0.001)
                {
                    //Logger.Log("switch to reserve");
                    failFlag = true;
                    
                }

                SimulationTime.AdvanceStep();
            }
        }
    }
}
