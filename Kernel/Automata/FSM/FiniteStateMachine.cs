using System;
using System.Collections.Generic;
using System.Text;

namespace Rabbit.Kernel.Automata.FSM
{
    /**
     * The FiniteStateMachine is an abstract machine called also finite state automaton.
     * FSMs have a state and a finite number of possible states.
     * FSM could change it's state according to a set of Transition rules.
     * The FSM is a controller, which controls the set of rules and the current state.
     * 
     * 
     */ 
    public interface FiniteStateMachine<S, T> //It is a good idea to extend from State interface for custom States
                                              where T:TransitionRule<S>
    {


        /**
         * Calculates the next state of the FSM
         * according to the list of transition rules
         * 
         */
        S UpdateState();

        /**
         * The list of finite states of the FSM
         */ 
        IList<S> GetFiniteStates();

        /**
         * <returns> the current state of the FSM </returns>
         */ 
        S GetState();


        // TRANSITION RULES ------------------------------------------------
        IList<T> GetRules();


        //void AddRule(TransitionRule rule);

    }
}
