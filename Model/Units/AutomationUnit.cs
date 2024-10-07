using Model.Manchester;
using Model.Sensors;
using Model.Time;
using Model.Variant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Units
{
    internal class AutomationUnit : BaseUnit
    {
        private SensorUnit SensorHost;

        public AutomationUnit(SensorUnit sensorHost)
        {
            SensorHost = sensorHost;
            SensorHost.isEnabled = true;
            SensorHost.UseReserve = false;

            SimulationTime.AddAction(Update);
        }
        public override ResponceWord HandleMKIOWord(CommandWord word)
        {
            AUCommandWord? command = word as AUCommandWord;
            if (command == null)
                throw new ArgumentException();
                // return ResponceWord.FAIL;

            if (command.useReserve)
                SensorHost.UseReserve = true;
            else
                SensorHost.UseReserve = false;

            return ResponceWord.OK;
        }

        private void Update(double DeltaTime)
        {
        }
    }
}
