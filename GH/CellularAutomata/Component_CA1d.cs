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
using Rabbit.Kernel.CellularAutomata.Impl.Elementary;
using Rabbit.Kernel.ABMS.Space.Grid;
using Rabbit.Kernel.Geometry.Util;
using Rabbit.GH.CellularAutomata;

using Rhino.Geometry;

namespace Rabbit.GH.CellularAutomata
{
    /**
     * GH Component that create a Cellular Automata object, based on specified number of rows and columns
     */ 
    public class Component_CA1d : Component_CABase
    {

        /**
         * Constructor
         */
        public Component_CA1d()
//            : base("Cellular Grid", "C Grid", "Creates a Cellular grid, based on a specified grid of points. A Cell is created for each point.")
            : base("1D Cellular automaton model", "1D CA", "Creates a 1D Cellular automaton model, based on a serie of points.", Component_CABase.CATEGORY_CA_MODEL)
        {
        }

        protected override void RegisterInputParams(GH_Component.GH_InputParamManager inputManager)
        {
            inputManager.Register_GenericParam("Cell prototype", "C", "Cell Prototype to be created for each point.", GH_ParamAccess.item);//name, nick, description, defaul, isList
            inputManager.Register_PointParam("Serie of Points", "P", "Serie of points.", GH_ParamAccess.list);//name, nick, description, defaul, isList
            //optional configuration, if null - each cell is in it's default state
            inputManager.Register_GenericParam("State Configuration", "SC", "State Configuration - specifies initial state for each cell in the serie.", GH_ParamAccess.list);//name, nick, description, defaul, isList
            Params.Input[2].Optional = true;

        }

        protected override void RegisterOutputParams(GH_OutputParamManager outputManager)
        {
            outputManager.Register_GenericParam("Cellular Automaton", "CA", "The Cellular automaton");//name, nick, description

        }


        /**
         * Does the computation
         */ 
        protected override void SolveRabbitInstance(IGH_DataAccess DA)
        {

            List<GH_Point> pointsList = new List<GH_Point>();
            DA.GetDataList(1, pointsList);

            int XDimension = pointsList.Count;

            IList<Point3d> genericPoints = GH_PointUtils.ConvertToOnPoints(pointsList);


            //Get the cell prototype------------------------------------------------------------
            GH_ObjectWrapper cellPrototypeWrapper = null;
            DA.GetData<GH_ObjectWrapper>(0, ref cellPrototypeWrapper);
            ICell cellPrototype = (ICell)cellPrototypeWrapper.Value;

            //----------------------------------------------------------------------------------
            List<GH_ObjectWrapper> stateConfigurations = new List<GH_ObjectWrapper>();
            DA.GetDataList(2, stateConfigurations);

            GH_ObjectWrapper stateConfigWrapper1;
            OnStateConfig stateConfig = null;
            foreach (GH_ObjectWrapper stateConfigWrapper in stateConfigurations)
            {
                if (stateConfigWrapper != null)// Custom configuration defined
                {
                    if (stateConfigWrapper.Value.GetType() == typeof(OnStateConfig))
                    {
                        stateConfig = (OnStateConfig)stateConfigWrapper.Value;
                        //get the user-defined points that define custom Cell State
                        IList<Point3d> userConfigurationGHPoints = stateConfig.GetPoints();
                        //get the real configuration points, by finding the points that are closer to the user defined configuration points
                        IList<Point3d> realConfigurationPoints = PointUtils.GetClosestPoints(userConfigurationGHPoints, genericPoints);
                        stateConfig.SetPoints(realConfigurationPoints);
                    }
                }
            }
            //----------------------------------------------------------------------------------

            //build the grid
            Grid1d<ICell> space1d = new Grid1d<ICell>(XDimension);

            CellState ghAliveState = new GH_CellState(new GH_Boolean(true));
            CellState ghDeadState = new GH_CellState(new GH_Boolean(false));
            //populate the grid
            for (int i = 0; i < XDimension; i++)
            {
                ICell cell = cellPrototype.Clone();
                cell.SetId(i);//very important as this identifies the cell
                if (stateConfig != null)
                {
                    foreach (Point3d configurationPoint in stateConfig.GetPoints())
                        if (pointsList[i].Value.X == configurationPoint.X && pointsList[i].Value.Y == configurationPoint.Y && pointsList[i].Value.Z == configurationPoint.Z)
                            cell.SetState(stateConfig.GetState());
                }

                cell.SetAttachedObject(pointsList[i]);
                space1d.Add(cell, i);
            }

            //build neighborhood
            for (int i = 0; i < XDimension; i++)
            {
                ElementaryCell cell = (ElementaryCell)space1d.GetObjectAt(i);
                //IList<ICell> neighbors = new List<ICell>(2);
                if (i > 0)
                    cell.SetLeftNeighbor((ElementaryCell)space1d.GetObjectAt(i-1));//neighbors.Add(cells[(i - 1)%XDimension]);
                else if (i==0)
                    cell.SetLeftNeighbor((ElementaryCell)space1d.GetObjectAt(XDimension-1));//neighbors.Add(cells[(i - 1)%XDimension]);

                if(i<XDimension-1)
                    cell.SetRightNeighbor((ElementaryCell)space1d.GetObjectAt(i + 1));//neighbors.Add(cells[(i + 1)%XDimension]);
                else if(i==XDimension-1)
                    cell.SetRightNeighbor((ElementaryCell)space1d.GetObjectAt(0));

            }


            CA ca = new CA(space1d);

            //set the output parameters
            DA.SetData(0, ca);

        }




        /**
         * The Guid of the component
         */ 
        public override Guid ComponentGuid
        {
            get {
                return new Guid("{9D2583DD-6CF5-497c-8C40-C9A394548397}"); 
            }
        }

        /**
         * Space/Config component
         */
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
                return Resources.Custom_24x24_1DCA;
            }
        }


    }
}
