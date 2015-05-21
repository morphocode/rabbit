using System;
using System.Text;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;

using Rhino.Geometry;

using Grasshopper.Kernel;
using Grasshopper.Kernel.Types;

using Rabbit.Properties;
using Rabbit.Kernel.TurtleGraphics;
using Rabbit.Kernel.TurtleGraphics.Commands;
using Rabbit.Kernel.RLogo;

using RMA.OpenNURBS;



namespace Rabbit.GH.LSystems
{
    /**
     * GH Component 
     * 
     * @Author MORPHOCODE.COM
     */ 
    public class Component_Turtle : Component_LSBase
    {

        //private static On3dPoint _defaultInitialPosition = new On3dPoint(0.0, 0.0, 0.0);
        //private static On3dVector _defaultInitialHeading = new On3dVector(1.0, 0.0, 0.0);
        private static Plane _defaultInitialOrientation = new Plane(new Point3d(0, 0, 0), new Vector3d(1, 0, 0), new Vector3d(0, 1, 0));//origin, x, y ?

        private Canvas canvas;// = new Canvas();

        /**
         * Constructor
         */
        public Component_Turtle()
            : base("Turtle", "Turtle", "The 'Turtle' is a virtual robot that moves in space and creates geometry. Each symbol in the source string is interpreted as a command that drives the Ninja.")
        {
            this.canvas = new Canvas();
        }

        protected override void RegisterInputParams(GH_Component.GH_InputParamManager inputManager)
        {

            inputManager.Register_StringParam("Source String", "S", "Source String. Each symbol in the string represents a command that drives the Turtle.", GH_ParamAccess.item);//name, nick, description, default, isList

            //Default step & step scale
            inputManager.Register_DoubleParam("Step length", "L", "Length of the turle's step.", 10.0, GH_ParamAccess.item);//name, nick, description, default, isList
            inputManager.Register_DoubleParam("Step length scale", "dL", "Step length scale", 0.9, GH_ParamAccess.item);//name, nick, description, default, isList

            inputManager.Register_DoubleParam("Angle", "A", "Default angle of the turle used for rotation.", 90.0, GH_ParamAccess.item);//name, nick, description, default, isList
            inputManager.Register_DoubleParam("Angle scale", "dA", "Default angle scale", 0.9, GH_ParamAccess.item);//name, nick, description, default, isList
                        
            //Position + Orientation of the turtle:
            inputManager.Register_PlaneParam("Initial Position and Orientation", "O", "Initial Position and Orientation of the Turtle", _defaultInitialOrientation, GH_ParamAccess.item);//name, nick, description, default, isList

            inputManager.Register_GenericParam("Tube Settings", "TS", "Tube Settings", GH_ParamAccess.item);//name, nick, description, default, isList
            Params.Input[6].Optional = true;
        }

        protected override void RegisterOutputParams(GH_OutputParamManager pManager)
        {
            pManager.Register_GenericParam("Edges", "E", "Edge lines created by the turtle");//name, nick, description
            pManager.Register_GenericParam("Vertices", "V", "Vertices created by the turtle");
            pManager.Register_GenericParam("Tube geometry", "T", "Tubes created by the turtle");
            pManager.Register_GenericParam("Planes", "P", "Turtle orientation planes");
            pManager.Register_GenericParam("Section Profiles", "S", "Section Profiles");
        }

        /**
         * Does the computation
         */ 
        protected override void SolveRabbitInstance(IGH_DataAccess DA)
        {

            //PARSE THE INPUT PARAMS:               
            String sourceString = null;
            DA.GetData<String>(0, ref sourceString);//param index, place holder

            Double defaultStep = 0.0;
            DA.GetData<Double>(1, ref defaultStep);//param index, variable

            Double stepLengthScale = 0.0;
            DA.GetData<Double>(2, ref stepLengthScale);//param index, variable

            Double defaultAngleIncrement = 0.0;
            DA.GetData<Double>(3, ref defaultAngleIncrement);

            Double defaultAngleScale = 0.7;
            DA.GetData<Double>(4, ref defaultAngleScale);//param index, variable
            
            GH_Plane ghPlane = null;
            DA.GetData<GH_Plane>(5, ref ghPlane);//param index, variable
            Plane rhinoPlane = ghPlane.Value;


            //Loft settings
            Boolean loftSkeleton = false;

            GH_ObjectWrapper loftSkeletonSettingsWrapper = null;
            DA.GetData<GH_ObjectWrapper>(6, ref loftSkeletonSettingsWrapper);


            Double defaultThickness = -1;
            Double defaultThicknessScale = -1;
            Curve profile = new Circle(5).ToNurbsCurve();
            Plane profilePivot = Plane.Unset;
            if (loftSkeletonSettingsWrapper != null && loftSkeletonSettingsWrapper.Value != null)
            {
                loftSkeleton = true;
                GH_TubeSettings loftSkeletonSettings = (GH_TubeSettings)loftSkeletonSettingsWrapper.Value;

                defaultThickness = loftSkeletonSettings.defaultThickness;
                defaultThicknessScale = loftSkeletonSettings.defaultThicknessScale;
                profile = loftSkeletonSettings.profile;
                profilePivot = loftSkeletonSettings.profilePivot;
            }

            //clear the canvas
            canvas.Clear();
            
            Turtle3d Turtle = new Turtle3d(canvas, new Point3d(rhinoPlane.Origin.X, rhinoPlane.Origin.Y, rhinoPlane.Origin.Z), new Plane(rhinoPlane.Origin, rhinoPlane.XAxis, rhinoPlane.YAxis), defaultStep, stepLengthScale, defaultAngleIncrement, profile, profilePivot, defaultThickness, defaultThicknessScale, loftSkeleton);//clone the input variables, because they could have been modified in a previous solution
            //interpret the specified source string
            RLogoInterpreter rlogoInterpreter = new RLogoInterpreter(Turtle);
            rlogoInterpreter.Interpret(sourceString);

            if (loftSkeleton)
            {
                Brep[] breps = Turtle.LoftSkeleton();
                foreach (Brep brep in breps)
                    canvas.AddGeometry(brep);
            }

            Boolean showTurtlePositions = false;

            //Set the resulting Graphics in the output:
            DA.SetDataList(0, canvas.GetEdges());
            DA.SetDataList(1, canvas.GetVertices());
            DA.SetDataList(2, canvas.GetGeometry());
            if(showTurtlePositions)
                DA.SetDataList(3, canvas.GetPlanes());
            DA.SetDataList(4, canvas.GetProfiles());
            //DA.SetDataTree
            
        }

        /**
         * Interpretative part of the LSystems
         * secondary exposure
         */ 
        public override GH_Exposure Exposure
        {
            get
            {
                return GH_Exposure.tertiary;
            }
        }

        /**
         * The Guid of the component
         */ 
        public override Guid ComponentGuid
        {
            get {
                return new Guid("{9D2582DD-6CF5-497a-8C40-C9E245498393}"); 
            }
        }


        /**
         * The icon of the component
         */
        protected override Bitmap Internal_Icon_24x24
        {
            get
            {
                return Resources.Custom_24x24_Turtle;
            }
        }

    }
}
