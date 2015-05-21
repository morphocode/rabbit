using System;
using System.Collections.Generic;
using System.Text;

namespace Rabbit.Kernel.RLogo
{
    /**
     * All elements of the elan language (e.g.,
     * that make up an abstract syntax tree) should
     * extend GrammarElement. Evaluating a language
     * construct with a context returns the value of
     * the evaluation and may have side-effects.
     */
    public abstract class GrammarElement
    {
        /**
         * Evaluate this language construct in some context. The
         * evaluation may have side-effects, e.g., moving a turtle.
         * @param is the context in which the evaluation takes place
         * @return the result of evaluating the construct
         */
        public abstract Object evaluate(Context c);

        //static protected Map ourMap = new TreeMap();
    }
}
