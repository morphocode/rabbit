using System;
using System.Collections.Generic;
using System.Text;

namespace Rabbit.Kernel.LSystems
{

    /**
     * 
     * A stochastic OL-system is an ordered quadruplet Gπ = V, ω, P, π.
        The alphabet V , the axiom ω and the set of productions P are defined
        as in an OL-system (page 4). Function π : P → (0, 1], called the
        probability distribution, maps the set of productions into the set of
        production probabilities. It is assumed that for any letter a ∈ V , the
        sum of probabilities of all productions with the predecessor a is equal
        to 1.
     * 
     * 
     */
    public class StochasticLSystem:AbstractLSystem 
    {

        private static Random RANDOM = new Random();

        protected Dictionary<ProductionRule, Double> RulesProbabilityDistributionLookUpMap;
        protected Dictionary<Symbol, IList<ProductionRule>> RulesLookUpMap;

        public StochasticLSystem(IList<Symbol> Alphabet, Word Axiom)
            : base(Alphabet, Axiom)
        {
            this.RulesProbabilityDistributionLookUpMap = new Dictionary<ProductionRule, Double>();
            this.RulesLookUpMap = new Dictionary<Symbol, IList<ProductionRule>>();
        }

        /**
         * Adds a RewriteRule with a Probability Distribution of 1.0
         * 
         */ 
        public override void AddRule(ProductionRule RewriteRule)
        {
            AddRewriteRule(RewriteRule, 1.0);
        }

        /**
         * Adds a RewriteRule with a certain Probability Distribution(should be between [0.0 - 1.0]). 
         * It is assumed that for any letter a ∈ V , the
        sum of probabilities of all productions with the predecessor a is equal
        to 1.
         * 
         */
        public void AddRewriteRule(ProductionRule RewriteRule, Double ProbabilityDistrubution)
        {
            base.AddRule(RewriteRule);
            //associate the rule to a given probability distribution
            RulesProbabilityDistributionLookUpMap.Add(RewriteRule, ProbabilityDistrubution);

            //Add the Rule to the List of Rules associated to that predecessor symbol:
            if(RulesLookUpMap.ContainsKey(RewriteRule.GetPredecessor())) {
                IList<ProductionRule> PredecessorRules = RulesLookUpMap[RewriteRule.GetPredecessor()];
                PredecessorRules.Add(RewriteRule);
            }
            else {//init the list if that's the first rule to be added for that symbol
                IList<ProductionRule> PredecessorRules = new List<ProductionRule>(1);
                PredecessorRules.Add(RewriteRule);
                //add to the lookup map:
                RulesLookUpMap.Add(RewriteRule.GetPredecessor(), PredecessorRules);
            }            
        }


        /**
         * @returns The RewriteRule associated with the specified Symbol(Predecessor) according to the Probability Distribution factor
         * The higher the Probability Distribution of a rule, the higher it is likely to be used
         * 
         */
        protected override ProductionRule GetProductionRule(Symbol Symbol, int SymbolIndex, Word Word)
        {
            //TODO: Get all rules for that symbol, order them by probability
            //Get a random number and choose one of them
            if (RulesLookUpMap.ContainsKey(Symbol))
            {
                IList<ProductionRule> PredecessorRules = RulesLookUpMap[Symbol];
                return GetMostProbableRewriteRule(PredecessorRules);               
            }
            else
                return null;
        }

        //TODO: find the most probable rule according to the probability distribution of each rule in the list 
        private ProductionRule GetMostProbableRewriteRule(IList<ProductionRule> RewriteRules)
        {
            SortedList<Double, ProductionRule> Probabilities = new SortedList<Double, ProductionRule>();
            foreach (ProductionRule RewriteRule in RewriteRules)
                Probabilities.Add(RulesProbabilityDistributionLookUpMap[RewriteRule], RewriteRule);


            double Random = RANDOM.NextDouble();            
            foreach (KeyValuePair<Double, ProductionRule> pair in Probabilities)
            {
                System.Console.WriteLine("Probability: " + pair+"; Random: "+Random);
                if (Random<pair.Key)
                    return pair.Value;
            }
                        
            return RewriteRules[0];
        }
    }
}
