using System;
using System.Collections.Generic;
using System.Text;

using Rabbit.Kernel.Util;

namespace Rabbit.Kernel.CellularAutomata.Impl.Life
{
    /**
     * A cell is born if there is a concrete number of neighbors
     */ 
    public class BornRule:NeighborhoodEvolutionRule
    {

        private IList<int> LivingNeighborsRequirements;

        public BornRule(int livingNeighborsRequirement)
        {
            this.LivingNeighborsRequirements = new List<int>(1);
            LivingNeighborsRequirements.Add(livingNeighborsRequirement);
        }

        public BornRule(IList<int> LivingNeighborsRequirements)
        {
            this.LivingNeighborsRequirements = LivingNeighborsRequirements;
        }


        public override CellState ApplyRule(LifeLikeCell cell)
        {
            IList<ICell> neighbors = cell.GetNeighbors();
            int aliveNeighborsCount = getAliveCellsCount(neighbors);
            if (!cell.IsAlive() && LivingNeighborsRequirements.Contains(aliveNeighborsCount))
                return cell.GetAliveState();

            else
                //return cell.GetState();//CellState.UNRESOLVED;
                return null;
        }


        public override String ToString()
        {
            return "Dead cell comes to life when it has exactly " + PresentationUtils.ListToString<int>(LivingNeighborsRequirements) + " living neighbors";
        }

    }
}
