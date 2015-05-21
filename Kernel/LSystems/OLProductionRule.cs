using System;
using System.Collections.Generic;
using System.Text;

namespace Rabbit.Kernel.LSystems
{
    /**
     * Context-Free production rule
     * 
     */
    public class OLProductionRule:ProductionRule
    {

        public OLProductionRule(Symbol predecessor, Word successor) : base(predecessor, successor) { }

        
        /**
         * Returns the successor string if the symbol is equal to the predecessor of the rule
         * returns null if rule could not be applied
         * 
         * 
         */ 
        public override Word Rewrite(Symbol symbol, int SymbolIndex, Word word)
        {
            //check if that rule could be applied for the specified symbol
            if (predecessor.Equals(symbol))
                return successor;
            else
                //throw new System.InvalidOperationException("This rule is not meant to be used with symbol: " + Symbol);
                return null;
        }

    }
}
