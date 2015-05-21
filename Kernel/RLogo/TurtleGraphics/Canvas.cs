using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

//using RMA.OpenNURBS;
using Rhino.Geometry;

namespace Rabbit.Kernel.TurtleGraphics
{
    /**
     * Canvas on which the Turtle draws
     * 
     * 
     */ 
    public class Canvas
    {

        private IList<Line> edges;
        private IList<Point3d> vertices;
        //generic geometry
        private IList<GeometryBase> geometryList;
        private IList<Plane> planes;
        private IList<Curve> profiles;
        

        public Canvas()
        {
            edges = new List<Line>();
            vertices = new List<Point3d>();
            geometryList = new List<GeometryBase>();
            planes = new List<Plane>();
            profiles = new List<Curve>();
        }

        public void AddVertex(Point3d vertex)
        {
            vertices.Add(vertex);
        }

        public IList<Point3d> GetVertices()
        {
            return vertices;
        }

        public void AddEdge(Line line)
        {
            edges.Add(line);
        }

        public IList<Line> GetEdges()
        {
            return edges;
        }

        public void AddGeometry(GeometryBase geometry)
        {
            geometryList.Add(geometry);
        }

        public IList<GeometryBase> GetGeometry()
        {
            return geometryList;
        }

        public void AddPlane(Plane plane)
        {
            planes.Add(plane);
        }

        public IList<Plane> GetPlanes()
        {
            return planes;
        }

        public void AddProfile(Curve profile)
        {
            profiles.Add(profile);
        }

        public IList<Curve> GetProfiles()
        {
            return profiles;
        }

        public void Clear()
        {
            edges.Clear();
            vertices.Clear();
            geometryList.Clear();
            planes.Clear();
            profiles.Clear();
        }

    }
}
