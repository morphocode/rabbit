using System;
using System.Collections.Generic;
using System.Text;

//using RMA.OpenNURBS;
using Rhino.Geometry;

using Rabbit.Kernel.Automata.FSM;

using Rabbit.Kernel.CellularAutomata;
using Rabbit.Kernel.CellularAutomata.Cells;

namespace Rabbit.GH.CellularAutomata
{
    /**
     * Specifies a custom state for a collection of points, which a resolved as cells by the OnCellularGridBuilder
     * 
     * 
     */ 
    public class OnStateConfig
    {

        private IList<Point3d> configurationPoints;
        private CellState state;

        public OnStateConfig(IList<Point3d> configurationPoints, CellState state)
        {
            this.configurationPoints = configurationPoints;
            this.state = state;
        }

        public CellState GetState()
        {
            return state;
        }

        public IList<Point3d> GetPoints()
        {
            return configurationPoints;
        }

        public void SetPoints(IList<Point3d> points)
        {
            this.configurationPoints = points;
        }


        public override String ToString()
        {
            return "Custom State Configuration";
        }
    }
}
