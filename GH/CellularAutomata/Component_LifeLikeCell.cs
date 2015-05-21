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
using Rabbit.Kernel.ABMS.Space.Grid;
using Rabbit.Kernel.Geometry.Util;
using Rabbit.GH.CellularAutomata;

using RMA.OpenNURBS;

namespace Rabbit.GH.CellularAutomata
{
    /**
     * GH Component that create a Cellular Automata object, based on specified number of rows and columns
     */
    public class Component_LifeLikeCell : Component_CABase
    {

        /**
         * Constructor
         */
        public Component_LifeLikeCell()
            : base("Life-Like Cell", "Life Cell", "Life-Like Cell Prototype. Each cell can be in one of the two possible states: Dead=0 or Alive=1. At each iteration, the new state of the cell is determinced according to a set of Survive / Born rules.", Component_CABase.CATEGORY_CA_MODEL)
        {
        }

        protected override void RegisterInputParams(GH_Component.GH_InputParamManager inputManager)
        {
            //inputManager.Register_GenericParam("Rules", "R", "Evolution rules that drive the behavior of a living cell. According to the rules a cell will either die or survive.", GH_ParamAccess.list);
            inputManager.Register_IntegerParam("Born Rules (Number of neigbors)", "B", "Born Rule(s). A new cell is born if surrounded by n 'alive' neighbors", GH_ParamAccess.list);//name, nick, description, default, isList
            Params.Input[0].Optional = true;
            inputManager.Register_IntegerParam("Survive Rules (Number of neighbors)", "S", "Survive Rule(s). A cell survives if surrounded by n 'alive' neighbors", GH_ParamAccess.list);//name, nick, description, default, isList
            Params.Input[1].Optional = true;
        }

        protected override void RegisterOutputParams(GH_OutputParamManager outputManager)
        {
            outputManager.Register_GenericParam("Living Cell Prototype", "LC", "Living cell prototype, to be populated on a grid");
        }


        /**
         * Does the computation
         */ 
        protected override void SolveRabbitInstance(IGH_DataAccess DA)
        {
            /*
            //Get the evolution rules of the cell
            List<GH_ObjectWrapper> EvolutionRulesWrapper = new List<GH_ObjectWrapper>();
            DA.GetDataList(0, EvolutionRulesWrapper);//param index, place holder    

            //init the list of evolution rules:
            List<CellularRule> EvolutionRules = new List<CellularRule>();
            foreach (GH_ObjectWrapper EvolutionRuleWrapper in EvolutionRulesWrapper)
                EvolutionRules.Add((CellularRule)EvolutionRuleWrapper.Value);
            */

            //get the inputs
            List<int> bornRuleNeighbors = new List<int>();
            DA.GetDataList<int>(0, bornRuleNeighbors);//param index, place holder
            List<int> survuveRuleNeighbors = new List<int>();
            DA.GetDataList<int>(1, survuveRuleNeighbors);//param index, place holder

            if (bornRuleNeighbors.Count == 0 && survuveRuleNeighbors.Count == 0) {
                this.AddRuntimeMessage(GH_RuntimeMessageLevel.Warning, "Unsufficient evolution rules: At least one rule should be defined.");
                return;
            }
            //----------------------------------------------------------------------
            //init the list of evolution rules:
            List<CellularRule> EvolutionRules = new List<CellularRule>();

            //init the Born Rules(if any)
            if (bornRuleNeighbors.Count > 0)
            {
                //creates the Evolution Rule:
                BornRule bornRule = new BornRule(bornRuleNeighbors);
                EvolutionRules.Add(bornRule);
            }

            //Init the Survive Rules(if any)
            if (survuveRuleNeighbors.Count > 0)
            {
                //creates the Evolution Rule:
                SurviveRule surviveRule = new SurviveRule(survuveRuleNeighbors);
                EvolutionRules.Add(surviveRule);
            }

            //Create the Life-Like Cell Prototype
            CellState ghAliveState = new GH_CellState(new GH_Boolean(true));
            CellState ghDeadState = new GH_CellState(new GH_Boolean(false));
            LifeLikeCell prototype = new LifeLikeCell(-1, ghAliveState, ghDeadState, EvolutionRules);

            //set the output parameters
            DA.SetData(0, prototype);
        }



        /**
         * The Guid of the component
         */ 
        public override Guid ComponentGuid
        {
            get {
                return new Guid("{9D2583DD-6CF5-497c-8C40-C9A290248197}"); 
            }
        }

        public override GH_Exposure Exposure
        {
            get
            {
                return GH_Exposure.secondary;
            }
        }

        /**
         * The icon of the component
         */
        protected override Bitmap Internal_Icon_24x24
        {
            get
            {
                return Resources.Custom_24x24_LivingCell;
            }
        }


    }
}
