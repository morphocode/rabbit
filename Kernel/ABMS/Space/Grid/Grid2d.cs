using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using Rabbit.Kernel.ABMS.Space.Projection;

namespace Rabbit.Kernel.ABMS.Space.Grid
{
    /**
     * Grid space that contains objects
     * TODO: implement this as matrix
     * 
     */ 
    public class Grid2d<A>:IProjection<A>
    {

        private int XDimension;
        private int YDimension;

        private Dictionary<A, Location> gridMap;
        private Dictionary<Location, A> reverseGridMap;
        //private ICell[,] cellArray;
        private IList<A> objectsList;


        public Grid2d(int XDimension, int YDimension) 
        {
            this.XDimension = XDimension;
            this.YDimension = YDimension;

            this.objectsList = new List<A>(XDimension * YDimension);
            this.gridMap = new Dictionary<A, Location>(XDimension * YDimension);
            this.reverseGridMap = new Dictionary<Location, A>(XDimension * YDimension);

        }


        /**
         * Adds a cell on a specific location in the grid
         */
        public void Add(A obj, Location location)
        {
                if (!isValidPosition(location))
                    throw new IndexOutOfRangeException(location + " is out of the Geography2D dimentions!");
                //if (objectsList.Contains(obj) || gridMap.ContainsKey(obj) || reverseGridMap.ContainsKey(location))
                    //throw new InvalidOperationException("Could not add more than one object on the same grid location!");

                //cellArray[position.GetX(), position.GetY()] = cell;
                objectsList.Add(obj);
                gridMap.Add(obj, location);
                reverseGridMap.Add(location, obj);
            
        }

        public A GetCell(int x, int y)
        {
            Location location = new Location(x, y);
            return GetObjectAt(location);
        }

        /**
          * @returns A cell on the specified position
          */
        public A GetObjectAt(Location location)
        {
            if (isValidPosition(location))
                return reverseGridMap[location];
            else
                return default(A);
        }

        /**
         * @return The position of a cell 
         */
        public Location GetPosition(A obj)
        {
            if (gridMap.ContainsKey(obj))
                return gridMap[obj];// worldMap[cell];
            else
                throw new InvalidOperationException("The specified cell is not part of the current lattice!");
        }

        /**
         * @return True - if the position is within the dimension of the world. 
         */
        private Boolean isValidPosition(Location location)
        {
            return isValidPosition(location.GetX(), location.GetY());
        }

        private Boolean isValidPosition(int row, int column)
        {
            if (row >= 0 && row < XDimension && column >= 0 && column < YDimension)
                return true;
            else
                return false;
        }


        /**
         * @returns The number of rows of this World
         */
        public int GetXDimension()
        {
            return XDimension;
        }

        public int GetYDimension()
        {
            return YDimension;
        }



        public override String ToString()
        {
            return "2D Grid with XDimension=" + XDimension + ", Y Dimension=" + YDimension;
        }


        public IList<A> GetObjects()
        {
            return objectsList;
        }

        public A GetObject(Object id)
        {
            int cellIndex = int.Parse(id.ToString());
            return GetObjects()[cellIndex];
        }



            /**
             * Position defines the exact location of a Cell within the 2D world.
             * 
             */
            public class Location
            {
                private int X;
                private int Y;

                private String hashCode;
                /**
                 * Constructor.
                 */
                public Location(int X, int Y)
                {
                    this.X = X;
                    this.Y = Y;
                    hashCode = X + ":" + Y;
                }

                public int GetX()
                {
                    return X;
                }

                public int GetY()
                {
                    return Y;
                }

                public override Boolean Equals(Object obj)
                {
                    if (obj == null)
                        return false;
                    if (!obj.GetType().Equals(this.GetType()))
                        return false;
                    else
                    {
                        Location p2 = (Location)obj;
                        return (p2.X == this.X) && (p2.Y == this.Y);
                    }
                }

                public override int GetHashCode()
                {
                    return hashCode.GetHashCode();
                }
            }
        }
}
