using System;
using System.Collections.Generic;
using System.Text;

namespace Rabbit.Kernel.Automata.FSM
{
    /**
     * A Discrete system could have an internal memory that stores the discrete states of the system and the time at which they occured.
     * 
     * 
     */
    public class InternalMemory<S> where S : State
    {
        //private Dictionary<int, S> timeToStateMap;
        //private Dictionary<S, int> stateToTimeMap;
        private IList<S> states;

        private int initialCapacity;

        public InternalMemory(int initialCapacity)
        {
            this.initialCapacity = initialCapacity;
            //timeToStateMap = new Dictionary<int, S>(initialCapacity);
            this.states = new List<S>(initialCapacity);

        }

        //public void Save(int time, S state) {
        public void Save(S state)
        {
            //timeToStateMap[time] = state;
            states.Add(state);

        }

        /**
         * Get the last state stored in the memory
         * 
         */
        public S GetLastState()
        {
            if (states.Count > 0)
                return states[states.Count-1];
            else
                return default(S);
        }

        public IList<S> GetAllStates()
        {
            return states;
        }

        /*
         * <returns> the State associated with the specified time </returns>
         *
        public S GetState(int discreteTime)
        {
            if (timeToStateMap.ContainsKey(discreteTime))
                return timeToStateMap[discreteTime];
            else
                return default(S);
        }
        */

        /*
         * returns the time moment associated with the specified state
         *
        public int GetTime(S discreteState)
        {
            //throw new NotImplementedException();
            if (timeToStateMap.ContainsValue(discreteState))
                foreach (KeyValuePair<int, S> pair in timeToStateMap)
                    if(pair.Value.Equals(discreteState)) //TODO: there could be multuple equal states!!! what about that??
                        return pair.Key;

            return -1;
        }
         * */

    }
}
