using System;
using System.Collections.Generic;
using System.Text;

namespace Rabbit.Kernel.LSystems
{
    /**
     * 
     * A string rewriting system consists of an initial
        string, called the seed, and a set of rules for specifying
        how the symbols in a string are rewritten as
        (replaced by) strings.
     * 
     * 
     */
    public abstract class AbstractStringRewritingSystem
    {

        /**
         * When a string rewriting system is used, the
            rules are applied to the seed to produce a new
            string, the rules are again applied to this string to
            produce another, and so on, forming a sequence of
            generations, with the seed being considered generation
            0. The sequence of generations constitute the
            language for the rewriting system.
         * 
         */
        public abstract Word Rewrite();

        /**
         * The seed/axiom of the Rewriting system
         */
        public abstract Word GetSeed();

        /**
         * returns the last generation that was generated
         * 
         */
        public abstract Word GetCurrentDerivation();

        /**
         * Adds a rewrite rule to the system
         */ 
        //public abstract void AddRule(RewriteRule rule);

        /**
         * The sequence of generations constitute the
           language for the rewriting system.
         * 
         */
        public abstract IList<Word> GetLanguage();

        /**
         * returns the index of current generation.
         * Each rewriting of a string results in new generation
         * 
         */ 
        public abstract int GetCurrentDerivationIndex();


    }
}
