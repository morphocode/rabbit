using System;
using System.Collections.Generic;
using System.Text;

namespace Rabbit.Kernel.LSystems
{
    /**
     * 
     * Represents a word in the language of a LSystem.
     * The word is a sequence of symbols.
     * 
     * If there are parametric symbols the words are called parametric words
     * 
     */ 
    public class Word
    {
        //string builder used to represent the word as a string
        private StringBuilder lwordBuilder;

        private IList<Symbol> symbols;

        private int size;

        public Word()
        {
            //lwordBuilder = new StringBuilder();
            symbols = new List<Symbol>();
            size = 0;            
        }

        public Word(Symbol symbol)
            : this()
        {
            Append(symbol);
        }

        public void Append(Symbol lsymbol)
        {            
            symbols.Add(lsymbol);
            //lwordBuilder.Append(lsymbol.ToString());
            size++;
        }

        /**
         * Appends another word to this word by appending all of it's symbols 
         * 
         */ 
        public void Append(Word word)
        {
            foreach (Symbol symbol in word.GetSymbols())
                Append(symbol);
        }

        public IList<Symbol> GetSymbols()
        {
            return symbols;
        }

        public Symbol SymbolAt(int index)
        {
            if (index >= 0 && index < symbols.Count)
                return symbols[index];
            else
                return null;
        }

        /**
         * <returns> The number of symbols that compose this word </returns>
         */ 
        public int GetSize()
        {
            return size;
        }

        public override String ToString()
        {
            //FIXME: build this at initialization and modify it only when needed
            ///do not build it every time a ToString() is called!!
            StringBuilder wordStringBuilder = new StringBuilder();
            foreach (Symbol lsymbol in symbols)
                wordStringBuilder.Append(lsymbol.ToString());

            return wordStringBuilder.ToString();
        }

    }
}
