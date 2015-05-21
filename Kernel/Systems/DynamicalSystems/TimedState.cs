using System;
using System.Collections.Generic;
using System.Text;

namespace Rabbit.Kernel.Systems.DynamicalSystems
{
    public interface TimedState
    {
        /*
         * returns the exact time at which this state was produced
         */ 
        int GetDiscreteTime();

        void SetTime(int time);
    }
}
