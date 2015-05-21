using System;
using System.Collections.Generic;
using System.Text;

namespace Rabbit.Kernel.Language
{
    public class ListTokenStream: ITokenStream
    {
        private IList<Token> tokens;

        private IEnumerator<Token> enumerator;

        public ListTokenStream(IList<Token> tokens)
        {
            this.tokens = tokens;
            this.enumerator = tokens.GetEnumerator();
            enumerator.Reset();
        }

        public Token NextToken()
        {
            enumerator.MoveNext();
            return enumerator.Current;
        }



    }
}
