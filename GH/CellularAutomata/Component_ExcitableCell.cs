using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;


using Grasshopper.Kernel;
using Grasshopper.Kernel.Types;
using Grasshopper.Kernel.Data;
using Grasshopper.GUI;
using Grasshopper;

using Rabbit.Properties;
using Rabbit.Kernel.CellularAutomata;
using Rabbit.Kernel.CellularAutomata.Cells;
using Rabbit.Kernel.CellularAutomata.Impl.Greenberg_Hastings;
using Rabbit.Kernel.Geometry.Util;
using Rabbit.GH.CellularAutomata;

using RMA.OpenNURBS;

namespace Rabbit.GH.CellularAutomata
{
    /**
     * GH Component that create a Cellular Automata object, based on specified number of rows and columns
     */
    public class ExcitableCellComponent : Component_CABase
    {

        /**
         * Constructor
         */
        public ExcitableCellComponent()
            : base("Excitable Cell", "E Cell", "Excitable cell: represents a fraction of the excitable medium. Each cell can be in one of the three following states: Resting, Excited, Refractory.", Component_CABase.CATEGORY_CA_MODEL)
        {
        }

        protected override void RegisterInputParams(GH_Component.GH_InputParamManager inputManager)
        {
            inputManager.Register_IntegerParam("Treshold", "T", "Treshold", GH_ParamAccess.item);
            inputManager.Register_GenericParam("Resting State(s)", "R", "Resting state(s)", GH_ParamAccess.item);
            inputManager.Register_GenericParam("Excited State", "E", "Excited state", GH_ParamAccess.list);
            inputManager.Register_GenericParam("Refractory State(s)", "R", "Refractory state(s)", GH_ParamAccess.list);
        }

        protected override void RegisterOutputParams(GH_OutputParamManager outputManager)
        {
            outputManager.Register_GenericParam("Excitable Cell Prototype", "EC", "Excitable cell prototype.");//name, nick, description            
        }


        /**
         * Does the computation
         */ 
        protected override void SolveRabbitInstance(IGH_DataAccess DA)
        {
            
            //Get the treshold
            int treshold = 0;
            DA.GetData<int>(0, ref treshold);

            //Get the resting colour
            IGH_Goo restingColour = null;
            DA.GetData<IGH_Goo>(1, ref restingColour);
            GH_CellState restingState = new GH_CellState(restingColour);

            //Get the excited states ---------------------------------------------
            List<IGH_Goo> excitedColours = new List<IGH_Goo>();
            DA.GetDataList(2, excitedColours);
            List<CellState> excitedStates = new List<CellState>();
            foreach (IGH_Goo colour in excitedColours)
                excitedStates.Add(new GH_CellState(colour));

            //Get the refractory states ------------------------------------------
            List<IGH_Goo> refractoryColours = new List<IGH_Goo>();
            DA.GetDataList(3, refractoryColours);//param index, place holder    
            List<CellState> refractoryStates = new List<CellState>();
            foreach (IGH_Goo colour in refractoryColours)
                refractoryStates.Add(new GH_CellState(colour));

            ExcitableCell prototype = new ExcitableCell(-1, restingState, refractoryStates, excitedStates, treshold);//, //EvolutionRules);

            //set the output parameters
            DA.SetData(0, prototype);
        }


        /**
         * Controller component
         */
        public override GH_Exposure Exposure
        {
            get
            {
                return GH_Exposure.secondary;
            }
        }

        /**
         * The Guid of the component
         */ 
        public override Guid ComponentGuid
        {
            get {
                return new Guid("{9D2583DD-6CF5-497c-8C40-C9A290318197}"); 
            }
        }

        /**
         * The icon of the component
         */
        protected override Bitmap Internal_Icon_24x24
        {
            get
            {
                return Resources.Custom_24x24_ExcitableMedia;
            }
        }


    }
}
