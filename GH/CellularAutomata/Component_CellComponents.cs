using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

using RMA.OpenNURBS;

using Grasshopper.Kernel;
using Grasshopper.Kernel.Data;
using Grasshopper.Kernel.Types;
using Grasshopper.Kernel.Parameters;

using Rabbit.Properties;
using Rabbit.Kernel.CellularAutomata;
using Rabbit.Kernel.CellularAutomata.Cells;
using Rabbit.Kernel.CellularAutomata.Configuration;
using Rabbit.Kernel.Automata.FSM;

using Rabbit.GH.CellularAutomata;

namespace Rabbit.GH.CellularAutomata
{
    /**
     * GH Component that gets a Cell of Cellular Automata as an input and returns it's state and it's position within the 2D Grid
     * 
     * @author MORPHOCODE.COM
     * 
     */ 
    public class Component_CellComponents : Component_CABase
    {


        /**
         * Constructor
         */
        public Component_CellComponents()
            : base("Cell Components", "Cells", "Lists all cells in the Cellular Automaton with their states at a specific moment of time.")
        {
        }

   
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager inputManager)
        {
            inputManager.Register_GenericParam("Cellular Automaton Configuration", "C", "CA configuration.", GH_ParamAccess.item);//name, nick, description, defaul, isList
            //inputManager.Register_GenericParam("Cellular Automaton", "CA", "Cellular Automaton, containing the cells", GH_ParamAccess.item);//name, nick, description, defaul, isList
            //Param_GenericObject cellStateParam = new Param_GenericObject();
            //cellStateParam.Optional = true;
            //inputManager.RegisterParam(cellStateParam, "Filter State", "F", "Optional Filter parameter. If specified, only cells with the speciefied state are listed.", false);//name, nick, description, defaul, isList
            
            //the problem here is that a default value should be specified, but it is not allowed for GenericParams
            inputManager.Register_GenericParam("Filter State", "F", "Optional parameter that filters cells by state.", GH_ParamAccess.item);//name, nick, description, defaul, isList
            Params.Input[1].Optional = true;
            
        }

        protected override void RegisterOutputParams(GH_OutputParamManager OutputManager)
        {
            OutputManager.Register_GenericParam("Point Cell(s)", "P", "Points representing cells with a specific state");//name, nick, description
            OutputManager.Register_GenericParam("Cell State", "CS", "State of each cell in the list");//name, nick, description
            OutputManager.Register_IntegerParam("Time", "t", "The time associated with each state.");//name, nick, description
            
        }

        /**
         * Does the computation
         */ 
        protected override void SolveRabbitInstance(IGH_DataAccess DA)
        {
            

            //Get the INPUT
            //GH_ObjectWrapper CAWrapper = null;
            //DA.GetData<GH_ObjectWrapper>(1, ref CAWrapper);//param index, place holder           
            //CA CA = (CA)CAWrapper.Value;

            GH_ObjectWrapper CAConfigurationWrapper = null;             
            DA.GetData<GH_ObjectWrapper>(0, ref CAConfigurationWrapper);//param index, place holder           
            ICAConfig CAConfiguration = (ICAConfig)CAConfigurationWrapper.Value;

            int time = CAConfiguration.GetAssociatedTime();// CA.GetMemory().GetTime(CAConfiguration);
            IEnumerable<ICell> cells = null;
            
            
            //Filter the cells, if filter is specified
            IGH_Goo filterStateValue = null;
            DA.GetData<IGH_Goo>(1, ref filterStateValue);
            if (filterStateValue == null)//no filter specified
                cells = CAConfiguration.GetCells();//CA.GetGrid().GetObjects();
            else
            {
                //filter cells with the specified state
                CellState filterCS = new GH_CellState(filterStateValue);
                //CellState filterCS = new CellState(true);
                cells = filterCells(CAConfiguration, CAConfiguration.GetCells(), filterCS);
            }


            //OUTPUT
            ArrayList cellTimes = new ArrayList();
            ArrayList cellIndexes = new ArrayList();
            ArrayList cellStates = new ArrayList();

            foreach (ICell cell in cells)
            {
                cellTimes.Add(time);
                //cellTimes.Add(CAConfiguration.GetCellState(cell).Equals(filterCS));
                //cellTimes.Add(new GH_Boolean(true).Value.Equals(new GH_Boolean(true).Value));
                //cellTimes.Add(new GH_Colour(255,0,0,0).Value.Equals(new GH_Colour(255,0,0,0).Value));
                //cellIndexes.Add(cell.GetId());
                cellIndexes.Add(cell.GetAttachedObject());
                //the value of the state is a GH_Goo instance
                cellStates.Add(CAConfiguration.GetCellState(cell).GetValue());//.GetValue().GetType());
            }

            //set the output parameters
            DA.SetDataList(0, cellIndexes);
            DA.SetDataList(1, cellStates);
            DA.SetDataList(2, cellTimes);

        }

        
        private IList<ICell> filterCells(ICAConfig CAConfiguration, IEnumerable<ICell> cells, CellState filterCS)
        {
            //TODO: use an utility class
            IList<ICell> filteredCells = new List<ICell>();
            foreach (ICell cell in cells)
            {
                if (CAConfiguration.GetCellState(cell).Equals(filterCS))
                    filteredCells.Add(cell);
            }

            return filteredCells;
        }

        /**
         * Stats/decomposer component
         * 
         */
        public override GH_Exposure Exposure
        {
            get
            {
                return GH_Exposure.quinary;
            }
        }

        /**
         * The Guid of the component
         */ 
        public override Guid ComponentGuid
        {
            get {
                return new Guid("{9D2583DD-6CF5-497c-8C40-C9A290598319}"); 
            }
        }

        /**
         * The icon of the component
         */
        protected override Bitmap Internal_Icon_24x24
        {
            get
            {
                return Resources.Custom_24x24_Cells;
            }
        }

    }
}
