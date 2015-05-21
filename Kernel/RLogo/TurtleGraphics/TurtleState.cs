using System;
using System.Collections.Generic;
using System.Text;

using Rhino.Geometry;

namespace Rabbit.Kernel.TurtleGraphics
{
    /**
     * The TurtleState is defined by a position in space and heading vector.
     * The heading defines the orientation of the turtle
     */
    public class TurtleState
    {

        private Plane orientation;
        /** Position */ //should be accessed directly as a class member in order to be modified
        private Point3d position;
        private double stepLength;
        private double thickness;
        private Curve profile;

        public TurtleState(Point3d position, Plane orientation, double currentStepLength, double thickness, Curve profile)
        {
            this.position = position;
            this.orientation = orientation;
            this.stepLength = currentStepLength;
            this.thickness = thickness;
            this.profile = profile;
        }

        public double GetThickness()
        {
            return thickness;
        }

        public double GetStepLength()
        {
            return stepLength;
        }

        public void SetStepLength(double newStepLength)
        {
            this.stepLength = newStepLength;
        }

        public Point3d GetPosition()
        {
            return position;
        }

        public void SetPosition(Point3d position)
        {
            this.position = position;
        }

        public Plane GetOrientation()
        {
            return orientation;
        }

        public Curve GetProfile()
        {
            return profile;
        }
 
    }
}
