using System;
using System.Collections.Generic;
using System.Text;

using Rabbit.Kernel.CellularAutomata;

namespace Rabbit.Kernel.CellularAutomata.Impl.Life
{
    /**
     * EvolutionRule which change the state of the cell according to the states of it's neighbors.
     * 
     * 
     */ 
    public abstract class NeighborhoodEvolutionRule:LifeLikeRule
    {

        public NeighborhoodEvolutionRule():base() { }


        /**
         * 
         */
        protected int getAliveCellsCount(IList<ICell> cells)
        {
            int aliveCellsCount = 0;
            foreach (ICell cell in cells)
                if (((LifeLikeCell)cell).IsAlive())
                    aliveCellsCount++;

            return aliveCellsCount;
        }

        public override CellState SolveState(CellState cellState, Object neighbors)
        {
            throw new NotImplementedException();
        }


    }
}
