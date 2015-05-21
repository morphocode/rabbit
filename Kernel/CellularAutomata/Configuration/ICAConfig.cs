using System;
using System.Collections.Generic;
using System.Text;

using Rabbit.Kernel.Automata.FSM;

namespace Rabbit.Kernel.CellularAutomata.Configuration
{
    public interface ICAConfig:IStateConfig<ICell, CellState>
    {

        IEnumerable<ICell> GetCells();

        int GetAssociatedTime();
    }
}
