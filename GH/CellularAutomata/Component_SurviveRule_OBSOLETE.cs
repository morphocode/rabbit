using System;
using System.Collections.Generic;
using System.Text;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Types;
using System.Drawing;

using Rabbit.Properties;
using Rabbit.Kernel.CellularAutomata;
using Rabbit.Kernel.CellularAutomata.Impl.Life;


namespace Rabbit.GH.CellularAutomata
{
    /**
     * GH Component that creates an DieOnOverCrowdEvolutionRule
     */
    public class Component_SurviveRule_OBSOLETE : Component_CABase
    {
        
        /**
         * Constructor
         */
        public Component_SurviveRule_OBSOLETE()
            : base("Survive Rule [OBSOLETE]", "Survive", "A cell survives if surrounded by N neighbors in 'alive' state")
        {
        }

        protected override void RegisterInputParams(GH_Component.GH_InputParamManager inputManager)
        {
            inputManager.Register_IntegerParam("Neigbors", "N", "Number of neighbors in 'alive' state", 2, GH_ParamAccess.list);//name, nick, description, default, isList
        }

        protected override void RegisterOutputParams(GH_OutputParamManager pManager)
        {
            pManager.Register_GenericParam("Survive Rule", "R", "Survive rule");//name, nick, description
        }

        /**
         * Does the computation
         */ 
        protected override void SolveRabbitInstance(IGH_DataAccess DA)
        {
            List<int> livingNeighborsRequirements = new List<int>();

            //get the input parameters
            DA.GetDataList<int>(0, livingNeighborsRequirements);//param index, place holder

            //creates the Evolution Rule:
            SurviveRule SurviveRule = new SurviveRule(livingNeighborsRequirements);
            
            //set the output parameters
            DA.SetData(0, SurviveRule);            
        }

        /**
         * Interpretative part of the LSystems
         * secondary exposure
         */
        public override GH_Exposure Exposure
        {
            get
            {
                return GH_Exposure.hidden;
            }
        }

        /**
         * The Guid of the component
         */ 
        public override Guid ComponentGuid
        {
            get {
                return new Guid("{9D2583DD-6CF5-497c-8C40-C9A321898397}"); 
            }
        }

        /**
         * The icon of the component
         */
        protected override Bitmap Internal_Icon_24x24
        {
            get
            {
                return Resources.Custom_24x24_SurviveRule;
            }
        }


    }
}
