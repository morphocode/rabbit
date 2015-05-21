using System;
using System.Collections.Generic;
using System.Text;


using Rabbit.Kernel.ABMS.Space.Grid;
using Rabbit.Kernel.CellularAutomata.Impl.Life;
using Rabbit.Kernel.Util;

namespace Rabbit.Kernel.CellularAutomata.Configuration
{
    public class RandomCAConfig: ICAConfig
    {

        private Random randomGenerator;

        private IList<CellState> states;

        public RandomCAConfig(IList<CellState> states) {
            randomGenerator = new Random();
            this.states = states;
        }

        public CellState GetCellState(ICell cell)
        {
            //CellState randomCellState;
            int randomIndex = randomGenerator.Next(0, states.Count);
            return states[randomIndex];

        }

        public override String ToString()
        {
            return "Random State Configuration distributing randomly the following Cell states: " + PresentationUtils.ListToString(states);
        }

        public IEnumerable<ICell> GetCells()
        {
            throw new SystemException("Not implemented!");
        }

        public int GetAssociatedTime()
        {
            throw new SystemException("Not implemented!");
        }
    }
}
