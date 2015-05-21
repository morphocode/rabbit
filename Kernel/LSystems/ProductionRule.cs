using System;
using System.Collections.Generic;
using System.Text;

namespace Rabbit.Kernel.LSystems
{
    /**
     * 
     * 

     *   The rules describe how to form strings from the language's alphabet that are valid according to the language's syntax. 
     *   A grammar does not describe the meaning of the strings or what can be done with them in whatever context — only their form.
     * 
     * 
     * 
     */
    public abstract class ProductionRule:RewriteRule
    {

        protected Symbol predecessor;

        protected Word successor;

        // implicit priority ot the rules
        // Context-Sensitive rules have higher priority then Context-Free rules by default
        // the priority is used to sort the rules
        public static int RulePriority = 0;

        //private double ProbabilityDistribution;


        public ProductionRule(Symbol predecessor, Word successor)
        {
            this.predecessor = predecessor;
            this.successor = successor;
        }

        public Symbol GetPredecessor()
        {
            return predecessor;
        }

        public Word GetSuccessor()
        {
            return successor;
        }

        /**
         * returns the successor string for the specified symbol, null if the rule could not be applied
         * 
         * 
         */
        public abstract Word Rewrite(Symbol symbol, int SymbolIndex, Word word);


        public override String ToString()
        {
            StringBuilder ruleDescription = new StringBuilder();
            ruleDescription.Append(predecessor.ToString()).Append(" = ").Append(successor.ToString());
            return ruleDescription.ToString();
        }

    }
}
