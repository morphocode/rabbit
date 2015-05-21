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
    public class Component_TubeSettings : Component_LSBase
    {


        /**
         * Constructor
         */
        public Component_TubeSettings()
            : base("Tube Settings", "Tube S.", "Custom tube settings.")
        {
        }

        protected override void RegisterInputParams(GH_Component.GH_InputParamManager inputManager)
        {

            inputManager.Register_DoubleParam("Thickness", "T", "Default thickness", 1.0, GH_ParamAccess.item);//name, nick, description, default, isList
            inputManager.Register_DoubleParam("Thickness scale", "dT", "Default thickness scale", 0.9, GH_ParamAccess.item);//name, nick, description, default, isList
            //profile settings
            inputManager.Register_CurveParam("Profile curve", "P", "Default profile curve", GH_ParamAccess.item);//name, nick, description, default, isList
            inputManager.Register_PlaneParam("Profile Pivot", "PP", "Profile pivot plane", Plane.WorldXY, GH_ParamAccess.item);//name, nick, description, default, isList
        }

        protected override void RegisterOutputParams(GH_OutputParamManager pManager)
        {
            pManager.Register_GenericParam("Tube Settings", "TS", "Tube Settings");//name, nick, description
        }

        /**
         * Does the computation
         */ 
        protected override void SolveRabbitInstance(IGH_DataAccess DA)
        {

            //GET THE INPUT PARAMS:               
            Double defaultThickness = 1.0;
            DA.GetData<Double>(0, ref defaultThickness);

            Double defaultThicknessScale = 0.7;
            DA.GetData<Double>(1, ref defaultThicknessScale);//param index, variable

            //Curve defaultProfile = new Circle(5.0).ToNurbsCurve();
            Curve profile = null;
            DA.GetData<Curve>(2, ref profile);//param index, variable

            Plane profilePivot = Plane.Unset;
            DA.GetData<Plane>(3, ref profilePivot);//param index, variable

            //init the LoftSkeletonSettings object
            GH_TubeSettings settings = new GH_TubeSettings(defaultThickness, defaultThicknessScale, profile, profilePivot);
            
            //Set the resulting Graphics in the output:
            DA.SetData(0, settings);
            
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
                return new Guid("{9D2582DD-6CF5-497a-8C40-C9E245391672}"); 
            }
        }


        /**
         * The icon of the component
         */
        protected override Bitmap Internal_Icon_24x24
        {
            get
            {
                return Resources.Custom_24x24_LoftSkeletonSettings;
            }
        }

    }
}
