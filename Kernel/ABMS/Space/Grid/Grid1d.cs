using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using Rabbit.Kernel.ABMS.Space.Projection;
using Rabbit.Kernel.CellularAutomata.Cells;
using Rabbit.Kernel.CellularAutomata;



namespace Rabbit.Kernel.ABMS.Space.Grid
{
    public class Grid1d<A>:IProjection<A>
    {
        private int XDimension;

        private A[] cells;

        private IList<A> cellList;

        public Grid1d(int XDimension)
        {
            this.XDimension = XDimension;
            cells = new A[XDimension];
            cellList = new List<A>(XDimension);
        }


        public void Add(A cell, int xPosition)
        {
            cells[xPosition] = cell;
            cellList.Add((A)cell);
        }

        public IList<A> GetObjects()
        {
            return cellList;
        }

        public A GetObjectAt(int xPosition)
        {
            return cells[xPosition];
        }


        public override String ToString()
        {
            return "1D Grid with XDimension=" + XDimension;
        }


    }
}
