using System;
using System.Collections.Generic;
using System.Text;

using Rabbit.Kernel.Language;

namespace Rabbit.Kernel.LSystems
{
    /**
     * 
     * LSystem Interpreter interprets a specified LSystem and generates a LSystemLanguage described by that LSystem rewrite system( which is a variant of formal grammar)
     * 
     * 
     * 
     */ 
    public class LSystemParser:IParser
    {
        public static Char RuleDerivationChar = '=';


        public static LSystemParser Instance = new LSystemParser();

        private ILexer lexer;

        protected LSystemParser()
        {
            char[] separators = {};
            lexer = new SimpleLexer(separators);
        }

        /**
         * Parses a specified String source written in a specified format that describes the Grammar of the LSystem.
         * That means the axiom, the rules, etc..
         * 
         */ 
        public /*AbstractLSystem*/Object Parse(ITokenStream input)
        {
            //should returna an Abstract LSystem
            throw new NotImplementedException();
        }


        /**
         * Parses a ProductionRule object from the specified source string
         * 
         * 
         */ 
        public ProductionRule ParseRule(String source)
        {
            try
            {
                //DUMB implementation for now
                //Use a real language parser!!

                //the simplest case: OL Production Rule:
                //TODO: check for an exception

                String predecessorStr = source.Substring(0, source.IndexOf(RuleDerivationChar));
                Symbol predecessor = Symbol.valueOf(predecessorStr);
                int arrowIndex = source.IndexOf(RuleDerivationChar);
                int length = source.Length;


                String successorString = source.Substring(arrowIndex + 1, length - arrowIndex - 1);//index, length
                Word successor = ParseWord(successorString);
                //System.Console.WriteLine(source.IndexOf(ArrowToken));
                //System.Console.WriteLine(predecessorStr);
                //System.Console.WriteLine(successor);

                return new OLProductionRule(predecessor, successor);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Invalid production rule format: "+source+"! Please use the following format: PREDECESSOR=SUCCESSOR");
            }
        }
    
        public Word ParseWord(String wordSource)
        {
            Word word = new Word();

            //using the lexer to parse the character sequence into symbols            
            ITokenStream tokenStream = lexer.Analyze(wordSource);
            Token nextToken = null;
            while ((nextToken = tokenStream.NextToken()) != null)
            {
                word.Append(Symbol.valueOf(nextToken.ToString()));//in this case a token = a symbol
            }

            return word;
        }

    }
}
