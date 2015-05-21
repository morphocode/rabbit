using System;
using System.Collections.Generic;
using System.Text;

using Rabbit.Kernel.CellularAutomata.Configuration;
using Rabbit.Kernel.ABMS.Space.Grid;


namespace Rabbit.Kernel.CellularAutomata.Configuration
{
    /**
     * CAConfiguration defines the state of a cellular automata at a exact point at the Time.
     * Holds the state of each cell of the CA.
     * 
     * Memento Design pattern
     * 
     * @author http://MORPHOCODE.COM
     */ 
    public class CAConfig : ICAConfig
    {

        private Dictionary<ICell, CellState> cellStates;

        private CellState defaultCellState;

        private int discreteTime;//the time at which this state was produced

        //private CA ca;

        public CAConfig(int discreteTime, CellState defaultCellState) //, ref CA ca)
        {
            //this.name = name;
            this.defaultCellState = defaultCellState;
            this.discreteTime = discreteTime;
            cellStates = new Dictionary<ICell, CellState>();

            //this.ca = ca;
        }

        public int GetAssociatedTime()
        {
            return discreteTime;
        }

        /*
        public CA getCA()
        {
            return ca;
        }*/

        /**
         * Specify a state for that cell. 
         * If a state was present overwrites it.
         */ 
        public void AddCellState(ICell cell, CellState cellState)
        {
            if (cellStates.ContainsKey(cell))
            {
                //remove the old state if the clien wants to specify a new one
                cellStates.Remove(cell);
                //throw new InvalidOperationException("Cell state was already specified! You could specify only one state per cell!");
            }
            cellStates.Add(cell, cellState);
        }

        
        public CellState GetCellState(ICell cell)
        {
            if (cellStates.ContainsKey(cell))
                return cellStates[cell];
            else
                return defaultCellState;
                //return null;
        }

        public IEnumerable<ICell> GetCells()
        {
            return cellStates.Keys;
        }
 
        public override string ToString()
        {
            return string.Format("CA Configuration at t={0}, containing {1} cell states", discreteTime, cellStates.Count);
        }
    }
}
