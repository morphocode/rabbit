using System;
using System.Collections.Generic;
using System.Text;

namespace Rabbit.Kernel.LSystems
{
    /**
     * the simplest class of L-systems, those which are
     * deterministic and context-free, called D0L-systems.
     * If there is exactly one production for each symbol, then the L-system is said to be deterministic (a deterministic context-free L-system is popularly called a D0L-system). 
     * 
     * 
     */
    public class DeterministicLSystem:AbstractLSystem
    {

        protected Dictionary<Symbol, ProductionRule> RulesLookUpMap;

        public DeterministicLSystem(IList<Symbol> Alphabet, Word Axiom)
            : base(Alphabet, Axiom)
        {
            this.RulesLookUpMap = new Dictionary<Symbol, ProductionRule>();
        }


        /**
         * do not allow more than 1 rule for a given symbol
         * Deterministic L-Systems allow only one production rule for each symbol
         * Deterministic LSystems do not allow setting different Probability Distributions for the Rewrite rules
         */
        public override void AddRule(ProductionRule RewriteRule)
        {
            if(RulesLookUpMap.ContainsKey(RewriteRule.GetPredecessor()))
                throw new InvalidProgramException("Could not add RewriteRule for Symbol: '" + RewriteRule.GetPredecessor()+"', because there is already a rule added for that symbol. Deterministic L-Systems have only one production rule per symbol by definition!");

            //Add the rule to the list of rules
            base.AddRule(RewriteRule);

            this.RulesLookUpMap.Add(RewriteRule.GetPredecessor(), RewriteRule);
        }

        /**
         * @returns The RewriteRule associated with the specified Symbol(Predecessor)
         * 
         */
        protected override ProductionRule GetProductionRule(Symbol symbol, int symbolIndex, Word word) 
        {
            if (RulesLookUpMap.ContainsKey(symbol))
                return RulesLookUpMap[symbol];
            else
                return null;
        }

        /**
         * 
         */ 
        public override String ToString()
        {
            return base.ToString();
        }

    }
}
