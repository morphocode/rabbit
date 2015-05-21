using System;
using System.Collections.Generic;
using System.Text;

namespace Rabbit.Kernel.Language
{

    /**
     * Simple Lexer Implementation that gets a character sequence as an input and returns a list of tokens as an ouput
     * 
     * 
     * In computer science, lexical analysis is the process of converting a sequence of characters into a sequence of tokens. A program or function which performs lexical analysis is called a lexical analyzer, lexer or scanner. A lexer often exists as a single function which is called by a parser or another function.
     */
    public class SimpleLexer:ILexer
    {

        private char[] separators;

        public SimpleLexer(char[] separators)
        {
            this.separators = separators;
        }

        public ITokenStream Analyze(String input)
        {
            IList<Token> tokens = new List<Token>();
            /*
            String[] tokenStrings = input.Split(separators);
            foreach (String tokenString in tokenStrings)
            {
                tokens.Add(new Token(tokenString));
            }*/
            
            for (int i = 0; i < input.Length; i++)
            {
                StringBuilder tokenBuilder = new StringBuilder();
                char character = input[i];
                //if(char.IsDigit(input, i))
                tokenBuilder.Append(character);
                Token token = new Token(character.ToString());
                tokens.Add(token);
            }

            return new ListTokenStream(tokens);
        }

        /*
        private char NextDigit(String input, int currentCharIndex)
        {
            if(currentCharIndex>=input.Length) return null;
            char character = input[currentCharIndex + 1];
            if(character.IsDigit()) return character;
            else
                return null;
        }
         * 
         * */

    }
}
