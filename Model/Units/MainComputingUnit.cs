using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Manchester;
using Model.Time;
using Model.Variant;

namespace Model.Units
{
    internal class MainComputingUnit : BaseUnit
    {
        private Interface MKIO;

        public MainComputingUnit(Interface @interface)
        {
            MKIO = @interface;

            SimulationTime.AddAction(Update);
        }

        public override ResponceWord HandleMKIOWord(CommandWord word)
        {
            throw new NotSupportedException();
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
                MKIO.SendWord(AUCommandWord.WordUseReserve, 1);
            }
        }
    }
}
