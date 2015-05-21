using System;
using System.Collections.Generic;
using System.Text;

using RMA.OpenNURBS;

using Rabbit.Kernel.ABMS.Space.Grid;
using Rabbit.Kernel.CellularAutomata.Configuration;
using Rabbit.Kernel.CellularAutomata.Impl.Life;

namespace Rabbit.Kernel.CellularAutomata
{
    /**
     * Builds a Grid2d out of OpenNurbs points
     * 
     * 
     */ 
    public class SimpleCellularGridBuilder: CellularGridBuilder
    {

        ICAConfig initialConfiguration;

        private int latticeRows, latticeColumns;

        public SimpleCellularGridBuilder():base(null)//LivingCell.GOL_LivingCellPrototype)
        {//second arg should be list of points


            //analyze the geometric input
            //grid size
            latticeRows = 10;
            latticeColumns = 10;

            //configuration
            //initialConfiguration = new CAConfig(0, LivingCell.DeadState);


        }

        protected override Grid2d<ICell> CreateCellularGrid()
        {
            return new Grid2d<ICell>(latticeRows, latticeColumns);
        }

        protected override void PopulateGrid()
        {
            throw new NotImplementedException();
        }
        

        //remove that!
        public ICAConfig GetCfg()
        {
            return initialConfiguration;
        }

        protected override ICAConfig GetInitialConfiguration()
        {
            return initialConfiguration;
        }


        protected override INeighborhoodStrategy GetNeighborhoodStrategy()
        {
            return new MooreNeighborhoodStrategy(cellularGrid, false);
        }

    }
}
