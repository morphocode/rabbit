using System.Collections.Generic;
using System;

using Rabbit.Kernel.Automata.FSM;
using Rabbit.Kernel.ABMS.Space.Grid;
using Rabbit.Kernel.CellularAutomata.Cells;

namespace Rabbit.Kernel.CellularAutomata.Impl.Elementary
{
    /**
     * A cell do not provides method to change it's state! It's state could be changed only by the transition rules.
     * That's because the changes in the state usually are not applied immediately
     * 
     * A cell is a Finite State Machine. It has a state and a finite number of states.
     *
     * For ex. a cell could have 2 states: Dead or Alive
     * The cell changes it's state according to a set of Evolution rules.
     */
    public class ElementaryCell : Cell
    {

        private CellState aliveState;
        private CellState deadState;

        private ElementaryCell leftNeighbor, rightNeighbor;

        /** Spits more info, if needed in ToString()..Used for debug and info */
        private Boolean isPrototype = false;
        /** Binary rule as String, used for debug */
        private String binaryRule = null;

        public ElementaryCell(int id, CellState aliveState, CellState deadState, IList<CellularRule> elementaryRules) 
            : base(id, new List<CellState>(2) { aliveState, deadState }, elementaryRules) 
        {
            this.aliveState = aliveState;
            this.deadState = deadState;
            this.cellState = deadState;
        }

        /*
        public LivingCell(int id, IList<CellularRule> livingRules)
            : base(id, LivingStates, livingRules)
        {
            this.cellState = deadState;
        }*/


        public void SetLeftNeighbor(ElementaryCell leftNeighbor)
        {
            this.leftNeighbor = leftNeighbor;
        }

        public void SetRightNeighbor(ElementaryCell rightNeighbor)
        {
            this.rightNeighbor = rightNeighbor;
        }

        public override CellState GetDefaultState()
        {
            return deadState;
        }


        public Boolean IsAlive()
        {
            return cellState.Equals(aliveState);
        }

        public CellState GetAliveState()
        {
            return aliveState;
        }

        public CellState GetDeadState()
        {
            return deadState;
        }

        public override ICell Clone()
        {
            return new ElementaryCell(-1, aliveState, deadState, transitionRules);
        }

        /**
        * In the process of computation. Each cell evolves according to it's context.
        * Each cell mutates depending on its neighbors.
        * @return The new state of the cell
        * 
        */
        //none of the rules returned a new CellState, so DIE:
        public override CellState UpdateState()
        {
            //_time++;
            CellState EvolvedState = GetEvolvedState();
            if (EvolvedState != null)
                return EvolvedState;
            else
                return GetDeadState();
        }
        //

        /**
         * If this cell is a prototype, ToString() spits more info
         * FIXME: Prototype should not be part of the Cell API(the Kernel) as it is specific to how this cell is used in the GH interface
         * being a prototype is not part of the CA nature(it's part of the Rabbit GH integration)
         */ 
        public void setIsPrototype(Boolean isPrototype)
        {
            this.isPrototype = isPrototype;
        }

        protected CellState SolveEvolvedState()
        {
            ElementaryRule elementaryRule = matchRule(leftNeighbor.cellState, cellState, rightNeighbor.cellState);

            if(elementaryRule==null)
                throw new SystemException("Internal error..Can't determine which rule to use for the current pattern of states!");

            return elementaryRule.GetResultingState();   

        }

        /**
         * matches the rule that can be applied for a givent pattern(the states of the cell and its immediate neighbors 
         */ 
        private ElementaryRule matchRule(CellState leftNeighborState, CellState selfState, CellState rightNeighborState)
        {
            foreach (ElementaryRule rule in transitionRules)
            {
                if(rule.matches(leftNeighborState, selfState, rightNeighborState))
                    return rule;
            }

            return null;

        }

   
        protected CellState GetEvolvedState()
        {
            //if (leftNeighbor.IsAlive()) return aliveState;
            return SolveEvolvedState();
        }

        public static int ToDecimal(string bin)
        {
            long l = Convert.ToInt64(bin, 2);
            int i = (int)l;
            return i;
        }
        
        private String formatBinaryRule()
        {
            String binaryRule = "";
            binaryRule += formatCellState(matchRule(aliveState, aliveState, aliveState).GetResultingState());
            binaryRule += formatCellState(matchRule(aliveState, aliveState, deadState).GetResultingState());
            binaryRule += formatCellState(matchRule(aliveState, deadState, aliveState).GetResultingState());
            binaryRule += formatCellState(matchRule(aliveState, deadState, deadState).GetResultingState());
            binaryRule += formatCellState(matchRule(deadState, aliveState, aliveState).GetResultingState());
            binaryRule += formatCellState(matchRule(deadState, aliveState, deadState).GetResultingState());
            binaryRule += formatCellState(matchRule(deadState, deadState, aliveState).GetResultingState());
            binaryRule += formatCellState(matchRule(deadState, deadState, deadState).GetResultingState());
            return binaryRule;
        }

        private String formatCellState(CellState cellState)
        {
            if (cellState.Equals(aliveState))
                return "1";
            else if (cellState.Equals(deadState))
                return "0";
            throw new SystemException("Internal error..Invalid Cell State for an Elementary Cell: "+cellState.ToString());
        }

        public override String ToString()
        {
            if (isPrototype)
            {
                if (binaryRule == null)
                    this.binaryRule = formatBinaryRule();

                return string.Format("Elementary Cell Prototype, Following Rule: {0}({1})", ToDecimal(binaryRule), binaryRule);
            }
            else
                return string.Format("{0} Elementary Cell", IsAlive() ? "Alive" : "Dead");
        }

    }

}
