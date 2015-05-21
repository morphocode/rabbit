using System;
using System.Collections.Generic;
using System.Text;
using Grasshopper.Kernel;
using System.Drawing;
using Rabbit.Properties;

namespace Rabbit.GH
{

    /**
     * Base component for all components within Rabbit.
     * Every component should inherit this one.
     * Provides place for common code shared by every component within Rabbit.
     * 
     * @author HTTP://MORPHOCODE.COM
     * 
     */ 
    public abstract class Component_RabbitBase : GH_Component
    {

        public static String RABBIT_CATEGORY = "Rabbit";

        /**
         * Constructor
         */ 
        public Component_RabbitBase(String name, String nick, String description, String subCategory):
            base(name, nick, "MORPHOCODE:  " + description, Component_RabbitBase.RABBIT_CATEGORY, subCategory)//base(name, abbreviation, description, category, subcategory)
        {

        }

 
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager inputManager)
        {
            //common code here
        }

        protected override void RegisterOutputParams(GH_OutputParamManager pManager)
        {
            //common code here
        }

        /**
         * Provides place for common logic for each component
         *
         */ 
        protected sealed override void SolveInstance(IGH_DataAccess DA)
        {

            //do the Component computation
            SolveRabbitInstance(DA);
        }

        /**
         * Rabbit components should implement this method instead of the standard GH SolveInstance
         */ 
        protected abstract void SolveRabbitInstance(IGH_DataAccess DA);

        /**
         * The Guid of the component
         */ 
        public override Guid ComponentGuid
        {
            get {
                return new Guid("{AFFF17BD-5975-460b-9883-525AE0677088}"); 
            }
        }

        /**
         * The icon of the component
         *
        protected override Bitmap Internal_Icon_24x24
        {
            get
            {
                return Resources.Resources.Custom_24x24_Rabbit;
            }
        }*/

        /*
        protected override string HelpDescription
        {
            get
            {
                return (this.Description + " Distance is measured from the tangent discontinuities along the curve. Since it is highly unlikely that 3D curves have coplanar segments a given distance from the corners, fillet segments will be composed of Bi-Arcs rather than single arcs.");
            }
        }*/

        /*
         * //specifies whether the component is displayed in the toolbar or only in the drop down list of subcategory
        public override GH_Exposure Exposure
        {
            get
            {
                return GH_Exposure.dropdown_only;
            }
        }
         * */
 




    }
}
