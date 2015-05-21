using System;
using System.Collections.Generic;
using System.Text;

namespace Rabbit.Kernel.LSystems
{
    /**
     * A Symbol is a base concept in the LSystems theory.
     * It is the base part that compose the words of the LSystem's language.
     * (Symbol -> Word -> Language)
     * It is the smallest discrete part that defines the grammar of the LSystem.
     * The predecessor in the production rules is a LSymbol
     * 
     *  
     */
    public class Symbol
    {
        private String stringRepresentation;

        public Symbol(String stringRepresentation)
        {
            this.stringRepresentation = stringRepresentation;
        }

        public Symbol(Char symbolChar) {
            stringRepresentation = symbolChar.ToString();
        }

        public override Boolean Equals(Object aSymbol)
        {
            return this.stringRepresentation.Equals(aSymbol.ToString());
        }

        public override int GetHashCode() {
            return stringRepresentation.GetHashCode();
        }


        public override String ToString()
        {
            return stringRepresentation;
        }


        public static Symbol valueOf(Char symbolChar)
        {
            return new Symbol(symbolChar);
        }

        public static Symbol valueOf(String symbolString)
        {
            return new Symbol(symbolString);
        }

    }
}
