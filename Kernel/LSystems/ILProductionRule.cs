using System;
using System.Collections.Generic;
using System.Text;

namespace Rabbit.Kernel.LSystems
{
    /**
     * Context-Sensitive Rule that is being applied depending of the context in which the predecessor occurs
     * In order to keep specifications of L-systems short, the usual notion
        of IL-systems has been modified here by allowing productions with
        different context lengths to coexist within a single system. Furthermore,
        context-sensitive productions are assumed to have precedence
        over context-free productions with the same strict predecessor. Consequently,
        if a context-free and a context-sensitive production both apply
        to a given letter, the context-sensitive one should be selected. If no production
        applies, this letter is replaced by itself as previously assumed
        for OL-systems.
     * 
     */
    public class ILProductionRule:ProductionRule
    {
        private Word leftContext;

        private Word rightContext;

        public ILProductionRule(Symbol predecessor, Word successor, Word leftContext, Word rightContext):base(predecessor, successor)
        {
            this.leftContext = leftContext;
            this.rightContext = rightContext;
        }



        /**
         * Returns a result if the specified symbol matchs the left and right context of the Rule
         * 
         * 
         */
        public override Word Rewrite(Symbol symbol, int SymbolIndex, Word word)
        {
            //check if that rule could be applied for the specified symbol
            if (predecessor.Equals(symbol))
            {
                //TODO: use regex
                //string sPattern = "^\\d{3}-\\d{3}-\\d{4}$";
                //if(System.Text.RegularExpressions.Regex.IsMatch(s, sPattern)

                //check if the context matches
                String symbolLeft = word.ToString().Substring(0, SymbolIndex);
                String symbolRight = word.ToString().Substring(SymbolIndex, word.ToString().Length-1);
                if (symbolLeft.EndsWith(leftContext.ToString()) && symbolRight.StartsWith(rightContext.ToString()))
                    return successor;
                else
                    return null;//context do not matches
            }
            else
                //throw new System.InvalidOperationException("This rule is not meant to be used with symbol: " + Symbol);
                return null;
        }
    }
}
