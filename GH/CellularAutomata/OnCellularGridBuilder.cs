using System;
using System.Collections.Generic;
using System.Text;

//using RMA.OpenNURBS;
using Rhino.Geometry;

using Rabbit.Kernel.ABMS.Space.Grid;
using Rabbit.Kernel.CellularAutomata;
using Rabbit.Kernel.CellularAutomata.Cells;
using Rabbit.Kernel.CellularAutomata.Configuration;


namespace Rabbit.GH.CellularAutomata
{
    /**
     * Builds a Grid2d out of OpenNurbs points
     * 
     * 
     */ 
    public class OnCellularGridBuilder: CellularGridBuilder
    {

        private ICAConfig initialConfiguration;

        private INeighborhoodStrategy neighborhoodStrategy;

        private int XDimension, YDimension;

        private IList<Point3d> pointLattice;

        private OnStateConfig stateConfig;
        private Boolean random;
        private Boolean custom;

        private RandomCAConfig randomConfig;

        public OnCellularGridBuilder(ICell cellPrototype, IList<Point3d> pointLattice, int XDimension, int YDimension)
            : base(cellPrototype)
        {
            this.XDimension = XDimension;
            this.YDimension = YDimension;
            this.pointLattice = pointLattice;
        }

        public OnCellularGridBuilder(ICell cellPrototype, IList<Point3d> pointLattice, int XDimension, int YDimension, OnStateConfig stateConfig)
            : this(cellPrototype, pointLattice, XDimension, YDimension) 
        {
            random = false;
            custom = true;
            this.stateConfig = stateConfig;
        }

        public OnCellularGridBuilder(ICell cellPrototype, IList<Point3d> pointLattice, int XDimension, int YDimension, RandomCAConfig randomConfig)
            : this(cellPrototype, pointLattice, XDimension, YDimension)
        {
            random = true;
            custom = false;
            this.randomConfig = randomConfig;
        }

        protected override Grid2d<ICell> CreateCellularGrid()
        {
            return new Grid2d<ICell>(XDimension, YDimension);
        }

        protected override void PopulateGrid()
        {
            int index = 0;
            //populate the grid with cells            
            for (int r = 0; r < cellularGrid.GetXDimension(); r++)
            {
                for (int c = 0; c < cellularGrid.GetYDimension(); c++)
                {
                    Grid2d<ICell>.Location position = new Grid2d<ICell>.Location(r, c);
                    ICell cell = cellPrototype.Clone();
                    cell.SetAttachedObject(pointLattice[index]);
                    cell.SetId(index);
                    cellularGrid.Add(cell, position);
                    index++;
                }
            }            
        }
        
        //private ICAConfig CreateICAConfig(IList<On3dPoint> latticePoints, IList<On3dPoint> configurationPoints)
        //creates an ICAConfig instance out of the OpenNurbs state configuration
        private ICAConfig CreateICAConfig(OnStateConfig stateConfig)
        {
            
            CAConfig cfg = new CAConfig(0, cellPrototype.GetState());
            int latticePointIndex = 0;
            foreach (Point3d latticePoint in pointLattice)
            {
                foreach (Point3d configurationPoint in stateConfig.GetPoints())
                    if (latticePoint.X==configurationPoint.X && latticePoint.Y==configurationPoint.Y && latticePoint.Z==configurationPoint.Z)
                        cfg.AddCellState(cellularGrid.GetObject(latticePointIndex), stateConfig.GetState());

                latticePointIndex++;
            }//foreach

            return cfg;
        }


        protected override ICAConfig GetInitialConfiguration()
        {
            if (initialConfiguration == null)
            {
                if (random) initialConfiguration = randomConfig;
                else if (custom) initialConfiguration = CreateICAConfig(stateConfig);
                else initialConfiguration = null;//no configuration defined
            }
            return initialConfiguration;
        }


        protected override INeighborhoodStrategy GetNeighborhoodStrategy()
        {
            if(neighborhoodStrategy==null)//lazy initialization
                neighborhoodStrategy = new MooreNeighborhoodStrategy(cellularGrid, false);

            return neighborhoodStrategy;
        }

    }
}
