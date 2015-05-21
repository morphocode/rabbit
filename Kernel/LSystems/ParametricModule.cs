using System;
using System.Collections.Generic;
using System.Text;

namespace Rabbit.Kernel.LSystems
{
    /*
     * A parametric module is a symbol witch have some parameters attached.
     * There is a formal definition of the parametric module: for ex: M(t)
     * And real-value parametric module: M(10)
     * 
     * Parametric modules could have a list of parameters: M(x, y, z)
     * 
     * 
     */ 
    public class ParametricModule:Symbol
    {

        private String symbolString;
        private IList<LParameter> parameters;

        /**
         * Constructor
         * 
         */ 
        public ParametricModule(String symbolString, IList<LParameter> parameters):base(symbolString)
        {
            this.parameters = parameters;
        }



    }
}
