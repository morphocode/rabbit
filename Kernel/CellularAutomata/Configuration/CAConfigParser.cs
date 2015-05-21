using System;
using System.Collections.Generic;
using System.Text;

using Rabbit.Kernel.Language;
using Rabbit.Kernel.CellularAutomata.Configuration;
using Rabbit.Kernel.CellularAutomata.Impl.Life;


namespace Rabbit.Kernel.CellularAutomata
{
    
    public class CAConfigParser:IParser
    {

        private static Token stateSeparator = new Token(",");
        private static Token rowStart = new Token("{");
        private static Token rowEnd = new Token("}");
        //private static Token newLine = new Token("\n");

        /**
         * <returns> IPattern </returns>
         */ 
        public Object Parse(ITokenStream tokenStream)
        {

            Token token;
            CAConfig pattern = new CAConfig( 0, null);//LivingCell.DeadState);//Determine the size of the configuration!
            //CellState[,] patternState = new CellState[,];

            IList<IList<CellState>> rows = new List<IList<CellState>>();
            IList<CellState> row = null;
            Boolean rowOpened = false;
            int rowIndex = 0;
            int columnIndex = 0;
            while((token=tokenStream.NextToken()) != null) {

                if(token.Equals(stateSeparator)) {
                    //token=tokenStream.NextToken();
                    //do nothing
                }
                else if(token.Equals(rowStart)) {//init a new row
                    if(rowOpened) throw new InvalidOperationException(string.Format("Missing enclosing bracket! Please, enclose the row, before creating a new one"));
                    row = new List<CellState>();
                    rowOpened=true;
                    rowIndex++;
                    columnIndex = 0;
                }
                else if(token.Equals(rowEnd)) {//add the existing row to the list of rows
                    rows.Add(row);
                    rowOpened = false;
                }
                else if(!char.IsWhiteSpace(token.ToString(), 0)) {//State Token
                    if (row == null || !rowOpened) throw new InvalidOperationException("You should define a cell state within a Row. Use the following format: {D,A,D,D} ");

                    CellState cellState = null;//CellState.ValueOf(token.ToString());
                    row.Add(cellState);
                    pattern.AddCellState(null, cellState);
                    columnIndex++;
                }
            }

            return pattern;
        }

    }
}
