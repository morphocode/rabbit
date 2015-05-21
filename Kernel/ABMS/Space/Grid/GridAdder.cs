using System;
using System.Collections.Generic;
using System.Text;

using Rabbit.Kernel.ABMS.Space.Projection;

namespace Rabbit.Kernel.ABMS.Space.Grid
{
    public class GridAdder<O>:IAdder<Grid2d<O>.Location, O>
    {

        private Grid2d<O> grid;

        public GridAdder(Grid2d<O> grid)
        {
            this.grid = grid;

        }


        public void Add(Grid2d<O>.Location location, O o) {
            //grid.Add(o, location);
        }

    }
}
