using System;
using System.Collections.Generic;
using System.Text;

namespace Rabbit.Kernel.CellularAutomata
{
    /**
     * Abstraction of a strategy that builds neighborhoods of cells
     * 
     */ 
    public interface INeighborhoodStrategy
    {

        /**
         * <returns> the list of neighbors of the specified cell</returns>
         * 
         */ 
        IList<ICell> BuildNeighborhood(ICell cell);


    }
}
