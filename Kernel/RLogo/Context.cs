using System;
using System.Collections.Generic;
using System.Text;

namespace Rabbit.Kernel.RLogo
{
    public interface Context
    {
        /**
         * If there local/global variables a variable might
         * have a different value depending on the Context,
         * so Contexts should support determining the value
         * of an identifier (this method may be superflous).
         * @param name is the identifier being evaluated in this Context
         * @return the value of the identifier in this Context
         */
        //public double execute(BuiltInTurtleFunction b);

        //public void ask(Expression[] indices, StrictInstructionList sil);

        //public void tell(Expression[] indices);
    }
}
