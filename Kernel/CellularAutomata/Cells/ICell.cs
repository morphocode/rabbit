using System;
using System.Collections.Generic;
using System.Text;

using Rabbit.Kernel.Automata.FSM;
using Rabbit.Kernel.CellularAutomata.Cells;

namespace Rabbit.Kernel.CellularAutomata
{
    /**
     * A cell in a Cellular Automata
     * 
     * 
     */
    public interface ICell : FiniteStateMachine<CellState, CellularRule>
    {

        /**
         * Identifier of the cell
         * Could be a position, or an index
         */
        Object GetId();

        void SetId(Object id);


        // ATTACHED OBJECT - TODO: better use a Wrapper but fix the problem with casting ---------------------------------------------------
        Object GetAttachedObject();

        void SetAttachedObject(Object obj);

        // STATE ---------------------------------------------------
        /**
         * <returns> the state of the cell </returns>
         */ 
        new CellState GetState();

        /**
         * <returns> all possible states of the cell </returns>
         */ 
        new IList<CellState> GetFiniteStates();

        /**
         * Sets the initial state of the cell
         * 
         */
        void SetState(CellState cellState);

        CellState GetDefaultState();



        // EVOLUTION RULES ---------------------------------------------------

        /**
         * <returns> The evolution rules used by the cell when changing state </returns>
         */
        new IList<CellularRule> GetRules();

        void SetTransitionRules(List<CellularRule> transitionRules);



        // CONNECTIONS/CLUSTERING ---------------------------------------------------

        /**
         * The neighbors of the cell
         * 
         */
        IList<ICell> GetNeighbors();

        /**
         * 
         * define all neighbors of the current cell
         */
        void SetNeighbors(IList<ICell> neighbors);

        //factory-method
        //Prototype design pattern
        ICell Clone();



    }
}
