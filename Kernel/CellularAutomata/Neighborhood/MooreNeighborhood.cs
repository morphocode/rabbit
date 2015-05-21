using System;
using System.Collections.Generic;
using System.Text;

using Rabbit.Kernel.ABMS.Space.Grid;

namespace Rabbit.Kernel.CellularAutomata
{
    /**
     * In cellular automata, the Moore neighborhood comprises the eight cells surrounding a central cell on a two-dimensional square lattice. The neighborhood is named after Edward F. Moore, a pioneer of cellular automata theory. It is one of the two most commonly used neighborhood types, the other one being the 4-cell von Neumann neighborhood. The well known Conway's Game of Life, for example, uses the Moore neighborhood. It is similar to the notion of 8-connected pixels in computer graphics.
     * The concept can be extended to higher dimensions, for example forming a 26-cell cubic neighborhood for a cellular automaton in three dimensions.
     * The Moore neighbourhood of a point is the points at a Chebyshev distance of 1.
     * 
     */
    public class MooreNeighborhoodStrategy:INeighborhoodStrategy
    {

        private Grid2d<ICell> grid2d;

        //whether or not to include the cell itself in the neighbourhood
        private Boolean includeCell;

        public MooreNeighborhoodStrategy(Grid2d<ICell> regularCellularLattice, bool includeCell)
        {
            this.grid2d = regularCellularLattice;
            this.includeCell = includeCell;
        }

 
        /**
         * Builds the neighbors list for this cell.
         */ 
        public IList<ICell> BuildNeighborhood(ICell cell)
        {
            return BuildMooreNeighbors(cell);
        }

        private int GetWestX(int x)
        {
            if (x == 0) return grid2d.GetXDimension() - 1;//inifinite boundary - torus
            else return x - 1;
        }

        private int GetEastX(int x)
        {
            if (x == grid2d.GetXDimension() - 1) return 0;//inifinite boundary - torus
            else return x + 1;
        }

        private int GetNorthY(int Y)
        {
            if (Y == grid2d.GetYDimension() - 1) return 0;//inifinite boundary - torus
            else return Y + 1;
        }

        private int GetSouthY(int Y)
        {
            if (Y == 0) return grid2d.GetYDimension() - 1;//inifinite boundary - torus
            else return Y - 1;
        }

        /**
         * @return the so-called Moore neighborhood: the 8 neighbors of a cell at North, South, East, West, NW, NE, SW, SE
         */
        private IList<ICell> BuildMooreNeighbors(ICell cell)
        {
            IList<ICell> neighbors = new List<ICell>();
            Grid2d<ICell>.Location cellPosition = grid2d.GetPosition(cell);
            int X = cellPosition.GetX();
            int Y = cellPosition.GetY();

            ICell northNeighbor = grid2d.GetCell(X, GetNorthY(Y));
            ICell southNeighbor = grid2d.GetCell(X, GetSouthY(Y));
            ICell eastNeighbor = grid2d.GetCell(GetEastX(X), Y);
            ICell westNeighbor = grid2d.GetCell(GetWestX(X), Y);
            ICell northEastNeighbor = grid2d.GetCell(GetEastX(X), GetNorthY(Y));
            ICell northWestNeighbor = grid2d.GetCell(GetWestX(X), GetNorthY(Y));
            ICell southEastNeighbor = grid2d.GetCell(GetEastX(X), GetSouthY(Y));
            ICell southWestNeighbor = grid2d.GetCell(GetWestX(X), GetSouthY(Y));


            //add the neighbors to the list, if they exist(at the boundary cells have no all of the 4 neighbors)
            if (northNeighbor != null) neighbors.Add(northNeighbor);
            if (southNeighbor != null) neighbors.Add(southNeighbor);
            if (eastNeighbor != null) neighbors.Add(eastNeighbor);
            if (westNeighbor != null) neighbors.Add(westNeighbor);
            if (northEastNeighbor != null) neighbors.Add(northEastNeighbor);
            if (northWestNeighbor != null) neighbors.Add(northWestNeighbor);
            if (southEastNeighbor != null) neighbors.Add(southEastNeighbor);
            if (southWestNeighbor != null) neighbors.Add(southWestNeighbor);

            //add the cell-itself to the neighborhood
            if (includeCell)
                neighbors.Add(cell);

            return neighbors;
        }


    }
}
