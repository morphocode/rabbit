using System;
using System.Collections.Generic;
using System.Text;

namespace Rabbit.Kernel.LSystems
{
    /**
     * A Symbol Mapper maps a meaning to an abstract symbol.
     * The meaning is some kind of an object, it could be a value, an expression, a geometry, an instruction(for a turtle for ex.)
     * Usually different kind of interpreters expect specific meanings. For ex. The surface growth component expects integer values associated with the symbols
     * 
     */ 
    public class SymbolMap<M>//M=the meaning
    {

        protected Dictionary<Symbol, M> symbol2MeaningMap;

        /**
         * Constructor.
         */
        public SymbolMap() 
        {
            symbol2MeaningMap = new Dictionary<Symbol, M>();
        }

        public void Map(Symbol symbol, M meaning)
        {
            if (!symbol2MeaningMap.ContainsKey(symbol))
                symbol2MeaningMap.Add(symbol, meaning);
            else //replace the meaning
                symbol2MeaningMap[symbol] = meaning;
        }

        /**
         * Returns the Meaning mapped to that Symbol
         * Null if there is no associated meanings
         * 
         */ 
        public M GetMeaning(Symbol symbol)
        {
            if (symbol2MeaningMap.ContainsKey(symbol))
                return symbol2MeaningMap[symbol];
            else
                return default(M);
        }


        public override String ToString()
        {
            StringBuilder symbolMapDescription = new StringBuilder();
            symbolMapDescription.AppendLine(string.Format("Symbol Map contains the following {0} meanings:", symbol2MeaningMap.Count));
            symbolMapDescription.AppendLine(string.Format("Symbol <-> Meaning"));                    
            //symbolMapDescription.Append(symbol2MeaningMap.ToString());
            foreach (Symbol symbol in symbol2MeaningMap.Keys)
            {
                symbolMapDescription.AppendLine(String.Format("{0} <-> {1}", symbol, symbol2MeaningMap[symbol]));
            }
            return symbolMapDescription.ToString();
        }
    }
}
