using System;
using System.Collections.Generic;
using System.Text;

using Grasshopper.Kernel;
using Grasshopper.Kernel.Data;
using Grasshopper.Kernel.Types;

using Rhino.Geometry;

namespace Rabbit.GH.LSystems
{
    /** 
     * Encapsulates settins on how to loft the skeleton produced by the turtle.
     */ 
    public class GH_TubeSettings
    {
        public double defaultThickness;
        public double defaultThicknessScale;
        public Curve profile;
        public Plane profilePivot;

        public GH_TubeSettings(double defaultThickness, double defaultThicknessScale, Curve profile, Plane profilePivot) {
            this.defaultThickness = defaultThickness;
            this.defaultThicknessScale = defaultThicknessScale;
            this.profile = profile;
            this.profilePivot = profilePivot;
        }

    }
}
