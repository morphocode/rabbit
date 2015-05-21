using System.Collections.Generic;
using System.Collections;
using System;


using Rabbit.Kernel.Systems.DynamicalSystems;
using Rabbit.Kernel.ABMS.Space.Grid;
using Rabbit.Kernel.ABMS.Space.Projection;
using Rabbit.Kernel.CellularAutomata.Configuration;
using Rabbit.Kernel.CellularAutomata.Impl.Life;

/**
 * AUTHOR: MORPHOCODE.COM
 * Licence: Creative commons
 */
namespace Rabbit.Kernel.CellularAutomata
{
    /**
     *  A CellularAutomata is a computational model that puts together a finite number of cells within a n-th dimensional space.
     *  The Cells are connected with each other. Could be connected locally(in a neighborhood).
     *  Each Cell could interract with the cells that it is connected to.
     *  Each cell has some specific behavior and could change it's own state depending on the interactions.
     *  
     *  The Cellular Automaton is a dynamical system i.e. it depends on time(time series). The states of the cells are function of time.
     *  
     * @author http://MORPHOCODE.COM
     */ 
    public class CA : DynamicalSystem   {

        //EVENTS -----------------------------------------------------------------------------
        // Change Events fired when the CA is changed
        //a delegate type for the event must be declared, if none is already declared
        public delegate void ChangedEventHandler(object sender, EventArgs e);

        //the event itself
        public event ChangedEventHandler ChangeEvent;


        //Cellular space
        private IProjection<ICell> grid;

        //list of all states calculated by the Cellular Automaton
        private TimedMemory<ICAConfig> _memory;

        private ICAConfig currentConfiguration;

        private int currentTime = 0;//TODO: get that from the timer!!


        /**
         * Constructor. Creates a cellular automata
         */
        public CA(IProjection<ICell> grid)
        {

            this.grid = grid;
            this._memory = new TimedMemory<ICAConfig>();
            ICAConfig initialConfig = BuildConfiguration(grid);
            _memory.Save(0, initialConfig);
            this.currentConfiguration = initialConfig;

            this.grid = grid;

            //listen for events on the global clock:
            DiscreteTimer.Instance.TickTackEvent += new DiscreteTimer.TickTackEventHandler(OnTickTack);
        }

        private ICAConfig BuildConfiguration(IProjection<ICell> grid)
        {
            CAConfig configuration = new CAConfig(_memory.GetObjects().Count, grid.GetObjects()[0].GetDefaultState());
            foreach (ICell cell in grid.GetObjects())
                configuration.AddCellState(cell, cell.GetState());

            return configuration;
        }

        
        private void OnTickTack(Object sender, EventArgs args)
        {
            currentConfiguration = Update(DiscreteTimer.Instance.GetTime());
            //notify all listeners that the CA has changed
            FireEvent(EventArgs.Empty);
        }


        /**
         * @returns The World in which the cells are positioned
         */ 
        public IProjection<ICell> GetGrid()
        {
            return grid;
        }

        public ICAConfig GetCurrentConfiguration()
        {
            return currentConfiguration;
        }

        private void FireEvent(EventArgs e)
        {
            if (ChangeEvent != null)
                ChangeEvent(this, e);
        }


        public TimedMemory<ICAConfig> GetMemory()
        {
            return _memory;
        }

        /**
         * <returns> the CA configuration for t=discreteTime
         * 
         */ 
        public ICAConfig Update(int discreteTime)
        {

            ICAConfig configuration = _memory.GetObject(discreteTime);
            int timeGap = discreteTime - _memory.GetObjects().Count;

            if (configuration == null)
            {
                for (int t = 0; t <= timeGap; t++)
                {
                    configuration = Update();
                    _memory.Save(currentTime, configuration);
                }
                return configuration;
            }
            else
            {
                return configuration;//_memory.GetState(discreteTime);
            }
        }

        /**
         * The CA evolves to a new state, according to the behavior of each cell.
         * The old generations is archieved in the list of generations
         */ 
        public ICAConfig Update() {
            currentTime++;
            CAConfig nextConfiguration = new CAConfig(_memory.GetObjects().Count, grid.GetObjects()[0].GetDefaultState());
            foreach (ICell cell in grid.GetObjects())
            {
                //do not alter the cells' states directly
                //as the states affect the evolution
                //the result of the evolution is saved
                //and the cells are updated when all cells evolved
                CellState evolvedCellState = cell.UpdateState();
                nextConfiguration.AddCellState(cell, evolvedCellState);
            }
            //update the cells's states according to the evolution
            foreach (ICell cell in grid.GetObjects())
                cell.SetState(nextConfiguration.GetCellState(cell));

            return nextConfiguration;
        }

        public override String ToString()
        {
            return "Cellular Automata evolved " + DiscreteTimer.Instance.GetTime() + " times, based on "+grid.ToString() ;
        }

    }
}
