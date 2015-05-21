using System.Collections.Generic;
using System;

using Rabbit.Kernel.Automata.FSM;
using Rabbit.Kernel.ABMS.Space.Grid;
using Rabbit.Kernel.CellularAutomata.Cells;

namespace Rabbit.Kernel.CellularAutomata.Impl.Life
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
    public class LifeLikeCell : Cell
    {


        //private static CellState aliveState = new CellState(true);
        //private static CellState deadState = new CellState(false);


        //CONWAY GameOfLife rules ----------------------------------------------------------
        //private static LivingCell GOL_LivingCellPrototype = new LivingCell(-1, ConwayGOLRules);

        public static BornRule BornOnExactly3LivingNeighbors = new BornRule(3);
        public static SurviveRule SurviveOn2LivingNeighbors = new SurviveRule(2);
        public static SurviveRule SurviveOn3LivingNeighbors = new SurviveRule(3);

        public static IList<CellularRule> ConwayGOLRules = new List<CellularRule>(2) {
            SurviveOn2LivingNeighbors,
            SurviveOn3LivingNeighbors,
            BornOnExactly3LivingNeighbors
        };

        //temp, use booleans instead
        //private static CellState[] LivingStates = { aliveState, deadState };

        private CellState aliveState;
        private CellState deadState;


        public LifeLikeCell(int id, CellState aliveState, CellState deadState, IList<CellularRule> livingRules) 
            : base(id, new List<CellState>(2) { aliveState, deadState }, livingRules) 
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
            return new LifeLikeCell(-1, aliveState, deadState, transitionRules);
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


        protected CellState GetEvolvedState()
        {
            //iterate over all Evolution Rules, 
            // The priority of the rules is by their index
            //The first Rule that returns a Resolve CellState defines the new State of the cell
            //if every Rule returns an UNRESOLVED cell state
            // the Cell does not change its state(does not evolve)
            foreach (LifeLikeRule evolutionRule in transitionRules)
            {
                CellState EvolvedState = evolutionRule.ApplyRule(this);
                if (EvolvedState != null)
                    return EvolvedState;
            }
            return null;//LivingCell.DeadState;
        }

        public override String ToString()
        {
            String cellInfo = string.Format("Life-Like Cell [Current State: {0}]; Evolution Rules:", (IsAlive() ? "Alive" : "Dead"));
            foreach (LifeLikeRule evolutionRule in transitionRules)
                cellInfo += "\n" + evolutionRule.ToString();
            return cellInfo;
        }

    }

}
