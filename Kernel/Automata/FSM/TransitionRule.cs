using System;
using System.Collections.Generic;
using System.Text;

namespace Rabbit.Kernel.Automata.FSM
{
    /**
     * Abstraction of a rule that is used by the Finite State Machine to change it's state.
     * 
     * I is a type parameter that specify what input is expected by this Finite State Machine
     */ 
    public interface TransitionRule<S> //where S:State
    {

        /**
         * <returns> The next state</returns>
         * The next state depends on the current state and on the input(symbol)
         */ 
        S SolveState(S currentState, Object input);
    }
}
