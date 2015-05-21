using System;
using System.Collections.Generic;
using System.Text;

using Rabbit.Kernel.Util;

namespace Rabbit.Kernel.CellularAutomata.Impl.Life
{
    /**
     * A cell survives to the next generation depending on a number of neighbors
     */ 
    public class SurviveRule:NeighborhoodEvolutionRule
    {

        private IList<int> LivingNeighborsRequirements;

        public SurviveRule(int livingNeighborsRequirement)
        {
            this.LivingNeighborsRequirements = new List<int>(1);
            LivingNeighborsRequirements.Add(livingNeighborsRequirement);
        }

        public SurviveRule(IList<int> LivingNeighborsRequirements)
        {
            this.LivingNeighborsRequirements = LivingNeighborsRequirements;
        }


        public override CellState ApplyRule(LifeLikeCell cell)
        {
            IList<ICell> neighbors = cell.GetNeighbors();
            int aliveNeighborsCount = getAliveCellsCount(neighbors);
            if (cell.IsAlive() && LivingNeighborsRequirements.Contains(aliveNeighborsCount))
                return cell.GetState();
            else
                return null;
        }

        public override String ToString()
        {
            return "A living cell remains alive only when surrounded by " + PresentationUtils.ListToString<int>(LivingNeighborsRequirements) + " living neighbors";
        }

    }
}
