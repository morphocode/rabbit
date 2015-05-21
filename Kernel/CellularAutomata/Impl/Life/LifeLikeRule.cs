using System;
using System.Collections.Generic;
using System.Text;

using Rabbit.Kernel.Automata.FSM;
using Rabbit.Kernel.CellularAutomata.Cells;

namespace Rabbit.Kernel.CellularAutomata.Impl.Life
{
    /**
     * The behavior of the cellular automata as a system is drivven by the behavior of each individual cell.
     * 
     * A CellBehavior defines what the cell does in every generation.
     * The state of the cell changes as the evolution goes.
     * The cell acts according to some simple rules.
     * This class defines what are these rules and makes possible to combine multiple evolution rules
     * 
     * A flyweight object.
     * 
     * @author MORPHOCODE.COM
     */ 
    public abstract class LifeLikeRule:CellularRule {

        public LifeLikeRule()
        {
        }

        public abstract CellState ApplyRule(LifeLikeCell cell);


        public abstract CellState SolveState(CellState currentState, Object input);

    }
}
