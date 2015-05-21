using System;
using System.Collections.Generic;
using System.Text;

using Rabbit.Kernel.Automata.FSM;
using Rabbit.Kernel.CellularAutomata.Cells;

namespace Rabbit.Kernel.CellularAutomata.Impl.Elementary
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
    public class ElementaryRule : CellularRule {

        private CellState leftCellState, rightCellState, middleCellState, resultingCellState;

        public ElementaryRule(CellState leftCellState, CellState middleCellState, CellState rightCellState, CellState resultingCellState)
        {
            this.leftCellState = leftCellState;
            this.middleCellState = middleCellState;
            this.rightCellState = rightCellState;
            this.resultingCellState = resultingCellState;
        }

        public CellState ApplyRule(ICell cell)
        {
            throw new SystemException("Not implemented");
        }


        public  CellState SolveState(CellState currentState, Object input)
        {
            throw new SystemException("Not implemented");
        }

        public CellState GetResultingState()
        {
            return resultingCellState;
        }

        public Boolean matches(CellState currentLeftCellState, CellState currentState, CellState currentRightCellState)
        {
            return currentLeftCellState.Equals(leftCellState) && currentState.Equals(middleCellState) && currentRightCellState.Equals(rightCellState);
        }

    }
}
