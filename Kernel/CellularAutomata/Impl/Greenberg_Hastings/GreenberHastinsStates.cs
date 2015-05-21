using System;
using System.Collections.Generic;
using System.Text;

using Rabbit.Kernel.CellularAutomata;

namespace Rabbit.Kernel.CellularAutomata.Impl.Greenberg_Hastings
{
    public class GreenberHastinsStates
    {

        public static CellState RestingState = new CellState("Resting");
        public static CellState ExcitedState = new CellState("Excited");
        public static CellState RecoveryState = new CellState("Recovery");
    }
}
