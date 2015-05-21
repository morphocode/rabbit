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
using Rabbit.Kernel.CellularAutomata.Impl.Life;
using Rabbit.Kernel.CellularAutomata.Impl.Elementary;
using Rabbit.Kernel.ABMS.Space.Grid;
using Rabbit.Kernel.Geometry.Util;
using Rabbit.GH.CellularAutomata;

using RMA.OpenNURBS;

namespace Rabbit.GH.CellularAutomata
{
    /**
     * GH Component that create a Cellular Automata object, based on specified number of rows and columns
     */
    public class Component_ElementaryCell : Component_CABase
    {

        private static CellState GH_ALIVE_STATE = new GH_CellState(new GH_Boolean(true));
        private static CellState GH_DEAD_STATE = new GH_CellState(new GH_Boolean(false));

        /**
         * Constructor
         */
        public Component_ElementaryCell()
            : base("Elementary Cell", "El. Cell", "Elementary Cells have two states: Dead = 0 or Alive = 1. At each iteration, the new state of the cell is determined according to it's current state and the states of the two nearest cells(left and right neighbour).", Component_CABase.CATEGORY_CA_MODEL)
        {
        }

        protected override void RegisterInputParams(GH_Component.GH_InputParamManager inputManager)
        {
            inputManager.Register_BooleanParam("111", "111", "New state for this configuration of states", GH_ParamAccess.item);
            inputManager.Register_BooleanParam("110", "110", "New state for this configuration of states", GH_ParamAccess.item);
            inputManager.Register_BooleanParam("101", "101", "New state for this configuration of states", GH_ParamAccess.item);
            inputManager.Register_BooleanParam("100", "100", "New state for this configuration of states", GH_ParamAccess.item);
            inputManager.Register_BooleanParam("011", "011", "New state for this configuration of states", GH_ParamAccess.item);
            inputManager.Register_BooleanParam("010", "010", "New state for this configuration of states", GH_ParamAccess.item);
            inputManager.Register_BooleanParam("001", "001", "New state for this configuration of states", GH_ParamAccess.item);
            inputManager.Register_BooleanParam("000", "000", "New state for this configuration of states", GH_ParamAccess.item);
        }

        protected override void RegisterOutputParams(GH_OutputParamManager outputManager)
        {
            outputManager.Register_GenericParam("Elementary Cell Prototype", "C", "Elementary cell prototype.");
        }


        /**
         * Does the computation
         */ 
        protected override void SolveRabbitInstance(IGH_DataAccess DA)
        {
            //Get the evolution rules of the cell
            Boolean rule111_state = false;
            DA.GetData<Boolean>(0, ref rule111_state);
            Boolean rule110_state = false;
            DA.GetData<Boolean>(1, ref rule110_state);
            Boolean rule101_state = false;
            DA.GetData<Boolean>(2, ref rule101_state);
            Boolean rule100_state = false;
            DA.GetData<Boolean>(3, ref rule100_state);
            Boolean rule011_state = false;
            DA.GetData<Boolean>(4, ref rule011_state);
            Boolean rule010_state = false;
            DA.GetData<Boolean>(5, ref rule010_state);
            Boolean rule001_state = false;
            DA.GetData<Boolean>(6, ref rule001_state);
            Boolean rule000_state = false;
            DA.GetData<Boolean>(7, ref rule000_state);

            ElementaryRule rule111 = new ElementaryRule(GH_ALIVE_STATE, GH_ALIVE_STATE, GH_ALIVE_STATE, (rule111_state)? GH_ALIVE_STATE : GH_DEAD_STATE);
            ElementaryRule rule110 = new ElementaryRule(GH_ALIVE_STATE, GH_ALIVE_STATE, GH_DEAD_STATE, (rule110_state) ? GH_ALIVE_STATE : GH_DEAD_STATE);
            ElementaryRule rule101 = new ElementaryRule(GH_ALIVE_STATE, GH_DEAD_STATE, GH_ALIVE_STATE, (rule101_state) ? GH_ALIVE_STATE : GH_DEAD_STATE);
            ElementaryRule rule100 = new ElementaryRule(GH_ALIVE_STATE, GH_DEAD_STATE, GH_DEAD_STATE, (rule100_state) ? GH_ALIVE_STATE : GH_DEAD_STATE);
            ElementaryRule rule011 = new ElementaryRule(GH_DEAD_STATE, GH_ALIVE_STATE, GH_ALIVE_STATE, (rule011_state) ? GH_ALIVE_STATE : GH_DEAD_STATE);
            ElementaryRule rule010 = new ElementaryRule(GH_DEAD_STATE, GH_ALIVE_STATE, GH_DEAD_STATE, (rule010_state) ? GH_ALIVE_STATE : GH_DEAD_STATE);
            ElementaryRule rule001 = new ElementaryRule(GH_DEAD_STATE, GH_DEAD_STATE, GH_ALIVE_STATE, (rule001_state) ? GH_ALIVE_STATE : GH_DEAD_STATE);
            ElementaryRule rule000 = new ElementaryRule(GH_DEAD_STATE, GH_DEAD_STATE, GH_DEAD_STATE, (rule000_state) ? GH_ALIVE_STATE : GH_DEAD_STATE);

            List<CellularRule> rules = new List<CellularRule>();
            rules.Add(rule111);
            rules.Add(rule110);
            rules.Add(rule101);
            rules.Add(rule100);
            rules.Add(rule011);
            rules.Add(rule010);
            rules.Add(rule001);
            rules.Add(rule000);



            ElementaryCell prototype = new ElementaryCell(-1, GH_ALIVE_STATE, GH_DEAD_STATE, rules);
            prototype.setIsPrototype(true);

            //set the output parameters
            DA.SetData(0, prototype);
        }



        /**
         * The Guid of the component
         */ 
        public override Guid ComponentGuid
        {
            get {
                return new Guid("{9D2583DD-6CF5-497c-8C40-C9A290338527}"); 
            }
        }

        public override GH_Exposure Exposure
        {
            get
            {
                return GH_Exposure.primary;
            }
        }

        /**
         * The icon of the component
         */
        protected override Bitmap Internal_Icon_24x24
        {
            get
            {
                return Resources.Custom_24x24_ElementaryCell;
            }
        }


    }
}
