using System;
using System.Collections.Generic;
using System.Text;

namespace Rabbit.Kernel.Language
{
    public interface IParser
    {

        /**
         * Parses a stream of tokens into an Abstract Syntax Tree
         * 
         * 
         */ 
        Object Parse(ITokenStream tokenStream);
    }
}
