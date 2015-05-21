using System;
using System.Collections.Generic;
using System.Text;

namespace Rabbit.Kernel.LSystems
{
    /**
     * Parametric L-systems associate a vector of real-valued parameters with
        each symbol, collectively forming a parametric module. A module with symbol
        s ∈ V and parameters a1, a2, . . . , an ∈ R is written s(a1, a2, . . . , an). Strings of parametric modules form parametric words. It is important to differentiate
        the real-valued actual parameters of modules, from the formal parameters
        specified in productions. In practice, formal parameters are given unique3
        identifier names when specifying productions.
     * 
     * 
     * 
     */
    public class LParameter
    {
        private String identifier;

        public LParameter(String identifier)
        {
            this.identifier = identifier;
        }

    }
}
