using System;
using System.Collections.Generic;
using System.Text;

using Rabbit.Kernel.ABMS.Space.Grid;
using Rabbit.Kernel.CellularAutomata.Configuration;

namespace Rabbit.Kernel.CellularAutomata
{

    /**
     * Builds a Cellular Automaton in a few steps:
     * 1.) Create a Space (Grid for ex.)
     * 2.) Populate the space/grid with a list of cells
     * 3.) Build connections between cells - neighborhoods
     * 4.) Define evolution rules
     * 
     */ 
    public abstract class CellularGridBuilder
    {

        //protected ICellFactory cellFactory;
        protected ICell cellPrototype;

        public CellularGridBuilder(ICell cellPrototype)
        {
            this.cellPrototype = cellPrototype;
            //this.cellularGrid = CreateCellularGrid();
        }


        protected Grid2d<ICell> cellularGrid;

        public Grid2d<ICell> BuildCellularGrid()
        {
            //create the grid
            cellularGrid = CreateCellularGrid();

            //add cells to the grid:
            PopulateGrid();

            //initialize cell state:  
       
            ICAConfig initialConfiguration = GetInitialConfiguration();
            if (initialConfiguration != null)
                foreach (ICell cell in cellularGrid.GetObjects())
                   cell.SetState(initialConfiguration.GetCellState(cell));
            

            //build neighborhoods:
            foreach (ICell cell in cellularGrid.GetObjects())
                ((ICell)cell).SetNeighbors(GetNeighborhoodStrategy().BuildNeighborhood(cell));

            //return the grid
            return cellularGrid;
        }



        protected abstract Grid2d<ICell> CreateCellularGrid();

        protected abstract void PopulateGrid();
        /*
        protected void PopulateGrid()
        {
            //Populate the grid:
            int index = 0;
            //populate the grid with cells            
            for (int r = 0; r < cellularGrid.GetXDimension(); r++)
            {
                for (int c = 0; c < cellularGrid.GetYDimension(); c++)
                {
                    Grid2d<ICell>.Location position = new Grid2d<ICell>.Location(r, c);
                    ICell cell = cellPrototype.Clone();
                    cell.SetId(index);
                    cellularGrid.Add(cell, position);
                    index++;
                }
            }
        }*/


        protected abstract ICAConfig GetInitialConfiguration();

        protected abstract INeighborhoodStrategy GetNeighborhoodStrategy();

        

    }
}
