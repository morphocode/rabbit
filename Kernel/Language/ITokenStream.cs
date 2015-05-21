using System;
using System.Collections.Generic;
using System.Text;

namespace Rabbit.Kernel.Language
{
    /***
     * 
     * The beauty of the token stream concept is that parsers and lexers are not affected--they are merely consumers and producers of streams.  Stream objects are filters that produce, process, combine, or separate token streams for use by consumers.   Existing lexers and parsers may be combined in new and interesting ways without modification. 
     * http://www.antlr2.org/doc/streams.html
     * 
     * 
     * 
     */
    public interface ITokenStream
    {

        Token NextToken();
    }
}
