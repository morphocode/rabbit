using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

using Grasshopper.Kernel;
using Grasshopper.Kernel.Types;
using Grasshopper.Kernel.Data;
using Grasshopper.GUI;
using Grasshopper;

using Rabbit.Properties;
using Rabbit.Kernel.CellularAutomata;
using Rabbit.Kernel.CellularAutomata.Configuration;
using Rabbit.Kernel.CellularAutomata.Cells;
using Rabbit.Kernel.ABMS.Space.Grid;
using Rabbit.Kernel.Geometry.Util;
using Rabbit.GH.CellularAutomata;

using RMA.OpenNURBS;

namespace Rabbit.GH.CellularAutomata
{
    /**
     * GH Component that create a Random configuration for a set of states
     */ 
    public class RandomStateConfigComponent : Component_CABase
    {

        /**
         * Constructor
         */
        public RandomStateConfigComponent()
            : base("Random State configuration", "R State", "Random State configuration")
        {
        }

        protected override void RegisterInputParams(GH_Component.GH_InputParamManager inputManager)
        {
            inputManager.Register_GenericParam("Set of states", "S", "A Set of valid states for the current Cell Prototype.", GH_ParamAccess.list);//name, nick, description, defaul, isList
        }

        protected override void RegisterOutputParams(GH_OutputParamManager outputManager)
        {
            outputManager.Register_GenericParam("Random State Configuration", "R", "Random State Configuration");//name, nick, description            
        }


        protected override void AppendAdditionalComponentMenuItems(ToolStripDropDown iMenu)
        {
            ToolStripMenuItem item = GH_DocumentObject.Menu_AppendGenericMenuItem(iMenu, "New Random Configuration...", new EventHandler(this.Menu_NewRandomConfigClicked), null, null, true, false);
            item.Font = new Font(item.Font, FontStyle.Bold);
        }

        private void Menu_NewRandomConfigClicked(Object sender, EventArgs ea)
        {
            //update the solution
            base.ExpireSolution(true);
        }

        /**
         * Does the computation
         */ 
        protected override void SolveRabbitInstance(IGH_DataAccess DA)
        {
            List<CellState> states = new List<CellState>();

            //get all Goo states
            List<IGH_Goo> gooStates = new List<IGH_Goo>();
            DA.GetDataList(0, gooStates);
            foreach (IGH_Goo gooState in gooStates)
                states.Add(new GH_CellState(gooState)); 

            //creates the Ranom configuration
            RandomCAConfig randomConfig = new RandomCAConfig(states);
            
            //set the output parameters
            DA.SetData(0, randomConfig);
        }

        /**
         * The Guid of the component
         */ 
        public override Guid ComponentGuid
        {
            get {
                return new Guid("{9D2583DD-6CF5-497c-8C40-C9B330211697}"); 
            }
        }

        /**
         * Space/Config component
         */
        public override GH_Exposure Exposure
        {
            get
            {
                return GH_Exposure.tertiary;
            }
        }

        /**
         * The icon of the component
         */
        protected override Bitmap Internal_Icon_24x24
        {
            get
            {
                return Resources.Custom_24x24_RandomStateConfig;
            }
        }


    }
}
