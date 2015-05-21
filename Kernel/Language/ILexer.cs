using System;
using System.Collections.Generic;
using System.Text;

namespace Rabbit.Kernel.Language
{

    /**
     * A Lexer(a.k.a.) Scanner separates a given input on tokens.
     * A lexer is part of the analysis of a language. 
     * The lexer separates the content on tokens which could be used by a Parser.
     * 
     * 
     */ 
    public interface ILexer
    {

        ITokenStream Analyze(String input);
    }
}
