using System;
using System.Collections.Generic;
using System.Text;

using Grasshopper.Kernel;
using Grasshopper.Kernel.Types;

using Rabbit.Kernel.CellularAutomata;

namespace Rabbit.GH.CellularAutomata
{
    /**
     * CellState that wraps GH primitives.
     * Overrides Equals method by comparing the inner values of the Goo objects
     * 
     * 
     */ 
    public class GH_CellState:CellState 
    {
        //private GH_TypeHandler typeHandler;

        public GH_CellState(IGH_Goo goo):base(goo)
        {
         }

        public override Boolean Equals(Object that)
        {
            if (that == null) return false;
            if (that.GetType() != this.GetType()) return false;

            //FIXME: use a reflection to get the Value property of the GH_Goo
            //if a value property is missing, it seems that we could not compare Goos (or different method should be used)

            // GH_Goo.Equals(GH_Goo) compare the GUIDs which are different in the common case
            // that's why we need to compare GH_Goo inner values
            // unfortunately the Value property is not generic for GH_Goo
            // only some GH_Goo have value assigned
            GH_CellState thatState = (GH_CellState)that;

            if (value.GetType() == GH_TypeLib.t_gh_colour)
            {
                GH_Colour thisGH_Colour = (GH_Colour)value;
                GH_Colour thatGH_Colour = (GH_Colour)thatState.value;

                return thisGH_Colour.Value.Equals(thatGH_Colour.Value);
            }
            else if (value.GetType() == GH_TypeLib.t_gh_bool)
            {
                GH_Boolean thisGH_Boolean = (GH_Boolean)this.value;
                GH_Boolean thatGH_Boolean = (GH_Boolean)thatState.value;
                return thisGH_Boolean.Value.Equals(thatGH_Boolean.Value);
            }
            else if (value.GetType() == GH_TypeLib.t_gh_int)
            {
                GH_Integer thisGH_Integer = (GH_Integer)this.value;
                GH_Integer thatGH_Integer = (GH_Integer)thatState.value;
                return thisGH_Integer.Value.Equals(thatGH_Integer.Value);
            }
            else if (value.GetType() == GH_TypeLib.t_gh_string)
            {
                GH_String thisGH_String = (GH_String)this.value;
                GH_String thatGH_String = (GH_String)thatState.value;
                return thisGH_String.Value.Equals(thatGH_String.Value);
            }

            throw new NotSupportedException("Could not compare GH_Goo type: " + value.GetType());
        }

        public override int GetHashCode()
        {
            if (value.GetType() == GH_TypeLib.t_gh_colour)
                return ((GH_Colour)value).Value.GetHashCode();
            else if (value.GetType() == GH_TypeLib.t_gh_bool)
                return ((GH_Boolean)value).Value.GetHashCode();
            else if (value.GetType() == GH_TypeLib.t_gh_int)
                return ((GH_Integer)value).Value.GetHashCode();
            else if (value.GetType() == GH_TypeLib.t_gh_string)
                return ((GH_String)value).Value.GetHashCode();
            else
                throw new NotSupportedException("Could not get the hashCode of GH_Goo type: " + value.GetType());
        }
    }
}
