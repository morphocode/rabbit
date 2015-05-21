using System;
using System.Collections.Generic;
using System.Text;


namespace Rabbit.Kernel.Automata.FSM
{
    /**
     * Contains pairs of (StateMachine, State)
     * Saves the states of a set of StateMachines
     * 
     * 
     */ 
    public interface IStateConfig<F, S> where S : State
                                          
    {

        /**
         * <returns> the state of the specified state machine </returns>
         * 
         */ 
        S GetCellState(F finiteStateMachine);

    }
}
