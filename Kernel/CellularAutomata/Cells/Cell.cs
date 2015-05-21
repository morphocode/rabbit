using System.Collections.Generic;
using System;
using System.Text;

using Rabbit.Kernel.Automata.FSM;
using Rabbit.Kernel.ABMS.Space.Grid;
using Rabbit.Kernel.CellularAutomata.Cells;
using Rabbit.Kernel.CellularAutomata.Impl.Life;
using Rabbit.Kernel.Util;

namespace Rabbit.Kernel.CellularAutomata
{
    /**
     * A cell is a Finite State Machine. It has a state and a finite number of states.
     *
     * For ex. a cell could have 2 states: Dead or Alive
     * The cell changes it's state according to a set of Evolution rules.
     */
    public abstract class Cell : ICell
    {

        //current state of the cell
        protected CellState cellState;

        //finite number of possible states for this cell
        protected IList<CellState> finiteStates;

        //is the state of the cell initialized
        private Boolean isInitialized = false;

        /** the unique Id of the Cell */
        //protected double id;
        //FIXME: implement this as a GUID
        private int id;

        //private int _time;

        //rules that drive the change in state
        protected IList<CellularRule> transitionRules;

        // Networking
        private IList<ICell> neighbors;
        

        //Attached object
        private Object attachedObject;

        /**
         * Constructor.
         * Cell with a random state. Could be alive or dead
         */
        public Cell(int id, IList<CellState> finiteStates, IList<CellularRule> transitionRules)
        {
            this.id = id;
            this.finiteStates = finiteStates;
            this.transitionRules = transitionRules;
        }

  
        // PROTOTYPE --------------------------------------------------------------------------------------------
        public abstract ICell Clone();

        // ATTACHED Object --------------------------------------------------------------------------------------------
        public Object GetAttachedObject()
        {
            return attachedObject;
        }

        public void SetAttachedObject(Object obj)
        {
            this.attachedObject = obj;
        }

        // IDENTIFIER --------------------------------------------------------------------------------------------
        public Object GetId()
        {
            return id;
        }

        public void SetId(Object id)
        {
            this.id = (int)id;
        }

        // STATES --------------------------------------------------------------------------------------------
        public void SetState(CellState cellState)
        {
            //if (isInitialized) throw new InvalidOperationException("Cell was already initialized!");

            //check if this state is one of the finite states of this cell
            if(!finiteStates.Contains(cellState))
              throw new InvalidOperationException(string.Format("The specified state: {0} is invalid for this type of cell! Please use one of the following: {1}", cellState, PresentationUtils.ListToString(finiteStates)));
            //if it is valid, initialize the cell
            this.cellState = cellState;
            isInitialized = true;
        }

        /*
        public void SetState(CellState CellState)
        {
            this.cellState = CellState;
        }*/

        public CellState GetState()
        {
            return cellState;
        }

        public IList<CellState> GetFiniteStates()
        {
            return finiteStates;
        }

        public abstract CellState GetDefaultState();
 
        //NETWORKING -----------------------------------------------------------
        /**
         * @returns the list of neighbors of that cell 
         */
        public IList<ICell> GetNeighbors() {
            return neighbors;
        }

        //this is called by the cellular space when all cells are resolved
        public void SetNeighbors(IList<ICell> neighbors)
        {
            this.neighbors = neighbors;
        }


        /**
         * The Cellular automaton updates fires events on the cell. Each cell change it's state according to the transition rules.
         * Each cell mutates depending on its neighbors.
         * @return The new state of the cell
         * 
         */
        public abstract CellState UpdateState();


        // EVOLUTION RULES ---------------------------------------------------

        /**
         * <returns> The evolution rules used by the cell when changing state </returns>
         */
        public IList<CellularRule> GetRules()
        {
            return transitionRules;
        }

        public void SetTransitionRules(List<CellularRule> livingRules)
        {
            //IList<LivingRule> castedList = (IList<LivingRule>)livingRules;
            //this.livingRules = castedList;
            transitionRules = livingRules;
        }



        public override bool Equals(object obj)
        {

            if(obj==null || !obj.GetType().Equals(this.GetType()))
                return false;

            ICell thatCell = (ICell)obj;
            return thatCell.GetId().Equals(this.id);
        }

        public override int GetHashCode()
        {
            return id.GetHashCode();
        }

        public override String ToString()
        {
            return "[Cell: state=" + cellState + "]";
        }

    }//Cell

}
