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
using Rabbit.Kernel.CellularAutomata.Cells;
using Rabbit.Kernel.ABMS.Space.Grid;
using Rabbit.Kernel.Geometry.Util;

using Rabbit.GH.CellularAutomata;


using RMA.OpenNURBS;

namespace Rabbit.GH.CellularAutomata
{
    /**
     * GH Component that create a Cellular Automata object, based on specified number of rows and columns
     */ 
    public class CustomStateConfigComponent : Component_CABase
    {

        /**
         * Constructor
         */
        public CustomStateConfigComponent()
            : base("Custom State configuration", "C State", "Custom State Configuration. Specifies custom state for a selection of cells/points")
        {
        }

        protected override void RegisterInputParams(GH_Component.GH_InputParamManager inputManager)
        {
            inputManager.Register_PointParam("Points", "P", "Selection of Points/cells with a custom initial state", GH_ParamAccess.list);//name, nick, description, defaul, isList
            inputManager.Register_GenericParam("Custom initial State", "S", "Custom initial State for the selected cells/points", GH_ParamAccess.item);//name, nick, description, defaul, isList
        }

        protected override void RegisterOutputParams(GH_OutputParamManager outputManager)
        {
            outputManager.Register_GenericParam("Custom State Configuration", "SC", "Custom State Configuration");//name, nick, description            
        }

        /**
         * Does the computation
         */ 
        protected override void SolveRabbitInstance(IGH_DataAccess DA)
        {


            //get the user-defined points that define custom Cell State
            List<GH_Point> userConfigurationGHPoints = new List<GH_Point>();
            DA.GetDataList(0, userConfigurationGHPoints);

            //Get the specified initial state:
            IGH_Goo gooCellStateValue = null;
            DA.GetData<IGH_Goo>(1, ref gooCellStateValue);
            //CellState initialCellState = new CellState(GH_TypeUtils.ConvertToSystemType(gooCellStateValue)); 
            CellState initialCellState = new GH_CellState(gooCellStateValue); 

            //creates the Cellular space
            OnStateConfig stateConfig = new OnStateConfig(GH_PointUtils.ConvertToOnPoints(userConfigurationGHPoints), initialCellState);
            
            //set the output parameters
            DA.SetData(0, stateConfig);
        }

        /**
         * The Guid of the component
         */ 
        public override Guid ComponentGuid
        {
            get {
                return new Guid("{9D2583DD-6CF5-497c-8C40-C9B310218397}"); 
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
                return Resources.Custom_24x24_CustomStateConfig;
            }
        }


    }
}
