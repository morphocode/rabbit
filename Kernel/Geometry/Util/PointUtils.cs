using System;
using System.Collections.Generic;
using System.Text;

//using RMA.OpenNURBS;
using Rhino.Geometry;

namespace Rabbit.Kernel.Geometry.Util
{
    /**
     * Utility class that provides some common features related to point manipulation
     * 
     */ 
    public class PointUtils
    {

        private PointUtils() { }

        /**
          * returns a list of all points from latticePoints that are closer to each point in points
          */
        public static Point3d GetClosestPoint(Point3d point, IList<Point3d> latticePoints)
        {
            if (latticePoints == null || point == null || latticePoints.Count == 0)
                throw new InvalidOperationException("point or lattice of points are invalid/empty!");

            //IList<On3dPoint> closestPoints = new List<On3dPoint>(points.Count);
            double minDistance = Double.MaxValue;
            double distance;
            int closestPointIndex = -1;
            int pointIndex = 0;
            foreach (Point3d latticePoint in latticePoints)
            {
                distance = point.DistanceTo(latticePoint);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestPointIndex = pointIndex;
                }
                pointIndex++;
            }

            return latticePoints[closestPointIndex];
        }

        public static IList<Point3d> GetClosestPoints(IList<Point3d> points, IList<Point3d> latticePoints)
        {
            List<Point3d> closestPoints = new List<Point3d>(points.Count);
            foreach (Point3d point in points)
            {
                closestPoints.Add(GetClosestPoint(point, latticePoints));
            }

            return closestPoints;
        }

    }
}
