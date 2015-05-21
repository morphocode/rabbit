using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Rabbit.Kernel.Systems.DynamicalSystems
{
    /**
     * Memory that stores Timed states
     * 
     */ 
    public class TimedMemory<O>
    {

        //private LinkedList<TS> states;
        //private Stack<TS> states;
        //stores an object related to a specific time moment
        private Dictionary<int, O> memoryMap;
        private Dictionary<O, int> reverseMemoryMap;


        private O lastObject;
        //private int currentTime;

        public TimedMemory()
        {
            //states = new LinkedList<TS>();
            //states = new Stack<TS>();
            //currentTime = 0;
            this.memoryMap = new Dictionary<int, O>();
            this.reverseMemoryMap = new Dictionary< O, int>();
        }

        public void Save(int time, O obj )
        {
            //ts.SetTime(currentTime);
            //currentTime++;
            //states.AddLast(ts);
            //states.Push(ts);
            memoryMap.Add(time, obj);
            reverseMemoryMap.Add(obj, time);
            lastObject = obj;
        }

        public O GetObject(int time)
        {
            /*
            foreach (O obj in states)
                if (timedState.GetDiscreteTime() == time)
                    return timedState;

            //no State for that time
            return default(TS);
             */
            if(memoryMap.ContainsKey(time))
                return memoryMap[time];
            else
                return default(O);
        }

        public O GetLastState()
        {
            //return states.Last.Value;
            //return states.Pop();
            return lastObject;
        }

        //returns the time moment associated with this object
        public int GetTime(O obj)
        {
            return reverseMemoryMap[obj];
        }

        //returns all states from t=0 to time=t
        public ICollection<O> GetStates(int time)
        {
            List<O> states = new List<O>();
            foreach(KeyValuePair<int, O> pair in memoryMap)
                if(pair.Key<=time)
                    states.Add(pair.Value);
            //return memoryMap.Values;
            return states;
        }

        public ICollection<O> GetObjects()
        {
            //return states;
            return memoryMap.Values;
            //return states.ToArray();
        }

        public void Clear()
        {
            //states.Clear();
            //currentTime = 0;
            memoryMap.Clear();
        }

        //removes the last 'n' states
        public void Clear(int numberOfStates)
        {
            /*
            currentTime -= numberOfStates;

            for (int i = 0; i < numberOfStates; i++)
                //states.Pop();
                states.RemoveLast();
             */
            throw new NotImplementedException();
        }
    }
}

