using System;
using System.Collections.Generic;
using System.Text;

namespace Rabbit.Kernel.RLogo.Instructions.Expressions
{
    /**
     * the base class of all expressions,
     * the {@link #evaluate evaluate} method uses
     * the hook/template method
     * {@link #value value} to return a Double value,
     * the template method returns a double.
     */

    public abstract class Expression: Instruction
    {
        public Object evaluate(Context c)
        {
            return (Double)value(c);
        }
        
        public abstract double value(Context c);
    }

}
