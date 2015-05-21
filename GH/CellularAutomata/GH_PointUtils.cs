using System;
using System.Collections.Generic;
using System.Text;

//using RMA.OpenNURBS;
using Rhino.Geometry;
using Grasshopper.Kernel.Types;

namespace Rabbit.GH.CellularAutomata
{
    /**
     * Utility class that provides some common functionality related to GH to ON points converting
     * 
     */ 
    public class GH_PointUtils
    {

        private GH_PointUtils() { }


        /**
         * converts a list of GH_Points into a list of On3dPoint
         */
        public static IList<Point3d> ConvertToOnPoints(IList<GH_Point> ghPoints)
        {
            IList<Point3d> ONPoints = new List<Point3d>(ghPoints.Count);
            foreach (GH_Point ghPoint in ghPoints)
                ONPoints.Add(ghPoint.Value);

            return ONPoints;
        }

    }
}
