using System;
using System.Collections.Generic;
using System.Text;

using Rabbit.Kernel.CellularAutomata.Cells;

namespace Rabbit.Kernel.CellularAutomata
{

    /**
     * CellState defines the state of a cell. 
     * Basically it is a wrapper around of some kind of primitive(Boolean, integer, etc.)
     * 
     * 
     * @author HTTP://MORPHOCODE.COM
     * 
     */ 
    public class CellState:ICellState
    {

        //maps a name to a CellState
        //private static Dictionary<String, CellState> cacheMap = new Dictionary<String, CellState>();

        protected Object value;

        /**
         * Constructor.
         * 
         */
        public CellState(Object value)
        {
            this.value = value;
        }


        public Object GetValue()
        {
            return value;
        }



        public override Boolean Equals(Object that)
        {
            if (that == null) return false;
            if (that.GetType() != this.GetType()) return false;

            CellState thatState = (CellState) that;

            return value.Equals(thatState.value);
        }

        
        public override string ToString()
        {
            return value.ToString();
        }


        /**
         * Uses a pool. Caches an already created cell state
         */
        /*
        public static CellState ValueOf(String nickName)
        {
            String name = nickName;
            if (nickName.Equals("D"))
                name = "DEAD";
            else if (nickName.Equals("A"))
                name = "ALIVE";
            
            if (cacheMap.ContainsKey(name))
                return cacheMap[name];
            else
            {
                CellState cellState = new CellState(name);
                cacheMap.Add(name, cellState);//save to cache for further retrieval
                return cellState;
            }
        }
         * */

    }
}
