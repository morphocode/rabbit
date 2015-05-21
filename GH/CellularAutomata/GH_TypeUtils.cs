using System;
using System.Collections.Generic;
using System.Text;

using RMA.OpenNURBS;
using Grasshopper.Kernel.Types;
using Grasshopper.Kernel;

namespace Rabbit.GH.CellularAutomata
{
    /**
     * Utility class that provides some common functionality related to GH to ON points converting
     * 
     */ 
    public class GH_TypeUtils
    {

        private GH_TypeUtils() { }


        /**
         *  Goo.CastTo<> supports cross-type castings i.e. Integers could be cast to Strings. We need strong typing.
         *  GH_integer -> integer
         *  GH_boolean -> boolean
         * 
         * returns the System primitive wrapped by the GH_Goo object
         * returns a Boolean out of GH_Boolean
         * returns an Integer out of GH_Integer
         * 
         */
        public static Object ConvertToSystemType(GH_Goo<Object> goo)
        {
            if (goo.GetType() == GH_TypeLib.t_gh_bool)//GH_BooleanPrototype.GetType())//check if it is a boolean
            {
                Boolean result = false;
                goo.CastTo<Boolean>(ref result);
                return result;
            }
            else if (goo.GetType() == GH_TypeLib.t_gh_int)
            {
                int result = -1;
                goo.CastTo<int>(ref result);
                return result;
            }
            else if (goo.GetType() == GH_TypeLib.t_gh_string)
            {
                String result = null;
                goo.CastTo<String>(ref result);
                return result;
            }
            else if (goo.GetType() == GH_TypeLib.t_gh_objwrapper)
            {
                GH_ObjectWrapper result = null;
                goo.CastTo<GH_ObjectWrapper>(ref result);
                return result.Value;
            }
            throw new NotSupportedException("Could not get the System object for Goo of type" + goo.GetType());
        }

    }
}
