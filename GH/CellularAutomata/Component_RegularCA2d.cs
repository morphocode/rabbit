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
using Rabbit.Kernel.ABMS.Space.Grid;
using Rabbit.Kernel.Geometry.Util;
using Rabbit.GH.CellularAutomata;

//using RMA.OpenNURBS;
using Rhino.Geometry;

namespace Rabbit.GH.CellularAutomata
{
    /**
     * GH Component that create a Cellular Automata object, based on specified number of rows and columns
     */ 
    public class Component_RegularCA2d : Component_CABase
    {

        /**
         * Constructor
         */
        public Component_RegularCA2d()
            : base("2D Regular Cellular automaton model", "2D CA", "Creates a 2D Cellular automaton model, based on a regular grid of points.", Component_CABase.CATEGORY_CA_MODEL)
        {
        }

        protected override void RegisterInputParams(GH_Component.GH_InputParamManager inputManager)
        {
            inputManager.Register_GenericParam("Cell prototype", "C", "Cell Prototype, populated on the grid", GH_ParamAccess.item);//name, nick, description, defaul, isList
            inputManager.Register_PointParam("Grid of Points", "P", "Grid of points. A Cell is created for each point.", GH_ParamAccess.list);//name, nick, description, defaul, isList
            //optional configuration, if null - each cell is in it's default state
            inputManager.Register_GenericParam("State Configuration", "SC", "State Configuration - specifies initial state for each cell of the grid", GH_ParamAccess.list);//name, nick, description, defaul, isList
            Params.Input[2].Optional = true;

        }

        protected override void RegisterOutputParams(GH_OutputParamManager outputManager)
        {
            //outputManager.Register_GenericParam("Cellular Grid", "S", "Cellular Space based on a grid");//name, nick, description            
            outputManager.Register_GenericParam("Cellular Automaton", "CA", "The Cellular automaton");//name, nick, description

        }


        /**
         * Does the computation
         */ 
        protected override void SolveRabbitInstance(IGH_DataAccess DA)
        {

                //Get the Structure of the Data of points, so that the underlying grid logic could be retrieved -------
                DA.DisableGapLogic();
                if (DA.Iteration > 0)
                {
                    return;
                }

                //Get the points as GH Structure, because the dimensions of the space are based on the tree's structure:
                GH_Structure<GH_Point> ghPointsTree = (GH_Structure<GH_Point>)this.Params.Input[1].VolatileData;
                //get all points as a flat list:
                List<GH_Point> ghPointsList = ghPointsTree.FlattenData();

                //expects a 2D grid:
                int XDimension = ghPointsTree.Paths.Count;
                int YDimension = ghPointsList.Count / XDimension;

           
                IList<Point3d> latticePoints = GH_PointUtils.ConvertToOnPoints(ghPointsList);



                //Get the cell prototype---------------------------------------------------------------------------------- 
                GH_ObjectWrapper cellPrototypeWrapper = null;
                DA.GetData<GH_ObjectWrapper>(0, ref cellPrototypeWrapper);
                ICell cellPrototype = (ICell)cellPrototypeWrapper.Value;

                //Get the State configurations-----------------------------------------------------------------------------
                OnCellularGridBuilder gridBuilder = null;

                List<GH_ObjectWrapper> stateConfigurations = new List<GH_ObjectWrapper>();
                DA.GetDataList(2, stateConfigurations);

                foreach(GH_ObjectWrapper stateConfigWrapper in stateConfigurations) {
                    if (stateConfigWrapper != null)// Custom configuration defined
                    {
                        if (stateConfigWrapper.Value.GetType() == typeof(OnStateConfig))
                        {
                            OnStateConfig stateConfig = (OnStateConfig)stateConfigWrapper.Value;
                            //get the user-defined points that define custom Cell State
                            IList<Point3d> userConfigurationGHPoints = stateConfig.GetPoints();
                            //get the real configuration points, by finding the points that are closer to the user defined configuration points
                            IList<Point3d> realConfigurationPoints = PointUtils.GetClosestPoints(userConfigurationGHPoints, latticePoints);
                            stateConfig.SetPoints(realConfigurationPoints);
                            gridBuilder = new OnCellularGridBuilder(cellPrototype, latticePoints, XDimension, YDimension, stateConfig);
                        }
                        else if (stateConfigWrapper.Value.GetType() == typeof(RandomCAConfig))
                        {
                            RandomCAConfig stateConfig = (RandomCAConfig)stateConfigWrapper.Value;
                            gridBuilder = new OnCellularGridBuilder(cellPrototype, latticePoints, XDimension, YDimension, stateConfig);
                        }
                        else
                        {
                            AddRuntimeMessage(GH_RuntimeMessageLevel.Error, "Not supported configuration type!");
                        }
                    }
                }
                if (stateConfigurations.Count == 0) // no configuration defined
                    gridBuilder = new OnCellularGridBuilder(cellPrototype, latticePoints, XDimension, YDimension);//, new RandomCAConfig(cellPrototype.GetFiniteStates()));

                //build the grid
                Grid2d<ICell> grid2d = gridBuilder.BuildCellularGrid();
                CA ca = new CA(grid2d);

                //set the output parameters
                DA.SetData(0, ca);

        }



        /**
         * The Guid of the component
         */ 
        public override Guid ComponentGuid
        {
            get {
                return new Guid("{9D2583DD-6CF5-497c-8C40-C9A290218397}"); 
            }
        }

        /**
         * Space/Config component
         */
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
                return Resources.Custom_24x24_CellularGrid;
            }
        }


    }
}
