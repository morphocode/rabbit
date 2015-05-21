using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

using Grasshopper.Kernel;
using Grasshopper.Kernel.Data;
using Grasshopper.Kernel.Types;

using Rabbit.Properties;
using Rabbit.Kernel.CellularAutomata;
using Rabbit.Kernel.CellularAutomata.Configuration;
using Rabbit.Kernel.Systems.DynamicalSystems;


namespace Rabbit.GH.CellularAutomata
{
    /**
     * GH Component that gets a Cellular Automata as an input and makes it evolves. The Evolution changes the state of the CA.
     * 
     */ 
    public class Component_CAEvolver : Component_CABase
    {
        
        /**
         * Constructor
         */
        public Component_CAEvolver()
            : base("CA Evolver", "CA Evolver", "Drives the evolution of the cellular automaton according to the specified time. ")
        {
        }

        protected override void RegisterInputParams(GH_Component.GH_InputParamManager inputManager)
        {
            inputManager.Register_IntegerParam("Discrete time", "t", "Discrete Time.", 0, GH_ParamAccess.item);
            inputManager.Register_GenericParam("Cellular Automata", "CA", "Cellular Automaton.", GH_ParamAccess.item);//name, nick, description, defaul, isList            
        }

        protected override void RegisterOutputParams(GH_OutputParamManager outputManager)
        {
            outputManager.Register_GenericParam("CA Configuration", "C", "The last CA configuration calculated for time=t");//name, nick, description
            outputManager.Register_GenericParam("Memory", "M", "Memory, containg a list of all CA configurations");//name, nick, description
        }


        /**
         * Does the computation
         */ 
        protected override void SolveRabbitInstance(IGH_DataAccess DA)
        {
            GH_ObjectWrapper CAWrapper = null;             
            //get the input parameters
            DA.GetData<GH_ObjectWrapper>(1, ref CAWrapper);//param index, place holder           

            //unwrap the CA object
            CA CA = (CA) CAWrapper.Value;

            //get the time
            int time = 0;
            DA.GetData<int>(0, ref time);

            DiscreteTimer.Instance.SetTime(time);

            ICAConfig configuration = CA.GetCurrentConfiguration();//CalculateConfiguration(time); 

            //set the output parameters
            DA.SetData(0, configuration);
            DA.SetDataList(1, CA.GetMemory().GetStates(DiscreteTimer.Instance.GetTime()));
            //DA.SetData(2, CA);

        }

        /**
         * Controller component
         */ 
        public override GH_Exposure Exposure
        {
            get
            {
                return GH_Exposure.quarternary;
            }
        }

        /**
         * The Guid of the component
         */ 
        public override Guid ComponentGuid
        {
            get {
                return new Guid("{9D2583DD-6CF5-497c-8C40-C9A290598398}"); 
            }
        }


        /**
         * The icon of the component
         */
        protected override Bitmap Internal_Icon_24x24
        {
            get
            {
                return Resources.Custom_24x24_Evolve;
            }
        }


    }
}
