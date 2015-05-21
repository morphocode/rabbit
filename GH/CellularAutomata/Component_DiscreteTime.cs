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

namespace Rabbit.GH.CellularAutomata
{
    /**
     * GH Component that gets a Cellular Automata as an input and makes it evolves. The Evolution changes the state of the CA.
     * 
     */ 
    public class Component_DiscreteTime : Component_CABase
    {
        
        //value, kept for each instance of the Component
        private int CurrentTime = 0;

        private bool lastResetValue = true;

        /**
         * Constructor
         */
        public Component_DiscreteTime()
            : base("Discrete Time", "Time", "Discrete time controls the evolution of a Cellular Automaton.")
        {
        }

        protected override void RegisterInputParams(GH_Component.GH_InputParamManager inputManager)
        {
            inputManager.Register_BooleanParam("Reset", "r", "Time reset.", false, GH_ParamAccess.item);
        }

        protected override void RegisterOutputParams(GH_OutputParamManager outputManager)
        {
            outputManager.Register_IntegerParam("Discrete time", "t", "Discrete time");//name, nick, description
        }


        /**
         * Does the computation
         */ 
        protected override void SolveRabbitInstance(IGH_DataAccess DA)
        {

            Boolean resetValue = false;
            DA.GetData<bool>(0, ref resetValue);

            if (lastResetValue != resetValue) //user clicked the toggle
            {
                this.CurrentTime = 0;
                this.lastResetValue = resetValue;
            } 
            else // default behavior: increment the time
                this.CurrentTime++;          

            //set the output parameters
            DA.SetData(0, this.CurrentTime);

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
                return new Guid("{9D2583DD-6CF5-497c-8C40-C9A290431398}"); 
            }
        }


        /**
         * The icon of the component
         */
        protected override Bitmap Internal_Icon_24x24
        {
            get
            {
                return Resources.Custom_24x24_Time;
            }
        }


    }
}
