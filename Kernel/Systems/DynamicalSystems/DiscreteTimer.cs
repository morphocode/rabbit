using System;
using System.Collections.Generic;
using System.Text;

namespace Rabbit.Kernel.Systems.DynamicalSystems
{
    public class DiscreteTimer
    {
        private int currentTime;

        public static DiscreteTimer Instance = new DiscreteTimer();

        //a delegate type for the event must be declared, if none is already declared
        public delegate void TickTackEventHandler(object sender, EventArgs e);

        //the event itself
        public event TickTackEventHandler TickTackEvent;

        private DiscreteTimer()
        {
            currentTime = 0;
        }

        public int GetTime()
        {
            return currentTime;
        }

        public void SetTime(int time)
        {
            currentTime = time;
            FireTickTackEvent(EventArgs.Empty);
        }

        public void TickTack()
        {
            currentTime++;
            FireTickTackEvent(EventArgs.Empty);
        }

        public void Reset()
        {
            currentTime = 0;
            FireTickTackEvent(EventArgs.Empty);
        }


        private void FireTickTackEvent(EventArgs e)
        {
            if (TickTackEvent != null)
                TickTackEvent(this, e);
        }

    }
}
