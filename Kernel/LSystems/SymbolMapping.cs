using System;
using System.Collections.Generic;
using System.Text;

namespace Rabbit.Kernel.LSystems
{
    /**
     * Maps a symbol to a concrete meaning.
     * The meaning is some kind of an object. A symbol could mean an integer, a solig, a curve, etc.
     * For ex A=10, or A=Solid, A=Curve, A=TurtleCommand, etc.
     * 
     * 
     */ 
    public class SymbolMapping
    {
        //NOTE: for now juse use the simple SymbolMapper class
        //in the future The SymbolMapper could use SymbolMapping objects
        //for now just use a simple map

        private Symbol symbol;
        private Object meaning;

        public SymbolMapping(Symbol symbol, Object meaning)
        {
            this.symbol = symbol;
            this.meaning = meaning;
        }

        public Symbol GetSymbol()
        {
            return symbol;
        }

        public Object GetMeaning()
        {
            return meaning;
        }
    }
}
